using System;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation.AspNetCore;
using Bing.Wallpaper.Infrastructure.Validations;

namespace Bing.Wallpaper.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
        public static IServiceCollection AddValidatorIntercepter(this IServiceCollection services)
        {
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}

