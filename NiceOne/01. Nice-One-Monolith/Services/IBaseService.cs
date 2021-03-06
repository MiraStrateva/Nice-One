﻿namespace NiceOne.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IBaseService<TEntity> 
        where TEntity : class
    {
        Task<TEntity> FindAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> search = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool ascending = true);
        Task SaveAsync(TEntity entity);
        Task CreateAsync(TEntity entity);
    }
}
