using Bing.Wallpaper.Options;
using Bing.Wallpaper.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Bing.Wallpaper.Services;
using Bing.Wallpaper.Models;

namespace Bing.Wallpaper.Service.App
{
    class Program
    {
        private const string PREFIX = "DOTNETCORE_";
        private const string APP_SETTINGS_FILENAME = "appsettings";
        private const string HOST_SETTINGS_FILENAME = "hostsettings";
        private const string SETTINGS_FILE_EXT = ".json";

        static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) => new HostBuilder()
               .ConfigureHostConfiguration(config =>
               {
                   config.SetBasePath(Directory.GetCurrentDirectory());
                   config.AddJsonFile($"{HOST_SETTINGS_FILENAME}{SETTINGS_FILE_EXT}", optional: true);
                   config.AddEnvironmentVariables(prefix: PREFIX);
                   config.AddCommandLine(args);
               })
               .ConfigureAppConfiguration((context, config) =>
               {
                   // Application Configuration
                   config.SetBasePath(Directory.GetCurrentDirectory());
                   config.AddJsonFile($"{APP_SETTINGS_FILENAME}{SETTINGS_FILE_EXT}", optional: true);
                   config.AddJsonFile($"{APP_SETTINGS_FILENAME}.{context.HostingEnvironment.EnvironmentName}{SETTINGS_FILE_EXT}", optional: true);
                   config.AddEnvironmentVariables();
                   config.AddCommandLine(args);
               })
               //.ConfigureContainer(config => {
               //    // Configure the instantiated dependency injection container

               //})
               .ConfigureServices((context, services) =>
               {
                   //Adds services to the Dependency Injection container

                   var envVars = Environment.GetEnvironmentVariables();

                   services.AddLogging();
                   services.Configure<CollectorOptions>(context.Configuration.GetSection(CollectorOptions.Name));

                   services.AddSingleton<IImageService<BingImage>, BingImageService>();
                   services.AddSingleton<ILocalFileService, LocalFileService>();

                   var defaultConnection = context.Configuration.GetConnectionString("Default");

                   if (envVars.Contains($"ConnectionStrings__Default"))
                   {
                       defaultConnection = envVars[$"ConnectionStrings__Default"].ToString();
                   }

                   var migrationAssemblyName = typeof(Bing.Wallpaper.Data.SqlServer.PlaceholderType).Assembly.FullName;

                   services.AddDbContext<DefaultDatabaseContext>(x =>
                   {
                       x.UseSqlServer(defaultConnection, options =>
                       {
                           //options.MigrationsAssembly("SampleService.Data.SqlServer");
                           options.MigrationsAssembly(migrationAssemblyName);
                       });
                   });

                   services.AddHostedService<App>();
               })
               .ConfigureLogging((context, logging) =>
               {
                   //Configure Logging
                   logging.ClearProviders();
                   logging.AddConsole();
                   //logging.SetMinimumLevel(LogLevel.Trace);
                   logging.AddNLog();

               })
               .UseConsoleLifetime(options =>
               {

               });

    }
}

