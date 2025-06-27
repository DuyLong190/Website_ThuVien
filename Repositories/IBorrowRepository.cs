using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface IBorrowRepository
    {
        IEnumerable<Borrow> GetAll();
        Borrow GetById(int id);
        void Add(Borrow borrow);
        void Update(Borrow borrow);
        void Delete(int id);
    }
} 