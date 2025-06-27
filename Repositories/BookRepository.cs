using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;
using System.Threading.Tasks;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Include(b => b.Category).ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Category).ToListAsync();
        }

        public IEnumerable<Book> GetByCategory(int categoryId)
        {
            return _context.Books.Include(b => b.Category).Where(b => b.CategoryId == categoryId).ToList();
        }
    }
} 