using abcde.Model.Base;

namespace abcde.Model.Filters
{
    public class AuditFilter : BaseTenantFilter
    {
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Type { get; set; }
    }
}
