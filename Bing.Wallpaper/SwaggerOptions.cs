
using kr.bbon.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using kr.bbon.AspNetCore.Options;

namespace Bing.Wallpaper
{
    public class SwaggerOptions : ConfigureSwaggerOptions
    {
        public SwaggerOptions(IApiVersionDescriptionProvider provider, IOptionsMonitor<AppOptions> appOptionsAccessor)
            : base(provider, appOptionsAccessor)
        {
            options = appOptionsAccessor.CurrentValue;
        }

        public override string AppTitle => options.Title;

        private readonly AppOptions options;
    }
}
