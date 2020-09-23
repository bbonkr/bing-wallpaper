using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services
{
    public class LocalFileService : ILocalFileService
    {
        public LocalFileService(IOptionsMonitor<AppOptions> optionMonitor)
        {
            this.appOptions = optionMonitor.CurrentValue;
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<ImageInfo> Save(ImageModel image)
        {
            ImageInfo result = new ImageInfo();
            var baseUrl = image.GetBaseUrl();
            var imageUrl = $"{baseUrl}{image.Url}";

            var destinationDirectory = appOptions.DestinationPath;

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            var response = await client.GetAsync(imageUrl);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                if (response.Content.Headers.Contains("content-type"))
                {
                    var contentType = response.Content.Headers.GetValues("content-type").FirstOrDefault();

                    result.ContentType = contentType;
                }

                var fileName = image.GetFileName();

                if (String.IsNullOrEmpty(fileName))
                {
                    throw new ArgumentException("Does not find a file name");
                }

                var fileNameWithoutExtension = fileName;
                var fileExtension = String.Empty;

                if (fileName.Contains("."))
                {
                    var fileNamesToken = fileName.Split('.');
                    
                    fileNameWithoutExtension = String.Join(".", fileNamesToken.Take(fileNamesToken.Length - 1));

                    fileExtension = $".{ fileName.Split('.').LastOrDefault() }";
                }

                var saveFileName = $"{fileNameWithoutExtension}-{DateTimeOffset.UtcNow.Ticks}{fileExtension}";


                var filePath = Path.Combine(destinationDirectory, saveFileName);


                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    result.FileSize = responseStream.Length;

                    responseStream.Position = 0;

                    using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        await responseStream.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        fileStream.Close();
                    }
                    responseStream.Close();
                }

                result.BaseUrl = image.GetBaseUrl();
                result.Url = image.Url;
                result.FilePath = filePath;
                result.FileName = fileName;
                result.Directory = destinationDirectory;
                result.Hash = image.Hsh;
                result.CreatedAt = DateTimeOffset.UtcNow;
            }

            return result;
        }

        private readonly AppOptions appOptions;
        private readonly HttpClient client;
    }
}
