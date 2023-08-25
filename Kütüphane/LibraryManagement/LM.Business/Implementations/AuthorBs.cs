using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.Author;
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
    public class AuthorBs : IAuthorBs
    {
        private readonly IAuthorRepository _repo;
        private readonly IMapper _mapper;

        public AuthorBs(IAuthorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<AuthorGetDto>> GetByIdAsync(int authorId)
        {
            if (authorId <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var author = await _repo.GetByIdAsync(authorId);

            if (author == null)
                throw new NotFoundException("Kaynak bulunamadı.");

            var dto = _mapper.Map<AuthorGetDto>(author);

            return ApiResponse<AuthorGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsAsync()
        {
            var authors = await _repo.GetAllAsync(c => !c.IsDeleted);

            if (authors.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<AuthorGetDto>>(authors);

            return ApiResponse<List<AuthorGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByAuthorNameAsync(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
                throw new BadRequestException("Yazar adı boş olamaz.");

            var authors = await _repo.GetByAuthorNameAsync(authorName);

            var returnList = _mapper.Map<List<AuthorGetDto>>(authors);

            return ApiResponse<List<AuthorGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }        

        public async Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByBirthDateAsync(DateTime birthDate)
        {
            var authors = await _repo.GetByBirthDateAsync(birthDate);

            if (authors.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<AuthorGetDto>>(authors);

            return ApiResponse<List<AuthorGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<Author>> InsertAsync(AuthorPostDto dto)
        {
            if (dto.AuthorName.Length < 2)
                throw new BadRequestException("Yazar adı en az 2 karakterden oluşmalıdır");            

            var entity = _mapper.Map<Author>(dto);

            var insertedAuthor = await _repo.InsertAsync(entity);

            return ApiResponse<Author>.Success(StatusCodes.Status200OK, insertedAuthor);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(AuthorPutDto dto)
        {
            if (dto.AuthorId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır.");
            if (dto.AuthorName.Length < 2)
                throw new BadRequestException("Yazar adı en az 2 karakterden oluşmalıdır");            

            var entity = _mapper.Map<Author>(dto);

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
