using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Infra.Data.Context;

namespace UserAPI.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        protected readonly UserApiContext _context;

        public RepositoryBase(UserApiContext context)
        {
            _context = context;
        }

        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }

        public void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TEntity Find(object id, bool excluded = false)
        {
            var entity = _context.Set<TEntity>().Find(id);

            if (excluded)
                return entity;
            else
                return entity == null || entity.Excluded == true ? null : entity;
        }

        public IQueryable<TEntity> QueryAll(
            Expression<Func<TEntity, bool>> predicate = null, bool bringExcluded = false,
            params Expression<Func<TEntity, object>>[] includeProperty)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(predicate ?? All());

            if (!bringExcluded)
                query = GetUnexcludedEntities(query);

            //only brings childs which is in both sides (inner join)
            foreach (var item in includeProperty)
            {
                query = query.Include(item);
            }

            return query;
        }

        private Type GetObjectType<T>(Expression<Func<T, object>> expr)
        {
            if ((expr.Body.NodeType == ExpressionType.Convert) ||
                (expr.Body.NodeType == ExpressionType.ConvertChecked))
            {
                var unary = expr.Body as UnaryExpression;
                if (unary != null)
                    return unary.Operand.Type;
            }
            return expr.Body.Type;
        }

        public Expression<Func<TEntity, bool>> All()
        {
            return x => true;
        }

        /// <summary>
        /// Returns only unexcluded entities (not including childs)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IQueryable<TEntity> GetUnexcludedEntities(IQueryable<TEntity> query)
        {
            return query.Where(entity => !entity.Excluded);
        }

        /// <summary>
        /// Returns only unexcluded child entities, not tested yet
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Expression<Func<TEntity, bool>> GetUnexcludedChildEntities(Expression<Func<TEntity, object>> item)
        {
            MemberExpression memberExp = (MemberExpression)item.Body;
            string expression = memberExp.ToString();

            int skip = 2; //skip letter representation in expression
            string depth = expression.Substring(skip, expression.Length - skip);
            depth += $".{nameof(IEntity.Excluded)}"; //always check for the Excluded property, must always be the last element on expression

            ParameterExpression param = Expression.Parameter(typeof(TEntity));

            Expression parent = param;
            foreach (var way in depth.Split('.'))
                parent = Expression.Property(parent, way);

            Expression valueExp = Expression.Constant(false); //excluded value must be false to bring only valid data
            Expression exp = Expression.Equal(parent, valueExp);

            //check null childs
            //Expression valueExp2 = Expression.Constant(null,typeof(object));

            //var t1 = Expression.Property(param,"Job");
            //Expression exp2 = Expression.Equal(t1, valueExp2);

            //var t2 = Expression.Or(exp2, exp);

            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(exp, param);

            return lambda;
        }

        public TEntity GetById(
            Guid id,
            bool bringExcluded = false,
            params Expression<Func<TEntity, object>>[] includeProperty)
        {
            var query = _context.Set<TEntity>()
                .AsNoTracking()
                .Where(x => bringExcluded == false ? x.Id == id && !x.Excluded : x.Id == id);

            //only brings childs which is in both sides (inner join)
            foreach (var item in includeProperty)
                query = query.Include(item);

            return query.FirstOrDefault();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
