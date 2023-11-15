using abcde.Model.Base;

namespace abcde.Model
{
    public class Organisation : BaseTenantEntity
    {
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int LicenseCount { get; set; }
        public DateTime? LicenseValidFrom { get; set; }
        public DateTime? LicenseValidTo { get; set; }
        public string? ConnectionStringCode { get; set; }
        public List<Address> Addresses { get; set; }
    }
}