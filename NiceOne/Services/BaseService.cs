using Microsoft.EntityFrameworkCore;
using NiceOne.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NiceOne.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity:class
    {
        private NiceOneDbContext Data;
        public BaseService(NiceOneDbContext data)
            => Data = data;

        public async Task<TEntity> FindAsync(int id)
            => await Data.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> search = null,
            Expression<Func<TEntity, object>> orderBy = null, 
            bool ascending = true)
        {
            var query = this.Data.Set<TEntity>().AsQueryable();

            if (search != null)
            {
                query = query.Where(search);
            }

            if (orderBy != null)
            {
                query = ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync<TEntity>();
        }

        public async Task SaveAsync(TEntity entity)
        {
            this.Data.Update(entity);
            await this.Data.SaveChangesAsync();
        }
    }
}
