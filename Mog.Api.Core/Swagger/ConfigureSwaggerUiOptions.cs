using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Mog.Api.Core.Swagger
{
    public sealed class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerSettings _settings;

        public ConfigureSwaggerUiOptions(IApiVersionDescriptionProvider versionDescriptionProvider, IOptions<SwaggerSettings> settings)
        {
            Debug.Assert(versionDescriptionProvider != null, $"{nameof(versionDescriptionProvider)} != null");
            Debug.Assert(settings != null, $"{nameof(versionDescriptionProvider)} != null");

            _provider = versionDescriptionProvider;
            _settings = settings?.Value ?? new SwaggerSettings();
        }

        public void Configure(SwaggerUIOptions options)
        {
            _provider
                .ApiVersionDescriptions
                .ToList()
                .ForEach(description =>
                {
                    options.SwaggerEndpoint(
                        $"./{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                });
        }
    }
}
