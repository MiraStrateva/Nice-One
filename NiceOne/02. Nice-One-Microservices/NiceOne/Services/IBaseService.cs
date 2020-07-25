namespace NiceOne.Services
{
    using MassTransit.Monitoring.Performance;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IBaseService<TDBContext, TEntity> 
        where TEntity : class
        where TDBContext : DbContext
    {
        Task<TEntity> FindAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> search = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool ascending = true);
        Task MarkMessageAsPublished(int id);
        Task SaveAsync(TEntity entity, params Message[] messages);
        Task CreateAsync(TEntity entity, params Message[] messages);
    }
}
