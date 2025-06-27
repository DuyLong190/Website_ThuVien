using Microsoft.AspNetCore.Mvc;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BorrowController : Controller
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBorrowDetailRepository _borrowDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowController(IBorrowRepository borrowRepository, IBorrowDetailRepository borrowDetailRepository, IBookRepository bookRepository, UserManager<ApplicationUser> userManager)
        {
            _borrowRepository = borrowRepository;
            _borrowDetailRepository = borrowDetailRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var borrows = _borrowRepository.GetAll();
            return View(borrows);
        }

        public IActionResult Create()
        {
            ViewBag.Books = _bookRepository.GetAll();
            ViewBag.Users = _userManager.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string userId, List<int> bookIds, List<int> quantities)
        {
            if (string.IsNullOrEmpty(userId) || bookIds == null || quantities == null || bookIds.Count != quantities.Count)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                ViewBag.Books = _bookRepository.GetAll();
                ViewBag.Users = _userManager.Users.ToList();
                return View();
            }

            // Kiểm tra số lượng sách còn lại
            for (int i = 0; i < bookIds.Count; i++)
            {
                var book = _bookRepository.GetById(bookIds[i]);
                if (book == null)
                {
                    ModelState.AddModelError("", $"Sách với ID {bookIds[i]} không tồn tại.");
                    ViewBag.Books = _bookRepository.GetAll();
                    ViewBag.Users = _userManager.Users.ToList();
                    return View();
                }
                if (quantities[i] > book.Quantity)
                {
                    ModelState.AddModelError("", $"Số lượng mượn cho sách '{book.Title}' vượt quá số lượng còn lại ({book.Quantity}).");
                    ViewBag.Books = _bookRepository.GetAll();
                    ViewBag.Users = _userManager.Users.ToList();
                    return View();
                }
            }

            var borrow = new Borrow
            {
                UserId = userId,
                BorrowDate = DateTime.Now,
                IsReturned = false
            };
            _borrowRepository.Add(borrow);

            for (int i = 0; i < bookIds.Count; i++)
            {
                var borrowDetail = new BorrowDetail
                {
                    BorrowId = borrow.Id,
                    BookId = bookIds[i],
                    Quantity = quantities[i]
                };
                _borrowDetailRepository.Add(borrowDetail);

                // Trừ số lượng sách
                var book = _bookRepository.GetById(bookIds[i]);
                book.Quantity -= quantities[i];
                _bookRepository.Update(book);
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 