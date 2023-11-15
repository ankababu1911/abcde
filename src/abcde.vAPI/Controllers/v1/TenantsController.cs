using abcde.Model;
using abcde.Data.Interfaces;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.vAPI.Controllers.Base;
using abcde.vAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace abcde.vAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class TenantsController : GenericsTenantController<Tenant, BaseTenantSummary, TenantFilter>
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantRepository repository, ITenantService tenantService) : base(repository)
        {
            _tenantService = tenantService;
        }

        [HttpGet("users")]
        public IActionResult GetUsersForTenant()
        {
            var users = _tenantService.GetTenants(new Guid(TenantId));
            return Ok(users);
        }

		[AllowAnonymous]
        [HttpGet("GetTenantByConnectionStringCode")]
        public async Task<IActionResult> GetTenantByConnectionStringCode(string connectionStringCode)
        {
            try
            {
                var data = await _tenantService.GetTenantByConnectionStringCodeAsync(connectionStringCode);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}