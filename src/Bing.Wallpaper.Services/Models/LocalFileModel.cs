using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Services.Models
{
    public class LocalFileModel
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string Directory { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
