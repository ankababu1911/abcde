using abcde.Data.Interfaces.Base;
using abcde.Model.Filters;
using abcde.Model.Summary;
using abcde.Model;
using Microsoft.AspNetCore.Mvc;
using abcde.Model.ViewModels;

namespace abcde.Data.Interfaces
{
    public interface IWorkItemRepository : IGenericTenantAsyncRepository<WorkItem, WorkItemSummary, WorkItemFilter>
    {
        /// <summary>
        /// get all work items for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkItem>> GetAllWorkItemsAsync(string userId);

        /// <summary>
        /// get all incompleted tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkItem>> GetAllIncompletedTasks(string userId);

        /// <summary>
        /// Add a Categories to a WorkItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        
        Task AddCategoryToWorkItemAsync(Guid id, List<WorkItemCategory> categories);
       Task<WorkItem> GetWorkItemAsync(Guid id);
        Task UpdateCategoriesAsync(WorkItemViewModel entity);
    }    
}
