using System.Linq.Expressions;
using LinqKit;
using abcde.Model.Base;
using System;

namespace abcde.Data.Predicates.Base
{
    public class BaseTenantFilterPredicate<TEntity, TFilter>
       where TEntity : BaseTenantEntity
       where TFilter : BaseTenantFilter
    {
        protected ExpressionStarter<TEntity> predicate;

        public async Task<Expression<Func<TEntity, bool>>> GetPredicate(TFilter filter)
        {
            predicate = PredicateBuilder.New<TEntity>();

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id == filter.Id);
            }
            if (filter.IsActive.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == filter.IsActive);
            }
            if (!string.IsNullOrEmpty(filter.TenantId))
            {
                predicate = predicate.And(x => x.TenantId == filter.TenantId);
            }

            return await Task.Run(() => predicate);
        }
    }
}
