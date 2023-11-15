using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using abcde.Data.Interfaces.Base;
using abcde.Model.Base;
using Serilog;

//using abcde.API.ActionFilters;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace abcde.vAPI.Controllers.Base
{
    //[Authorize]
    //[ServiceFilter(typeof(ValidateEntityExistsAttribute<BaseEntity>))]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public abstract class GenericsController<TEntity, TSummary, TFilter> : Controller
        where TEntity : BaseEntity
        where TSummary : BaseSummary
        where TFilter : BaseFilter, new()
    {
        protected readonly IGenericAsyncRepository<TEntity, TSummary, TFilter> GenericAsyncRepository;

        #region ctor

        protected GenericsController(IGenericAsyncRepository<TEntity, TSummary, TFilter> genericAsyncRepository)
        {
            GenericAsyncRepository = genericAsyncRepository;
        }

        #endregion ctor

        /// <summary>
        /// Get claim value
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        protected async Task<string> GetClaimValue()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            string claimValue = string.Empty;

            if (identity != null)
            {
                claimValue = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            }

            return await Task.Run(() => claimValue);
        }

        /// <summary>
        /// Get first
        /// </summary>
        /// <returns></returns>
        [HttpGet("first")]
        public async Task<IActionResult> GetFirst()
        {
            try
            {
                return Ok(await GenericAsyncRepository.GetFirstAsync());
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
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var result = await GenericAsyncRepository.GetAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public virtual async Task<IActionResult> GetAll()
        {
            return base.Ok(await GenericAsyncRepository.GetAllAsync());
        }

        /// <summary>
        /// Get count
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            return base.Ok(await GenericAsyncRepository.GetCountAsync());
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/{id}")]
        public async Task<IActionResult> GetSummary(Guid id)
        {
            return Ok(await GenericAsyncRepository.GetSummaryAsync(id));
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/all")]
        public virtual async Task<IActionResult> GetAllSummary()
        {
            return base.Ok(await GenericAsyncRepository.GetAllSummaryAsync());
        }

        /// <summary>
        /// Get Filtered
        /// </summary>
        /// <returns></returns>
        [HttpGet("filtered")]
        public virtual async Task<IActionResult> GetFiltered([FromQuery] TFilter filter)
        {
            return Ok(await GenericAsyncRepository.GetFilteredAsync(filter));
        }

        /// <summary>
        /// Get Filtered Summary
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary/filtered")]
        public virtual async Task<IActionResult> GetSummaryFiltered([FromQuery] TFilter filter)
        {
            var r = await GenericAsyncRepository.GetFilteredSummaryAsync(filter);

            return Ok(r);
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
                entity.LastModifiedBy = await GetClaimValue();

                return Ok(await GenericAsyncRepository.UpdateAsync(entity));
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpPut("Summary")]
        public virtual async Task<IActionResult> PutSummary([FromBody] TSummary entity)
        {
            try
            {
                entity.LastModifiedBy = await GetClaimValue();

                return Ok(await GenericAsyncRepository.UpdateSummaryAsync(entity));
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
                entity.CreatedBy = await GetClaimValue();

                var result = await GenericAsyncRepository.InsertAsync(entity);

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
        public async Task Delete(Guid id)
        {
            await GenericAsyncRepository.DeleteAsync(id, await GetClaimValue());
        }

        /// <summary>
        /// Basic Health Check
        ///  </summary>
        /// <returns></returns>
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(DateTime.Now);
        }
    }
}