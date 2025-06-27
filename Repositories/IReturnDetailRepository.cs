using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface IReturnDetailRepository
    {
        IEnumerable<ReturnDetail> GetAll();
        ReturnDetail GetById(int id);
        void Add(ReturnDetail returnDetail);
        void Update(ReturnDetail returnDetail);
        void Delete(int id);
    }
} 