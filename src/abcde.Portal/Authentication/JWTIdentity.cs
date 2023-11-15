using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace abcde.Portal.Authentication
{
    public static class JWTIdentity
    {
        public static async Task<ClaimsIdentity> Process(string token)
        {
            var claims = await GetClaimsFromJwt(token);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }

        public static JObject DecodePayload(string token)
        {
            var parts = token.Split('.');
            var payload = parts[1];
            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));

            return JObject.Parse(payloadJson);
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }

        private static async Task<IEnumerable<Claim>> GetClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            claims.Add(new Claim(ClaimTypes.Sid, jwt));

            keyValuePairs.TryGetValue("role", out var roles);
            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    claims.AddRange(parsedRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }

            keyValuePairs.TryGetValue(ApplicationClaimTypes.Permission, out var permissions);
            if (permissions != null)
            {
                if (permissions.ToString().Trim().StartsWith("["))
                {
                    var parsedPermissions = JsonSerializer.Deserialize<string[]>(permissions.ToString());
                    claims.AddRange(parsedPermissions.Select(permission => new Claim(ApplicationClaimTypes.Permission, permission)));
                }
                else
                {
                    claims.Add(new Claim(ApplicationClaimTypes.Permission, permissions.ToString()));
                }
                keyValuePairs.Remove(ApplicationClaimTypes.Permission);
            }

            keyValuePairs.TryGetValue("nameid", out var nameid);
            if (nameid != null)
            {
                if (nameid.ToString().Trim().StartsWith("["))
                {
                    var parsedNameId = JsonSerializer.Deserialize<string[]>(nameid.ToString());
                    claims.AddRange(parsedNameId.Select(nameid => new Claim(ClaimTypes.NameIdentifier, nameid)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, nameid.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.NameIdentifier);
            }

            // TenantId
            keyValuePairs.TryGetValue("name", out var name);
            if (name != null)
            {
                if (name.ToString().Trim().StartsWith("["))
                {
                    var parsedNameId = JsonSerializer.Deserialize<string[]>(name.ToString());
                    claims.AddRange(parsedNameId.Select(name => new Claim(ClaimTypes.Name, name)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Name, name.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.Name);
            }

            // Expiry
            keyValuePairs.TryGetValue("exp", out var expiry);
            if (expiry != null)
            {
                if (expiry.ToString().Trim().StartsWith("["))
                {
                    var parsedNameId = JsonSerializer.Deserialize<string[]>(expiry.ToString());
                    claims.AddRange(parsedNameId.Select(expiry => new Claim(ClaimTypes.Expiration, expiry)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Expiration, expiry.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.Expiration);
            }

            // SubTenantId (PrimarySid)
            keyValuePairs.TryGetValue("primarysid", out var primarySid);
            if (primarySid != null)
            {
                if (primarySid.ToString().Trim().StartsWith("["))
                {
                    var parsedNameId = JsonSerializer.Deserialize<string[]>(primarySid.ToString());
                    claims.AddRange(parsedNameId.Select(primarySid => new Claim(ClaimTypes.PrimarySid, primarySid)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.PrimarySid, primarySid.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.PrimarySid);
            }

            // DepartmentId (PrimaryGroupSid)
            keyValuePairs.TryGetValue("primarygroupsid", out var primaryGroupSid);
            if (primaryGroupSid != null)
            {
                if (primaryGroupSid.ToString().Trim().StartsWith("["))
                {
                    var parsedNameId = JsonSerializer.Deserialize<string[]>(primaryGroupSid.ToString());
                    claims.AddRange(parsedNameId.Select(primaryGroupSid => new Claim(ClaimTypes.PrimaryGroupSid, primaryGroupSid)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.PrimaryGroupSid, primaryGroupSid.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.PrimarySid);
            }

            return await Task.Run(() => claims);
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }

    public static class ApplicationClaimTypes
    {
        public const string Permission = "Permission";
    }
}
