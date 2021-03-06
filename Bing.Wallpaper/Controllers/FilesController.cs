using System;
using System.Net;
using System.Threading.Tasks;

using Bing.Wallpaper.Repositories;
using Bing.Wallpaper.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    public class FilesController : ApiControllerBase
    {
        public FilesController(IImageRepository repository, ILocalFileService fileService, ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.fileService = fileService;
            this.logger = loggerFactory.CreateLogger<ImagesController>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync(string id)
        {
            try
            {
                var record = await repository.FindByIdAsync(id);

                if (record == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var buffer = await fileService.ReadAsync(record.FilePath);

                if(buffer == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                logger.LogInformation($"Download: {record.FileName}{record.FileExtension}");

                return File(buffer, record.ContentType, $"{record.FileName}{record.FileExtension}");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private readonly IImageRepository repository;
        private readonly ILocalFileService fileService;
        private readonly ILogger logger;
    }
}
