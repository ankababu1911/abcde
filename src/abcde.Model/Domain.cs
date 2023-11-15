using abcde.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace abcde.Model
{
    public class Domain : BaseTenantEntity
    {
        /// <summary>
        /// Parent domain id if any
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Parent Domain
        /// </summary>
        public virtual Domain Parent { get; set; }        

        /// <summary>
        /// Children Domain
        /// </summary>
        public virtual ICollection<Domain> Children { get; set; }

        // Navigation property to represent the many-to-many relationship between Domain and Users
        public virtual ICollection<DomainUser> DomainUsers { get; set; }
    }
}