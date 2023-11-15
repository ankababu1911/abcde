using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Identity
{
    public class AddUserModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Domain { get; set; }
    }
}