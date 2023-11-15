using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model.Filters;
using abcde.Model.Summary;
using abcde.Model;
using System.Globalization;
using abcde.Model.Exceptions;
using System.Net.Http.Json;
using abcde.Model.ViewModels;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace abcde.Client.Services
{
    public class WorkItemService : BaseService<WorkItem, WorkItemSummary, WorkItemFilter>, IWorkItemService
    {
        private readonly string updateIsPinnedStatus = "UpdateIsPinned";
        private readonly string getAllIncompletedTasks = "GetAllIncompletedTasks";
        private readonly string savePrioritizedTasks = "SavePrioritizedTasks";
        private readonly string getPrioritizedTasks = "GetPrioritizedTasks";
        private readonly string createWorkItem = "CreateWorkItem";
        private readonly string updateWorkItem = "UpdateWorkItem";

        private readonly string filter;       
        public WorkItemService(HttpClient httpClient) : base(httpClient)
        {
            BaseResource = "WorkItems";            
        }

        ///<see cref="IWorkItemService.GetAllIncompletedTasks(string)"/>
        public async Task<List<WorkItem>> GetAllIncompletedTasks()
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, getAllIncompletedTasks);                
                var response = await httpClient.GetFromJsonAsync<IEnumerable<WorkItem>>(url);
                if(response!=null)
                {
                    return response.ToList();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        
        public Task<WorkItem> GetInstanceAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        ///<see cref="IWorkItemService.GetPrioritizedTasks()"/>
        public async Task<List<WorkItem>> GetPrioritizedTasks()
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, getPrioritizedTasks);
                var response = await httpClient.GetFromJsonAsync<IEnumerable<WorkItem>>(url);
                if (response != null)
                {
                    return response.ToList();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// see ref = "IWorkItemService.CreateWorkItem(WorkItemViewModel)"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ClientException"></exception>
        public async Task<WorkItem> CreateWorkItem( WorkItemViewModel entity)
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource,createWorkItem);
                var response = await httpClient.PostAsJsonAsync<WorkItemViewModel>(url, entity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<WorkItem>();
                   
                 return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            

        }

        /// <summary>
        /// see ref = "IWorkItemService.UpdateWorkItem(WorkItemViewModel)"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ClientException"></exception>
        public async Task<WorkItem> UpdateWorkItem( WorkItemViewModel entity)
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, updateWorkItem);
                var response = await httpClient.PostAsJsonAsync<WorkItemViewModel>(url, entity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<WorkItem>();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }



        ///<see cref="IWorkItemService.SavePrioritizedTasks(List{Guid})"/>
        public async Task<string> SavePrioritizedTasks(List<Guid> workItems)
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, savePrioritizedTasks);
                var response = await httpClient.PostAsJsonAsync(url, workItems);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }                

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///<see cref="IWorkItemService.UpdateIsPinned(Guid, bool)"/>
        public async Task<string> UpdateIsPinned(Guid id, bool isPinned)
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, $"{updateIsPinnedStatus}?workItemId={id}&isPinned={isPinned}");
                var response = await httpClient.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }     

        
    }
}
