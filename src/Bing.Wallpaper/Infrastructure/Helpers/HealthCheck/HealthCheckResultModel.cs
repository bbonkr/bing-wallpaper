using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bing.Wallpaper.Infrastructure.Helpers.HealthCheck;

public class HealthCheckResultModel
{
    private readonly HealthReport _healthReport;
    public HealthCheckResultModel(HealthReport healthReport)
    {
        _healthReport = healthReport;
    }

    public string Status
    {
        get => _healthReport.Status switch
        {
            HealthStatus.Healthy => "ok",
            _ => _healthReport.Status.ToString(),
        };
    }

    public double TotalDuration { get => _healthReport.TotalDuration.TotalMilliseconds; }
}
