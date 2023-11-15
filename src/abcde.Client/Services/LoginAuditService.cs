using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Summary;


namespace abcde.Client.Services
{
    public class LoginAuditService : BaseService<LoginAudit, LoginAuditSummary, BaseTenantFilter>, ILoginAuditService
    {
        public LoginAuditService(HttpClient httpClient) : base(httpClient) => BaseResource = "LoginAudits";
    }
}
