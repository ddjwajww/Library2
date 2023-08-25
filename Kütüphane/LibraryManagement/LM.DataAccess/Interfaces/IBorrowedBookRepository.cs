using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IBorrowedBookRepository : IBaseRepository<BorrowedBook>
    {
        Task<List<BorrowedBook>> GetByUserIdAsync(int userId, params string[] includeList);
        Task<List<BorrowedBook>> GetByBookIdAsync(int bookId, params string[] includeList);
        Task<List<BorrowedBook>> GetByBorrowDateAsync(DateTime borrowDate, params string[] includeList);
        Task<List<BorrowedBook>> GetByReturnDateAsync(DateTime returnDate, params string[] includeList);
        Task<List<BorrowedBook>> GetByReturnedAsync(bool isReturned, params string[] includeList);
        Task<BorrowedBook> GetByIdAsync(int borrowedBookId, params string[] includeList);
    }
}
