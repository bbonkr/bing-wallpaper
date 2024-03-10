
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using Bing.Wallpaper.Mediator.Images.Commands;
using Bing.Wallpaper.Services.Models;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Models;
using kr.bbon.AspNetCore.Mvc;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Wallpaper.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Area(DefaultValues.AreaName)]
[Route(DefaultValues.RouteTemplate)]
[ApiExceptionHandlerFilter]
public class BingImagesController : ApiControllerBase
{
    public BingImagesController(
          IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<object>))]
    public async Task<IActionResult> CollectImagesAsync(BingImageServiceGetRequestModel model)
    {

        var command = new AddImageCommand(model.StartIndex, model.Take);

        var result = await mediator.Send(command);

        return StatusCode(HttpStatusCode.OK, $"{result.CollectedCount:n0} images collected.");
    }

    private readonly IMediator mediator;
}
