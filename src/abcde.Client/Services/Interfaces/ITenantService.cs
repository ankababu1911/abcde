using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.Model.Identity;

namespace abcde.Client.Services.Interfaces
{
    public interface ITenantService : IGenericService<Tenant, BaseTenantSummary, TenantFilter>
    {
        Task<List<ApplicationUser>> GetUsersForTenant();
    }
}