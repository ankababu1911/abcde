using abcde.Model;
using abcde.Data.Interfaces;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.vAPI.Controllers.Base;

namespace abcde.vAPI.Controllers.v1
{
    public class TenantSettingsController : GenericsTenantController<TenantSettings, BaseTenantSummary, TenantFilter>
    {
        public TenantSettingsController(ITenantSettingsRepository repository) : base(repository)
        { }
    }
}
