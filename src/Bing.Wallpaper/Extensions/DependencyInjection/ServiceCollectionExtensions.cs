using System;
using System.Collections.Generic;
using System.Reflection;
using Bing.Wallpaper.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Wallpaper.Extensions.DependencyInjection;

#nullable enable
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidatorIntercepter(this IServiceCollection services)
    {
        services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services, Type? type = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        Assembly assembly;
        if (type == null)
        {
            assembly = Assembly.GetExecutingAssembly();
        }
        else
        {
            assembly = type.Assembly;
        }

        services.AddValidatorsFromAssemblies(new List<Assembly> { assembly }, serviceLifetime);

        return services;
    }
}

#nullable restore
