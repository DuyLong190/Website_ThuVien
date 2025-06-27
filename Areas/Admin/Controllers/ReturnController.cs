using Microsoft.AspNetCore.Mvc;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;

namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReturnController : Controller
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IReturnDetailRepository _returnDetailRepository;
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBorrowDetailRepository _borrowDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _context;

        public ReturnController(IReturnRepository returnRepository, IReturnDetailRepository returnDetailRepository, IBorrowRepository borrowRepository, IBorrowDetailRepository borrowDetailRepository, IBookRepository bookRepository, ApplicationDbContext context)
        {
            _returnRepository = returnRepository;
            _returnDetailRepository = returnDetailRepository;
            _borrowRepository = borrowRepository;
            _borrowDetailRepository = borrowDetailRepository;
            _bookRepository = bookRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var returns = _returnRepository.GetAll();
            return View(returns);
        }

        public IActionResult Create(int borrowId)
        {
            var borrow = _borrowRepository.GetById(borrowId);
            if (borrow == null || borrow.IsReturned)
            {
                return NotFound();
            }
            ViewBag.Borrow = borrow;
            ViewBag.BorrowDetails = _borrowDetailRepository.GetByBorrowId(borrowId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int borrowId, List<int> bookIds, List<int> quantities)
        {
            var borrow = _borrowRepository.GetById(borrowId);
            if (borrow == null || borrow.IsReturned || bookIds == null || quantities == null || bookIds.Count != quantities.Count)
            {
                return NotFound();
            }

            // Lấy danh sách chi tiết mượn
            var borrowDetails = _borrowDetailRepository.GetByBorrowId(borrowId).ToList();
            bool valid = true;
            string errorMsg = "";
            foreach (var bd in borrowDetails)
            {
                int idx = bookIds.IndexOf(bd.BookId);
                if (idx == -1)
                {
                    valid = false;
                    errorMsg = $"Bạn phải trả đủ sách '{bd.Book.Title}'.";
                    break;
                }
                if (quantities[idx] != bd.Quantity)
                {
                    valid = false;
                    errorMsg = $"Số lượng trả cho sách '{bd.Book.Title}' phải đúng bằng số lượng đã mượn ({bd.Quantity}).";
                    break;
                }
            }
            if (!valid || bookIds.Count != borrowDetails.Count)
            {
                ViewBag.Borrow = borrow;
                ViewBag.BorrowDetails = borrowDetails;
                ModelState.AddModelError("", errorMsg != "" ? errorMsg : "Bạn phải trả đủ và đúng số lượng tất cả các sách đã mượn.");
                return View();
            }

            var ret = new Return
            {
                BorrowId = borrowId,
                ReturnDate = DateTime.Now
            };
            _returnRepository.Add(ret);

            for (int i = 0; i < bookIds.Count; i++)
            {
                var returnDetail = new ReturnDetail
                {
                    ReturnId = ret.Id,
                    BookId = bookIds[i],
                    Quantity = quantities[i]
                };
                _returnDetailRepository.Add(returnDetail);

                // Cộng lại số lượng sách
                var book = _bookRepository.GetById(bookIds[i]);
                if (book != null)
                {
                    book.Quantity += quantities[i];
                    _bookRepository.Update(book);
                }
            }

            // Cập nhật trạng thái đã trả
            borrow.IsReturned = true;
            _borrowRepository.Update(borrow);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var ret = _returnRepository.GetById(id);
            if (ret == null)
            {
                return NotFound();
            }
            // Lấy thêm thông tin chi tiết trả
            var details = _returnDetailRepository.GetAll().Where(d => d.ReturnId == id).ToList();
            ViewBag.Details = details;
            return View(ret);
        }

        [HttpGet]
        public async Task<IActionResult> ReportDamage(int returnId)
        {
            var returnEntity = await _context.Returns
                .Include(r => r.Borrow)
                .ThenInclude(b => b.BorrowDetails)
                .ThenInclude(d => d.Book)
                .FirstOrDefaultAsync(r => r.Id == returnId);
            if (returnEntity == null) return NotFound();
            var model = returnEntity.Borrow.BorrowDetails.Select(d => new BookDamageReportViewModel
            {
                BookId = d.BookId,
                BookTitle = d.Book.Title,
                UserId = returnEntity.Borrow.UserId,
                ReturnId = returnId
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportDamage([FromForm] List<BookDamageReportViewModel> reports)
        {
            if (reports == null || reports.Count == 0)
            {
                ModelState.AddModelError("", "Không nhận được dữ liệu báo cáo lỗi nào!");
                if (reports == null) reports = new List<BookDamageReportViewModel>();
                foreach (var r in reports)
                {
                    var book = await _context.Books.FindAsync(r.BookId);
                    r.BookTitle = book?.Title ?? "";
                }
                return View(reports);
            }
            foreach (var report in reports)
            {
                if (!string.IsNullOrEmpty(report.Description))
                {
                    var damage = new BookDamageReport
                    {
                        BookId = report.BookId,
                        UserId = report.UserId,
                        ReturnId = report.ReturnId,
                        Description = report.Description,
                        FineAmount = report.FineAmount,
                        ReportDate = DateTime.Now,
                        IsResolved = false
                    };
                    _context.BookDamageReports.Add(damage);
                    // Cập nhật tình trạng sách
                    var book = await _context.Books.FindAsync(report.BookId);
                    if (book != null && !string.IsNullOrEmpty(report.Condition))
                        book.Condition = report.Condition;
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DamageReports(bool? isResolved)
        {
            var query = _context.BookDamageReports.Include(r => r.Book).Include(r => r.User).Include(r => r.Return).AsQueryable();
            if (isResolved.HasValue)
                query = query.Where(r => r.IsResolved == isResolved.Value);
            var list = await query.OrderByDescending(r => r.ReportDate).ToListAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> MarkResolved(int id)
        {
            var report = await _context.BookDamageReports.FindAsync(id);
            if (report != null)
            {
                report.IsResolved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("DamageReports");
        }
    }
} 