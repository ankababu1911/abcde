using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.Data.Interfaces
{
    public interface ILoginAuditRepository : IGenericAsyncRepository<LoginAudit, LoginAuditSummary, LoginAuditFilter>
    {
    }
}
