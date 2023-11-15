using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.Model.Identity;
using System.Globalization;

namespace abcde.Client.Services
{
    public class TenantService : BaseService<Tenant, BaseTenantSummary, TenantFilter>, ITenantService
    {
        private const string tenantUsers = "users";

        public TenantService(HttpClient httpClient) : base(httpClient) => BaseResource = "Tenants";

        public async Task<List<ApplicationUser>> GetUsersForTenant()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/", BaseResource, tenantUsers);
            var users = await GetAsync<List<ApplicationUser>>(url);
            return users;
        }
    }
}