using System;
using System.Linq;
using System.Linq.Expressions;

namespace UserAPI.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IEntity
    {
        void Add(TEntity obj);
        void Delete(TEntity obj);
        void Update(TEntity obj);
        IQueryable<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate = null, bool bringExcluded = false,params Expression<Func<TEntity, object>>[] includeProperty);

        TEntity GetById(Guid id, bool bringExcluded = false, params Expression<Func<TEntity, object>>[] includeProperty);
        TEntity Find(object id, bool excluded = false);

    }
}
