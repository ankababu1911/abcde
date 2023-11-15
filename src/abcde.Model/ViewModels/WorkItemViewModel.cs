using abcde.Model;
using abcde.Model.Base;

namespace abcde.Model.ViewModels
{
    //View Model For WorkItem
    public class WorkItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? OriginalPlannedEndDateTime {get ; set;}

        public Priority Priority { get; set; }

        public Importance Important { get; set; }

        public Urgency Urgent { get; set; }

        public Complexity Complexity { get; set; }

        public Guid? ParentId { get; set; }

        public List<WorkItemCategory> Categories { get; set; }

        public YesNo IHaveToDo { get; set; }

        public YesNo IWantToDo { get; set; }

        public Guid UserId { get; set; }

        public string TenantId { get; set; }
    }
}
