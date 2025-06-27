using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanPhucLongQuang_DoAnWeb.Data;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public class ReturnDetailRepository : IReturnDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ReturnDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ReturnDetail returnDetail)
        {
            _context.ReturnDetails.Add(returnDetail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var returnDetail = _context.ReturnDetails.Find(id);
            if (returnDetail != null)
            {
                _context.ReturnDetails.Remove(returnDetail);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ReturnDetail> GetAll()
        {
            return _context.ReturnDetails.Include(rd => rd.Book).ToList();
        }

        public ReturnDetail GetById(int id)
        {
            return _context.ReturnDetails.FirstOrDefault(rd => rd.Id == id);
        }

        public void Update(ReturnDetail returnDetail)
        {
            _context.ReturnDetails.Update(returnDetail);
            _context.SaveChanges();
        }
    }
} 