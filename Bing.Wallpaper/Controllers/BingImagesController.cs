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
        public async Task<IActionResult> GetImagesAsync()
        {
            var now = DateTimeOffset.UtcNow;
            try
            {
                var bingImages = await imageService.Get();

                if (bingImages == null)
                {
                    //logger.LogInformation("이미지 정보를 수집 중 예외가 발생했습니다.");
                    //return StatusCode(500, ErrorModel.GetErrorModel(500, "Server error"));
                    throw new HttpStatusException<object>(HttpStatusCode.InternalServerError, "Server error", default);
                }

                if (!String.IsNullOrEmpty(bingImages.Message))
                {
                    //logger.LogInformation("이미지 정보 수집 절차 메시지: {message}", bingImages.Message);
                    //return StatusCode(400, ErrorModel.GetErrorModel(400, bingImages.Message));
                    throw new HttpStatusException<object>(HttpStatusCode.BadRequest, "Does not Have image information.", default);
                }

                if (bingImages.Images.Count == 0)
                {
                    //logger.LogInformation("이미지 정보를 수집 결과가 없습니다.");
                    //return StatusCode(404, ErrorModel.GetErrorModel(404, "Does not Have image information."));

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
                    //return StatusCode(404, ErrorModel.GetErrorModel(404, "Could not find today images."));
                    throw new HttpStatusException<object>(HttpStatusCode.NotFound, "Could not find today images.", default);
                }

                databaseContext.Images.AddRange(result.ToArray());

                await databaseContext.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //return StatusCode(500, ErrorModel.GetErrorModel(500, ex.Message));
                throw new HttpStatusException<object>(HttpStatusCode.InternalServerError, ex.Message, default);
            }
        }

        private readonly DefaultDatabaseContext databaseContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService localFileService;
        private readonly ILogger logger;
    }
}
