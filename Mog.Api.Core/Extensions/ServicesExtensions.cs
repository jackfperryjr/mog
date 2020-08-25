using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Security;
using Mog.Api.Core.Swagger;

namespace Mog.Api.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApiVersionWithExplorer(this IServiceCollection services)
        {
            return services
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = false;
                    options.ReportApiVersions = true;
                });
        }

        public static IServiceCollection AddEntityFramework<IT, T>(this IServiceCollection services, IConfiguration configuration) 
            where T:DbContext, IT where IT : class
        {
            var migrationAssembly = typeof(T).Assembly.GetName().Name;

            return services.AddEntityFrameworkSqlServer()
                .AddDbContext<T>((serviceProvider, options) =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        x => x.MigrationsAssembly(migrationAssembly)
                    ).UseInternalServiceProvider(serviceProvider))
                .AddScoped<IT, T>();
        }

        public static IServiceCollection AddSwaggerOptions(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigureOptions<SwaggerOptions>, ConfigureSwaggerOptions>()
                .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUiOptions>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfigurationSection configSection)
        {
            var appSettings = configSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = "ChocoboApi",
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudiences = new List<string>() { "MoogleApi" }
                    };
                });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            return services;
        }

        public static IServiceCollection AddFactories(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(IFactoryBase);

            return ServiceCollection(services, assembly, interfaceType);
        }

        public static IServiceCollection AddStores(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(IStoreBase);

            return ServiceCollection(services, assembly, interfaceType);
        }
        
        public static IServiceCollection AddCache(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(ICacheBase);

            return ServiceCollection(services, assembly, interfaceType);
        }

        private static IServiceCollection ServiceCollection(IServiceCollection services, Assembly assembly, Type interfaceType)
        {
            var typesToRegister = assembly
                .GetTypes()
                .Where(x =>
                    !string.IsNullOrEmpty(x.Namespace)
                    && x.IsClass
                    && interfaceType.IsAssignableFrom(x))
                .ToList();

            foreach (var type in typesToRegister)
            {
                var repositoryInterfaces = type.GetInterfaces()
                    .Where(interfaceType.IsAssignableFrom);

                foreach (var repositoryInterface in repositoryInterfaces)
                {
                    services.AddTransient(repositoryInterface, type);
                }
            }

            return services;
        }
    }
}
