using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend.Helpers;

public class HeaderForInternalApiFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Add header only for specific endpoint
        if (context.ApiDescription.RelativePath.Contains("/internal") || context.ApiDescription.RelativePath.Contains("/cron"))
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = [];
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-access-token",
                In = ParameterLocation.Header,
                Description = "The access token for authentication.",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
