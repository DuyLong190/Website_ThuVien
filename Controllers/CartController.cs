using Microsoft.AspNetCore.Mvc;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Extensions;
using QuanPhucLongQuang_DoAnWeb.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "Cart";

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Thêm sách vào giỏ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            var book = _context.Books.Find(bookId);
            if (book == null || quantity < 1 || quantity > book.Quantity)
            {
                TempData["Error"] = "Sách không tồn tại hoặc số lượng không hợp lệ.";
                return RedirectToAction("Books", "BookOrder");
            }
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    BookId = book.Id,
                    Title = book.Title,
                    ImageUrl = book.ImageUrl,
                    Quantity = quantity
                });
            }
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            TempData["Success"] = $"Đã thêm '{book.Title}' vào giỏ hàng!";
            return RedirectToAction("Index");
        }

        // Xem giỏ hàng
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            return View(cart);
        }

        // Xóa sách khỏi giỏ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int bookId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                cart.Items.Remove(item);
                HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }

        // Đặt sách (tất cả sách trong giỏ)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            if (!cart.Items.Any())
            {
                TempData["Error"] = "Giỏ hàng trống.";
                return RedirectToAction("Index");
            }
            // Kiểm tra tồn kho
            foreach (var item in cart.Items)
            {
                var book = _context.Books.Find(item.BookId);
                if (book == null || item.Quantity > book.Quantity)
                {
                    TempData["Error"] = $"Sách '{item.Title}' không đủ số lượng.";
                    return RedirectToAction("Index");
                }
            }
            // Tạo đơn đặt giữ
            var order = new BookOrder
            {
                UserId = user.Id,
                OrderDate = System.DateTime.Now,
                IsPickedUp = false
            };
            _context.BookOrders.Add(order);
            await _context.SaveChangesAsync();
            foreach (var item in cart.Items)
            {
                var book = _context.Books.Find(item.BookId);
                book.Quantity -= item.Quantity;
                _context.Books.Update(book);
                var orderDetail = new BookOrderDetail
                {
                    BookOrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity
                };
                _context.BookOrderDetails.Add(orderDetail);
            }
            await _context.SaveChangesAsync();
            // Xóa giỏ hàng
            HttpContext.Session.Remove(CartSessionKey);
            TempData["Success"] = "Đặt giữ sách thành công!";
            return RedirectToAction("MyOrders", "BookOrder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int bookId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null && quantity > 0)
            {
                // Kiểm tra tồn kho
                var book = _context.Books.Find(bookId);
                if (book != null && quantity <= book.Quantity)
                {
                    item.Quantity = quantity;
                    HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
                    TempData["Success"] = $"Đã cập nhật số lượng cho '{item.Title}'.";
                }
                else
                {
                    TempData["Error"] = $"Số lượng vượt quá tồn kho cho '{item.Title}'.";
                }
            }
            return RedirectToAction("Index");
        }
    }
} 