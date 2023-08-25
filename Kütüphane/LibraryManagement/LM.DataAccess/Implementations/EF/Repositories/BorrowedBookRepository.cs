using Infrastructure.DataAccess.EF;
using LM.DataAccess.Implementations.EF.Context;
using LM.DataAccess.Interfaces;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Implementations.EF.Repositories
{
    public class BorrowedBookRepository : BaseRepository<BorrowedBook, LibraryManagementDBContext>, IBorrowedBookRepository
    {
        public async Task<List<BorrowedBook>> GetByBookIdAsync(int bookId, params string[] includeList)
        {
            return await GetAllAsync(bb => bb.BookId == bookId && bb.IsDeleted == false, includeList);
        }

        public async Task<List<BorrowedBook>> GetByBorrowDateAsync(DateTime borrowDate, params string[] includeList)
        {
            return await GetAllAsync(bb => bb.BorrowDate == borrowDate && bb.IsDeleted == false, includeList);
        }

        public async Task<BorrowedBook> GetByIdAsync(int borrowedBookId, params string[] includeList)
        {
            return await GetAsync(bb => bb.BorrowedBookId == borrowedBookId && bb.IsDeleted == false, includeList);
        }

        public async Task<List<BorrowedBook>> GetByReturnDateAsync(DateTime returnDate, params string[] includeList)
        {
            return await GetAllAsync(bb => bb.ReturnDate == returnDate && bb.IsDeleted == false, includeList);
        }

        public async Task<List<BorrowedBook>> GetByReturnedAsync(bool isReturned, params string[] includeList)
        {
            return await GetAllAsync(bb => bb.Returned == isReturned && bb.IsDeleted == false, includeList);
        }

        public async Task<List<BorrowedBook>> GetByUserIdAsync(int userId, params string[] includeList)
        {
            return await GetAllAsync(bb => bb.UserId == userId && bb.IsDeleted == false, includeList);
        }
    }
}
