using abcde.Model.Base;

namespace abcde.Model.Summary
{
    public class TenantAdminSummary : BaseSummary
    {
        public string TenantId { get; set; }
        public TenantStatus Status { get; set; }
    }
}
