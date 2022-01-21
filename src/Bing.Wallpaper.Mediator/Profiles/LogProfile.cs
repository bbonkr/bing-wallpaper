using AutoMapper;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Mediator.Models;
using kr.bbon.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Profiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        var javascriptDateConvert = new JavascriptDateConverter();

        CreateMap<Log, LogModel>()
            .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => javascriptDateConvert.ToJavascriptDateMilliseconds(src.TimeStamp)))
            .ForMember(dest => dest.ResolvedAt, opt => opt.MapFrom(src => javascriptDateConvert.ToJavascriptDateMilliseconds(src.ResolvedAt)));
    }
}
