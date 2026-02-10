using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Wallpaper.Options;
using ImageMagick;
using Microsoft.Extensions.Options;


namespace Bing.Wallpaper.Services;

public interface IImageFileService
{
    Task<string> GenerateThumbnailAsync(string imageFilePath, string thumbnailDirectory);

    Task<string> GenerateThumbnailAsync(string imageFilePath);

    string GetThumbnailFilePath(string imageFilePath, string thumbnailDirectory);
    string GetThumbnailFilePath(string imageFilePath);

    bool HasThumbnail(string imageFilePath);

    (int width, int height) GetImageResolution(string imageFilePath);
}

public class ImageFileService : IImageFileService
{
    public ImageFileService(IOptionsMonitor<CollectorOptions> collectorOptionsMonitor)
    {
        collectorOptions = collectorOptionsMonitor.CurrentValue ?? throw new ArgumentException(CollectorOptions.ExceptionMessage, nameof(collectorOptionsMonitor));

        //MagickNET.Initialize();
    }

    public Task<string> GenerateThumbnailAsync(string imageFilePath, string thumbnailDirectory)
    {
        PreprocessThumbnailDirectory(thumbnailDirectory);

        var thumbnailFilePath = GetThumbnailFilePath(imageFilePath, thumbnailDirectory);

        if (!File.Exists(thumbnailFilePath))
        {
            //MagickNET.SetNativeLibraryDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            using (var image = new MagickImage(imageFilePath))
            {
                //image.Resize(120, 0);
                image.Thumbnail(64, 0);

                image.Write(thumbnailFilePath);
            }
        }

        return Task.FromResult(thumbnailFilePath);
    }

    public (int width, int height) GetImageResolution(string imageFilePath)
    {
        var w = 0;
        var h = 0;

        if (File.Exists(imageFilePath))
        {
            using (var image = new MagickImage(imageFilePath))
            {
                w = (int)image.Width;
                h = (int)image.Height;
            }
        }

        return (width: w, height: h);
    }

    public async Task<string> GenerateThumbnailAsync(string imageFilePath)
    {
        return await GenerateThumbnailAsync(imageFilePath, collectorOptions.ThumbnailPath);
    }

    public bool HasThumbnail(string imageFilePath, string thumbnailDirectory)
    {
        var thumbnailFilePath = GetThumbnailFilePath(imageFilePath, thumbnailDirectory);

        return File.Exists(thumbnailFilePath);
    }

    public bool HasThumbnail(string imageFilePath)
    {
        return HasThumbnail(imageFilePath, collectorOptions.ThumbnailPath);
    }

    public string GetThumbnailFilePath(string imageFilePath, string thumbnailDirectory)
    {
        PreprocessThumbnailDirectory(thumbnailDirectory);

        var (name, extension) = GetFileNamePart(imageFilePath);

        var thumbnailFilePath = Path.Combine(thumbnailDirectory, $"{name}{extension}");

        return thumbnailFilePath;
    }

    public string GetThumbnailFilePath(string imageFilePath)
    {
        return GetThumbnailFilePath(imageFilePath, collectorOptions.ThumbnailPath);
    }

    public (string Name, string Extension) GetFileNamePart(string imageFilePath)
    {
        var file = new FileInfo(imageFilePath);

        if (!file.Exists)
        {
            throw new FileNotFoundException($"Could not find a file. ({imageFilePath})");
        }

        var tokens = file.Name.Split('.');
        string name = string.Empty;
        string extension = string.Empty;
        if (tokens.Length > 0)
        {
            name = string.Join(".", tokens.Take(tokens.Length - 1));
            if (tokens.Length > 1)
            {
                extension = $".{tokens.Last()}";
            }
        }
        else
        {
            name = file.Name;
        }

        return (Name: name, Extension: extension);
    }

    private void PreprocessThumbnailDirectory(string thumbnailDirectory)
    {
        if (!Directory.Exists(thumbnailDirectory))
        {
            Directory.CreateDirectory(thumbnailDirectory);
        }
    }


    private readonly CollectorOptions collectorOptions;
}
