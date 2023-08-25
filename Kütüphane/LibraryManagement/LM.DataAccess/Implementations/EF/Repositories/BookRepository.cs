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
    public class BookRepository : BaseRepository<Book, LibraryManagementDBContext>, IBookRepository
    {
        public async Task<List<Book>> GetByAuthorAsync(int authorId, params string[] includeList)
        {
            return await GetAllAsync(book => book.AuthorId == authorId && book.IsDeleted == false, includeList);
        }

        public async Task<List<Book>> GetByAvailableCopiesAsync(int min, int max, params string[] includeList)
        {
            return await GetAllAsync(book => book.AvailableCopies > min && book.AvailableCopies < max && book.IsDeleted == false, includeList);
        }

        public async Task<List<Book>> GetByCategoryAsync(int categoryId, params string[] includeList)
        {
            return await GetAllAsync(book => book.CategoryId == categoryId && book.IsDeleted == false, includeList);
        }

        public async Task<Book> GetByIdAsync(int bookId, params string[] includeList)
        {
            return await GetAsync(book => book.BookId == bookId && book.IsDeleted == false, includeList);
        }        

        public async Task<List<Book>> GetByTitleAsync(string title, params string[] includeList)
        {
            return await GetAllAsync(book => book.Title == title && book.IsDeleted == false, includeList);
        }

        public async Task<List<Book>> GetByTotalCopiesAsync(int min, int max, params string[] includeList)
        {
            return await GetAllAsync(book => book.TotalCopies > min && book.TotalCopies < max && book.IsDeleted == false, includeList);
        }
    }
}
