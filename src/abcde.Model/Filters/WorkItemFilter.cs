using abcde.Model.Base;

namespace abcde.Model.Filters
{
    public class WorkItemFilter : BaseTenantFilter
    {
        public Guid UserId { get; set; }
    }
}