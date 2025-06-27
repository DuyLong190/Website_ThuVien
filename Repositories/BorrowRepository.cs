using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var borrow = _context.Borrows.Find(id);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Borrow> GetAll()
        {
            return _context.Borrows.Include(b => b.User).ToList();
        }

        public Borrow GetById(int id)
        {
            return _context.Borrows.Include(b => b.User).FirstOrDefault(b => b.Id == id);
        }

        public void Update(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
            _context.SaveChanges();
        }
    }
} 