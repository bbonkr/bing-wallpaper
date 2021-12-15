using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services.Models;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services;

public class BingImageService : IImageService<BingImage>
{
    // https://www.bing.com/HPImageArchive.aspx?format=js&idx=1&n=10&mkt=en-US
    private const string BASE_URL = "https://www.bing.com/HPImageArchive.aspx";

    public BingImageService(ILoggerFactory loggerFactory)
    {
        logger = loggerFactory.CreateLogger<BingImageService>();
        client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<ImagesModel<BingImage>> Get(BingImageServiceGetRequestModel model)
    {
        ImagesModel<BingImage> result = new ImagesModel<BingImage>();

        var url = $"{BASE_URL}{model.CreateQueryString()}";

        try
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var json = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<ImagesModel<BingImage>>(json, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true,
                });

                return result;
            }
        }
        catch (Exception ex)
        {
            var exceptionMessage = ex.Message;
            if (ex is AggregateException)
            {
                exceptionMessage = ((AggregateException)ex).InnerExceptions.FirstOrDefault()?.Message;
            }

            exceptionMessage = $"[SERVICE] {nameof(BingImageService)}.{nameof(Get)}: {exceptionMessage}";

            logger.LogError(ex, exceptionMessage);

            result.Message = exceptionMessage;
        }


        return new ImagesModel<BingImage>();
    }

    private readonly ILogger logger;
    private readonly HttpClient client;
}
