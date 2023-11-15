using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace abcde.Model.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string _Id => Id.ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool HasChangedPassword { get; set; }
        public string TenantId { get; set; }
        public string SubTenantId { get; set; }
        public string DepartmentId { get; set; }

        [NotMapped]
        public string Fullname => $"{FirstName} {LastName}";
        public string Name { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
    public class IdentityRole : IdentityRole<Guid>
    {
    }
}