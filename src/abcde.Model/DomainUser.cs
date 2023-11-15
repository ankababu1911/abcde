using abcde.Model.Base;
using abcde.Model.Identity;

namespace abcde.Model
{
    public class DomainUser : BaseEntity
    {
        // Foreign keys
        /// <summary>
        /// Domain id
        /// </summary>
        public Guid DomainID { get; set; }//foreignkey to Domain.Id

        /// <summary>
        /// User id
        /// </summary>
        public Guid UserID { get; set; } //foreignkey to ApplicationUser.Id

        /// <summary>
        /// Is the user the head of the domain
        /// </summary>
        public bool IsDomainHead { get; set; }

        // Navigation properties to represent the relationships
        //todo: need to revisit this.
        public virtual Domain Domain { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}