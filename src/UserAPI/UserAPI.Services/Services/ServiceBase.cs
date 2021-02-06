using System;
using System.Linq;
using System.Linq.Expressions;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Services.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntity
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public void Delete(TEntity obj)
        {
            _repository.Delete(obj);
        }

        public TEntity Find(object id, bool excluded = false)
        {
            return _repository.Find(id, excluded);
        }


        public TEntity GetById(Guid id, bool bringExcluded = false,params Expression<Func<TEntity, object>>[] includeProperty)
        {
            return _repository.GetById(id,bringExcluded, includeProperty);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public IQueryable<TEntity> QueryAll(
            Expression<Func<TEntity, bool>> predicate = null, 
            bool bringExcluded = false, 
            params Expression<Func<TEntity, object>>[] includeProperty)
        {
            return _repository.QueryAll(predicate, bringExcluded, includeProperty);
        }

    }
}
