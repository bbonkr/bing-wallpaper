using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;

using Microsoft.Extensions.DependencyInjection;

namespace Bing.Wallpaper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDtoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.CreateMap<ImageInfo, ImageItemModel>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.Ticks))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Metadata.Title))
                .ForMember(dest => dest.Copyright, opt => opt.MapFrom(src => src.Metadata.Copyright))
                .ForMember(dest => dest.CopyrightLink, opt => opt.MapFrom(src => src.Metadata.CopyrightLink));

                options.CreateMap<ImageInfo, ImageItemDetailModel>()
                .IncludeBase<ImageInfo, ImageItemModel>()
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));

                options.CreateMap<AppLog, LogModel>();
            });

            return services;
        }
    }
}
