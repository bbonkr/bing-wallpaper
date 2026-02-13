using System.Threading.Tasks;
using Asp.Versioning;
using Bing.Wallpaper.Mediator.Images.Queries;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Mvc;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ResponseCache(CacheProfileName = "File-Response-Cache")]
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

        logger.LogInformation("Download: {Filename}", result.FileName);

        return File(result.Buffer, result.ContentType, result.FileName);
    }

    [HttpGet("{fileName}")]
    [Produces(typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ResponseCache(CacheProfileName = "File-Response-Cache")]
    public async Task<IActionResult> GetFileByFileNameAsync(
        [FromRoute] string fileName,
        [FromQuery] string type = "")
    {
        var query = new FindByImageFileNameQuery { FileName = fileName, Type = type, };

        var result = await mediator.Send(query);

        logger.LogInformation("Download: {Filename}", result.FileName);

        var contentTypeProvider = new FileExtensionContentTypeProvider();

        if (!contentTypeProvider.TryGetContentType(result.FileName, out string contentType))
        {
            contentType = "application/octet-stream";
        }

        return File(result.Buffer, contentType, result.FileName);
    }


    private readonly IMediator mediator;
    private readonly ILogger logger;
}
