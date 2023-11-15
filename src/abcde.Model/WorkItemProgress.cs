using abcde.Model.Base;

namespace abcde.Model
{
    public class WorkItemProgress : BaseTenantEntity
    {
        /// <summary>
        /// Date time of the progress Item
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Work Item Id
        /// </summary>
        public Guid WorkItemId { get; set; }        
        /// <summary>
        /// Current Status
        /// </summary>
        public DayPlanStatus CurrentStatus { get; set; }
        
        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }

        //navigation properties
        public virtual WorkItem WorkItem { get; set; }

    }
}
