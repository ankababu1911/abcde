using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Client.Services
{
    public class AuditService : BaseService<Audit, BaseSummary, AuditFilter>, IAuditService
    {
        public AuditService(HttpClient httpClient) : base(httpClient) => BaseResource = "Audits";
    }
}
