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
        public LocalFileService(IOptionsMonitor<CollectorOptions> optionMonitor)
        {
            this.appOptions = optionMonitor.CurrentValue;
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<LocalFileModel> SaveAsync(ImageModel image)
        {
            //ImageInfo result = new ImageInfo();
            var result = new LocalFileModel();

            var baseUrl = image.GetBaseUrl();
            var imageUrl = $"{baseUrl}{image.Url}";
            var now = DateTimeOffset.UtcNow;

            var destinationDirectory = appOptions.DestinationPath;

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            result.Directory = destinationDirectory;

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

                // TODO: Needs refactoring.

                //result.BaseUrl = image.GetBaseUrl();
                //result.Url = image.Url;
                //result.FilePath = filePath;
                //result.FileName = fileName;
                //result.Directory = destinationDirectory;
                //result.Hash = image.Hsh;
                //result.CreatedAt = now;
                //result.Metadata = new ImageMetadata
                //{
                //    Title = image.Title,
                //    Origin = image.GetSourceTitle(),
                //    Copyright = image.Copyright,
                //    CopyrightLink = image.CopyrightLink,
                //};
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
    }
}
