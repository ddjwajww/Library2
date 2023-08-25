using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IBaseRepository<TEntity> 
        where TEntity : class,IEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includeList);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList);
        Task<TEntity> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
