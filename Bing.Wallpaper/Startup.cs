using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bing.Wallpaper.Data;
using Bing.Wallpaper.Jobs;
using Bing.Wallpaper.Models;
using Bing.Wallpaper.Options;
using Bing.Wallpaper.Repositories;
using Bing.Wallpaper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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

            
            services.AddScoped<IImageService<BingImage>, BingImageService>();
            services.AddScoped<ILocalFileService, LocalFileService>();
            
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IAppLogRepository, AppLogRepository>();

            var envVars = Environment.GetEnvironmentVariables();

            var connectionString = Configuration.GetConnectionString("Default");
            if (envVars.Contains("ASPNETCORE_CONNECTION_STRING"))
            {
                connectionString = envVars["ASPNETCORE_CONNECTION_STRING"].ToString();
            }

            services.AddDbContext<DefaultDatabaseContext>(options =>
            {
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


            services.AddScheduler(builder =>
            {
                builder.AddJob<BingImageJob>( configure: options =>
                {
                    /*
                     * -------------------------------------------------------------------------------------------------------------
                     *                                        Allowed values    Allowed special characters   Comment
                     *
                     * 忙式式式式式式式式式式式式式 second (optional)       0-59              * , - /                      
                     * 弛 忙式式式式式式式式式式式式式 minute                0-59              * , - /                      
                     * 弛 弛 忙式式式式式式式式式式式式式 hour                0-23              * , - /                      
                     * 弛 弛 弛 忙式式式式式式式式式式式式式 day of month      1-31              * , - / L W ?                
                     * 弛 弛 弛 弛 忙式式式式式式式式式式式式式 month           1-12 or JAN-DEC   * , - /                      
                     * 弛 弛 弛 弛 弛 忙式式式式式式式式式式式式式 day of week   0-6  or SUN-SAT   * , - / # L ?                Both 0 and 7 means SUN
                     * 弛 弛 弛 弛 弛 弛
                     * * * * * * *                     
                     * -------------------------------------------------------------------------------------------------------------
                    */
                    options.CronSchedule = "* * 5 * * *";
                    options.CronTimeZone = TimeZoneInfo.Local.Id;
                    options.RunImmediately = true;
                });
            });

            services.AddDtoMapper();

            //services.AddControllers();
            services.AddControllersWithViews();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bing Image Collector", Version = "v1" });
            });
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bing Image Collector v1.0"));
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
            });
        }
    }
}
