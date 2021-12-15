using System.Threading.Tasks;

using Bing.Wallpaper.Mediator.Images.Queries;
using Bing.Wallpaper.Mediator.Models;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Models;
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
public class ImagesController : ApiControllerBase
{
    public ImagesController(
        IMediator mediator,
        ILogger<ImagesController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<IPagedModel<ImageItemModel>>))]
    public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10)
    {
        var query = new ImagesQuery
        {
            Page = page,
            Limit = take,
            Keyword = string.Empty,
        };

        var records = await mediator.Send(query);

        return StatusCode(StatusCodes.Status200OK, records);
    }

    private readonly IMediator mediator;
    private readonly ILogger logger;
}
