using abcde.Model;
using abcde.Model.Identity;

namespace abcde.vAPI.Clients.TWPortal
{
    public class StubTWPortalClient : ITWPortalService
    {
        public Task<bool> AddTmUserAsync(AddTmUserRequest addTmUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetConnectionStringCodeAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetConnectionStringCodeByEmailAsync(string emailId)
        {
            throw new NotImplementedException();
        }

        public Task<Organisation> ValidateOrganisationAdministrator(VerifyOrganisation verifyOrganisation)
        {
            return string.IsNullOrEmpty(verifyOrganisation.EncryptedOrganisationId)
               ? null
               : Task.FromResult(new Organisation()
               {
                   Name = "New Org",
                   TenantId = verifyOrganisation.EncryptedOrganisationId,
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
               });
        }
    }
}