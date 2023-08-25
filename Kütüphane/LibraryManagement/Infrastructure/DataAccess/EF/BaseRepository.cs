using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class,IEntity
        where TContext : DbContext,new()
    {       
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList)
        {
            using var context = new TContext();

            IQueryable<TEntity> dbSet = context.Set<TEntity>();

            if (includeList != null)
            {
                foreach (var include in includeList)
                {
                    dbSet = dbSet.Include(include);
                }
            }

            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includeList)
        {
            using var context = new TContext();

            IQueryable<TEntity> dbSet = context.Set<TEntity>();

            if (includeList != null)
            {
                foreach (var include in includeList)
                {
                    dbSet = dbSet.Include(include);
                }
            }

            if (predicate == null)
                return await dbSet.ToListAsync();

            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using var context = new TContext();

            var entityEntry = await context.Set<TEntity>().AddAsync(entity);

            await context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var context = new TContext();

            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(TEntity entity)
        {
            using var context = new TContext();

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
