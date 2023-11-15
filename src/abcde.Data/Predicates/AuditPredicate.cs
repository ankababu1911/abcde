using LinqKit;
using System.Linq.Expressions;
using abcde.Model;
using abcde.Data.Predicates.Base;
using abcde.Model.Filters;

namespace abcde.Data.Predicates
{
    public class AuditPredicate : BaseTenantFilterPredicate<Audit, AuditFilter>
    {
        public new async Task<Expression<Func<Audit, bool>>> GetPredicate(AuditFilter filter)
        {
            var predicate = await base.GetPredicate(filter);

            // IF WE WANTED TO ADD ADDITIONAL FILTERS WE WOULD DO IT LIKE THIS 
            if (filter.DateFrom != null)
            {
                predicate = predicate.And(x => x.Datestamp >= filter.DateFrom);
            }

            if (filter.DateTo != null)
            {
                predicate = predicate.And(x => x.Datestamp <= filter.DateTo);
            }

            if (!string.IsNullOrEmpty(filter.Type))
            {
                predicate = predicate.And(x => x.Entity == filter.Type);
            }

            return await Task.Run(() => predicate);
        }
    }
}
