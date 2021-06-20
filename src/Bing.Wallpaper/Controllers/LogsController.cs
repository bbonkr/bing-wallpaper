﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Bing.Wallpaper.Repositories;

using kr.bbon.AspNetCore.Mvc;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using kr.bbon.AspNetCore.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using Bing.Wallpaper.Models;
using MediatR;
using Bing.Wallpaper.Mediator.Logs.Queries;
using Microsoft.AspNetCore.Http;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area(DefaultValues.AreaName)]
    [Route(DefaultValues.RouteTemplate)]
    [ApiExceptionHandlerFilter]
    public class LogsController : ApiControllerBase
    {
        public LogsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<IPagedModel<LogModel>>))]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10, string level = "", string keyword = "")
        {
            var query = new LogsQuery
            {
                Page = page,
                Limit = take,
                Level = level,
                Keyword = keyword,
            };

            var result = await mediator.Send(query);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        
        private readonly IMediator mediator;
    }
}
