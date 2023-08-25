using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> GetByCategoryNameAsync(string categoryName);
        Task<Category> GetByIdAsync(int categoryId);
    }
}
