using abcde.Model.Identity;

namespace abcde.Mobile.Services.Auth
{
    public interface IRegisterService
    {
        Task<RegisterResult> RegisterAsync(RegisterModel registerModel);
    }
}
