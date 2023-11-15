using abcde.Data.Interfaces.Base;
using abcde.Data.Services;
using abcde.Model.Base;
using abcde.Model.Identity;
using abcde.vAPI.ActionFilters;
using abcde.vAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System.Security.Claims;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace abcde.vAPI.Controllers.Base
{
    [Authorize]
    [ServiceFilter(typeof(ValidateEntityExistsAttribute<BaseEntity>))]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public abstract class GenericsTenantController<TEntity, TSummary, TFilter> : Controller
        where TEntity : BaseTenantEntity
        where TSummary : BaseTenantSummary
        where TFilter : BaseTenantFilter, new()
    {
        protected readonly IGenericTenantAsyncRepository<TEntity, TSummary, TFilter> GenericTenantAsyncRepository;
        private readonly IIdentityService identityService;

        protected const string ClaimUserName = "username";
        protected const string ClaimName = "name";
        protected const string ClaimUserRole = "role";

        private const string httpContextItemTenantId = "tenantId";
        private const string httpContextItemSubTenantId = "subtenantId";
        private const string httpContextItemDepartmentId = "departmentId";

        protected string _TenantId;

        #region ctor

        protected GenericsTenantController(IGenericTenantAsyncRepository<TEntity, TSummary, TFilter> genericAsyncRepository)
        {
            GenericTenantAsyncRepository = genericAsyncRepository;
        }

        protected GenericsTenantController(IGenericTenantAsyncRepository<TEntity, TSummary, TFilter> genericAsyncRepository, ITenantHandlerService service)
        {
            GenericTenantAsyncRepository = genericAsyncRepository;
            _TenantId = service.Tenant;
        }

        protected GenericsTenantController(IGenericTenantAsyncRepository<TEntity, TSummary, TFilter> genericAsyncRepository,
            IIdentityService identityService,
            ITenantHandlerService service)
        {
            GenericTenantAsyncRepository = genericAsyncRepository;
            this.identityService = identityService;
            _TenantId = service.Tenant;
        }

        #endregion ctor

        /// <summary>
        /// Validation attribute attempts to fetch the TenantId passed
        /// through as the subdomain and saves in HttpContext items
        /// </summary>
        protected string TenantId
        {
            get
            {
                var item = GetClaimValue(Constants.TenantID);

                return item == null ? string.Empty : item.ToString();
            }
        }

        /// <summary>
        /// Validation attribute attempts to fetch the TenantId passed
        /// through as the subdomain and saves in HttpContext items
        /// </summary>
        protected string UserId
        {
            get
            {
                var item = GetClaimValue(Constants.UserID);

                return item == null ? string.Empty : item.ToString();
            }
        }

        /// <summary>
        /// Get claim value
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        protected async Task<string> GetClaimValueAsync(string claim)
        {
            string claimValue = string.Empty;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                claimValue = identity.Claims.Where(c => c.Type == claim).Select(c => c.Value).SingleOrDefault();
            }

            return await Task.Run(() => claimValue);
        }

        /// <summary>
        /// Get claim value
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        protected string GetClaimValue(string claim)
        {
            string claimValue = string.Empty;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                claimValue = identity.Claims.Where(c => c.Type == claim).Select(c => c.Value).SingleOrDefault();
            }

            return claimValue;
        }

        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns></returns>
        protected UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                };
            }
            return null;
        }

        /// <summary>
        /// Get first, test helper method
        /// </summary>
        /// <returns></returns>
        [HttpGet("first")]
        public async Task<IActionResult> GetFirst()
        {
            try
            {
                return Ok(await GenericTenantAsyncRepository.GetFirstAsync());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get
        ///  </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getAsync")]
        public virtual async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await GenericTenantAsyncRepository.GetAsync(TenantId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest();
            }
        }

        /// <summary>
        /// Get by id (int)
        ///  </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var result = await GenericTenantAsyncRepository.GetAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get by id (string)
        ///  </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byString/{id}")]
        public virtual async Task<IActionResult> GetAsync(string id)
        {
            var result = await GenericTenantAsyncRepository.GetAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public virtual async Task<IActionResult> GetAll()
        {
            var data = GenericTenantAsyncRepository.Get();
            return base.Ok(data.ToList());
        }

        /// <summary>
        /// Get count
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            return base.Ok(await GenericTenantAsyncRepository.GetCountAsync());
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/{id}")]
        public async Task<IActionResult> GetSummary(Guid id)
        {
            return Ok(await GenericTenantAsyncRepository.GetSummaryAsync(id));
        }

        /// <summary>
        /// Get summary using TenantId
        /// </summary>
        /// <returns></returns>
        [HttpGet("getSummary")]
        public async Task<IActionResult> GetSummaryAsync()
        {
            return Ok(await GenericTenantAsyncRepository.GetSummaryAsync(TenantId));
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/all")]
        public virtual async Task<IActionResult> GetAllSummary()
        {
            try
            {
                return base.Ok(await GenericTenantAsyncRepository.GetFilteredSummaryAsync(new TFilter
                {
                    TenantId = TenantId,
                }));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get filtered
        /// </summary>
        /// <returns></returns>
        [HttpGet("filtered")]
        public virtual async Task<IActionResult> GetFiltered([FromQuery] TFilter filter)
        {
            filter.TenantId = TenantId;

            return Ok(await GenericTenantAsyncRepository.GetFilteredAsync(filter));
        }

        /// <summary>
        /// Get filtered summary
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/filtered")]
        public virtual async Task<IActionResult> GetSummaryFiltered([FromQuery] TFilter filter)
        {
            filter.TenantId = TenantId;

            return Ok(await GenericTenantAsyncRepository.GetFilteredSummaryAsync(filter));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] TEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(TenantId))
                {
                    entity.TenantId = TenantId;
                }

                entity.LastModifiedBy = GetCurrentUser().Email;

                return Ok(await GenericTenantAsyncRepository.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                var modelStateDictionary = new ModelStateDictionary();

                modelStateDictionary.AddModelError("PUT", ex.Message);

                return BadRequest(modelStateDictionary);
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.TenantId))
                {
                    entity.TenantId = TenantId;
                }

                //entity.CreatedBy = GetCurrentUser().Email;

                var result = await GenericTenantAsyncRepository.InsertAsync(entity);

                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                var modelStateDictionary = new ModelStateDictionary();

                modelStateDictionary.AddModelError("POST", ex.Message);

                return BadRequest(modelStateDictionary);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await GenericTenantAsyncRepository.DeleteAsync(id, await GetClaimValueAsync(ClaimUserName));

            return await Task.Run(() => Ok());
        }

        /// <summary>
        /// Basic health check
        ///  </summary>
        /// <returns></returns>
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(DateTime.Now);
        }
    }
}