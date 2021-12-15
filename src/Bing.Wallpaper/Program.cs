using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Options;
using Bing.Wallpaper;
using kr.bbon.AspNetCore.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bing.Wallpaper.Jobs;
using Bing.Wallpaper.Mediator.DependencyInjection;
using Bing.Wallpaper.Jobs.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Configuration.AddEnvironmentVariables();
builder.Services.ConfigureAppOptions(builder.Configuration);

builder.Services.Configure<MvcOptions>(options => {
    options.CacheProfiles.Add("File-Response-Cache", new CacheProfile
    {
        Duration = (int)TimeSpan.FromDays(365).TotalSeconds,
    });
});

// Register Configuration
// https://github.com/NLog/NLog/wiki/ConfigSetting-Layout-Renderer
//NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = Configuration;
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<DefaultDatabaseContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly("Bing.Wallpaper.Data.SqlServer");
});
});

builder.Services.AddDomainService(builder.Configuration);

builder.Services.AddBingImageCollectingJob(builder.Configuration);

builder.Services.AddControllersWithViews();

var defaultVersion = new ApiVersion(1, 0);

builder.Services.AddApiVersioningAndSwaggerGen(defaultVersion);

builder.Host.UseNLog();

// Configure
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetService<DefaultDatabaseContext>();
    databaseContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUIWithApiVersioning();

    using (var scope = app.Services.CreateScope())
    {
        var collectorOptionsAccessor = scope.ServiceProvider.GetService<IOptionsMonitor<CollectorOptions>>();
        var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Startup");
        var options = collectorOptionsAccessor.CurrentValue.ToJson();
        logger.LogDebug($"[Options: Collector] values: {Environment.NewLine}{options}");
    }
}

// Use proxy
// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllers();

    endpoints.MapDefaultControllerRoute();
    endpoints.MapFallbackToController("Index", "Home");
});

// Start
var loggerInstance = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    app.Run();
}
catch (Exception ex)
{

    loggerInstance.Error(ex, "Stopped webapp because of exception");
}
finally
{
    NLog.LogManager.Flush();
    NLog.LogManager.Shutdown();
}