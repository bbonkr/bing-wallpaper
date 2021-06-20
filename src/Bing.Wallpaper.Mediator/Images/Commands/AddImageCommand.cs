using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Images.Commands
{
    public class AddImageCommand : IRequest<AddImageCommandResult>
    {
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
            ILocalFileService fileService,
            ILogger<AddImageCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
            this.fileService = fileService;
            this.logger = logger;
        }

        public string Name { get; } = nameof(AddImageCommandHandler);

        public async Task<AddImageCommandResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;
            string message = string.Empty;

            var bingImages = await imageService.Get();

            if (bingImages == null)
            {
                message = "An exception occurred while collecting image information.";
                logger.LogInformation(message);
                throw new Exception(message);
            }

            if (!String.IsNullOrEmpty(bingImages.Message))
            {
                message = $"Image information collection procedure message: {bingImages.Message}";
                logger.LogInformation(message);
                throw new Exception(bingImages.Message);
            }

            if (bingImages.Images.Count == 0)
            {
                message = "There are no collection results.";
                logger.LogInformation(message);
                throw new Exception(message);
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
                    }
                });
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
        private readonly ILogger logger;
    }
}
