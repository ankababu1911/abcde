using abcde.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace abcde.Model
{
    public class User : BaseTenantEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public string ChangePasswordCode { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string Token { get; set; }        
    }
}
