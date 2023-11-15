using abcde.Client.Interfaces;
using abcde.Mobile.Helpers;
using abcde.Model.Identity;

namespace abcde.Mobile.Services.Auth
{
    public class LoginService : ILoginService
    {
        private IAPIGateway _apiGateway;

        public LoginService(IAPIGateway aPIGateway)
        {
            _apiGateway = aPIGateway;
        }

        public async Task<LoginResult> LoginAsync(LoginModel loginModel)
        {
            var result = await _apiGateway.IdentityService.Login(loginModel);
            if (result != null && result.Successful)
            {
                await LocalStorage.Insert<LoginResult>(Constants.LoginResult, result);
            }
            return result;
        }
    }
}