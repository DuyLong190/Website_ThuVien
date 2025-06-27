using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public class BorrowDetailRepository : IBorrowDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(BorrowDetail borrowDetail)
        {
            _context.BorrowDetails.Add(borrowDetail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var borrowDetail = _context.BorrowDetails.Find(id);
            if (borrowDetail != null)
            {
                _context.BorrowDetails.Remove(borrowDetail);
                _context.SaveChanges();
            }
        }

        public IEnumerable<BorrowDetail> GetAll()
        {
            return _context.BorrowDetails.ToList();
        }

        public BorrowDetail GetById(int id)
        {
            return _context.BorrowDetails.FirstOrDefault(bd => bd.Id == id);
        }

        public void Update(BorrowDetail borrowDetail)
        {
            _context.BorrowDetails.Update(borrowDetail);
            _context.SaveChanges();
        }

        public IEnumerable<BorrowDetail> GetByBorrowId(int borrowId)
        {
            return _context.BorrowDetails.Include(bd => bd.Book).Where(bd => bd.BorrowId == borrowId).ToList();
        }
    }
} 