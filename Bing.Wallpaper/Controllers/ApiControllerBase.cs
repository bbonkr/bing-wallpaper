using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Bing.Wallpaper.Models;

namespace Bing.Wallpaper.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        public override ObjectResult StatusCode([ActionResultStatusCode] int statusCode, [ActionResultObjectValue] object value)
        {
            return base.StatusCode(statusCode, ApiResponseModelFactory.Create(statusCode, string.Empty, value));
        }

        protected ObjectResult StatusCode<T>(HttpStatusCode statusCode, string message, T value)
        {
            return base.StatusCode((int)statusCode, ApiResponseModelFactory.Create(statusCode, message, value));
        }

        protected ObjectResult StatusCode<T>(HttpStatusCode statusCode, T value)
        {
            return base.StatusCode((int)statusCode, ApiResponseModelFactory.Create(statusCode, string.Empty, value));
        }

        protected ObjectResult StatusCode(HttpStatusCode statusCode, string message)
        {
            return base.StatusCode((int)statusCode, ApiResponseModelFactory.Create<object>(statusCode, message, default(object)));
        }
    }
}
