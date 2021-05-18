using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bing.Wallpaper.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Wallpaper.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuation)
        {
            services.AddScoped<IImageService<BingImage>, BingImageService>();
            services.AddScoped<ILocalFileService, LocalFileService>();
            services.AddTransient<IImageFileService, ImageFileService>();

            return services;
        }
    }
}
