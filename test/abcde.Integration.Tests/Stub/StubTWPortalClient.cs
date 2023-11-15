using abcde.Model;
using abcde.Model.Identity;
using abcde.Test.Data;
using abcde.vAPI.Clients.TWPortal;

namespace abcde.Integration.Tests.Stub
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

        public async Task<Organisation> ValidateOrganisationAdministrator(VerifyOrganisation verifyOrganisation)
        {
            if (string.IsNullOrEmpty(verifyOrganisation.EncryptedOrganisationId))
            {
                return null;
            }
            else
            {
                return await Task.Run(() => OrganisationData.Get(verifyOrganisation.EncryptedOrganisationId));
            }
        }
    }
}