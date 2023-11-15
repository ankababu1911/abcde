using abcde.Data.Predicates.Base;
using abcde.Model;
using abcde.Model.Filters;
using LinqKit;
using System.Linq.Expressions;

namespace abcde.Data.Predicates
{
    public class NotePredicate : BaseTenantFilterPredicate<Note, NoteFilter>
    {
        public new async Task<Expression<Func<Note, bool>>> GetPredicate(NoteFilter filter)
        {
            var predicate = await base.GetPredicate(filter);

            if (!string.IsNullOrEmpty(filter.DateString))
            {
                predicate = predicate.And(x => x.Date.Date == Convert.ToDateTime(filter.DateString).Date);
            }
            if (filter.UserId.HasValue)
            {
                predicate = predicate.And(x => x.UserId == filter.UserId);
            }

            return await Task.Run(((() => predicate)));
        }
    }
}