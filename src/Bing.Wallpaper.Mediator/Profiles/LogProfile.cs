using AutoMapper;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Mediator.Models;
using kr.bbon.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Profiles
{
    public class LogProfile :Profile
    {
        public LogProfile()
        {
            var javascriptDateConvert = new JavascriptDateConverter();

            CreateMap<AppLog, LogModel>()
                .ForMember(dest => dest.Logged, opt => opt.MapFrom(src => src.Logged.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.LoggedAt, opt => opt.MapFrom(src => javascriptDateConvert.ToJavascriptDateMilliseconds(src.Logged)));
        }
    }
}
