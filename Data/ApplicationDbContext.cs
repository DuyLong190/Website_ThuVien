using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Models;

namespace QuanPhucLongQuang_DoAnWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BorrowDetail> BorrowDetails { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnDetail> ReturnDetails { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<BookOrderDetail> BookOrderDetails { get; set; }
        public DbSet<BookDamageReport> BookDamageReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Khoa học" },
                new Category { Id = 2, Name = "Văn học" },
                new Category { Id = 3, Name = "Lịch sử" },
                new Category { Id = 4, Name = "Công nghệ" },
                new Category { Id = 5, Name = "Thiếu nhi" }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Vũ trụ trong vỏ hạt dẻ", Author = "Stephen Hawking", Description = "Sách khoa học nổi tiếng", Quantity = 5, CategoryId = 1, Condition = "Bình thường" },
                new Book { Id = 2, Title = "Dế mèn phiêu lưu ký", Author = "Tô Hoài", Description = "Tác phẩm thiếu nhi kinh điển", Quantity = 10, CategoryId = 5, Condition = "Bình thường" },
                new Book { Id = 3, Title = "Lược sử thời gian", Author = "Stephen Hawking", Description = "Khoa học vũ trụ", Quantity = 7, CategoryId = 1, Condition = "Bình thường" },
                new Book { Id = 4, Title = "Sự sống bất tử", Author = "Nick Lane", Description = "Sinh học hiện đại", Quantity = 4, CategoryId = 1, Condition = "Bình thường" },
                new Book { Id = 5, Title = "Chí Phèo", Author = "Nam Cao", Description = "Tác phẩm hiện thực phê phán", Quantity = 8, CategoryId = 2, Condition = "Bình thường" },
                new Book { Id = 6, Title = "Tắt đèn", Author = "Ngô Tất Tố", Description = "Văn học hiện thực", Quantity = 6, CategoryId = 2, Condition = "Bình thường" },
                new Book { Id = 7, Title = "Việt Nam sử lược", Author = "Trần Trọng Kim", Description = "Lịch sử Việt Nam", Quantity = 5, CategoryId = 3, Condition = "Bình thường" },
                new Book { Id = 8, Title = "Lịch sử thế giới", Author = "Nhiều tác giả", Description = "Tổng quan lịch sử thế giới", Quantity = 3, CategoryId = 3, Condition = "Bình thường" },
                new Book { Id = 9, Title = "Công nghệ 4.0", Author = "Nguyễn Văn A", Description = "Giới thiệu về công nghệ hiện đại", Quantity = 12, CategoryId = 4, Condition = "Bình thường" },
                new Book { Id = 10, Title = "Lập trình C#", Author = "Lê Văn B", Description = "Học lập trình cơ bản với C#", Quantity = 9, CategoryId = 4, Condition = "Bình thường" },
                new Book { Id = 11, Title = "Trí tuệ nhân tạo", Author = "Phạm Văn C", Description = "Cơ bản về AI", Quantity = 7, CategoryId = 4, Condition = "Bình thường" },
                new Book { Id = 12, Title = "Những người khốn khổ", Author = "Victor Hugo", Description = "Tác phẩm kinh điển thế giới", Quantity = 6, CategoryId = 2, Condition = "Bình thường" },
                new Book { Id = 13, Title = "Đắc nhân tâm", Author = "Dale Carnegie", Description = "Kỹ năng sống và giao tiếp", Quantity = 15, CategoryId = 2, Condition = "Bình thường" },
                new Book { Id = 14, Title = "Bí mật của nước", Author = "Masaru Emoto", Description = "Khám phá về nước và cuộc sống", Quantity = 8, CategoryId = 1, Condition = "Bình thường" },
                new Book { Id = 15, Title = "Lịch sử các phát minh", Author = "Nhiều tác giả", Description = "Những phát minh thay đổi thế giới", Quantity = 5, CategoryId = 3, Condition = "Bình thường" },
                new Book { Id = 16, Title = "Thám tử lừng danh Conan", Author = "Gosho Aoyama", Description = "Truyện tranh trinh thám nổi tiếng", Quantity = 20, CategoryId = 5, Condition = "Bình thường" },
                new Book { Id = 17, Title = "Harry Potter và Hòn đá Phù thủy", Author = "J.K. Rowling", Description = "Tiểu thuyết thiếu nhi nổi tiếng", Quantity = 13, CategoryId = 5, Condition = "Bình thường" },
                new Book { Id = 18, Title = "Cây cam ngọt của tôi", Author = "José Mauro de Vasconcelos", Description = "Tác phẩm cảm động về tuổi thơ", Quantity = 11, CategoryId = 5, Condition = "Bình thường" },
                new Book { Id = 19, Title = "Sapiens: Lược sử loài người", Author = "Yuval Noah Harari", Description = "Lịch sử phát triển của loài người", Quantity = 7, CategoryId = 3, Condition = "Bình thường" },
                new Book { Id = 20, Title = "Tôi tài giỏi, bạn cũng thế!", Author = "Adam Khoo", Description = "Kỹ năng phát triển bản thân", Quantity = 10, CategoryId = 2, Condition = "Bình thường" }
            );
        }
    }
}
