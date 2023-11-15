using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Dtos;
using abcde.Model.Identity;
using abcde.Model.Identity.DTOs;
using System.Globalization;

namespace abcde.Client.Services
{
    public class IdentityService : BaseService<User, BaseSummary, BaseTenantFilter>, IIdentityService
    {
        private readonly string resourceValidate = "ValidateOrg";
        private readonly string resourceLogin = "login";
        private readonly string resourceChangePassword = "changePassword";
        private readonly string resourceResetPassword = "ResetPassword";
        private readonly string resourceRegisterIdentity = "register";
        private readonly string resourceLoginIdentity = "login";
        private readonly string resourceForgotPassword = "forgotPassword";
        private readonly string resourceChangeForgottenPassword = "changeForgottenPassword";
        private readonly string resourceCreateUser = "createUser";

        public IdentityService(HttpClient httpClient) : base(httpClient) => BaseResource = "Identity";

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="ValidateModel"></param>
        /// <returns></returns>
        public async Task<RegisterResult> VerifyOrganisation(VerifyOrganisation dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceValidate);

            return await PostAsync<VerifyOrganisation, RegisterResult>(url, dto);
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public async Task<RegisterResult> Register(RegisterModel dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceRegisterIdentity);

            return await PostAsync<RegisterModel, RegisterResult>(url, dto);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<LoginResult> Login(LoginModel dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceLoginIdentity);

            return await PostAsync<LoginModel, LoginResult>(url, dto);
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<AuthenticateDto> Authenticate(AuthenticateDto dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceLogin);

            return await PostAsync(url, dto);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<AuthenticateDto> Update(AuthenticateDto dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}", BaseResource);

            return await PutAsync(url, dto);
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<CustomIdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceChangePassword);

            return await PostAsync<ChangePasswordModel, CustomIdentityResult>(url, model);
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<CustomIdentityResult> ResetPassword(ChangePasswordModel model)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceResetPassword);

            return await PostAsync<ChangePasswordModel, CustomIdentityResult>(url, model);
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordDto> ForgotPassword(ForgotPasswordDto dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceForgotPassword);

            return await PostAsync(url, dto);
        }

        /// <summary>
        /// Change forgotten password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ChangePasswordDto> ChangeForgottenPassword(ChangePasswordDto dto)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceChangeForgottenPassword);

            return await PostAsync(url, dto);
        }

        public async Task<RegisterResult> CreateUser(CreateUserRequest model)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceCreateUser);

            return await PostAsync<CreateUserRequest, RegisterResult>(url, model);
        }
    }
}