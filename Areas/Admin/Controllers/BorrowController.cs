using Microsoft.AspNetCore.Mvc;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BorrowController : Controller
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBorrowDetailRepository _borrowDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowController(IBorrowRepository borrowRepository, IBorrowDetailRepository borrowDetailRepository, IBookRepository bookRepository, UserManager<ApplicationUser> userManager)
        {
            _borrowRepository = borrowRepository;
            _borrowDetailRepository = borrowDetailRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var borrows = _borrowRepository.GetAll();
            return View(borrows);
        }

        public IActionResult Create()
        {
            ViewBag.Books = _bookRepository.GetAll();
            ViewBag.Users = _userManager.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string userId, List<int> bookIds, List<int> quantities)
        {
            if (string.IsNullOrEmpty(userId) || bookIds == null || quantities == null || bookIds.Count != quantities.Count)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                ViewBag.Books = _bookRepository.GetAll();
                ViewBag.Users = _userManager.Users.ToList();
                return View();
            }

            // Kiểm tra số lượng sách còn lại
            for (int i = 0; i < bookIds.Count; i++)
            {
                var book = _bookRepository.GetById(bookIds[i]);
                if (book == null)
                {
                    ModelState.AddModelError("", $"Sách với ID {bookIds[i]} không tồn tại.");
                    ViewBag.Books = _bookRepository.GetAll();
                    ViewBag.Users = _userManager.Users.ToList();
                    return View();
                }
                if (quantities[i] > book.Quantity)
                {
                    ModelState.AddModelError("", $"Số lượng mượn cho sách '{book.Title}' vượt quá số lượng còn lại ({book.Quantity}).");
                    ViewBag.Books = _bookRepository.GetAll();
                    ViewBag.Users = _userManager.Users.ToList();
                    return View();
                }
            }

            var borrow = new Borrow
            {
                UserId = userId,
                BorrowDate = DateTime.Now,
                IsReturned = false
            };
            _borrowRepository.Add(borrow);

            for (int i = 0; i < bookIds.Count; i++)
            {
                var borrowDetail = new BorrowDetail
                {
                    BorrowId = borrow.Id,
                    BookId = bookIds[i],
                    Quantity = quantities[i]
                };
                _borrowDetailRepository.Add(borrowDetail);

                // Trừ số lượng sách
                var book = _bookRepository.GetById(bookIds[i]);
                book.Quantity -= quantities[i];
                _bookRepository.Update(book);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var borrow = _borrowRepository.GetById(id);
            if (borrow == null)
            {
                return NotFound();
            }
            var user = _userManager.Users.FirstOrDefault(u => u.Id == borrow.UserId);
            var borrowDetails = _borrowDetailRepository.GetByBorrowId(borrow.Id).ToList();
            var viewModel = new Areas.Admin.ViewModels.BorrowDetailsViewModel
            {
                Borrow = borrow,
                User = user,
                BorrowDetails = borrowDetails
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var borrow = _borrowRepository.GetById(id);
            if (borrow == null)
            {
                return NotFound();
            }
            if (borrow.IsReturned)
            {
                TempData["ErrorMessage"] = "Phiếu mượn đã trả, không thể chỉnh sửa.";
                return RedirectToAction("Details", new { id });
            }
            var user = _userManager.Users.FirstOrDefault(u => u.Id == borrow.UserId);
            var borrowDetails = _borrowDetailRepository.GetByBorrowId(borrow.Id).ToList();
            ViewBag.StatusList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
            {
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Chưa trả", Value = "false", Selected = !borrow.IsReturned },
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Đã trả", Value = "true", Selected = borrow.IsReturned }
            };
            ViewBag.Books = _bookRepository.GetAll();
            var viewModel = new Areas.Admin.ViewModels.BorrowDetailsViewModel
            {
                Borrow = borrow,
                User = user,
                BorrowDetails = borrowDetails
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, bool isReturned, List<int> bookIds, List<int> quantities)
        {
            var borrow = _borrowRepository.GetById(id);
            if (borrow == null)
            {
                return NotFound();
            }
            if (borrow.IsReturned)
            {
                TempData["ErrorMessage"] = "Phiếu mượn đã trả, không thể chỉnh sửa.";
                return RedirectToAction("Details", new { id });
            }
            borrow.IsReturned = isReturned;
            _borrowRepository.Update(borrow);

            var oldDetails = _borrowDetailRepository.GetByBorrowId(borrow.Id).ToList();
            var error = false;
            string errorMsg = null;
            // Xoá các BorrowDetail không còn trong danh sách mới
            foreach (var old in oldDetails)
            {
                if (!bookIds.Contains(old.BookId))
                {
                    var book = _bookRepository.GetById(old.BookId);
                    book.Quantity += old.Quantity;
                    _bookRepository.Update(book);
                    _borrowDetailRepository.Delete(old.Id);
                }
            }
            // Thêm mới hoặc cập nhật số lượng
            for (int i = 0; i < bookIds.Count; i++)
            {
                var bookId = bookIds[i];
                var quantity = quantities[i];
                var book = _bookRepository.GetById(bookId);
                var detail = oldDetails.FirstOrDefault(x => x.BookId == bookId);
                if (detail == null)
                {
                    if (quantity > book.Quantity)
                    {
                        error = true;
                        errorMsg = $"Số lượng mượn cho sách '{book.Title}' vượt quá số lượng còn lại ({book.Quantity}).";
                        break;
                    }
                }
                else
                {
                    if (quantity != detail.Quantity)
                    {
                        int diff = quantity - detail.Quantity;
                        if (diff > 0 && diff > book.Quantity)
                        {
                            error = true;
                            errorMsg = $"Số lượng mượn cho sách '{book.Title}' vượt quá số lượng còn lại ({book.Quantity}).";
                            break;
                        }
                    }
                }
            }
            if (error)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Id == borrow.UserId);
                var borrowDetails = _borrowDetailRepository.GetByBorrowId(borrow.Id).ToList();
                ViewBag.StatusList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                {
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Chưa trả", Value = "false", Selected = !borrow.IsReturned },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Đã trả", Value = "true", Selected = borrow.IsReturned }
                };
                ViewBag.Books = _bookRepository.GetAll();
                ModelState.AddModelError("", errorMsg);
                var viewModel = new Areas.Admin.ViewModels.BorrowDetailsViewModel
                {
                    Borrow = borrow,
                    User = user,
                    BorrowDetails = borrowDetails
                };
                return View(viewModel);
            }
            // Nếu không có lỗi thì thực hiện cập nhật như cũ
            for (int i = 0; i < bookIds.Count; i++)
            {
                var bookId = bookIds[i];
                var quantity = quantities[i];
                var book = _bookRepository.GetById(bookId);
                var detail = oldDetails.FirstOrDefault(x => x.BookId == bookId);
                if (detail == null)
                {
                    var newDetail = new BorrowDetail
                    {
                        BorrowId = borrow.Id,
                        BookId = bookId,
                        Quantity = quantity
                    };
                    _borrowDetailRepository.Add(newDetail);
                    book.Quantity -= quantity;
                    _bookRepository.Update(book);
                }
                else
                {
                    if (quantity != detail.Quantity)
                    {
                        int diff = quantity - detail.Quantity;
                        book.Quantity -= diff;
                        _bookRepository.Update(book);
                        detail.Quantity = quantity;
                        _borrowDetailRepository.Update(detail);
                    }
                }
            }
            return RedirectToAction("Details", new { id = borrow.Id });
        }
    }
} 