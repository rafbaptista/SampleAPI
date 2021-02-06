using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UserAPI.Services.WebApi.Configurations
{
    public static class VersioningConfig
    {
        public static void AddVersioningConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;

                //queryString + header versioning
                config.ApiVersionReader = ApiVersionReader.Combine
                (
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("api-version")
                );
            });
        }
    }
}
