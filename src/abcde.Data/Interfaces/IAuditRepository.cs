using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Data.Interfaces
{
    public interface IAuditRepository : IGenericAsyncRepository<Audit, BaseSummary, AuditFilter>
    { }
}
