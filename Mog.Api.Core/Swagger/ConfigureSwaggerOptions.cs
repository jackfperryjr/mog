using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Mog.Api.Core.Swagger
{
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        private readonly SwaggerSettings _settings;

        public ConfigureSwaggerOptions(IOptions<SwaggerSettings> settings)
        {
            _settings = settings?.Value ?? new SwaggerSettings();
        }

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
        }
    }
}