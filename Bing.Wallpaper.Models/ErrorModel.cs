using System.Net;

namespace Bing.Wallpaper.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public object Details { get; set; }

        public ErrorModel InnerError { get; set; }

        public static ErrorModel GetErrorModel(int code, string message, object details = default, ErrorModel innerError = default)
        {
            return new ErrorModel
            {
                StatusCode = code,
                Message = message,
                Details = details,
                InnerError = innerError,
            };
        }

        public static ErrorModel GetErrorModel(HttpStatusCode code, string message, object details = default, ErrorModel innerError = default)
        {
            return new ErrorModel
            {
                StatusCode = (int)code,
                Message = message,
                Details = details,
                InnerError = innerError,
            };
        }
    }
}
