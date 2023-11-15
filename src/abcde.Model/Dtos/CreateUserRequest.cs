using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Dtos
{
    public class CreateUserRequest
    {
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? DomainId { get; set; }
    }
}