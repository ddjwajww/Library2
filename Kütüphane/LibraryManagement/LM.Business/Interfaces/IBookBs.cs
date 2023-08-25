using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.Book;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IBookBs
    {
        Task<ApiResponse<List<BookGetDto>>> GetBooksAsync(params string[] includeList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByAuthorIdAsync(int author, params string[] includeList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByCategoryIdAsync(int category, params string[] includeList);        
        Task<ApiResponse<List<BookGetDto>>> GetBooksByAvailableCopiesAsync(int min, int max, params string[] includeList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByTotalCopiesAsync(int min, int max, params string[] includeList);
        Task<ApiResponse<BookGetDto>> GetByIdAsync(int bookId, params string[] includeList);
        Task<ApiResponse<Book>> InsertAsync(BookPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(BookPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);

    }
}
