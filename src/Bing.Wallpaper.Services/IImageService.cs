using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services;

public interface IImageService<T> where T : ImageModel
{
    Task<ImagesModel<T>> Get(BingImageServiceGetRequestModel model);
}
