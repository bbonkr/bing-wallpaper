using System.Collections.Generic;
using System.Text;

namespace Bing.Wallpaper.Services.Models;

public class BingImageServiceGetRequestModel
{
    /// <summary>
    /// Response format. <see cref="BingImageServiceGetFormats"/>
    /// </summary>
    public string Format { get; set; } = BingImageServiceGetFormats.Json;

    /// <summary>
    /// Start index of items
    /// </summary>
    public int StartIndex { get; set; } = 1;

    /// <summary>
    /// Take items max 8
    /// </summary>
    public int Take { get; set; } = 8;

    /// <summary>
    /// Market <see cref="BingImageServiceGetMarkets"/>
    /// </summary>
    public string Market { get; set; } = "en-US";

    public string CreateQueryString()
    {
        // ?format=js&idx=1&n=10&mkt=en-US

        List<string> args = new List<string>();

        if (string.IsNullOrWhiteSpace(Format))
        {
            Format = BingImageServiceGetFormats.Json;
        }

        if (StartIndex < 0)
        {
            StartIndex = 1;
        }

        if (Take < 0 || Take > 8)
        {
            Take = 8;
        }

        if (string.IsNullOrWhiteSpace(Market))
        {
            Market = BingImageServiceGetMarkets.EN_US;
        }

        args.Add($"format={Format ?? BingImageServiceGetFormats.Json }");
        args.Add($"idx={StartIndex}");
        args.Add($"n={Take}");
        args.Add($"mkt={Market}");

        return $"?{string.Join("&", args)}";
    }
}

public class BingImageServiceGetFormats
{
    public static string Json = "js";
    public static string Xml = "xml";
}

public class BingImageServiceGetMarkets
{
    public static string EN_US = "en-US";
}
