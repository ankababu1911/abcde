using abcde.Data.Interfaces;
using abcde.Data.Predicates;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using abcde.Model.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace abcde.Data.Repositories
{
    public class WorkItemRepository : GenericTenantAsyncRepository<WorkItem, WorkItemSummary, WorkItemFilter>, IWorkItemRepository
    {
        public WorkItemRepository(DataContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<WorkItem>> GetFilteredAsync(WorkItemFilter filter)
        {
            var predicate = await new WorkItemPredicate().GetPredicate(filter);

            var result = DbSet.Where(predicate).ToList();

            return await Task.Run(() => result);
        }

        ///<see cref="IWorkItemRepository.GetAllWorkItemsAsync(string)"/>
        public async Task<IEnumerable<WorkItem>> GetAllWorkItemsAsync(string userId)
        {
            var result = DataContext.Set<WorkItem>().Where(x => x.UserId.ToString() == userId).Include(x => x.Notes).Include(x => x.Children).Include(x=>x.WorkItemCategories).ToList();
            return await Task.Run(() => result);
        }



        /// <see cref="IWorkItemRepository.AddCategoryToWorkItemAsync(Guid, List{WorkItemCategory})"/>
        public async Task AddCategoryToWorkItemAsync(Guid id, List<WorkItemCategory> categories)
        {
            var WorkItemCategories = new List<WorkItemCategories>();
            foreach (var category in categories)
            {

                var workItemCategory = new WorkItemCategories
                {
                    WorkItemId = id,
                    CategoryId = (int)category
                };
                WorkItemCategories.Add(workItemCategory);

            }

            DataContext.Set<WorkItemCategories>().AddRange(WorkItemCategories);
            await DataContext.SaveChangesAsync();



        }

        /// <see cref="IWorkItemRepository.GetWorkItemAsync(Guid )"/>
        public async Task<WorkItem> GetWorkItemAsync(Guid id)
        {
            var result = DataContext.Set<WorkItem>().Where(x => x.Id == id).Include(x => x.WorkItemCategories).FirstOrDefault();
            return await Task.Run(() => result);

        }

        /// <see cref="IWorkItemRepository.UpdateCategoriesAsync(WorkItemViewModel)"/>
        public async Task UpdateCategoriesAsync(WorkItemViewModel entity)
        {
              var isWorkItemexist = await DbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
              var categoriesList = new List<WorkItemCategories>();
              var listSorted = new List<WorkItemCategory>();
              listSorted = entity.Categories;
            if (isWorkItemexist != null)
            {
                var context = DataContext.Set<WorkItemCategories>();
                var categories = context.Where(wc => wc.WorkItemId == entity.Id).ToList();
                
                var listFromTable = categories.Select(x => x.CategoryId).ToList();
                foreach (var wc in listFromTable)

                {
                    if (!listSorted.Contains((WorkItemCategory)wc))
                    {
                        var categoriesInDb = context.Where(w => w.CategoryId == wc).FirstOrDefault();
                        var categoryEntity = DataContext.Set<WorkItemCategories>().Find(categoriesInDb.Id);
                        if (categoryEntity != null)
                        {
                            context.Remove(categoryEntity);
                        }
                     
                    }

                    else
                    {
                        listSorted.Remove((WorkItemCategory)wc);

                    }

                }
                var finalList = new List<WorkItemCategories>();
                foreach (var l in listSorted)
                {
                    var workItemCategory = new WorkItemCategories

                    {
                        WorkItemId = entity.Id,
                        CategoryId = (int)l
                    };
                    finalList.Add(workItemCategory);
                }
                context.AddRange(finalList);
                await DataContext.SaveChangesAsync();
                
            }
        }

    


        ///<see cref="IWorkItemRepository.GetAllIncompletedTasks(string)"/>
        public async Task<IEnumerable<WorkItem>> GetAllIncompletedTasks(string userId)
        {
            //get all work items that are not completed and cancelled and no children
            var result = DataContext.Set<WorkItem>().Where(x => x.UserId.ToString() == userId 
                            && x.Status != ItemStatus.Completed 
                            && x.Status != ItemStatus.Cancelled 
                            && x.Children.Count == 0).Include(x => x.Children).ToList();

            return await Task.Run(() => result);
        }
    }
}