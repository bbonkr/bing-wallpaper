using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services.Models;

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
        public LocalFileService(IOptionsMonitor<CollectorOptions> optionMonitor, IImageFileService imageFileService)
        {
            this.appOptions = optionMonitor.CurrentValue;
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            this.imageFileService = imageFileService;
        }

        public async Task<LocalFileModel> SaveAsync(ImageModel image)
        {
            //ImageInfo result = new ImageInfo();
            var result = new LocalFileModel();

            var baseUrl = image.GetBaseUrl();
       
            var now = DateTimeOffset.UtcNow;

            var destinationDirectory = appOptions.DestinationPath;

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            result.Directory = destinationDirectory;

            var suffixes = new string[] { "_UHD", "_1920x1080" };

            foreach (var suffix in suffixes)
            {
                var imageInfo = image.GetFileName(suffix);
                var imageUrl = $"{baseUrl}{imageInfo.BaseUrl}";
                var response = await client.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    if (response.Content.Headers.Contains("content-type"))
                    {
                        var contentType = response.Content.Headers.GetValues("content-type").FirstOrDefault();

                        result.ContentType = contentType;
                    }
                    else
                    {
                        result.ContentType = "application/octet-stream";
                    }

                    var fileName = imageInfo.FileName;

                    if (String.IsNullOrEmpty(fileName))
                    {
                        throw new ArgumentException("Does not find a file name");
                    }

                    result.FileName = fileName;

                    var fileNameWithoutExtension = fileName;
                    var fileExtension = String.Empty;

                    if (fileName.Contains("."))
                    {
                        var fileNamesToken = fileName.Split('.');

                        fileNameWithoutExtension = String.Join(".", fileNamesToken.Take(fileNamesToken.Length - 1));

                        fileExtension = $".{ fileName.Split('.').LastOrDefault() }";
                    }

                    var saveFileName = $"{fileNameWithoutExtension}-{now.Ticks}{fileExtension}";


                    var filePath = Path.Combine(destinationDirectory, saveFileName);
                    result.FilePath = filePath;

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        result.Size = responseStream.Length;

                        responseStream.Position = 0;

                        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            await responseStream.CopyToAsync(fileStream);
                            await fileStream.FlushAsync();
                            fileStream.Close();
                        }
                        responseStream.Close();
                    }

                    var (width, height) = imageFileService.GetImageResolution(filePath);

                    result.Width = width;
                    result.Height = height;

                    await imageFileService.GenerateThumbnailAsync(filePath);

                    break;
                }
            }
            

            return result;
        }

        public async Task<byte[]> ReadAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        private readonly CollectorOptions appOptions;
        private readonly HttpClient client;
        private readonly IImageFileService imageFileService;
    }
}
