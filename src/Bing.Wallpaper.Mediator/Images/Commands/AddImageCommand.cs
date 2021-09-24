using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services;
using kr.bbon.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Images.Commands
{
    public class AddImageCommand : IRequest<AddImageCommandResult>
    {
        public AddImageCommand(int startIndex = 1, int take = 8)
        {
            StartIndex = startIndex;
            Take = take;
        }

        public int StartIndex { get; set; } = 1;

        public int Take { get; set; } = 8;
    }

    public class AddImageCommandResult
    {
        public int CollectedCount { get; set; }
    } 

    public class AddImageCommandHandler :IRequestHandler<AddImageCommand, AddImageCommandResult>
    {
        public AddImageCommandHandler(
            DefaultDatabaseContext dbContext,
            IImageService<BingImage> imageService,
            IImageFileService imageFileService,
            ILocalFileService fileService,
            ILogger<AddImageCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
            this.imageFileService = imageFileService;
            this.fileService = fileService;
            this.logger = logger;
        }

        public string Name { get; } = nameof(AddImageCommandHandler);

        public async Task<AddImageCommandResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;
            string message = string.Empty;

            var bingImages = await imageService.Get(new Services.Models.BingImageServiceGetRequestModel
            {
                StartIndex = request.StartIndex,
                Take = request.Take,
            });

            if (bingImages == null)
            {
                message = "An exception occurred while collecting image information.";
                logger.LogInformation(message);
                throw new ApiException(System.Net.HttpStatusCode.ServiceUnavailable, message);
            }

            if (!String.IsNullOrEmpty(bingImages.Message))
            {
                message = $"Image information collection procedure message: {bingImages.Message}";
                logger.LogInformation(message);
                throw new ApiException(HttpStatusCode.ServiceUnavailable, message);
            }

            if (bingImages.Images.Count == 0)
            {
                message = "There are no collection results.";
                logger.LogInformation(message);
                throw new ApiException(HttpStatusCode.NotFound, message);
            }

            var result = new List<ImageInfo>();

            foreach (var image in bingImages.Images)
            {
                if (dbContext.Images.Any(x => x.Hash == image.Hsh))
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
                    ContentType = savedFile.ContentType,
                    FileSize = savedFile.Size,
                    CreatedAt = now,
                    Metadata = new ImageMetadata
                    {
                        Title = image.Title,
                        Origin = image.GetSourceTitle(),
                        Copyright = image.Copyright,
                        CopyrightLink = image.CopyrightLink,
                        Width = savedFile.Width,
                        Height = savedFile.Height,
                    }
                });

                try
                {
                    if (!imageFileService.HasThumbnail(savedFile.FilePath))
                    {
                        await imageFileService.GenerateThumbnailAsync(savedFile.FilePath);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Fail to generate thumbnail. {ex.Message}");
                }

                logger.LogInformation($"Collected: {image.Title}");
            }

            var affectedCount = 0;
            if (result.Count > 0)
            {
                dbContext.Images.AddRange(result.ToArray());

                affectedCount = await dbContext.SaveChangesAsync(cancellationToken);
            }

            return new AddImageCommandResult
            {
                CollectedCount = affectedCount,
            };
        }

        private readonly DefaultDatabaseContext dbContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService fileService;
        private readonly IImageFileService imageFileService;
        private readonly ILogger logger;
    }
}
