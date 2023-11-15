using abcde.Client.Interfaces;
using abcde.Mobile.Services.Settings;
using abcde.Model;

namespace abcde.Mobile.Services.Goals
{
    public class GoalsService : IGoalsService
    {
        private readonly IAPIGateway _apiGateway;
        private readonly ISettingsService _settingsService;

        #region Constructor
        public GoalsService(IAPIGateway apiGateway, ISettingsService settingsService)
        {
            _apiGateway = apiGateway;
            _settingsService = settingsService;
            if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                _apiGateway.SetToken(_settingsService.AuthAccessToken);
            }
        }
        #endregion

        #region Methods
        ///<see cref="IGoalsService.AddGoal(WorkItem)"/>
        public async Task<WorkItem> AddGoal(WorkItem goal)
        {
            return await _apiGateway.WorkItemService.AddAsync(goal);
        }

        ///<see cref="IGoalsService.GetGoal(Guid)"/>
        public async Task<WorkItem> GetGoal(Guid id)
        {
           if(_settingsService.AuthAccessToken != null)
            {
              return await _apiGateway.WorkItemService.GetAsync(id);
            }
            else return default;
        }

        ///<see cref="IGoalsService.GetGoals()"/>
        public async Task<IEnumerable<WorkItem>> GetGoals()
        {
            if (_settingsService.AuthAccessToken != null)
            {
                return await _apiGateway.WorkItemService.GetAllAsync();
            }
            else return default;
        }

        ///<see cref="IGoalsService.UpdateGoal(WorkItem)"/>
        public async Task<WorkItem> UpdateGoal(WorkItem goal)
        {
            return await _apiGateway.WorkItemService.SaveAsync(goal);
        }

        ///<see cref="IGoalsService.UpdateIsPinned(Guid, bool)"/>
        public async Task<string> UpdateIsPinned(Guid goalId, bool isPinned)
        {            
            return await _apiGateway.WorkItemService.UpdateIsPinned(goalId, isPinned);            
        }

        ///<see cref="IGoalsService.GetAllIncompletedTasks()"/>
        public async Task<List<WorkItem>> GetAllIncompletedTasks()
        {
            return await _apiGateway.WorkItemService.GetAllIncompletedTasks();
        }

        ///<see cref="IGoalsService.SavePrioritizedTasks(List{Guid})" />
        public async Task<string> SavePrioritizedTasks(List<Guid> workItems)
        {
            return await _apiGateway.WorkItemService.SavePrioritizedTasks(workItems);
        }

        ///<see cref="IGoalsService.GetPrioritizedTasks()"/>
        public async Task<List<WorkItem>> GetPrioritizedTasks()
        {
            return await _apiGateway.WorkItemService.GetPrioritizedTasks();
        }
        #endregion
    }
}