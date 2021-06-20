using System;

namespace Bing.Wallpaper.Entities
{
    public class ImageInfo
    {
        public string Id { get; set; }

        public string Hash { get; set; }

        public string Url { get; set; }

        public string BaseUrl { get; set; }

        public string FilePath { get; set; }

        public string Directory { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public long FileSize { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// 추가 데이터
        /// </summary>
        public virtual ImageMetadata Metadata { get; set; }
    }
}
