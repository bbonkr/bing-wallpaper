using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Models;

public class BingImage : ImageModel
{
    public string UrlBase { get; set; }

    public string Copyright { get; set; }

    public string CopyrightLink { get; set; }

    public override string GetBaseUrl()
    {
        return "https://bing.com";
    }

    public override ImageFileInfo GetFileName(string suffix)
    {
        if (!String.IsNullOrEmpty(Url))
        {
            if (Url.Contains("?"))
            {
                var urlTokens = Url.Split('?', StringSplitOptions.RemoveEmptyEntries);
                if (urlTokens.Length > 1)
                {
                    var values = urlTokens[1].Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);

                    if (values.ContainsKey("id"))
                    {
                        var fileName = string.Empty;
                        if (values.TryGetValue("id", out fileName))
                        {
                            var fileNameTokens = fileName.Split('.');

                            //var name = string.Join(".", fileNameTokens.Length > 1 ? fileNameTokens.Take(fileNameTokens.Length - 1) : fileNameTokens);
                            var extension = fileNameTokens.Length > 1 ? $".{fileNameTokens.Last()}" : string.Empty;

                            var baseUrl = $"{this.UrlBase}{suffix}{extension}";

                            var name = baseUrl.Split('=').Last();

                            return new ImageFileInfo
                            {
                                BaseUrl = baseUrl,
                                FileName = name,
                            };
                        }
                    }
                }
            }

            Regex regex = new Regex("[?&]id=([^?&]+)");
            if (regex.Match(Url).Success)
            {
                //return regex.Match(Url).Groups.Values.LastOrDefault()?.Value;

                var matchGroup = regex.Match(Url).Groups;

                return new ImageFileInfo
                {
                    BaseUrl = Url,
                    FileName = matchGroup[matchGroup.Count - 1].Value,
                };
            }
        }

        return null;
    }

    //public override string GetUrlBase(string suffix = "_1920x1080")
    //{
    //    if (Url.Contains("?"))
    //    {
    //        var urlTokens = Url.Split('?', StringSplitOptions.RemoveEmptyEntries);
    //        if (urlTokens.Length > 1)
    //        {
    //            var values = urlTokens[1].Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);

    //            if (values.ContainsKey("id"))
    //            {
    //                var fileName = string.Empty;
    //                if (values.TryGetValue("id", out fileName))
    //                {
    //                    var extension = fileName.Split('.').Last();

    //                    return $"{this.UrlBase}{suffix}.{extension}";
    //                }
    //            }
    //        }
    //    }

    //    return Url;
    //}

    public override string GetSourceTitle()
    {
        return "Bing-Image";
    }
}
