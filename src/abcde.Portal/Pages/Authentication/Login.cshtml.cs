using System.Security.Claims;
using abcde.Model.Constants;
using abcde.Portal.Authentication;
using abcde.Portal.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace abcde.Portal.Pages.Authentication
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly EncryptionHelper _encryptionHelper;

        public LoginModel(EncryptionHelper encryptionHelper)
        {
            _encryptionHelper = encryptionHelper;
        }

        /// <summary>
        /// Process Token
        /// </summary>
        /// <remarks>
        /// Clear the existing cookie, process the token and using claims from the token, create a new cookie.
        /// </remarks>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string token, string redirectUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string returnUrl = !string.IsNullOrEmpty(redirectUrl)
                ? Url.Content($"~/{redirectUrl}")
                : Url.Content($"~/");

            var claimsIdentity = await JWTIdentity.Process(token);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = Request.Host.Value,
            };

            string expiryTimeString = claimsIdentity.Claims
                .FirstOrDefault(c => c.Type.Contains("expiration"))?.Value;

            if (expiryTimeString != null)
            {
                authProperties.ExpiresUtc = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddSeconds(long.Parse(expiryTimeString));
            }
            var request = HttpContext.Request;
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            if (request.Query.ContainsKey(CommonConstants.ConnectionStringCodeQueryStringParameter) &&
                request.Query.TryGetValue(CommonConstants.ConnectionStringCodeQueryStringParameter, out var qEncryptedConnectionStringCode))
            {
                var encryptedConnectionStringCode = qEncryptedConnectionStringCode.FirstOrDefault();
                if (!string.IsNullOrEmpty(encryptedConnectionStringCode))
                {
                    var decryptedConnectionStringCode = _encryptionHelper.DecryptAES(encryptedConnectionStringCode);

                    //now decryot the string
                    HttpContext.Response.Cookies.Append(CommonConstants.ConnectionStringCodeCookieName, decryptedConnectionStringCode);
                }
            }
            return LocalRedirect(returnUrl);
        }
    }
}