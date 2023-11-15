using abcde.Model;

namespace abcde.Mobile.Services.Goals
{
    public interface IGoalsService
    {
        /// <summary>
        /// Add a goal
        /// </summary>
        /// <param name="goal"></param>
        /// <returns>Work Item</returns>
        Task<WorkItem> AddGoal(WorkItem goal);

        /// <summary>
        /// Get all goals
        /// </summary>
        /// <returns>List of work items</returns>
        Task<IEnumerable<WorkItem>> GetGoals();

        /// <summary>
        /// Get a goal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Work item</returns>
        Task<WorkItem> GetGoal(Guid id);    

        /// <summary>
        /// Update a goal
        /// </summary>
        /// <param name="goal"></param>
        /// <returns>Work item</returns>
        Task<WorkItem> UpdateGoal(WorkItem goal);

        /// <summary>
        /// Update is pinned
        /// </summary>
        /// <param name="goalId"></param>
        /// <param name="isPinned"></param>
        /// <returns>string</returns>
        Task<string> UpdateIsPinned(Guid goalId, bool isPinned);

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

    }
}
