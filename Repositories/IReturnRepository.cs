using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface IReturnRepository
    {
        IEnumerable<Return> GetAll();
        Return GetById(int id);
        void Add(Return ret);
        void Update(Return ret);
        void Delete(int id);
    }
} 