using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Telemetry.Business;
using Telemetry.Repositories.Interfaces;

namespace TelemetryApi
{
    public class RequireAuthKeyAttribute :  Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var repository = context.HttpContext.RequestServices.GetService<IKeyRepository>();

            var request = context.HttpContext.Request;
            if (!request.Headers.TryGetValue("AuthToken", out var tokenValue))
            {
                context.Result = new UnauthorizedObjectResult("Missing AuthToken header");
                return;
            }

            string? privateKey = repository?.GetPrivateKey(tokenValue.ToString());
            if (string.IsNullOrEmpty(privateKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid AuthToken");
                return;
            }

            // Store privateKey in HttpContext.Items for later use in controller
            context.HttpContext.Items["PrivateKey"] = privateKey;
            context.HttpContext.Items["AuthToken"] = tokenValue.ToString();
        }
    }
    

}
