using System;
using System.Collections.Generic;
using System.Linq;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Options;
using kr.bbon.AspNetCore.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bing.Wallpaper.Mediator.DependencyInjection;
using Bing.Wallpaper.Jobs.DependencyInjection;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using Serilog;
using Serilog.Events;
using Bing.Wallpaper.Extensions.DependencyInjection;
using Bing.Wallpaper.Infrastructure.Filters;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using kr.bbon.Core.Models;

var mssqlSinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Log",
    SchemaName = "dbo",
};

var columnOptions = new ColumnOptions
{
    AdditionalColumns = new List<SqlColumn>
    {
        // https://github.com/serilog/serilog-sinks-mssqlserver#custom-property-columns
        new SqlColumn { ColumnName = "Payload", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = true,},
        new SqlColumn { ColumnName = "RequestUri", DataType = SqlDbType.NVarChar, DataLength =-1, AllowNull = true},
        new SqlColumn { ColumnName = "Errors", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = true },

        new SqlColumn { ColumnName = "UserIp", DataType = SqlDbType.NVarChar, DataLength = 128, AllowNull = true},
        new SqlColumn { ColumnName = "QueryString", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = true},
        new SqlColumn { ColumnName = "UserAgent", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = true },

        new SqlColumn { ColumnName = "IsResolved", DataType = SqlDbType.Bit, AllowNull = true },
        new SqlColumn { ColumnName = "ResolvedAt", DataType = SqlDbType.DateTime, AllowNull = true },
    },
};
columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.Store.Add(StandardColumn.LogEvent);

var assemblies = new List<Assembly> {
    typeof(Bing.Wallpaper.Mediator.PlaceHolder).Assembly,
};

var builder = WebApplication.CreateBuilder(args);

//var dotnetRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
//var isDotnetRunningInContainer = new string[] { "true", "1" }.Contains((dotnetRunningInContainer ?? string.Empty).ToLower());
//if (isDotnetRunningInContainer)
//{
//    // I don't want to use inotify when runs in container
//    builder.Host.ConfigureAppConfiguration((_, configuration) =>
//    {
//        foreach (var source in configuration.Sources)
//        {
//            if (source is FileConfigurationSource fileConfigurationSource)
//            {
//                fileConfigurationSource.ReloadOnChange = false;
//            }
//        }
//    });
//}

// Logger
builder.Host.UseSerilog(
    configureLogger: (context, services, configuration) => configuration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.MSSqlServer(
            connectionString: context.Configuration.GetConnectionString("Default"),
            sinkOptions: mssqlSinkOptions,
            columnOptions: columnOptions),
    writeToProviders: true);


// Configure services
builder.Configuration.AddEnvironmentVariables();
builder.Services.ConfigureAppOptions();

builder.Services.AddOptions<JsonSerializerOptions>()
    .Configure(options =>
    {
        options.AllowTrailingCommas = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.Configure<MvcOptions>(options =>
{
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

builder.Services.AddDomainService();

builder.Services.AddBingImageCollectingJob(builder.Configuration);

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<ApiExceptionHandlerWithLoggingFilter>();
    })
    .ConfigureDefaultJsonOptions()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.InvalidModelStateResponseFactory = context =>
         {
             PathString path = context.HttpContext.Request.Path;
             string method = context.HttpContext.Request.Method;
             string displayName = context.ActionDescriptor.DisplayName ?? string.Empty;

             var errors = context.ModelState.Values
                 .SelectMany(x => x.Errors)
                 .Select(error => JsonSerializer.Deserialize<ErrorModel>(error.ErrorMessage));

             var responseStatusCode = StatusCodes.Status400BadRequest;
             var responseModel = kr.bbon.AspNetCore.Models.ApiResponseModelFactory.Create(responseStatusCode, "Payload is invalid", errors);

             responseModel.Path = path.ToString();
             responseModel.Method = method;
             responseModel.Instance = displayName;

             context.HttpContext.Response.StatusCode = responseStatusCode;

             return new ObjectResult(responseModel);
         };
     });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();

builder.Services.AddForwardedHeaders();
builder.Services.AddValidatorIntercepter();

var defaultVersion = new ApiVersion(1, 0);

builder.Services.AddApiVersioningAndSwaggerGen(defaultVersion);


// Configure
var app = builder.Build();

app.UseDatabaseMigration<DefaultDatabaseContext>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUIWithApiVersioning();
    app.UseCors(config =>
    {
        config.AllowAnyHeader();
        config.AllowAnyMethod();
        config.AllowAnyOrigin();
    });

    using (var scope = app.Services.CreateScope())
    {
        var collectorOptionsAccessor = scope.ServiceProvider.GetService<IOptionsMonitor<CollectorOptions>>();
        var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Startup");
        var options = collectorOptionsAccessor.CurrentValue.ToJson();
        logger.LogDebug($"[Options: Collector] values: {Environment.NewLine}{options}");
    }
}

// Logging
app.UseRequestLogging();

// Use proxy
// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapFallbackToController("Index", "Home");
});


// Run web application

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // Two-stage initialization https://github.com/serilog/serilog-aspnetcore#two-stage-initialization

Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine("[Serilog DEBUG] {1:yyyy-MM-dd HH:mm:ss} {0}", msg, DateTime.UtcNow));

try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}