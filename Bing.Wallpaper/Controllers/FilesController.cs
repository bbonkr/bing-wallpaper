using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Bing.Wallpaper.Options;
using Bing.Wallpaper.Repositories;
using Bing.Wallpaper.Services;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Mvc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    [ApiExceptionHandlerFilter]
    public class FilesController : ApiControllerBase
    {
        public FilesController(
            IImageRepository repository,
            ILocalFileService fileService,
            IOptionsMonitor<CollectorOptions> appOptionsAccessor,
            IImageFileService imageFileService,
            ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.fileService = fileService;
            this.appOptions = appOptionsAccessor.CurrentValue;
            this.imageFileService = imageFileService;
            this.logger = loggerFactory.CreateLogger<ImagesController>();
        }

        [HttpGet("{id:Guid}")]
        [Produces(typeof(FileContentResult))]
        public async Task<IActionResult> GetFileByIdAsync(string id, [FromQuery] string type = "")
        {

            var record = await repository.FindByIdAsync(id);

            if (record == null)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "File record does not find.", default);
            }

            var fileInfo = new FileInfo(record.FilePath);

            if (!fileInfo.Exists)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "File does not exist.", default);
            }

            if (type?.ToLower() == "thumbnail")
            {
                try
                {
                    var thumbnailPath = appOptions.ThumbnailPath;
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
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            logger.LogInformation($"Download: {record.FileName}{record.FileExtension}");

            return File(buffer, record.ContentType, $"{record.FileName}{record.FileExtension}");
        }

        [HttpGet("{fileName}")]
        [Produces(typeof(FileContentResult))]
        public async Task<IActionResult> GetFileByFileNameAsync(string fileName, [FromQuery] string type = "")
        {

            var files = Directory.GetFiles(appOptions.DestinationPath, $"{fileName}*");

            if (files.Length == 0)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "File record does not find.", default);
            }

            var fileInfo = new FileInfo(files.FirstOrDefault());
            if (!fileInfo.Exists)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "File does not exist.", default);
            }

            if (type?.ToLower() == "thumbnail")
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
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            logger.LogInformation($"Download: {fileInfo.Name}");

            var contentTypeProvider = new FileExtensionContentTypeProvider();
            var contentType = "application/octet-stream";
            if (!contentTypeProvider.TryGetContentType(fileInfo.Name, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(buffer, contentType, fileInfo.Name);

        }


        private readonly IImageRepository repository;
        private readonly ILocalFileService fileService;
        private readonly IImageFileService imageFileService;
        private readonly CollectorOptions appOptions;
        private readonly ILogger logger;
    }
}
