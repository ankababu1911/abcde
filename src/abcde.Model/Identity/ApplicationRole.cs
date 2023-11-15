using Microsoft.AspNetCore.Identity;

namespace abcde.Model.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        { }

        public ApplicationRole(string roleName) : base(roleName)
        {
            Name = roleName;
            DisplayName = roleName;
        }

        public ApplicationRole(string roleName, string tenantId) : base(roleName)
        {
            Name = $"{roleName}.{tenantId}";
            DisplayName = roleName;
            TenantId = tenantId;
        }

        /// <summary>
        /// Display name 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Tenant level
        /// </summary>
        public string? TenantId { get; set; }

        /// <summary>
        /// Location, office or branch level
        /// </summary>
        public string? SubTenantId { get; set; }

        /// <summary>
        /// Department level
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// Is role active?
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Datestamp
        /// </summary>
        public DateTime Datestamp { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        /// <summary>
        /// Role claims
        /// </summary>
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
