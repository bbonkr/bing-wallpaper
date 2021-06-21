using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Mediator.Images.Commands;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;

using CronScheduler.Extensions.Scheduler;

using kr.bbon.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Wallpaper.Jobs
{
    public class BingImageJob : IScheduledJob
    {
        const string TAG = "[JOB - BING TODAY IMAGE]";

        public BingImageJob(
            //IServiceProvider provider
            IMediator mediator,
            IOptionsMonitor<CollectorOptions> collectorOptionAccessor,
            ILogger<BingImageJob> logger
            )
        {
            //this.provider = provider;
            //var scope = provider.CreateScope();

            //databaseContext = scope.ServiceProvider.GetRequiredService<DefaultDatabaseContext>();
            //imageService = scope.ServiceProvider.GetRequiredService<IImageService<BingImage>>();
            //fileService = scope.ServiceProvider.GetRequiredService<ILocalFileService>();
            //var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            //logger = loggerFactory.CreateLogger<BingImageJob>();
            //var collectorOptionAccessor = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<CollectorOptions>>();
            //collectorOptions = collectorOptionAccessor.CurrentValue;

            this.mediator = mediator;
            this.collectorOptions = collectorOptionAccessor.CurrentValue ?? throw new ArgumentException("");
            this.logger = logger;
        }

        public string Name { get; } = nameof(BingImageJob);

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // print options
            logger.LogInformation($"[{TAG}][{nameof(CollectorOptions)}]: ${collectorOptions.ToJson()}");

            var watch = new Stopwatch();
            var now = DateTimeOffset.UtcNow;
            string message = string.Empty;

            try
            {
                watch.Start();

                //var bingImages = await imageService.Get();

                //if (bingImages == null)
                //{
                //    message = "An exception occurred while collecting image information.";
                //    logger.LogInformation(message);
                //    //return StatusCode(500, ErrorModel.GetErrorModel(500, "Server error"));
                //    throw new Exception(message);
                //}

                //if (!String.IsNullOrEmpty(bingImages.Message))
                //{
                //    message = $"Image information collection procedure message: {bingImages.Message}";
                //    logger.LogInformation(message);
                //    //return StatusCode(400, ErrorModel.GetErrorModel(400, bingImages.Message));
                //    throw new Exception(bingImages.Message);
                //}

                //if (bingImages.Images.Count == 0)
                //{
                //    message = "There are no collection results.";
                //    logger.LogInformation(message);
                //    //return StatusCode(404, ErrorModel.GetErrorModel(404, "Does not Have image information."));
                //    throw new Exception(message);
                //}


                //var result = new List<ImageInfo>();

                //foreach (var image in bingImages.Images)
                //{
                //    if (databaseContext.Images.Any(x => x.Hash == image.Hsh))
                //    {
                //        continue;
                //    }

                //    var savedFile = await fileService.SaveAsync(image);

                //    result.Add(new ImageInfo
                //    {
                //        BaseUrl = image.GetBaseUrl(),
                //        Url = image.Url,
                //        FilePath = savedFile.FilePath,
                //        FileName = savedFile.FileName,
                //        Directory = savedFile.Directory,
                //        Hash = image.Hsh,
                //        ContentType = savedFile.ContentType,
                //        FileSize = savedFile.Size,
                //        CreatedAt = now,
                //        Metadata = new ImageMetadata
                //        {
                //            Title = image.Title,
                //            Origin = image.GetSourceTitle(),
                //            Copyright = image.Copyright,
                //            CopyrightLink = image.CopyrightLink,
                //        }
                //    });
                //}
                //var affectedCount = 0;
                //if (result.Count > 0)
                //{
                //    databaseContext.Images.AddRange(result.ToArray());

                //    affectedCount = await databaseContext.SaveChangesAsync(cancellationToken);
                //}

                var command = new AddImageCommand();

                var result =await mediator.Send(command);

                watch.Stop();
                logger.LogInformation($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed} Completed. Count of collecting images is {result.CollectedCount:n0}");
            }
            catch (Exception ex)
            {
                watch.Stop();
                logger.LogWarning($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed} Incompleted ({ex.Message})");
            }
            finally
            {
                
            }
        }

        //private readonly DefaultDatabaseContext databaseContext;
        //private readonly IImageService<BingImage> imageService;
        //private readonly ILocalFileService fileService;
        //private readonly ILogger logger;
        private readonly CollectorOptions collectorOptions;
        //private readonly IServiceProvider provider;
        private readonly IMediator mediator;
        private readonly ILogger logger;
    }
}
