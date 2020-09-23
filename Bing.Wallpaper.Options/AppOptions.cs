using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Options
{
    public class AppOptions
    {
        public const string App = "App";

        public string DestinationPath { get; set; }

        /// <summary>
        /// 실행시각 
        /// 
        /// 시각 => HH:mm:ss
        /// ex) 01:00:00
        /// </summary>
        public string RunAtTime { get; set; }
    }
}
