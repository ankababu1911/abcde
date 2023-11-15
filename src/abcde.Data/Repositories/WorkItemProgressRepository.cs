using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Data.Repositories
{
    public class WorkItemProgressRepository : GenericTenantAsyncRepository<WorkItemProgress, BaseTenantSummary, BaseTenantFilter>, IWorkItemProgressRepository
    {
        public WorkItemProgressRepository(DataContext context) : base(context)
        {
        } 

        ///<see cref="IWorkItemProgressRepository.SavePrioritizedTasks(List{WorkItemProgress}, string)"/>
        public async Task<bool> SavePrioritizedTasks(List<WorkItemProgress> tasks, string userId)
        {
            var context = DataContext.Set<WorkItemProgress>();
            var workItems = context.Where(x => x.UserId.ToString() == userId 
                                && x.Date.Date == DateTime.Today).ToList();
            if(workItems != null && workItems.Count > 0)
            {
                //delete all the work items
                context.RemoveRange(workItems);
            }
            //add all tasks to the work item progress table
            await context.AddRangeAsync(tasks);
            await DataContext.SaveChangesAsync();
            return true;
        }

        ///<see cref="IWorkItemProgressRepository.GetWorkItemProgressForTheDayAsync(string)"/>
        public async Task<IEnumerable<WorkItemProgress>> GetWorkItemProgressForTheDayAsync(string userId)
        {
            var result = DataContext.Set<WorkItemProgress>().Where(x => x.UserId.ToString() == userId
                                       && x.Date.Date == DateTime.Today).ToList();
            return await Task.Run(() => result);
        }
    }
}
