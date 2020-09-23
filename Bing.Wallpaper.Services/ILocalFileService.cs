using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services
{
    public interface ILocalFileService
    {
        Task<ImageInfo> Save(ImageModel image);        
    }
}
