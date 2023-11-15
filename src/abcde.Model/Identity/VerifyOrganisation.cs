using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Model.Identity
{
    public class VerifyOrganisation
    {
        public string EncryptedOrganisationId { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConnectionStringCode { get; set; }
    }
}