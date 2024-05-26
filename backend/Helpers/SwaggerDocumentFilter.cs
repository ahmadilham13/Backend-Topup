using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend.Helpers;

public class SwaggerDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// Applies the document filtering
    /// </summary>
    /// <param name="swaggerDoc">The <see cref="OpenApiDocument"/> Swagger document</param>
    /// <param name="context">The <see cref="DocumentFilterContext"/> object</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var swaggerDocVersion = swaggerDoc.Info.Version;

        // Apply filter only for internal API
        if (swaggerDocVersion == "internal-v1")
        {
            var nonMobileRoutes = swaggerDoc.Paths
                .Where(x => !x.Key.ToLower().Contains("internal"))
                .ToList();

            nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}
