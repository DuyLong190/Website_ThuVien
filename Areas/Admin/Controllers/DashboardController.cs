using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuanPhucLongQuang_DoAnWeb.Data;
using Microsoft.AspNetCore.Identity;
using QuanPhucLongQuang_DoAnWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuanPhucLongQuang_DoAnWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var totalBooks = await _context.Books.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var totalUsers = await _userManager.Users.CountAsync();
            var totalBorrows = await _context.Borrows.CountAsync();
            var returnedBorrows = await _context.Borrows.Where(b => b.IsReturned).CountAsync();
            var notReturnedBorrows = await _context.Borrows.Where(b => !b.IsReturned).CountAsync();

            // Thống kê mượn theo tháng
            var borrowByMonth = await _context.Borrows
                .GroupBy(b => new { b.BorrowDate.Year, b.BorrowDate.Month })
                .Select(g => new {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToListAsync();

            ViewBag.TotalBooks = totalBooks;
            ViewBag.TotalCategories = totalCategories;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalBorrows = totalBorrows;
            ViewBag.ReturnedBorrows = returnedBorrows;
            ViewBag.NotReturnedBorrows = notReturnedBorrows;
            ViewBag.BorrowByMonth = borrowByMonth;
            return View();
        }
    }
} 