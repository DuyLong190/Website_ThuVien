using Microsoft.AspNetCore.Mvc;
using QuanPhucLongQuang_DoAnWeb.Models;
using QuanPhucLongQuang_DoAnWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace QuanPhucLongQuang_DoAnWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetAll();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                book.ImageUrl = "/images/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book, IFormFile Image)
        {


            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                book.ImageUrl = "/images/" + fileName;
            }
            else
            {
                // Lấy lại ảnh cũ nếu không upload ảnh mới
                var oldBook = _bookRepository.GetById(book.Id);
                book.ImageUrl = oldBook?.ImageUrl;
            }


            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 