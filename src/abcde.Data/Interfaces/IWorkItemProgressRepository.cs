using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Data.Interfaces
{
    public interface IWorkItemProgressRepository: IGenericTenantAsyncRepository<WorkItemProgress, BaseTenantSummary, BaseTenantFilter>
    {
        /// <summary>
        /// save prioritized tasks
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> SavePrioritizedTasks(List<WorkItemProgress> tasks, string userId);

        /// <summary>
        /// Get work item progress for the day
        /// </summary>        
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkItemProgress>> GetWorkItemProgressForTheDayAsync(string userId);
    }
}
