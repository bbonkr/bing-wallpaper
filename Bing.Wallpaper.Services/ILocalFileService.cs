using Bing.Wallpaper.Entities;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Services.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services
{
    public interface ILocalFileService
    {
        Task<LocalFileModel> SaveAsync(ImageModel image);

        Task<byte[]> ReadAsync(string filePath);
    }
}
