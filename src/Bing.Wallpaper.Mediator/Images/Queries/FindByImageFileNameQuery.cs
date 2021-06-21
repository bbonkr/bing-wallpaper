using AutoMapper;
using Bing.Wallpaper.Data;
using Bing.Wallpaper.Mediator.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;
using kr.bbon.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Images.Queries
{
    public class FindByImageFileNameQuery :IRequest<FildImageResultModel>
    {
        public string FileName { get; set; }

        public string Type { get; set; }
    }

    public class FindByImageFileNameQueryHandler : IRequestHandler<FindByImageFileNameQuery, FildImageResultModel>
    {
        public FindByImageFileNameQueryHandler(
            DefaultDatabaseContext dbContext,
            ILocalFileService fileService,
            IMapper mapper,
            IOptionsMonitor<CollectorOptions> appOptionsAccessor,
            IImageFileService imageFileService,
            ILogger<FindByImageFileNameQueryHandler> logger)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
            this.collectorOptions = appOptionsAccessor.CurrentValue ?? throw new ArgumentException(CollectorOptions.ExceptionMessage, nameof(appOptionsAccessor));
            this.mapper = mapper;
            this.imageFileService = imageFileService;
            this.logger = logger;
        }
        public async Task<FildImageResultModel> Handle(FindByImageFileNameQuery request, CancellationToken cancellationToken)
        {
            var files = Directory.GetFiles(collectorOptions.DestinationPath, $"{request.FileName}*");

            if (files.Length == 0)
            {
                throw new DefaultHttpStatusException(HttpStatusCode.NotFound, "File record does not find.");
            }

            var fileInfo = new FileInfo(files.FirstOrDefault());
            if (!fileInfo.Exists)
            {
                throw new DefaultHttpStatusException(HttpStatusCode.NotFound, "File does not exist.");
            }

            if (ImageTypes.Thumbnail.Equals(request.Type?.ToLower() ?? string.Empty))
            {
                try
                {
                    string thumbnailFilePath;
                    if (!imageFileService.HasThumbnail(fileInfo.FullName))
                    {
                        thumbnailFilePath = await imageFileService.GenerateThumbnailAsync(fileInfo.FullName);
                        fileInfo = new FileInfo(thumbnailFilePath);
                    }
                    else
                    {
                        thumbnailFilePath = imageFileService.GetThumbnailFilePath(fileInfo.FullName);
                    }

                    fileInfo = new FileInfo(thumbnailFilePath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    fileInfo = new FileInfo(files.FirstOrDefault());
                }
            }


            var buffer = await fileService.ReadAsync(fileInfo.FullName);

            if (buffer == null)
            {
                throw new DefaultHttpStatusException(HttpStatusCode.NotFound, "File does not exist");
            }

            logger.LogInformation($"Download: {fileInfo.Name}");

            return new FildImageResultModel
            {
                Buffer = buffer,
                FileName = fileInfo.Name,
            };
        }

        private readonly DefaultDatabaseContext dbContext;
        private readonly ILocalFileService fileService;
        private readonly CollectorOptions collectorOptions;
        private readonly IImageFileService imageFileService;
        private readonly IMapper mapper;
        private readonly ILogger logger;
    }
}
