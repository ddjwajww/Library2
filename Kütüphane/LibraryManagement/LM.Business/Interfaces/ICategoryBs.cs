using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.Category;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface ICategoryBs
    {
        Task<ApiResponse<List<CategoryGetDto>>> GetCategoriesAsync();
        Task<ApiResponse<List<CategoryGetDto>>> GetCategoriesByCategoryNameAsync(string categoryName);
        Task<ApiResponse<CategoryGetDto>> GetByIdAsync(int categoryId);
        Task<ApiResponse<Category>> InsertAsync(CategoryPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(CategoryPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
