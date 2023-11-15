using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Client.Services.Interfaces
{
    public interface IAuditService : IGenericService<Audit, BaseSummary, AuditFilter>
    { }
}