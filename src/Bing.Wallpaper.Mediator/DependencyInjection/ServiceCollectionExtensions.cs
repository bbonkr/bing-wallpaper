using Bing.Wallpaper.Mediator;
using Bing.Wallpaper.Services.DependencyInjection;

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddApplicationServices();
        var thisAssembly = typeof(PlaceHolder).Assembly;

        services.AddAutoMapper(thisAssembly);
        services.AddMediatR(thisAssembly);

        return services;
    }
}
