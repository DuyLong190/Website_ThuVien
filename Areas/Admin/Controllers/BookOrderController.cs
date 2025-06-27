using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace QuanPhucLongQuang_DoAnWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/BookOrder/Unpicked
        public async Task<IActionResult> Unpicked()
        {
            var orders = await _context.BookOrders
                .Include(o => o.User)
                .Include(o => o.BookOrderDetails)
                .ThenInclude(d => d.Book)
                .Where(o => !o.IsPickedUp)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        // POST: Admin/BookOrder/ConfirmPickUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPickUp(int orderId)
        {
            var order = await _context.BookOrders
                .Include(o => o.BookOrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null || order.IsPickedUp)
            {
                TempData["Error"] = "Đơn không tồn tại hoặc đã được xác nhận.";
                return RedirectToAction("Unpicked");
            }
            // Tạo Borrow
            var borrow = new Borrow
            {
                UserId = order.UserId,
                BorrowDate = DateTime.Now,
                IsReturned = false
            };
            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();
            // Tạo BorrowDetail cho từng sách trong đơn
            foreach (var detail in order.BookOrderDetails)
            {
                var borrowDetail = new BorrowDetail
                {
                    BorrowId = borrow.Id,
                    BookId = detail.BookId,
                    Quantity = detail.Quantity
                };
                _context.BorrowDetails.Add(borrowDetail);
            }
            // Đánh dấu đã nhận
            order.IsPickedUp = true;
            _context.BookOrders.Update(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xác nhận giao sách và chuyển thành phiếu mượn!";
            return RedirectToAction("Unpicked");
        }
    }
} 