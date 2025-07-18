﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanPhucLongQuang_DoAnWeb.Data;

#nullable disable

namespace QuanPhucLongQuang_DoAnWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250626073522_AddBookDamageReportAndBookCondition")]
    partial class AddBookDamageReportAndBookCondition
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Stephen Hawking",
                            CategoryId = 1,
                            Condition = "Bình thường",
                            Description = "Sách khoa học nổi tiếng",
                            Quantity = 5,
                            Title = "Vũ trụ trong vỏ hạt dẻ"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Tô Hoài",
                            CategoryId = 5,
                            Condition = "Bình thường",
                            Description = "Tác phẩm thiếu nhi kinh điển",
                            Quantity = 10,
                            Title = "Dế mèn phiêu lưu ký"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Stephen Hawking",
                            CategoryId = 1,
                            Condition = "Bình thường",
                            Description = "Khoa học vũ trụ",
                            Quantity = 7,
                            Title = "Lược sử thời gian"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Nick Lane",
                            CategoryId = 1,
                            Condition = "Bình thường",
                            Description = "Sinh học hiện đại",
                            Quantity = 4,
                            Title = "Sự sống bất tử"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Nam Cao",
                            CategoryId = 2,
                            Condition = "Bình thường",
                            Description = "Tác phẩm hiện thực phê phán",
                            Quantity = 8,
                            Title = "Chí Phèo"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Ngô Tất Tố",
                            CategoryId = 2,
                            Condition = "Bình thường",
                            Description = "Văn học hiện thực",
                            Quantity = 6,
                            Title = "Tắt đèn"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Trần Trọng Kim",
                            CategoryId = 3,
                            Condition = "Bình thường",
                            Description = "Lịch sử Việt Nam",
                            Quantity = 5,
                            Title = "Việt Nam sử lược"
                        },
                        new
                        {
                            Id = 8,
                            Author = "Nhiều tác giả",
                            CategoryId = 3,
                            Condition = "Bình thường",
                            Description = "Tổng quan lịch sử thế giới",
                            Quantity = 3,
                            Title = "Lịch sử thế giới"
                        },
                        new
                        {
                            Id = 9,
                            Author = "Nguyễn Văn A",
                            CategoryId = 4,
                            Condition = "Bình thường",
                            Description = "Giới thiệu về công nghệ hiện đại",
                            Quantity = 12,
                            Title = "Công nghệ 4.0"
                        },
                        new
                        {
                            Id = 10,
                            Author = "Lê Văn B",
                            CategoryId = 4,
                            Condition = "Bình thường",
                            Description = "Học lập trình cơ bản với C#",
                            Quantity = 9,
                            Title = "Lập trình C#"
                        },
                        new
                        {
                            Id = 11,
                            Author = "Phạm Văn C",
                            CategoryId = 4,
                            Condition = "Bình thường",
                            Description = "Cơ bản về AI",
                            Quantity = 7,
                            Title = "Trí tuệ nhân tạo"
                        },
                        new
                        {
                            Id = 12,
                            Author = "Victor Hugo",
                            CategoryId = 2,
                            Condition = "Bình thường",
                            Description = "Tác phẩm kinh điển thế giới",
                            Quantity = 6,
                            Title = "Những người khốn khổ"
                        },
                        new
                        {
                            Id = 13,
                            Author = "Dale Carnegie",
                            CategoryId = 2,
                            Condition = "Bình thường",
                            Description = "Kỹ năng sống và giao tiếp",
                            Quantity = 15,
                            Title = "Đắc nhân tâm"
                        },
                        new
                        {
                            Id = 14,
                            Author = "Masaru Emoto",
                            CategoryId = 1,
                            Condition = "Bình thường",
                            Description = "Khám phá về nước và cuộc sống",
                            Quantity = 8,
                            Title = "Bí mật của nước"
                        },
                        new
                        {
                            Id = 15,
                            Author = "Nhiều tác giả",
                            CategoryId = 3,
                            Condition = "Bình thường",
                            Description = "Những phát minh thay đổi thế giới",
                            Quantity = 5,
                            Title = "Lịch sử các phát minh"
                        },
                        new
                        {
                            Id = 16,
                            Author = "Gosho Aoyama",
                            CategoryId = 5,
                            Condition = "Bình thường",
                            Description = "Truyện tranh trinh thám nổi tiếng",
                            Quantity = 20,
                            Title = "Thám tử lừng danh Conan"
                        },
                        new
                        {
                            Id = 17,
                            Author = "J.K. Rowling",
                            CategoryId = 5,
                            Condition = "Bình thường",
                            Description = "Tiểu thuyết thiếu nhi nổi tiếng",
                            Quantity = 13,
                            Title = "Harry Potter và Hòn đá Phù thủy"
                        },
                        new
                        {
                            Id = 18,
                            Author = "José Mauro de Vasconcelos",
                            CategoryId = 5,
                            Condition = "Bình thường",
                            Description = "Tác phẩm cảm động về tuổi thơ",
                            Quantity = 11,
                            Title = "Cây cam ngọt của tôi"
                        },
                        new
                        {
                            Id = 19,
                            Author = "Yuval Noah Harari",
                            CategoryId = 3,
                            Condition = "Bình thường",
                            Description = "Lịch sử phát triển của loài người",
                            Quantity = 7,
                            Title = "Sapiens: Lược sử loài người"
                        },
                        new
                        {
                            Id = 20,
                            Author = "Adam Khoo",
                            CategoryId = 2,
                            Condition = "Bình thường",
                            Description = "Kỹ năng phát triển bản thân",
                            Quantity = 10,
                            Title = "Tôi tài giỏi, bạn cũng thế!"
                        });
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookDamageReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("FineAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReturnId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReturnId");

                    b.HasIndex("UserId");

                    b.ToTable("BookDamageReports");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsPickedUp")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BookOrders");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookOrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BookOrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("BookOrderId");

                    b.ToTable("BookOrderDetails");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Borrows");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BorrowDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BorrowId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("BorrowId");

                    b.ToTable("BorrowDetails");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Khoa học"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Văn học"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lịch sử"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Công nghệ"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Thiếu nhi"
                        });
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Return", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BorrowId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BorrowId");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.ReturnDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReturnId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReturnId");

                    b.ToTable("ReturnDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Book", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookDamageReport", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Return", "Return")
                        .WithMany()
                        .HasForeignKey("ReturnId");

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Return");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookOrder", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookOrderDetail", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.BookOrder", "BookOrder")
                        .WithMany("BookOrderDetails")
                        .HasForeignKey("BookOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BookOrder");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Borrow", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BorrowDetail", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Borrow", "Borrow")
                        .WithMany("BorrowDetails")
                        .HasForeignKey("BorrowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Borrow");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Return", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Borrow", "Borrow")
                        .WithMany()
                        .HasForeignKey("BorrowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrow");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.ReturnDetail", b =>
                {
                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanPhucLongQuang_DoAnWeb.Models.Return", "Return")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ReturnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Return");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.BookOrder", b =>
                {
                    b.Navigation("BookOrderDetails");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Borrow", b =>
                {
                    b.Navigation("BorrowDetails");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("QuanPhucLongQuang_DoAnWeb.Models.Return", b =>
                {
                    b.Navigation("ReturnDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
