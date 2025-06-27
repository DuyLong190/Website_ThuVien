using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanPhucLongQuang_DoAnWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooksData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ImageUrl", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "Stephen Hawking", 1, "Sách khoa học nổi tiếng", null, 5, "Vũ trụ trong vỏ hạt dẻ" },
                    { 2, "Tô Hoài", 5, "Tác phẩm thiếu nhi kinh điển", null, 10, "Dế mèn phiêu lưu ký" },
                    { 3, "Stephen Hawking", 1, "Khoa học vũ trụ", null, 7, "Lược sử thời gian" },
                    { 4, "Nick Lane", 1, "Sinh học hiện đại", null, 4, "Sự sống bất tử" },
                    { 5, "Nam Cao", 2, "Tác phẩm hiện thực phê phán", null, 8, "Chí Phèo" },
                    { 6, "Ngô Tất Tố", 2, "Văn học hiện thực", null, 6, "Tắt đèn" },
                    { 7, "Trần Trọng Kim", 3, "Lịch sử Việt Nam", null, 5, "Việt Nam sử lược" },
                    { 8, "Nhiều tác giả", 3, "Tổng quan lịch sử thế giới", null, 3, "Lịch sử thế giới" },
                    { 9, "Nguyễn Văn A", 4, "Giới thiệu về công nghệ hiện đại", null, 12, "Công nghệ 4.0" },
                    { 10, "Lê Văn B", 4, "Học lập trình cơ bản với C#", null, 9, "Lập trình C#" },
                    { 11, "Phạm Văn C", 4, "Cơ bản về AI", null, 7, "Trí tuệ nhân tạo" },
                    { 12, "Victor Hugo", 2, "Tác phẩm kinh điển thế giới", null, 6, "Những người khốn khổ" },
                    { 13, "Dale Carnegie", 2, "Kỹ năng sống và giao tiếp", null, 15, "Đắc nhân tâm" },
                    { 14, "Masaru Emoto", 1, "Khám phá về nước và cuộc sống", null, 8, "Bí mật của nước" },
                    { 15, "Nhiều tác giả", 3, "Những phát minh thay đổi thế giới", null, 5, "Lịch sử các phát minh" },
                    { 16, "Gosho Aoyama", 5, "Truyện tranh trinh thám nổi tiếng", null, 20, "Thám tử lừng danh Conan" },
                    { 17, "J.K. Rowling", 5, "Tiểu thuyết thiếu nhi nổi tiếng", null, 13, "Harry Potter và Hòn đá Phù thủy" },
                    { 18, "José Mauro de Vasconcelos", 5, "Tác phẩm cảm động về tuổi thơ", null, 11, "Cây cam ngọt của tôi" },
                    { 19, "Yuval Noah Harari", 3, "Lịch sử phát triển của loài người", null, 7, "Sapiens: Lược sử loài người" },
                    { 20, "Adam Khoo", 2, "Kỹ năng phát triển bản thân", null, 10, "Tôi tài giỏi, bạn cũng thế!" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
