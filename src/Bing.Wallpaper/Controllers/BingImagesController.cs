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
using MediatR;
using Bing.Wallpaper.Mediator.Images.Commands;
using Microsoft.AspNetCore.Http;
using Bing.Wallpaper.Services.Models;

namespace Bing.Wallpaper.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel))]
        public async Task<IActionResult> CollectImagesAsync(BingImageServiceGetRequestModel model)
        {

            var command = new AddImageCommand(model.StartIndex, model.Take);

            var result = await mediator.Send(command);

            return StatusCode(HttpStatusCode.OK, $"{result.CollectedCount:n0} images collected.");
        }

        private readonly IMediator mediator;
    }
}
