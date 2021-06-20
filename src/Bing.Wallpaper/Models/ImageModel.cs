using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Models
{
    public class ImageItemModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public long CreatedAt { get; set; }

        public string Copyright { get; set; }

        public string CopyrightLink { get; set; }
    }

    public class ImageItemDetailModel : ImageItemModel
    {
        public string FilePath { get; set; }

        public string ContentType { get; set; }
    }
}
