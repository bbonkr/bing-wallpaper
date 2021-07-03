using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;
using Bing.Wallpaper.Services.Models;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Bing.Wallpaper.Service.App
{
    internal class App : IHostedService
    {
        public App(
            IHostApplicationLifetime hostApplicationLifetime,
            DefaultDatabaseContext databaseContext,
            IOptions<CollectorOptions> appOptionsAccessor,
            IImageService<BingImage> imageService,
            ILocalFileService fileService,
            ILoggerFactory loggerFactory)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.databaseContext = databaseContext;
            this.imageService = imageService;
            this.fileService = fileService;
            
            appOptions = appOptionsAccessor.Value ?? new CollectorOptions();

            logger = loggerFactory.CreateLogger<App>();

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                ValidateAppOptions();

                PrintOptions();

                timer.Start();

                logger.LogInformation("App Started.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                hostApplicationLifetime.StopApplication();
            }
            

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Stop();

            logger.LogInformation("App shut down");

            return Task.CompletedTask;
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var current = String.Format("{0:HH:mm:ss}", e.SignalTime);
            //logger.LogInformation($"current time: {current}");
            if (current == appOptions.RunAtTime)
            {
                try
                {
                    await RunService();
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
                finally
                {

                }

            }
        }

        private void ValidateAppOptions()
        {
            if(String.IsNullOrWhiteSpace(appOptions.DestinationPath))
            {
                throw new InvalidOperationException("파일 저장 위치가 지정되지 않았습니다.");
            }

            if (!Directory.Exists(appOptions.DestinationPath))
            {
                try
                {
                    Directory.CreateDirectory(appOptions.DestinationPath);
                }
                catch(Exception ex)
                {
                    throw new InvalidOperationException("파일 저장 위치를 작성할 수 없습니다.", ex);
                }
            }

            if(String.IsNullOrWhiteSpace(appOptions.RunAtTime))
            {
                throw new InvalidOperationException("실행 시각이 지정되지 않았습니다.");
            }

            if (!Regex.IsMatch(appOptions.RunAtTime,  @"\d{2}:\d{2}:\d{2}"))
            {
                throw new InvalidOperationException("실행 시각이 입력이 유효하지 않습니다.");
            }

            TimeSpan timeSpanValue = TimeSpan.Zero;
            if(!TimeSpan.TryParse(appOptions.RunAtTime, out timeSpanValue))
            {
                throw new InvalidOperationException("실행 시각이 입력이 유효하지 않습니다.");
            }
        }

        private async Task RunService()
        {
            var now = DateTimeOffset.UtcNow;
            logger.LogInformation("이미지 수집을 시작합니다");
            var reuqestModel = new BingImageServiceGetRequestModel();
            var serviceResult = await imageService.Get(reuqestModel);

            if (serviceResult == null)
            {
                throw new ApplicationException("이미지 정보를 수집 중 예외가 발생했습니다");
            }

            if (!String.IsNullOrWhiteSpace(serviceResult.Message))
            {
                throw new ApplicationException(serviceResult.Message);
            }

            if (serviceResult.Images.Count == 0)
            {
                logger.LogInformation("수집된 이미지가 없습니다");
            }
            else
            {
                var result = new List<ImageInfo>();

                foreach (var image in serviceResult.Images)
                {
                    if (databaseContext.Images.Any(x => x.Hash == image.Hsh))
                    {
                        continue;
                    }

                    var savedFile = await fileService.SaveAsync(image);

                    result.Add(new ImageInfo
                    {
                        BaseUrl = image.GetBaseUrl(),
                        Url = image.Url,
                        FilePath = savedFile.FilePath,
                        FileName = savedFile.FileName,
                        Directory = savedFile.Directory,
                        Hash = image.Hsh,
                        CreatedAt = now,
                        Metadata = new ImageMetadata
                        {
                            Title = image.Title,
                            Origin = image.GetSourceTitle(),
                            Copyright = image.Copyright,
                            CopyrightLink = image.CopyrightLink,
                        }
                    });
                }

                if (result.Count > 0)
                {
                    databaseContext.Images.AddRange(result.ToArray());

                    await databaseContext.SaveChangesAsync();
                }

                logger.LogInformation($"이미지 수집을 종료합니다. 수집된 이미지는 {serviceResult.Images.Count}건 입니다.");
            }
        }

        private void PrintOptions()
        {
            logger.LogInformation($"Destination path: {appOptions.DestinationPath}");
            logger.LogInformation($"Run at time: {appOptions.RunAtTime}");
        }

        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly DefaultDatabaseContext databaseContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService fileService;
        private readonly CollectorOptions appOptions;
        private readonly ILogger logger;
        private readonly System.Timers.Timer timer;
    }
}
