using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore;
using System.Net;
using kr.bbon.AspNetCore.Models;
using kr.bbon.Core;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    [ApiExceptionHandlerFilter]
    public class BingImagesController : ApiControllerBase
    {
        public BingImagesController(
            DefaultDatabaseContext databaseContext,
            IImageService<BingImage> imageService,
            ILocalFileService localFileService,
            ILogger<BingImagesController> logger)
        {
            this.databaseContext = databaseContext;
            this.imageService = imageService;
            this.localFileService = localFileService;
            this.logger = logger;
        }

        [HttpGet]
        [Produces(typeof(ApiResponseModel<IEnumerable<ImageInfo>>))]
        public async Task<IActionResult> GetImagesAsync()
        {
            var now = DateTimeOffset.UtcNow;

            var bingImages = await imageService.Get();

            if (bingImages == null)
            {
                throw new HttpStatusException<object>(HttpStatusCode.InternalServerError, "Server error", default);
            }

            if (!String.IsNullOrEmpty(bingImages.Message))
            {
                throw new HttpStatusException<object>(HttpStatusCode.BadRequest, "Does not Have image information.", default);
            }

            if (bingImages.Images.Count == 0)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "Does not Have image information.", default);
            }

            var result = new List<ImageInfo>();

            foreach (var image in bingImages.Images)
            {
                if (databaseContext.Images.Any(x => x.Hash == image.Hsh))
                {
                    continue;
                }

                var savedFile = await localFileService.SaveAsync(image);

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

            if (result.Count == 0)
            {
                throw new HttpStatusException<object>(HttpStatusCode.NotFound, "Could not find today images.", default);
            }

            databaseContext.Images.AddRange(result.ToArray());

            await databaseContext.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK, result);
        }

        private readonly DefaultDatabaseContext databaseContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService localFileService;
        private readonly ILogger logger;
    }
}
