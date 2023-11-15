using abcde.Model.Base;

namespace abcde.Model.Filters
{
    public class TenantAdminFilter : BaseFilter
    {
        public string Entity { get; set; }
        public int EntityId { get; set; }
    }
}
