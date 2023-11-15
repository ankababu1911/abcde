using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Data.Interfaces
{
    public interface ITenantSettingsRepository : IGenericTenantAsyncRepository<TenantSettings, BaseTenantSummary, BaseTenantFilter>
    {
        public Task<string> GetTenantCulture(string tenantId);
    }
}
