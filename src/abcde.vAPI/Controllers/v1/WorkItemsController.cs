using abcde.Model.Filters;
using abcde.Model.Summary;
using abcde.Model;
using abcde.vAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using abcde.Data.Interfaces;
using Serilog;
using abcde.vAPI.Services;
using Microsoft.AspNetCore.Authorization;
using abcde.Model.ViewModels;
using AutoMapper;

namespace abcde.vAPI.Controllers.v1
{
   
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class WorkItemsController : GenericsTenantController<WorkItem, WorkItemSummary, WorkItemFilter>
    {
        private readonly IValidator<WorkItemViewModel> _validator;    
        private readonly IWorkItemService _workItemService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        #region Constructor

        public WorkItemsController(IWorkItemRepository workItemRepository, 
            IWorkItemService workItemService, 
            IValidator<WorkItemViewModel> validator,
            IConfiguration configuration,
            IMapper mapper
           ) : base(workItemRepository)
        {
            _validator = validator;
            _workItemService = workItemService;
            _configuration = configuration;
            _mapper = mapper;
        }

        #endregion Constructor

        /// <summary>
        /// Create work item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        
        [HttpPost("CreateWorkItem")]
        public  async Task<IActionResult> CreateWorkItem([FromBody] WorkItemViewModel entity)
        {
            try
            {
                var result = await _validator.ValidateAsync(entity);
               
                if (result.IsValid)
                {
                   
                    var WorkItemMapped = _mapper.Map<WorkItem>(entity);

                    WorkItemMapped.CreatedBy = UserId;
                    WorkItemMapped.UserId = new Guid(UserId);

                    var isWorkItemCreated =  await base.Post(WorkItemMapped);

                    if (isWorkItemCreated != null && (entity.Categories.Count != 0))
                    {
                        await _workItemService.AddCategoryToWorkItemAsync(WorkItemMapped.Id, entity.Categories);

                    }
                    return isWorkItemCreated;
                }

                return BadRequest("WorkitemIsMissingInformationPleaseUpdate");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all work items for the user
        /// </summary>
        /// <returns></returns>
        public override async Task<IActionResult> GetAll()
        {
            try
            {                
                var data = await _workItemService.GetAllAsync(UserId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get WorkItem with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _workItemService.GetWorkItemAsync(id);
                if (result == null)
                {
                    return BadRequest("WorkItemNotFound");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update IsPinned flag for the work item
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="isPinned"></param>
        /// <returns></returns>
        [HttpPost("UpdateIsPinned")]
        public async Task<IActionResult> UpdateIsPinned(Guid workItemId, bool isPinned)
        {
            try
            {
                var response = await _workItemService.UpdateIsPinnedAsync(workItemId, isPinned, UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update WorkItem
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateWorkItem")]
        public  async Task<IActionResult> UpdateWorkItem([FromBody] WorkItemViewModel entity)
        {
            try
            {
                var result = await _validator.ValidateAsync(entity);
                if (result.IsValid)
                {

                    await _workItemService.UpdateWorkItemCategoriesAsync(entity);

                    var WorkItemMapped = _mapper.Map<WorkItem>(entity);

                    WorkItemMapped.CreatedBy = UserId;
                    WorkItemMapped.UserId = new Guid(UserId);
                    var entityUpdated = await _workItemService.UpdateteWorkItemAsync(WorkItemMapped);


                    return Ok(entityUpdated);
                }

                return BadRequest("WorkitemIsMissingInformationPleaseUpdate");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get all incompleted tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllIncompletedTasks")]
        public async Task<IActionResult> GetAllIncompletedTasks()
        {
            try
            {
                var data = await _workItemService.GetAllIncompletedTasks(UserId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Save prioritized tasks
        /// </summary>
        /// <param name="workItems"></param>
        /// <returns></returns>
        [HttpPost("SavePrioritizedTasks")]
        public async Task<IActionResult> SavePrioritizedTasks(List<Guid> workItems)
        {
            try
            {
                int maxPrioritizedTasks = Convert.ToInt32(_configuration["MaxPrioritizedTasks"]);
                //add this setting in appsettings.json

                if(workItems == null || !workItems.Any())
                {
                    return BadRequest("NoWorkItemsFound");
                }
                if(workItems.Count > maxPrioritizedTasks)
                {
                    return BadRequest("OnlyFourWorkItemsCanBePrioritized");
                }

                var response = await _workItemService.SavePrioritizedTasks(workItems, UserId, TenantId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get prioritized tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPrioritizedTasks")]
        public async Task<IActionResult> GetPrioritizedTasks()
        {
            try
            {
                var data = await _workItemService.GetPrioritizedTasks(UserId);
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