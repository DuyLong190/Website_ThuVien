using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuanPhucLongQuang_DoAnWeb.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuanPhucLongQuang_DoAnWeb.Services
{
    public class OrderCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var expiredOrders = _context.BookOrders
                        .Include(o => o.BookOrderDetails)
                        .Where(o => !o.IsPickedUp && o.OrderDate < DateTime.Now.AddDays(-2))
                        .ToList();

                    foreach (var order in expiredOrders)
                    {
                        // Nếu đã trừ Book.Quantity khi đặt, cộng lại số lượng
                        foreach (var detail in order.BookOrderDetails)
                        {
                            var book = _context.Books.FirstOrDefault(b => b.Id == detail.BookId);
                            if (book != null)
                                book.Quantity += detail.Quantity;
                        }
                        _context.BookOrders.Remove(order);
                    }
                    await _context.SaveChangesAsync();
                }
                // Chờ 24h rồi chạy lại
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
} 