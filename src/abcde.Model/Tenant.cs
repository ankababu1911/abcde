using abcde.Model.Base;
using abcde.Model.Identity;

namespace abcde.Model
{
    public class Tenant : BaseTenantEntity
    {
        /// <summary>
        /// Url friendly name
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public TenantStatus Status { get; set; }

        public string? ConnectionStringCode { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}