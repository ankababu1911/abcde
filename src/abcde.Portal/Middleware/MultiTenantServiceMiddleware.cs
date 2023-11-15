using abcde.Client.Interfaces;
using abcde.Model.Constants;
using abcde.Portal.Helpers;

namespace abcde.Portal.Middleware
{
    public class MultiTenantServiceMiddleware : IMiddleware
    {
        private readonly IAPIGateway _apiGateway;
        private readonly EncryptionHelper _encryptionHelper;

        public MultiTenantServiceMiddleware(IAPIGateway apiGateway, EncryptionHelper encryptionHelper)
        {
            _apiGateway = apiGateway;
            _encryptionHelper = encryptionHelper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;
            string encryptedConnectionStringCode = string.Empty;
            //first check if cookie is there or not
            if (request.Cookies.TryGetValue(CommonConstants.ConnectionStringCodeCookieName, out encryptedConnectionStringCode))
            {
                _apiGateway.SetConnectionStringCode(encryptedConnectionStringCode);
            }
            //now check if it is present in querystring
            else if (request.Query.ContainsKey(CommonConstants.ConnectionStringCodeQueryStringParameter) &&
                request.Query.TryGetValue(CommonConstants.ConnectionStringCodeQueryStringParameter, out var qEncryptedConnectionStringCode))
            {
                encryptedConnectionStringCode = qEncryptedConnectionStringCode.FirstOrDefault();
                if (!string.IsNullOrEmpty(encryptedConnectionStringCode))
                {
                    //now decryot the string
                    var decryptedConnectionStringCode = _encryptionHelper.DecryptAES(encryptedConnectionStringCode);
                    _apiGateway.SetConnectionStringCode(decryptedConnectionStringCode);//now set a cookie for future use for connectionstringcode
                    context.Response.Cookies.Append(CommonConstants.ConnectionStringCodeCookieName, decryptedConnectionStringCode);
                }
            }
            else
            {
                _apiGateway.SetConnectionStringCode(null);
            }
            _apiGateway.SetHeaders();
            await next(context);
        }
    }
}