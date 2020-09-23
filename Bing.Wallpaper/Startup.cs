using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bing.Wallpaper.Data;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Targets;
using NLog.Web;

namespace Bing.Wallpaper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Configuration
            // https://github.com/NLog/NLog/wiki/ConfigSetting-Layout-Renderer
            //NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = Configuration;

            services.AddControllers();

            services.AddTransient<IImageService<BingImage>, BingImageService>();
            services.AddTransient<ILocalFileService, LocalFileService>();


            var envVars = Environment.GetEnvironmentVariables();

            var connectionString = Configuration.GetConnectionString("Default");
            if (envVars.Contains("ASPNETCORE_CONNECTION_STRING"))
            {
                connectionString = envVars["ASPNETCORE_CONNECTION_STRING"].ToString();
            }

            services.AddDbContext<DefaultDatabaseContext>(options => {

                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("Bing.Wallpaper.Data.SqlServer");
                });
            });

            services.Configure<AppOptions>(options =>
            {
                options.DestinationPath = Configuration["App:DestinationPath"];

                if (envVars.Contains("ASPNETCORE_DESTINATION_PATH"))
                {
                    options.DestinationPath = envVars["ASPNETCORE_DESTINATION_PATH"].ToString();
                }
            });

            //var target = NLog.LogManager.Configuration.AllTargets.FirstOrDefault(x => x.Name == "database") as DatabaseTarget;
            //if (target != null)
            //{
            //    Console.WriteLine(">".PadRight(80, '>'));
            //    Console.WriteLine($"\t\tconnection string: { target.ConnectionString}");
            //    //target.ConnectionString = connectionString;

            //    //Console.WriteLine(">".PadRight(80, '>'));
            //    //Console.WriteLine($"\t\tconnection string: { target.ConnectionString}");
            //}

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            //GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("Default"));

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetService<DefaultDatabaseContext>();
                databaseContext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
