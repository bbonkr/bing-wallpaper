using Bing.Wallpaper.Services.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Wallpaper.Mediator.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddApplicationServices();
        var thisAssembly = typeof(PlaceHolder).Assembly;

        services.AddAutoMapper(options => { }, thisAssembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(thisAssembly);
        });

        return services;
    }
}
