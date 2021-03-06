﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services;

using CronScheduler.Extensions.Scheduler;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Wallpaper.Jobs
{
    public class BingImageJob : IScheduledJob
    {
        const string TAG = "[JOB - BING TODAY IMAGE]";

        public BingImageJob(IServiceProvider provider)
        {

            var scope = provider.CreateScope();

            databaseContext = scope.ServiceProvider.GetRequiredService<DefaultDatabaseContext>();
            imageService  = scope.ServiceProvider.GetRequiredService<IImageService<BingImage>>();
            fileService = scope.ServiceProvider.GetRequiredService<ILocalFileService>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            logger = loggerFactory.CreateLogger<BingImageJob>();
        }

        public string Name { get; } = nameof(BingImageJob);

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var watch = new Stopwatch();
            string message = string.Empty;

            try
            {
                watch.Start();

                var bingImages = await imageService.Get();

                if (bingImages == null)
                {
                    message = "An exception occurred while collecting image information.";
                    logger.LogInformation(message);
                    //return StatusCode(500, ErrorModel.GetErrorModel(500, "Server error"));
                    throw new Exception(message);
                }

                if (!String.IsNullOrEmpty(bingImages.Message))
                {
                    message = $"Image information collection procedure message: {bingImages.Message}";
                    logger.LogInformation(message);
                    //return StatusCode(400, ErrorModel.GetErrorModel(400, bingImages.Message));
                    throw new Exception(bingImages.Message);
                }

                if (bingImages.Images.Count == 0)
                {
                    message = "There are no collection results.";
                    logger.LogInformation(message);
                    //return StatusCode(404, ErrorModel.GetErrorModel(404, "Does not Have image information."));
                    throw new Exception(message);
                }


                var result = new List<ImageInfo>();

                foreach (var image in bingImages.Images)
                {
                    if (databaseContext.Images.Any(x => x.Hash == image.Hsh))
                    {
                        continue;
                    }

                    var imageInfo = await fileService.Save(image);
                    result.Add(imageInfo);
                }

                databaseContext.Images.AddRange(result.ToArray());

                await databaseContext.SaveChangesAsync();

                watch.Stop();
                logger.LogInformation($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed.ToString("mm:ss:fff")} Completed");
            }
            catch (Exception ex)
            {
                watch.Stop();
                logger.LogInformation($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed.ToString("mm:ss:fff")} Incompleted ({ex.Message})");
            }
            finally
            {
              
                
            }
        }

        private readonly DefaultDatabaseContext databaseContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService fileService;
        private readonly ILogger logger;
            
    }
}