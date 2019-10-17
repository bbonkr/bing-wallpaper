namespace Bing.Wallpaper.Models
{
    public class ErrorModel
    {
        public int code { get; set; }

        public string Message { get; set; }

        public static ErrorModel GetErrorModel(int code, string message)
        {
            return new ErrorModel
            {
                code = code,
                Message = message
            };
        }
    }
}
