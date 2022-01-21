using System;
namespace Bing.Wallpaper.Entities
{
	public class Log
	{
        public long Id { get; set; }

        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }  // XML data

        public string LogEvent { get; set; }    // JSON data

        public string Payload { get; set; }

        public string QueryString { get; set; }

        public string UserRoles { get; set; }

        public string UserIp { get; set; }

        public string RequestUri { get; set; }

        public string HttpMethod { get; set; }

        public bool? IsResolved { get; set; }

        public string UserAgent { get; set; }

        public string Errors { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}

