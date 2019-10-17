﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Models
{

    public class BingImage: ImageModel
    {
        public string UrlBase { get; set; }

        public string CopyRight { get; set; }

        public string CopyRightLink { get; set; }

        public override string GetBaseUrl()
        {
            return "https://bing.com";
        }

        public override string GetFileName()
        {
            if (!String.IsNullOrEmpty(Url))
            {
                Regex regex = new Regex("[?&]id=([^?&]+)");
                return regex.Match(Url).Groups.Values.LastOrDefault()?.Value;
            }

            return null;
        }

        public override string GetSourceTitle()
        {
            return "Bing-Image";
        }
    }
}
