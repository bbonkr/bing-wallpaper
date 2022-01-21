using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Models;

public class LogModel
{
    public long Id { get; set; }

    public string Message { get; set; }

    public string MessageTemplate { get; set; }

    public string Level { get; set; }

    public long TimeStamp { get; set; }

    public string Exception { get; set; }

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

    public long? ResolvedAt { get; set; }
}
