using abcde.Model.Base;
using abcde.Model.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace abcde.Model
{
    public class Note : BaseTenantEntity
    {
        /// <summary>
        /// Text
        /// </summary>
        public string NoteText { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        ///// <summary>
        ///// User Id
        ///// </summary>
        public Guid UserId { get; set; } //foreignkey to ApplicationUser.Id

        ///// <summary>
        ///// WorkItemId
        ///// </summary>
        [ForeignKey("WorkItem")]
        public Guid WorkItemId { get; set; }

        public virtual WorkItem WorkItem { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}