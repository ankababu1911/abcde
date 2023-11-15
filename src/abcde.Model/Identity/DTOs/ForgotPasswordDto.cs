using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Identity.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ChangePasswordCode { get; set; }
    }
}
