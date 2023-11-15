using abcde.Data;
using abcde.Data.Services;
using abcde.Model.Base;
using abcde.vAPI.Clients.TWPortal;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace abcde.vAPI.Middleware
{
    public class MultiTenantServiceMiddleware : IMiddleware
    {
        private readonly ITenantHandlerService setter;
        private const string httpContextItemConnectionCode = "CDE";
        private readonly IOptions<Model.Configuration.Options> appSettings;
        private readonly DataContext dataContext;
        private readonly ITWPortalService tWPortalService;

        public MultiTenantServiceMiddleware(IOptions<Model.Configuration.Options> appSettings, DataContext dataContext, ITWPortalService tWPortalService)
        {
            this.setter = new TenantHandlerService(null, appSettings, dataContext);
            this.appSettings = appSettings;
            this.dataContext = dataContext;
            this.tWPortalService = tWPortalService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var tenantId = GetClaimValue(Constants.TenantID, context);
            if (!string.IsNullOrEmpty(tenantId))
            {
                setter.SetTenant(tenantId);
            }
            var userId = GetClaimValue(Constants.UserID, context);

            var tenantConnCode = GetClaimValue(Constants.TenantConnCode, context);
            if (!string.IsNullOrEmpty(tenantConnCode))
            {
                setter.VerifyAndSetConnectionString(tenantConnCode);
            }
            else if (context.Request.Headers.TryGetValue(httpContextItemConnectionCode, out var tenant))
            {
                setter.VerifyAndSetConnectionString(tenant);
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                var code = await tWPortalService.GetConnectionStringCodeAsync(Guid.Parse(userId));
                setter.VerifyAndSetConnectionString(code);
            }
            else
            {
                var connectionString = appSettings.Value.ConnectionStrings["DefaultConnection"];
                if (!string.IsNullOrEmpty(connectionString))
                {
                    dataContext.SetConnectionString(connectionString);
                }
            }

            await next(context);
        }

        /// <summary>
        /// Get claim value
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        private string GetClaimValue(string claim, HttpContext context)
        {
            string claimValue = string.Empty;

            if (context.User.Identity is ClaimsIdentity identity)
            {
                claimValue = identity.Claims.Where(c => c.Type == claim).Select(c => c.Value).SingleOrDefault();
            }

            return claimValue;
        }
    }
}