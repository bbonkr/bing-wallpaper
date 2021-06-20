using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Options
{
    public class CollectorOptions
    {
        public const string ExceptionMessage = @"Check your appsettings.json. 
{ 
    ""Collector"" : {
        ""DestinationPath"": ""<Image file directory>"",
        ""ThumbnailPath"": ""<Thumbnail image file directory>"",
        ""Schedule"": ""<Cron style schedule e.g.) 0 0 1 0 0 0>""
    } 
}";

        public const string Name = "Collector";

        public string DestinationPath { get; set; }

        public string ThumbnailPath { get; set; }

        /// <summary>
        /// 실행시각 
        /// 
        /// 시각 => HH:mm:ss
        /// ex) 01:00:00
        /// </summary>
        public string RunAtTime { get; set; }

        /// <summary>
        /// Cron style schedule
        /// </summary>
        /// <example>
        /// <code>
        /// -------------------------------------------------------------------------------------------------------------
        ///                                        Allowed values    Allowed special characters   Comment
        /// 
        /// ┌───────────── second (optional)       0-59              * , - /                      
        /// │ ┌───────────── minute                0-59              * , - /                      
        /// │ │ ┌───────────── hour                0-23              * , - /                      
        /// │ │ │ ┌───────────── day of month      1-31              * , - / L W ?                
        /// │ │ │ │ ┌───────────── month           1-12 or JAN-DEC   * , - /                      
        /// │ │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT   * , - / # L ?                Both 0 and 7 means SUN
        /// │ │ │ │ │ │
        /// * * * * * *                     
        /// -------------------------------------------------------------------------------------------------------------
        /// </code>
        /// </example>
        public string Schedule { get; set; }
    }
}
