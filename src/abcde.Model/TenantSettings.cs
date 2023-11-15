using abcde.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace abcde.Model
{
    public class TenantSettings : BaseTenantEntity
    {
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Number of licenses for Organisation
        /// </summary>
        public int LicenseCount { get; set; }

        public List<Address> Addresses { get; set; }

        /// <summary>
        /// Tenant timezone
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// String representation e.g. en-GB
        /// </summary>
        public string TenantCulture { get; set; }

        /// <summary>
        ///    License valid from
        /// </summary>
        public DateTime? LicenseValidFrom { get; set; }

        /// <summary>
        ///   License valid to
        /// </summary>
        public DateTime? LicenseValidTo { get; set; }

        /// <summary>
        /// Date last synced with TW portal
        /// </summary>
        public DateTime? LastCheckedDateTime { get; set; }
    }
}