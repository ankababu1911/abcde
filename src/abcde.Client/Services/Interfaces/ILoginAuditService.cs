using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Summary;


namespace abcde.Client.Services.Interfaces
{
    public interface ILoginAuditService : IGenericService<LoginAudit, LoginAuditSummary, BaseTenantFilter>
    {
    }
}
