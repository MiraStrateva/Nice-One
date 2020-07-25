namespace NiceOne.Services
{
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class BaseService<TDBContext, TEntity> : IBaseService<TDBContext, TEntity> 
        where TEntity : class
        where TDBContext : DbContext
    {
        protected TDBContext Data { get; set; }
        protected BaseService(TDBContext data) 
            => Data = data;

        public virtual async Task<TEntity> FindAsync(int id)
            => await Data.Set<TEntity>().FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
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

        public virtual async Task SaveAsync(TEntity entity, params Message[] messages)
        {
            foreach (var message in messages)
            {
                this.Data.Add(message);
            }

            this.Data.Update(entity);
            await this.Data.SaveChangesAsync();
        }

        public virtual async Task CreateAsync(TEntity entity, params Message[] messages)
        {
            foreach (var message in messages)
            {
                this.Data.Add(message);
            }

            Data.Set<TEntity>().Add(entity);
            await Data.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Data.Set<TEntity>().Remove(entity);
            await Data.SaveChangesAsync();
        }

        public async Task MarkMessageAsPublished(int id)
        {
            var message = await Data.FindAsync<Message>(id);
            message.MarkAsPublished();

            await Data.SaveChangesAsync();
        }
    }
}
