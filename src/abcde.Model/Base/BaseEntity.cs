using System.ComponentModel.DataAnnotations;

namespace abcde.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        //public string SystemId { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string EntityNotes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Datestamp { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}