using System.Linq.Expressions;
using LinqKit;
using abcde.Model.Base;

namespace abcde.Data.Predicates.Base
{
    public abstract class BaseFilterPredicate<TEntity, TFilter>
       where TEntity : BaseEntity
       where TFilter : BaseFilter
    {
        public async Task<Expression<Func<TEntity, bool>>> GetPredicate(TFilter filter)
        {
            var predicate = PredicateBuilder.New<TEntity>();

            if (filter.Id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id == filter.Id);
            }
            if (filter.IsActive.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == filter.IsActive);
            }

            return await Task.Run(() => predicate);
        }
    }
}
