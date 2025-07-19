using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TelemetryApi
{
    public class AddAuthTokenHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add only for endpoints with [RequireAuthToken]
            var hasAuthToken = context.MethodInfo.GetCustomAttributes(typeof(RequireAuthKeyAttribute), false).Any();
            if (!hasAuthToken) return;

            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "AuthToken",
                In = ParameterLocation.Header,
                Description = "Authorization token provided on industry creation",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
