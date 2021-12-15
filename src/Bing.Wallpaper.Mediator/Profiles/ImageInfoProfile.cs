using AutoMapper;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Mediator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Profiles;

public class ImageInfoProfile : Profile
{
    public ImageInfoProfile()
    {
        CreateMap<ImageInfo, ImageItemModel>()
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.Ticks))
         .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Metadata.Title))
         .ForMember(dest => dest.Copyright, opt => opt.MapFrom(src => src.Metadata.Copyright))
         .ForMember(dest => dest.CopyrightLink, opt => opt.MapFrom(src => src.Metadata.CopyrightLink))
         .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Metadata.Width))
         .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Metadata.Height))
         ;


        CreateMap<ImageInfo, ImageItemDetailModel>()
        .IncludeBase<ImageInfo, ImageItemModel>()
        .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));
    }
}
