using System.Threading.Tasks;
using Asp.Versioning;
using Bing.Wallpaper.Mediator.Images.Queries;
using Bing.Wallpaper.Mediator.Models;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.Core.Models;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Area(DefaultValues.AreaName)]
[Route(DefaultValues.RouteTemplate)]
[ApiExceptionHandlerFilter]
[Produces(DefaultValues.ContentTypeApplicationJson)]
public class ImagesController : ApiControllerBase
{
    public ImagesController(
        IMediator mediator,
        ILogger<ImagesController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    /// <summary>
    /// Get images
    /// </summary>
    /// <param name="page"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedModel<ImageItemModel>>> GetAllAsync(int page = 1, int take = 10)
    {
        var query = new ImagesQuery
        {
            Page = page,
            Limit = take,
            Keyword = string.Empty,
        };

        var records = await mediator.Send(query);

        return Ok(records);
    }

    private readonly IMediator mediator;
    private readonly ILogger logger;
}
