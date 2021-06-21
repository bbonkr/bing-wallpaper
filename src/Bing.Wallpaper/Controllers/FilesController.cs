using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bing.Wallpaper.Mediator.Images.Queries;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area(DefaultValues.AreaName)]
    [Route(DefaultValues.RouteTemplate)]
    [ApiExceptionHandlerFilter]
    public class FilesController : ApiControllerBase
    {
        public FilesController(
            IMediator mediator,
            ILogger<FilesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet("{id:Guid}")]
        [Produces(typeof(FileContentResult))]
        public async Task<IActionResult> GetFileByIdAsync(
            [FromRoute] string id,
            [FromQuery] string type = "")
        {
            var query = new FindByImageIdQuery
            {
                Id = id,
                Type = type,
            };

            var result = await mediator.Send(query);

            logger.LogInformation($"Download: {result.FileName}");

            return File(result.Buffer, result.ContentType, result.FileName);
        }

        [HttpGet("{fileName}")]
        [Produces(typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        public async Task<IActionResult> GetFileByFileNameAsync(
            [FromRoute] string fileName, 
            [FromQuery] string type = "")
        {
            var query = new FindByImageFileNameQuery { FileName = fileName, Type = type, };

            var result = await mediator.Send(query);

            var contentTypeProvider = new FileExtensionContentTypeProvider();
            var contentType = "application/octet-stream";
            if (!contentTypeProvider.TryGetContentType(result.FileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(result.Buffer, contentType, result.FileName);
        }


        private readonly IMediator mediator;
        private readonly ILogger logger;
    }
}
