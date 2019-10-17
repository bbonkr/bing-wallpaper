using Bing.Wallpaper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services
{
    public interface IImageService<T> where T : ImageModel
    {
        Task<ImagesModel<T>> Get();
    }
}
