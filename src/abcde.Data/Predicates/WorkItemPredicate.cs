using abcde.Data.Predicates.Base;
using abcde.Model.Filters;
using abcde.Model;
using System.Linq.Expressions;
using LinqKit;

namespace abcde.Data.Predicates
{
    public class WorkItemPredicate : BaseTenantFilterPredicate<WorkItem, WorkItemFilter>
    {
        public new async Task<Expression<Func<WorkItem, bool>>> GetPredicate(WorkItemFilter filter)
        {
            var predicate = await base.GetPredicate(filter);

            if (filter.UserId != Guid.Empty)
            {
                predicate = predicate.And(x => x.UserId == filter.UserId);
            }

            return await Task.Run(((() => predicate)));
        }
    }
}