using abcde.Client.Interfaces;
using abcde.Model.Identity;

namespace abcde.Mobile.Services.Auth
{
    public class RegisterService : IRegisterService
    {
        private IAPIGateway _apiGateway;
        public RegisterService(IAPIGateway aPIGateway)
        {
            _apiGateway = aPIGateway;
        }
        public async Task<RegisterResult> RegisterAsync(RegisterModel registerModel)
        {
            return await _apiGateway.IdentityService.Register(registerModel);
        }
    }
}
