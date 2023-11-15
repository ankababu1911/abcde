using System.Security.Claims;

namespace abcde.Portal.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static DateTime GetExpiryDate(this ClaimsPrincipal principal)
        {
            var exp = principal.FindFirstValue(ClaimTypes.Expiration);

            if (exp != null)
            {
                var converted = long.TryParse(exp, out long expiryTicks);

                if (converted)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expiryTicks);

                    return dateTimeOffset.UtcDateTime;
                }

                return DateTime.Now;
            }

            return DateTime.Now;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetRole(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Role);
        }

        public static string GetTenantId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.PrimarySid);
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Sid);
        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal, string id)
        {
            var currentUserId = GetUserId(principal);

            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
