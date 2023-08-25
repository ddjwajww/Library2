using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<List<Book>> GetByTitleAsync(string title, params string[] includeList);
        Task<List<Book>> GetByAuthorAsync(int authorId, params string[] includeList);
        Task<List<Book>> GetByCategoryAsync(int categoryId, params string[] includeList);        
        Task<List<Book>> GetByAvailableCopiesAsync(int min, int max, params string[] includeList);
        Task<List<Book>> GetByTotalCopiesAsync(int min, int max, params string[] includeList);
        Task<Book> GetByIdAsync(int bookId, params string[] includeList);
    }
}
