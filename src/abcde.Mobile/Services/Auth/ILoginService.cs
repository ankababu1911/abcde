using abcde.Model.Identity;

namespace abcde.Mobile.Services.Auth
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginModel loginModel);
    }
}
