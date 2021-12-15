using System.Text.Json.Serialization;

namespace Bing.Wallpaper.Models;

public abstract class ImageModel
{
    public string Url { get; set; }

    public string Title { get; set; }

    public string Hsh { get; set; }

    public abstract string GetBaseUrl();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public abstract ImageFileInfo GetFileName(string suffix);

    /// <summary>
    /// Get remained URL that excludes the base URL.
    /// </summary>
    /// <param name="suffix">file name suffix</param>
    /// <returns></returns>
    //public abstract string GetUrlBase(string suffix);

    public abstract string GetSourceTitle();
}

public class ImageFileInfo
{
    public string BaseUrl { get; set; }

    public string FileName { get; set; }
}
