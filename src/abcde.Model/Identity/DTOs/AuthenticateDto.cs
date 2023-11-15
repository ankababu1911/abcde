using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Identity.DTOs
{
    public class AuthenticateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.{6,}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", ErrorMessage = "Password must have 1 uppercase and 1 number")]
        public string Password { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool Successful { get; set; }
        public string Error { get; set; }
    }
}
