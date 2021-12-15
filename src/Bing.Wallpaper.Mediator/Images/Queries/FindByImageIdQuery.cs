using AutoMapper;
using Bing.Wallpaper.Data;
using Bing.Wallpaper.Mediator.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

namespace Bing.Wallpaper.Mediator.Images.Queries;

public class FindByImageIdQuery : IRequest<FildImageResultModel>
{
    public string Id { get; set; }

    public string Type { get; set; }
}

public class FindByImageIdQueryHandler : IRequestHandler<FindByImageIdQuery, FildImageResultModel>
{
    public FindByImageIdQueryHandler(
        DefaultDatabaseContext dbContext,
        ILocalFileService fileService,
        IMapper mapper,
        IOptionsMonitor<CollectorOptions> appOptionsAccessor,
        IImageFileService imageFileService,
        ILogger<FindByImageIdQueryHandler> logger)
    {
        this.dbContext = dbContext;
        this.fileService = fileService;
        this.collectorOptions = appOptionsAccessor.CurrentValue ?? throw new ArgumentException(CollectorOptions.ExceptionMessage, nameof(appOptionsAccessor));
        this.mapper = mapper;
        this.imageFileService = imageFileService;
        this.logger = logger;
    }

    public async Task<FildImageResultModel> Handle(FindByImageIdQuery request, CancellationToken cancellationToken)
    {
        var message = string.Empty;

        var record = await dbContext.Images
            .Include(x => x.Metadata)
            .Where(x => x.Id == request.Id)
            .AsNoTracking()
            .Select(x => mapper.Map<ImageItemDetailModel>(x))
            .FirstOrDefaultAsync(cancellationToken);

        if (record == null)
        {
            message = "File record does not find.";
            throw new ApiException(HttpStatusCode.NotFound, message);
        }

        var fileInfo = new FileInfo(record.FilePath);

        if (!fileInfo.Exists)
        {
            message = "File does not exist.";
            throw new ApiException(HttpStatusCode.NotFound, message);
        }

        if (ImageTypes.Thumbnail.Equals(request.Type?.ToLower() ?? string.Empty))
        {
            try
            {
                var thumbnailPath = collectorOptions.ThumbnailPath;

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
                fileInfo = new FileInfo(record.FilePath);
            }
        }

        var buffer = await fileService.ReadAsync(fileInfo.FullName);

        if (buffer == null)
        {
            message = "File does not exist.";
            throw new ApiException(HttpStatusCode.NotFound, message);
        }

        return new FildImageResultModel
        {
            Buffer = buffer,
            ContentType = record.ContentType,
            FileName = $"{record.FileName}{record.FileExtension}",
        };
    }

    private readonly DefaultDatabaseContext dbContext;
    private readonly ILocalFileService fileService;
    private readonly CollectorOptions collectorOptions;
    private readonly IImageFileService imageFileService;
    private readonly IMapper mapper;
    private readonly ILogger logger;
}
