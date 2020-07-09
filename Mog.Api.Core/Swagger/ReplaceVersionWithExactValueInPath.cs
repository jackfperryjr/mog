using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mog.Api.Core.Swagger 
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach (var x in swaggerDoc.Paths)
            {
                paths.Add(x.Key.Replace("{description.GroupName}", swaggerDoc.Info.Version), x.Value);
            }

            swaggerDoc.Paths = paths;
        }
    }
}