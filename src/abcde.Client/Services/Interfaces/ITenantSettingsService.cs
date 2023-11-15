using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Client.Services.Interfaces
{
    public interface ITenantSettingsService : IGenericService<TenantSettings, BaseTenantSummary, TenantFilter>
    { }
}