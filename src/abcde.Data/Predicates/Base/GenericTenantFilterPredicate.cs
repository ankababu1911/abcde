using LinqKit;
using abcde.Model.Base;
using System.Linq.Expressions;

namespace abcde.Data.Predicates.Base
{
    public class GenericTenantFilterPredicate<TEntity, TFilter>
        where TEntity : BaseTenantEntity
        where TFilter : BaseTenantFilter
    {
        public async Task<Expression<Func<TEntity, bool>>> GetPredicate(TFilter filter)
        {
            var predicate = PredicateBuilder.New<TEntity>(true);

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id == filter.Id);
            }
            if (filter.IsActive != null)
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
