using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.Client.Services
{
    public class TenantAdminService : BaseService<Tenant, TenantAdminSummary, TenantAdminFilter>, ITenantAdminService
    {
        public TenantAdminService(HttpClient httpClient) : base(httpClient) => BaseResource = "TenantAdmins";

        //public override async Task<Tenant> GetInstanceAsync(Guid id)
        //{
        //    var guid = Guid.NewGuid();

        //    return id == 0 ? new Tenant() { TenantId = guid } : await this.GetAsync(id);
        //}
    }
}
