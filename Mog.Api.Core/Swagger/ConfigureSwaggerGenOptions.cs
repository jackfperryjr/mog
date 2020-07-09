using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mog.Api.Core.Swagger
{
    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerSettings _settings;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionDescriptionProvider,
            IOptions<SwaggerSettings> swaggerSettings)
        {
            Debug.Assert(versionDescriptionProvider != null, $"{nameof(versionDescriptionProvider)} != null");
            Debug.Assert(swaggerSettings != null, $"{nameof(swaggerSettings)} != null");

            _provider = versionDescriptionProvider;
            _settings = swaggerSettings.Value ?? new SwaggerSettings();
        }

        public void Configure(SwaggerGenOptions options)
        {
            //options.DescribeAllEnumsAsStrings();
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
            SetCommentsPathForSwaggerJsonAndUi(options);
            //SetDefaultVersion(options);
        }

        private void SetDefaultVersion(SwaggerGenOptions options)
        {
            // This call removes version from parameter, without it we will have version as parameter 
            // for all endpoints in swagger UI
            options.OperationFilter<RemoveVersionFromParameter>();

            // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        }

        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                _settings.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    _settings.Info.Description += " - DEPRECATED";
                }

                options.SwaggerDoc(description.GroupName, _settings.Info);
            }
        }

        private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //options.IncludeXmlComments(xmlPath);
        }
    }
}
