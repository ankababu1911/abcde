using abcde.Data.Interfaces;
using abcde.Data.Repositories;
using abcde.Data.Repositories.Base;
using abcde.Model;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using abcde.Model.ViewModels;
using AutoMapper;

namespace abcde.vAPI.Services
{
    public interface IWorkItemService
    {
        /// <summary>
        /// Get All Work Items for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<WorkItem>> GetAllAsync(string userId);

        /// <summary>
        /// Updates the IsPinned flag for the work item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPinned"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> UpdateIsPinnedAsync(Guid id, bool isPinned, string userId);

        /// <summary>
        /// get all incompleted tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>work items</returns>
        Task<List<WorkItem>> GetAllIncompletedTasks(string userId);

        /// <summary>
        /// save prioritized tasks
        /// </summary>
        /// <param name="workItems"></param>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <returns>bool</returns>
        Task<bool> SavePrioritizedTasks(List<Guid> workItems, string userId, string tenantId);

        /// <summary>
        /// get prioritized tasks
        /// </summary>        
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<WorkItem>> GetPrioritizedTasks(string userId);
       Task<WorkItemViewModel> GetWorkItemAsync(Guid id);
       Task UpdateWorkItemCategoriesAsync(WorkItemViewModel workItem);
        Task<WorkItem> UpdateteWorkItemAsync(WorkItem entity);
        Task AddCategoryToWorkItemAsync(Guid id, List<WorkItemCategory> categories);
    }

    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IWorkItemProgressRepository _workItemProgressRepository;
        public readonly IMapper _mapper;

        public WorkItemService(IWorkItemRepository workItemRepository, 
            IWorkItemProgressRepository workItemProgressRepository, IMapper mapper)

        {
            _workItemRepository = workItemRepository;
            _workItemProgressRepository = workItemProgressRepository;
            _mapper = mapper;
        }


        ///< see cref="IWorkItemService.GetAllAsync(string)>"
        public async Task<List<WorkItem>> GetAllAsync(string userId)
        {
            try
            {
                var response = await _workItemRepository.GetAllWorkItemsAsync(userId);
                //now loop through all these workitems and add childern entities based on their parent id and return the list
                //this is done to convert a flat list into a tree so we send back only leafnodes to the client
                foreach (var goal in response)
                {
                    var childern = response.Where(x => x.ParentId == goal.Id);
                    if (childern != null && childern.Any())
                    {
                        foreach (var task in childern)
                        {
                            var subTasks = response.Where(x => x.ParentId == task.Id);
                            if (subTasks != null && subTasks.Any())
                            {
                                if (task.Children == null)
                                {
                                    task.Children = new List<WorkItem>();
                                }
                                task.Children = subTasks.ToList();
                            }
                        }
                        if (goal.Children == null)
                        {
                            goal.Children = new List<WorkItem>();
                        }
                        goal.Children = childern.ToList();
                    }
                }
                var goals = response.Where(x => !x.ParentId.HasValue).ToList();
                return goals;
            }
            catch (Exception)
            {
                throw;
            }
        }

       public async Task<WorkItemViewModel> GetWorkItemAsync(Guid id)
        {
            try
            {
                var response = await _workItemRepository.GetWorkItemAsync(id);
               
                var CategoryList = new List<WorkItemCategory>();
                if (response != null)
                {

                    foreach (var category in response.WorkItemCategories)
                    {
                        CategoryList.Add((WorkItemCategory)category.CategoryId);

                    }
                   
                }
                var workItemMapped = _mapper.Map<WorkItemViewModel>(response);
                if (CategoryList.Count > 0)
                {
                    workItemMapped.Categories = CategoryList;
                }
                return workItemMapped;
            }

            catch (Exception)
            {
                throw;
            }

        }

        ///<see cref="IWorkItemService.UpdateIsPinnedAsync(Guid, bool, string)"/>
        public async Task<string> UpdateIsPinnedAsync(Guid id, bool isPinned, string userId)
        {
            try
            {
                var workItem = await _workItemRepository.GetAsync(id);

                if (workItem == null)
                {
                    return "WorkItemNotFound";
                }
                if (isPinned)
                {
                    var pinnedWorkItems = _workItemRepository.Get(x => x.UserId.ToString() == userId && x.IsPinned);
                    if (pinnedWorkItems != null && pinnedWorkItems.Count() >= 3)
                    {
                        return "YouCanOnlyPin3WorkItems";
                    }
                }
                workItem.IsPinned = isPinned;
                await _workItemRepository.UpdateAsync(workItem);
                return "UpdatedIsPinnedStatus";
            }
            catch (Exception)
            {
                throw;
            }

            
        }

        /// <summary>
        /// Add Category  to WorkItem if provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public async Task AddCategoryToWorkItemAsync(Guid id, List<WorkItemCategory> categories)
        {
            try
            {
                await _workItemRepository.AddCategoryToWorkItemAsync(id, categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///<see cref="IWorkItemService.GetAllIncompletedTasks(string)"/>
        public async Task<List<WorkItem>> GetAllIncompletedTasks(string userId)
        {
            var response = await _workItemRepository.GetAllIncompletedTasks(userId);
            return response.ToList();
        }

        ///<see cref="IWorkItemService.SavePrioritizedTasks(List{Guid}, string, string)"/>
        public async Task<bool> SavePrioritizedTasks(List<Guid> workItems, string userId, string tenantId)
        {
            List<WorkItemProgress> workItemProgresses = new List<WorkItemProgress>();
            //create work item progress objects
            foreach (var item in workItems)
            {
                workItemProgresses.Add(new WorkItemProgress
                {
                    WorkItemId = item,
                    UserId = Guid.Parse(userId),
                    Date = DateTime.Today,
                    CurrentStatus = DayPlanStatus.Planned,
                    Created = DateTime.UtcNow,
                    TenantId = tenantId,
                    IsActive = true
                });
            }
            //save the work item progress objects
            var response = await _workItemProgressRepository.SavePrioritizedTasks(workItemProgresses, userId);
            //update the work item status to inprogress
            foreach (var item in workItems)
            {
                var workItem = await _workItemRepository.GetAsync(item);
                workItem.Status = ItemStatus.InProgress;
                await _workItemRepository.UpdateAsync(workItem);
            }
            return response;
        }

        ///<see cref="IWorkItemService.GetPrioritizedTasks(string)"/>
        public async Task<List<WorkItem>> GetPrioritizedTasks(string userId)
        {            
            var tasks = await _workItemProgressRepository.GetWorkItemProgressForTheDayAsync(userId);
            if(tasks == null || !tasks.Any())
            {
                return null;
            }
            var workItems = new List<WorkItem>();
            foreach (var item in tasks)
            {
                var workItem = await _workItemRepository.GetAsync(item.WorkItemId);
                workItems.Add(workItem);
            }
            return workItems;
        }

        /// <summary>
        /// Modify Categories to WorkItem
        /// </summary>
        /// <param name="entity"></param>
        public async Task UpdateWorkItemCategoriesAsync(WorkItemViewModel entity)
        {
            try
            {

                await _workItemRepository.UpdateCategoriesAsync(entity);
            }
            catch (Exception)
            { throw; }

        }

        /// <summary>
        /// Modify WorkItem
        /// </summary>
        /// <param name="entity"></param>
        public async Task<WorkItem> UpdateteWorkItemAsync(WorkItem entity)
        {

            return await _workItemRepository.UpdateAsync(entity);
        }
    }
}