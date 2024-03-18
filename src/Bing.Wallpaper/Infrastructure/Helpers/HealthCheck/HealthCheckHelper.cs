using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bing.Wallpaper.Infrastructure.Helpers.HealthCheck;

public class HealthCheckHelper
{
    public const string HEALTH_CHECK_ROUTE = "/healthz";

    public static Task ResponseWriter(HttpContext httpContext, HealthReport healthReport)
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        httpContext.Response.ContentType = "application/json; charset=utf-8";

        HealthCheckResultModel result = new(healthReport);

        return httpContext.Response.WriteAsJsonAsync(result, jsonSerializerOptions, CancellationToken.None);
    }
}
