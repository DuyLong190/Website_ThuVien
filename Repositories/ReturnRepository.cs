using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly ApplicationDbContext _context;

        public ReturnRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Return ret)
        {
            _context.Returns.Add(ret);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ret = _context.Returns.Find(id);
            if (ret != null)
            {
                _context.Returns.Remove(ret);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Return> GetAll()
        {
            return _context.Returns
                .Include(r => r.Borrow)
                .ThenInclude(b => b.User)
                .Include(r => r.ReturnDetails)
                .ToList();
        }

        public Return GetById(int id)
        {
            return _context.Returns
                .Include(r => r.Borrow)
                .ThenInclude(b => b.User)
                .Include(r => r.ReturnDetails)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Update(Return ret)
        {
            _context.Returns.Update(ret);
            _context.SaveChanges();
        }
    }
} 