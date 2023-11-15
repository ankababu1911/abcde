using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.Client.Services.Interfaces
{
    public interface ITenantAdminService : IGenericService<Tenant, TenantAdminSummary, TenantAdminFilter>
    { }
}