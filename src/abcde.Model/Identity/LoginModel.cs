using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Identity
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string TenantId { get; set; }

        public bool RememberMe { get; set; }
    }
}
