namespace Bing.Wallpaper.Models
{
    public abstract class ImageModel
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Hsh { get; set; }

        public abstract string GetBaseUrl();

        public abstract string GetFileName();

        public abstract string GetSourceTitle();
    }
}
