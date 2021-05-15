using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Bing.Wallpaper.Repositories;

using kr.bbon.AspNetCore.Filters;
using kr.bbon.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    [ApiExceptionHandlerFilter]
    public class ImagesController : ApiControllerBase
    {
        public ImagesController(IImageRepository repository, ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.logger = loggerFactory.CreateLogger<ImagesController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10)
        {
            var records = await repository.GetAllAsync(page, take);

            records = records.Select(x =>
            {
                var item = x;

                var tokens = x.FileName.Split(".");
                item.FileName = string.Join(".", tokens.Take(tokens.Length - 1));
                item.FileExtension = $".{tokens.Last()}";

                return item;
            });

            return StatusCode((int)HttpStatusCode.OK, records);
        }

        private readonly IImageRepository repository;
        private readonly ILogger logger;
    }
}
