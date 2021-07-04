using Bing.Wallpaper.Data;
using Bing.Wallpaper.Jobs;
using Bing.Wallpaper.Options;
using CronScheduler.Extensions.Scheduler;
using kr.bbon.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Bing.Wallpaper
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAppOptions(Configuration);
            services.Configure<MvcOptions>(options => {
                options.CacheProfiles.Add("File-Response-Cache", new CacheProfile
                {
                    Duration = (int)TimeSpan.FromDays(365).TotalSeconds,
                });
            });

            // Register Configuration
            // https://github.com/NLog/NLog/wiki/ConfigSetting-Layout-Renderer
            //NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = Configuration;
            services.AddHttpContextAccessor();
            
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<DefaultDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("Bing.Wallpaper.Data.SqlServer");
                });
            });

            services.AddDomainService(Configuration);

            services.AddBingImageCollectingJob(Configuration);

            services.AddControllersWithViews();

            var defaultVersion = new ApiVersion(1, 0);

            services.AddApiVersioningAndSwaggerGen(defaultVersion);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetService<DefaultDatabaseContext>();
                databaseContext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUIWithApiVersioning();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var collectorOptionsAccessor = scope.ServiceProvider.GetService<IOptionsMonitor<CollectorOptions>>();
                    var logger = scope.ServiceProvider.GetService<ILogger<Startup>>();
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
        }
    }
}
