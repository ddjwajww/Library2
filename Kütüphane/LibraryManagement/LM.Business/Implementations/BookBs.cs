using AutoMapper;
using Infrastructure.DataAccess;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.Book;
using LM.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Implementations
{
    public class BookBs : IBookBs
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookBs(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksAsync(params string[] includeList)
        {
            var books = await _repo.GetAllAsync(c => !c.IsDeleted, includeList: includeList);

            if (books.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByAuthorIdAsync(int author, params string[] includeList)
        {
            if (author <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var books = await _repo.GetByAuthorAsync(author, includeList);

            if (books.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByAvailableCopiesAsync(int min, int max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("Maximum değer minimum değerden büyük olmalıdır");

            if (min < 0 || max < 0)
                throw new BadRequestException("Lütfen pozitif değerler giriniz");

            var books = await _repo.GetByAvailableCopiesAsync(min, max, includeList);

            if (books.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }
        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByCategoryIdAsync(int category, params string[] includeList)
        {
            if (category <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var books = await _repo.GetByCategoryAsync(category, includeList);

            if (books.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }        

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByTotalCopiesAsync(int min, int max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("Maximum değer minimum değerden büyük olmalıdır");

            if (min < 0 || max < 0)
                throw new BadRequestException("Lütfen pozitif değerler giriniz");

            var books = await _repo.GetByTotalCopiesAsync(min, max, includeList);

            if (books.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<BookGetDto>> GetByIdAsync(int bookId, params string[] includeList)
        {
            if (bookId <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var book = await _repo.GetByIdAsync(bookId, includeList);

            if (book == null)
                throw new NotFoundException("Kaynak bulunamadı");

            var dto = _mapper.Map<BookGetDto>(book);

            return ApiResponse<BookGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<Book>> InsertAsync(BookPostDto dto)
        {
            if (dto.Title.Length < 2)
                throw new BadRequestException("Kitap adı en az 2 karakterden oluşmalıdır");
            if (dto.CategoryId <= 0)
                throw new BadRequestException("Kategori id değeri 0'dan büyük olmalıdır");
            if (dto.AuthorId <= 0)
                throw new BadRequestException("Yazar id değeri 0'dan büyük olmalıdır");
            if (dto.TotalCopies <= 0)
                throw new BadRequestException("Toplam kopya sayısı 0'dan büyük olmalıdır");

            var entity = _mapper.Map<Book>(dto);

            var insertedBook = await _repo.InsertAsync(entity);

            return ApiResponse<Book>.Success(StatusCodes.Status200OK, insertedBook);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(BookPutDto dto)
        {
            if (dto.BookId <= 0)
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            if (dto.Title.Length < 2)
                throw new BadRequestException("Kitap adı en az 2 karakterden oluşmalıdır");
            if (dto.CategoryId <= 0)
                throw new BadRequestException("Kategori id değeri 0'dan büyük olmalıdır");
            if (dto.AuthorId <= 0)
                throw new BadRequestException("Yazar id değeri 0'dan büyük olmalıdır");
            if (dto.TotalCopies <= 0)
                throw new BadRequestException("Toplam kopya sayısı 0'dan büyük olmalıdır");

            var entity = _mapper.Map<Book>(dto);

            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }



    }
}
