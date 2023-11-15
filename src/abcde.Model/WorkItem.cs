using abcde.Model.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace abcde.Model
{
    public class WorkItem : BaseTenantEntity
    {
        /// <summary>
        /// Parent Id
        /// </summary>
        public Guid? ParentId { get; set; }
       
        /// <summary>
        /// Title
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Original Planned End Date Time
        /// </summary>
        public DateTime? OriginalPlannedEndDateTime { get; set; }

        /// <summary>
        /// End Date  Time
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Start Date  Time
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public ItemStatus Status { get; set; }

        /// <summary>
        /// Importance
        /// </summary>
        public Importance Important { get; set; }

        /// <summary>
        /// Urgency
        /// </summary>
        public Urgency Urgent { get; set; }

        /// <summary>
        /// Complexity
        /// </summary>
        public Complexity Complexity { get; set; }

        /// <summary>
        /// I Want To Do
        /// </summary>
        public YesNo IWantToDo { get; set; }

        /// <summary>
        /// I Have To Do
        /// </summary>
        public YesNo IHaveToDo { get; set; }

        /// <summary>
        /// user Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Parent WorkItem
        /// </summary>
        public virtual WorkItem Parent { get; set; }

        /// <summary>
        /// Children WorkItems
        /// </summary>
        public virtual ICollection<WorkItem> Children { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<WorkItemProgress> WorkItemProgress { get; set; }

        /// <summary>
        /// Is Pinned
        /// </summary>
        [DefaultValue(false)]
        public bool IsPinned { get; set; }

        public virtual ICollection<WorkItemCategories> WorkItemCategories { get; set; }

       
    }
}