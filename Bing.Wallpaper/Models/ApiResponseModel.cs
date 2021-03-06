using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Models
{
    public abstract class ApiResponseModelBase
    {
        public int StatusCode { get; init; }

        public string Message { get; init; }
    }

    public class ApiResponseModel : ApiResponseModelBase
    {

    }

    public class ApiResponseModel<T> : ApiResponseModelBase
    {
        public T Data { get; init; }
    }

    public class ApiResponseModelFactory
    {
        public static ApiResponseModel<T> Create<T>(HttpStatusCode statusCode, string message = "")
        {
            return Create((int)statusCode, message, default(T));
        }

        public static ApiResponseModel<T> Create<T>(HttpStatusCode statusCode, T data = default(T))
        {
            return Create((int)statusCode, string.Empty, data);
        }

        public static ApiResponseModel<T> Create<T>(HttpStatusCode statusCode, string message = "", T data = default(T))
        {
            return Create((int)statusCode, message, data);
        }

        public static ApiResponseModel<T> Create<T>(int statusCode, string message = "", T data = default(T))
        {
            return new ApiResponseModel<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
            };
        }
    }
}
