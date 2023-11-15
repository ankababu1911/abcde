using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using abcde.Portal.Extensions;

namespace abcde.Web.Authentication
{
    public class TokenExpiryAuthStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(60);

        public TokenExpiryAuthStateProvider(ILoggerFactory logger) : base(logger)
        { }

        protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            DateTime expiry = authenticationState.User.GetExpiryDate();

            return expiry < DateTime.UtcNow ? Task.FromResult(false) : Task.FromResult(true);
        }
    }
}
