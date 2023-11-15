using abcde.Model;
using AutoMapper;
using abcde.Model.ViewModels;

namespace abcde.vAPI.Mapping
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {

            CreateMap<WorkItemViewModel, WorkItem>();
            CreateMap<WorkItem, WorkItemViewModel>();

        }    
    }
}
