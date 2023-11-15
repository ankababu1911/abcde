using abcde.Model.Base;

namespace abcde.Model.Filters
{
    public class TenantFilter : BaseTenantFilter
    {
        public string Entity { get; set; }
        public int EntityId { get; set; }
    }
}
