using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Identity
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string _Id => Id.ToString();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public string TenantId { get; set; }
        public string Role { get; set; }
    }
}
