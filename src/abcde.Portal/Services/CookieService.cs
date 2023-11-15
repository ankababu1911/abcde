using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace abcde.Portal.Services
{
    // CookieService.cs

    public class CookieService
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCookie(string name, string value, int expirationDays)
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", name, value, expirationDays);
        }
    }
}