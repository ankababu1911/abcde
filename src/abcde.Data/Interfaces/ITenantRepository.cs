using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.Model.Identity;

namespace abcde.Data.Interfaces
{
    public interface ITenantRepository : IGenericTenantAsyncRepository<Tenant, BaseTenantSummary, TenantFilter>
    {
        /// <summary>
        /// Get all the users that belong to a tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<ApplicationUser> GetUsersForTenant(Guid tenantId);
        /// <summary>
        /// Get Tenant based on ConnectionStringCode
        /// </summary>
        /// <param name="connectionStringCode"></param>
        /// <returns>return Tenant details </returns>
        Task<List<Tenant>> GetTenantByConnectionStringCodeAsync(string connectionStringCode);
    }
}