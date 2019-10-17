using Bing.Wallpaper.Data;
using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BingImagesController : ControllerBase
    {
        public BingImagesController(
            DefaultDatabaseContext databaseContext,
            IImageService<BingImage> imageService,
            ILocalFileService localFileService)
        {
            this.databaseContext = databaseContext;
            this.imageService = imageService;
            this.localFileService = localFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var bingImages = await imageService.Get();

            if (bingImages == null)
            {
                return StatusCode(500, ErrorModel.GetErrorModel(500, "Server error"));
            }

            if (!String.IsNullOrEmpty(bingImages.Message))
            {
                return StatusCode(400, ErrorModel.GetErrorModel(400, bingImages.Message));
            }

            if (bingImages.Images.Count == 0)
            {
                return StatusCode(404, ErrorModel.GetErrorModel(404, "Does not Have image information."));
            }

            try
            {
                var result = new List<ImageInfo>();

                foreach (var image in bingImages.Images)
                {
                    if (databaseContext.Images.Any(x => x.Hash == image.Hsh))
                    {
                        continue;
                    }

                    var imageInfo = await localFileService.Save(image);
                    result.Add(imageInfo);
                }

                databaseContext.Images.AddRange(result.ToArray());

                await databaseContext.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorModel.GetErrorModel(500, ex.Message));
            }
        }

        private readonly DefaultDatabaseContext databaseContext;
        private readonly IImageService<BingImage> imageService;
        private readonly ILocalFileService localFileService;
    }
}
