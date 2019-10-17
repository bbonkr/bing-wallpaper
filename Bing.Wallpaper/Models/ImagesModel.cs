using System.Collections.Generic;

namespace Bing.Wallpaper.Models
{
    public class ImagesModel<T> where T : ImageModel
    {
        public IList<T> Images { get; set; } = new List<T>();

        public string Message { get; set; }
    }
}
