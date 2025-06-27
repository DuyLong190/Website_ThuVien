using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanPhucLongQuang_DoAnWeb.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Task<List<Book>> GetAllAsync();
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        IEnumerable<Book> GetByCategory(int categoryId);
    }
} 