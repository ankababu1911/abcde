using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Dtos;
using abcde.Model.Identity;
using abcde.Model.Identity.DTOs;

namespace abcde.Client.Services.Interfaces
{
    public interface IIdentityService : IGenericService<User, BaseSummary, BaseTenantFilter>
    {
        Task<RegisterResult> VerifyOrganisation(VerifyOrganisation dto);

        Task<RegisterResult> Register(RegisterModel dto);

        Task<LoginResult> Login(LoginModel dto);

        Task<AuthenticateDto> Authenticate(AuthenticateDto dto);

        Task<CustomIdentityResult> ChangePassword(ChangePasswordModel model);

        Task<CustomIdentityResult> ResetPassword(ChangePasswordModel model);

        Task<ForgotPasswordDto> ForgotPassword(ForgotPasswordDto dto);

        Task<ChangePasswordDto> ChangeForgottenPassword(ChangePasswordDto dto);

        Task<RegisterResult> CreateUser(CreateUserRequest model);
    }
}