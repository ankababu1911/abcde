using abcde.Model.Filters;
using abcde.Model.Summary;
using abcde.Model;
using abcde.Model.ViewModels;

namespace abcde.Client.Services.Interfaces
{
    public interface IWorkItemService : IGenericService<WorkItem, WorkItemSummary, WorkItemFilter>
    {
        /// <summary>
        /// Updates the IsPinned flag for the work item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPinned"></param>
        /// <returns></returns>
        Task<string> UpdateIsPinned(Guid id, bool isPinned);

        /// <summary>
        /// Get all incompleted tasks
        /// </summary>
        /// <returns></returns>
        Task<List<WorkItem>> GetAllIncompletedTasks();

        /// <summary>
        /// Save prioritized tasks
        /// </summary>
        /// <param name="workItems"></param>
        /// <returns></returns>
        Task<string> SavePrioritizedTasks(List<Guid> workItems);

        /// <summary>
        /// Get prioritized tasks
        /// </summary>
        /// <returns></returns>
        Task<List<WorkItem>> GetPrioritizedTasks();

        /// <summary>
        /// Create WorkItem
        /// </summary>
        /// <returns></returns>
        Task<WorkItem> CreateWorkItem(WorkItemViewModel entity);

        /// <summary>
        /// Update WorkItem
        /// </summary>
        /// <returns></returns>
        Task<WorkItem> UpdateWorkItem(WorkItemViewModel entity);
    }
}
