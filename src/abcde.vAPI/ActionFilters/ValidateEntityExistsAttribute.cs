using Microsoft.AspNetCore.Mvc.Filters;

namespace abcde.vAPI.ActionFilters
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter
    {
        private const string httpRequestHeaderTenantId = "tenantId";

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tenantId = context.HttpContext.Request.Headers[httpRequestHeaderTenantId].ToString();

            if (!context.HttpContext.Items.Keys.Contains(httpRequestHeaderTenantId))
            {
                context.HttpContext.Items.Add(httpRequestHeaderTenantId, tenantId);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
