using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Wallpaper;

internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private static readonly OpenApiContact DefaultContact = new()
    {
        Name = "Bing Wallpaper",
    };
    private static readonly OpenApiLicense DefaultLicense = new()
    {
        Name = "MIT",
    };

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var versionLabel = $"v{description.ApiVersion}";
            var info = new OpenApiInfo
            {
                Title = $"Bing Wallpaper API {versionLabel}",
                Version = versionLabel,
                Description = "REST API for Bing Wallpaper service.",
                Contact = DefaultContact,
                License = DefaultLicense,
            };

            if (description.IsDeprecated)
            {
                info.Description = "DEPRECATED: This API version has been deprecated. Please migrate to a newer version.";
            }

            options.SwaggerDoc(description.GroupName, info);
        }
    }
}
