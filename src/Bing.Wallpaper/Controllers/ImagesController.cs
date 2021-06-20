using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bing.Wallpaper.Mediator.Images.Queries;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Repositories;
using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Models;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area(DefaultValues.AreaName)]
    [Route(DefaultValues.RouteTemplate)]
    [ApiExceptionHandlerFilter]
    public class ImagesController : ApiControllerBase
    {
        public ImagesController(
            //IImageRepository repository, 
            //ILoggerFactory loggerFactory
            IMediator mediator, 
            ILogger<ImagesController> logger)
        {
            //this.repository = repository;
            //this.logger = loggerFactory.CreateLogger<ImagesController>();

            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<IPagedModel<ImageItemModel>>))]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10)
        {
            //var records = await repository.GetAllAsync(page, take);

            //foreach (var item in records.Items)
            //{
            //    var tokens = item.FileName.Split(".");
            //    item.FileName = string.Join(".", tokens.Take(tokens.Length - 1));
            //    item.FileExtension = $".{tokens.Last()}";
            //}

            var query = new ImagesQuery
            {
                Page = page,
                Limit = take,
                Keyword = string.Empty,
            };

            var records = await mediator.Send(query);

            return StatusCode((int)HttpStatusCode.OK, records);
        }

        //private readonly IImageRepository repository;
        private readonly IMediator mediator;
        private readonly ILogger logger;
    }
}
