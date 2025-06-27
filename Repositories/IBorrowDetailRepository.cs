using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface IBorrowDetailRepository
    {
        IEnumerable<BorrowDetail> GetAll();
        BorrowDetail GetById(int id);
        void Add(BorrowDetail borrowDetail);
        void Update(BorrowDetail borrowDetail);
        void Delete(int id);
        IEnumerable<BorrowDetail> GetByBorrowId(int borrowId);
    }
} 