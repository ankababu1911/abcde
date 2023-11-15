using abcde.vAPI.Controllers.Base;
using abcde.Model;
using abcde.Data.Interfaces;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.vAPI.Controllers.v1
{
    public class TenantAdminsController : GenericsController<Tenant, TenantAdminSummary, TenantAdminFilter>
    {
        public TenantAdminsController(ITenantAdminRepository repository) : base(repository)
        { }
    }
}
