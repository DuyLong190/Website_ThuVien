using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
} 