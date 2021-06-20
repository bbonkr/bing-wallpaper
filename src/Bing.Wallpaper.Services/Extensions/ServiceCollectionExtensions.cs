using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuation)
        {
            services.Configure<CollectorOptions>(options =>
            {
                configuation.GetSection(CollectorOptions.Name).Bind(options);
            });

            services.AddScoped<IImageService<BingImage>, BingImageService>();
            services.AddScoped<ILocalFileService, LocalFileService>();
            services.AddTransient<IImageFileService, ImageFileService>();

            return services;
        }
    }
}
