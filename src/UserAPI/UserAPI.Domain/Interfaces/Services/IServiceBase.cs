using System;
using System.Linq;
using System.Linq.Expressions;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class,IEntity
    {
        void Add(TEntity obj);
        void Delete(TEntity obj);
        void Update(TEntity obj);
        TEntity GetById(Guid id, bool bringExcluded = false, params Expression<Func<TEntity, object>>[] includeProperty);
        TEntity Find(object id, bool excluded = false);
        IQueryable<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate = null, bool bringExcluded = false, params Expression<Func<TEntity, object>>[] includeProperty);

    }
}
