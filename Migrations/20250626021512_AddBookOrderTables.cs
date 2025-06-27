using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanPhucLongQuang_DoAnWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddBookOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPickedUp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookOrderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookOrderDetails_BookOrders_BookOrderId",
                        column: x => x.BookOrderId,
                        principalTable: "BookOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookOrderDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderDetails_BookId",
                table: "BookOrderDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderDetails_BookOrderId",
                table: "BookOrderDetails",
                column: "BookOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrders_UserId",
                table: "BookOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOrderDetails");

            migrationBuilder.DropTable(
                name: "BookOrders");
        }
    }
}
