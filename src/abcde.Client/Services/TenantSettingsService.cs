using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Client.Services
{
    public class TenantSettingsService : BaseService<TenantSettings, BaseTenantSummary, TenantFilter>, ITenantSettingsService
    {
        public TenantSettingsService(HttpClient httpClient) : base(httpClient) => BaseResource = "TenantSettings";
    }
}
