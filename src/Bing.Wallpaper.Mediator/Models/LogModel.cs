using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Models;

public class LogModel
{
    /// <summary>
    /// 식별자
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 장치
    /// </summary>
    public string MachineName { get; set; }

    /// <summary>
    /// 작성시각
    /// </summary>
    public string Logged { get; set; }

    /// <summary>
    /// 작성시각 밀리초
    /// </summary>
    public double LoggedAt { get; set; }

    /// <summary>
    /// 로그 레벨
    /// </summary>
    public string Level { get; set; }

    /// <summary>
    /// 메시지
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 로거
    /// </summary>
    public string Logger { get; set; }

    /// <summary>
    /// 사이트
    /// </summary>
    public string Callsite { get; set; }

    /// <summary>
    /// 예외
    /// </summary>
    public string Exception { get; set; }
}
