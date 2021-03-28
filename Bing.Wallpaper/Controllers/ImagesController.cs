using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Bing.Wallpaper.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
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
            try
            {
                var records = await repository.GetAllAsync(page, take);

                logger.LogInformation($"{nameof(ImagesController)}.{nameof(GetAllAsync)} posts.count={records.Count():n0}");

                records = records.Select(x => {
                    var item = x;

                    var tokens=x.FileName.Split(".");
                    item.FileName = string.Join(".", tokens.Take(tokens.Length - 1));
                    item.FileExtension = $".{tokens.Last()}";

                    return item;
                });
                return StatusCode((int)HttpStatusCode.OK, records);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private readonly IImageRepository repository;
        private readonly ILogger logger;
    }
}
