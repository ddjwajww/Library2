using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.Author;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Implementations
{
    public class BorrowedBookBs : IBorrowedBookBs
    {
        private readonly IBorrowedBookRepository _repo;
        private readonly IMapper _mapper;

        public BorrowedBookBs(IBorrowedBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksAsync(params string[] includeList)
        {
            var borrowedBooks = await _repo.GetAllAsync(c => !c.IsDeleted, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByBookIdAsync(int bookId, params string[] includeList)
        {
            if (bookId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var borrowedBooks = await _repo.GetByBookIdAsync(bookId, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByBorrowDateAsync(DateTime borrowDate, params string[] includeList)
        {
            var borrowedBooks = await _repo.GetByBorrowDateAsync(borrowDate, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByReturnDateAsync(DateTime returnDate, params string[] includeList)
        {
            var borrowedBooks = await _repo.GetByReturnDateAsync(returnDate, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByUserIdAsync(int userId, params string[] includeList)
        {
            if (userId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var borrowedBooks = await _repo.GetByUserIdAsync(userId, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BorrowedBookGetDto>>> GetBorrowedBooksByReturnedAsync(bool isReturned, params string[] includeList)
        {
            var borrowedBooks = await _repo.GetByReturnedAsync(isReturned, includeList);

            if (borrowedBooks.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı");

            var returnList = _mapper.Map<List<BorrowedBookGetDto>>(borrowedBooks);

            return ApiResponse<List<BorrowedBookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<BorrowedBookGetDto>> GetByIdAsync(int borrowedBookId, params string[] includeList)
        {
            if (borrowedBookId <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var borrowedBook = await _repo.GetByIdAsync(borrowedBookId, includeList);

            if (borrowedBook == null)
                throw new NotFoundException("Kaynak Bulunamadı");

            var dto = _mapper.Map<BorrowedBookGetDto>(borrowedBook);

            return ApiResponse<BorrowedBookGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<BorrowedBook>> InsertAsync(BorrowedBookPostDto dto)
        {
            if (dto.BookId <= 0)
                throw new BadRequestException("Kitap id pozitif bir değer olmalıdır");
            if (dto.UserId <= 0)
                throw new BadRequestException("Kullanıcı id pozitif bir değer olmalıdır");

            var entity = _mapper.Map<BorrowedBook>(dto);

            var insertedBorrowedBook = await _repo.InsertAsync(entity);

            return ApiResponse<BorrowedBook>.Success(StatusCodes.Status200OK, insertedBorrowedBook);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(BorrowedBookPutDto dto)
        {
            if (dto.BorrowedBookId <= 0)
                throw new BadRequestException("Ödünç alınan kitap id pozitif bir değer olmalıdır.");
            if (dto.BookId <= 0)
                throw new BadRequestException("Kitap id 0'dan büyük bir değer olmalıdır");
            if (dto.UserId <= 0)
                throw new BadRequestException("Kullanıcı id 0'dan büyük bir değer olmalıdır");

            var entity = _mapper.Map<BorrowedBook>(dto);
            
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
