using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace abcde.Portal.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// Clear the existing external cookie and redirect to Tenant login
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl != "/")
            {
                return LocalRedirect(Url.Content(returnUrl));
            }

            return LocalRedirect(Url.Content($"~/"));
        }
    }
}
