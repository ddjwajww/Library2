using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IBorrowedBookBs 
    {
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksAsync(params string[] includeList);
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByUserIdAsync(int userId, params string[] includeList);
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByBookIdAsync(int bookId, params string[] includeList);
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByBorrowDateAsync(DateTime borrowDate, params string[] includeList);
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByReturnDateAsync(DateTime returnDate, params string[] includeList);
        Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByReturnedAsync(bool isReturned, params string[] includeList);
        Task<ApiResponse<BorrowedBookGetDto>> GetByIdAsync(int borrowedBookId, params string[] includeList);
        Task<ApiResponse<BorrowedBook>> InsertAsync(BorrowedBookPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(BorrowedBookPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);


    }
}
