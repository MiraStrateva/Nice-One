using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NiceOne.Services
{
    public interface IBaseService<TEntity> 
        where TEntity : class
    {
        Task<TEntity> FindAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> search = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool ascending = true);
        Task SaveAsync(TEntity entity);
    }
}
