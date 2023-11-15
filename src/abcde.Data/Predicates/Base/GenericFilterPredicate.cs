using LinqKit;
using abcde.Model.Base;
using System.Linq.Expressions;

namespace abcde.Data.Predicates.Base
{
    public class GenericFilterPredicate<TEntity, TFilter>
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
            if (filter.IsActive != null)
            {
                predicate = predicate.And(x => x.IsActive == filter.IsActive);
            }

            return await Task.Run(() => predicate);
        }
    }
}
