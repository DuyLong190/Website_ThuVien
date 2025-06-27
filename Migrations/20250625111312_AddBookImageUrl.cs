using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanPhucLongQuang_DoAnWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBookImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Khoa học" },
                    { 2, "Văn học" },
                    { 3, "Lịch sử" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "Stephen Hawking", 1, "Sách khoa học nổi tiếng", 5, "Vũ trụ trong vỏ hạt dẻ" },
                    { 2, "Tô Hoài", 2, "Tác phẩm thiếu nhi kinh điển", 10, "Dế mèn phiêu lưu ký" },
                    { 3, "Stephen Hawking", 1, "Khoa học vũ trụ", 7, "Lược sử thời gian" },
                    { 4, "Nick Lane", 1, "Sinh học hiện đại", 4, "Sự sống bất tử" },
                    { 5, "Nam Cao", 2, "Tác phẩm hiện thực phê phán", 8, "Chí Phèo" },
                    { 6, "Ngô Tất Tố", 2, "Văn học hiện thực", 6, "Tắt đèn" },
                    { 7, "Trần Trọng Kim", 3, "Lịch sử Việt Nam", 5, "Việt Nam sử lược" },
                    { 8, "Nhiều tác giả", 3, "Tổng quan lịch sử thế giới", 3, "Lịch sử thế giới" }
                });
        }
    }
}
