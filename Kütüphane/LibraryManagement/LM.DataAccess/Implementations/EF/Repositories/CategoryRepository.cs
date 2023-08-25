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
    public class CategoryRepository : BaseRepository<Category, LibraryManagementDBContext>, ICategoryRepository
    {
        public async Task<List<Category>> GetByCategoryNameAsync(string categoryName)
        {
            return await GetAllAsync(c => c.CategoryName == categoryName && c.IsDeleted == false);
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            return await GetAsync(c => c.CategoryId == categoryId && c.IsDeleted == false);
        }
    }
}
