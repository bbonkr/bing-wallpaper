using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Models
{
    public abstract class PagedModelQueryBase
    {
        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 10;

        public string Keyword { get; set; } = "";
    }
}
