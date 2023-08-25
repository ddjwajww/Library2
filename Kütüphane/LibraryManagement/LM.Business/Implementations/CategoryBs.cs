using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.Author;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.Category;
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
    public class CategoryBs : ICategoryBs
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryBs(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryGetDto>> GetByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var category = await _repo.GetByIdAsync(categoryId);

            if (category == null)
                throw new NotFoundException("Kaynak bulunamadı");

            var dto = _mapper.Map<CategoryGetDto>(category);

            return ApiResponse<CategoryGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<CategoryGetDto>>> GetCategoriesAsync()
        {
            var categories = await _repo.GetAllAsync(c => !c.IsDeleted);

            if (categories.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<CategoryGetDto>>(categories);

            return ApiResponse<List<CategoryGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<CategoryGetDto>>> GetCategoriesByCategoryNameAsync(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new BadRequestException("Kategori adı boş olamaz.");

            var categories = await _repo.GetByCategoryNameAsync(categoryName);

            var returnList = _mapper.Map<List<CategoryGetDto>>(categories);

            return ApiResponse<List<CategoryGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<Category>> InsertAsync(CategoryPostDto dto)
        {
            if (dto.CategoryName.Length < 2)
                throw new BadRequestException("Kategori adı en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Category>(dto);
            var insertedCategory = await _repo.InsertAsync(entity);

            return ApiResponse<Category>.Success(StatusCodes.Status200OK, insertedCategory);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(CategoryPutDto dto)
        {
            if (dto.CategoryId <= 0)
                throw new BadRequestException("Id değeri pozitif bir sayı olmalıdır.");

            if (dto.CategoryName.Length < 2)
                throw new BadRequestException("Kategori adı en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Category>(dto);

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
