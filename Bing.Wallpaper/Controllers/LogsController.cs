using System;
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

namespace Bing.Wallpaper.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Area("api")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    [ApiExceptionHandlerFilter]
    public class LogsController : ApiControllerBase
    {
        public LogsController(IAppLogRepository repository, ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.logger = loggerFactory.CreateLogger<ImagesController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10, string level = "", string keyword = "")
        {
            var records = await repository.GetAllAsync(page, take, level, keyword);

            return StatusCode((int)HttpStatusCode.OK, records);
        }

        private readonly IAppLogRepository repository;
        private readonly ILogger logger;
    }
}
