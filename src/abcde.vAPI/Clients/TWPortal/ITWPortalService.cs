using abcde.Model;
using abcde.Model.Identity;

namespace abcde.vAPI.Clients.TWPortal
{
    public interface ITWPortalService
    {
        /// <summary>
        ///     Add tm user
        /// </summary>
        /// <param name="addTmUserRequest"></param>
        /// <returns></returns>
        Task<bool> AddTmUserAsync(AddTmUserRequest addTmUserRequest);

        /// <summary>
        ///  Get connection string code by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetConnectionStringCodeAsync(Guid userId);

        /// <summary>
        ///  Get connection string code by email id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task<string> GetConnectionStringCodeByEmailAsync(string emailId);

        /// <summary>
        ///     Validate organisation administrator
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ClientException"></exception>
        Task<Organisation> ValidateOrganisationAdministrator(VerifyOrganisation verifyOrganisation);
    }
}