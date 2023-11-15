using abcde.Model;
using abcde.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Test.Data
{
    public static class OrganisationData
    {
        public static Organisation Get(string tenantId)
        {
            return new Organisation()
            {
                Name = "New Org",
                TenantId = tenantId,
                LicenseCount = 10,
                ContactEmail = "email@neworg.com",
                ContactName = "Mr Contact Name",
                PhoneNumber = "1234567890",
                Addresses = new List<Address>()
                {
                    new Address()
                    {
                        AddressLine1 = "Address Line 1",
                        AddressLine2 = "Address Line 2",
                        AddressTypeId = AddressType.Billing,
                        City = "City",
                        Country = "Country",
                        PostCode = "Post Code",
                        State = "State"
                    },
                    new Address()
                    {
                        AddressLine1 = "Address Line 1",
                        AddressLine2 = "Address Line 2",
                        AddressTypeId = AddressType.Correspondence,
                        City = "City",
                        Country = "Country",
                        PostCode = "Post Code",
                        State = "State"
                    }
                },
            };
        }
    }
}
