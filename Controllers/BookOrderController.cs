using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using QuanPhucLongQuang_DoAnWeb.Repositories;
namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    public class BookOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;

        public BookOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICategoryRepository categoryRepository)
        {
            _context = context;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        // GET: /BookOrder/Books
        public async Task<IActionResult> Books(string search, int? categoryId)
        {
            var categories = _categoryRepository.GetAll();
            var query = _context.Books.AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            }
            var books = await query.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId;
            return View(books);
        }

        // POST: /BookOrder/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(int bookId, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || quantity < 1 || quantity > book.Quantity)
            {
                TempData["Error"] = "Sách không tồn tại hoặc số lượng không hợp lệ.";
                return RedirectToAction("Books");
            }
            // (Tùy chọn) Giảm tạm số lượng sách để giữ chỗ
            book.Quantity -= quantity;
            _context.Books.Update(book);

            var order = new BookOrder
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                IsPickedUp = false
            };
            _context.BookOrders.Add(order);
            await _context.SaveChangesAsync();

            var orderDetail = new BookOrderDetail
            {
                BookOrderId = order.Id,
                BookId = bookId,
                Quantity = quantity
            };
            _context.BookOrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã đặt giữ thành công {quantity} cuốn '{book.Title}'!";
            return RedirectToAction("MyOrders");
        }

        // GET: /BookOrder/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);
            var orders = await _context.BookOrders
                .Include(o => o.BookOrderDetails)
                .ThenInclude(d => d.Book)
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooksPartial(string search)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            }
            var books = await query.ToListAsync();
            return PartialView("_BooksListPartial", books);
        }

        // GET: /BookOrder/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
} 