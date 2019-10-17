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
            services.AddControllers();

            services.AddTransient<IImageService<BingImage>, BingImageService>();
            services.AddTransient<ILocalFileService, LocalFileService>();


            var envVars = Environment.GetEnvironmentVariables();

            services.AddDbContext<DefaultDatabaseContext>(options => {
                var connectionString = Configuration.GetConnectionString("Default");

                if (envVars.Contains("ASPNETCORE_CONNECTION_STRING"))
                {
                    connectionString = envVars["ASPNETCORE_CONNECTION_STRING"].ToString();
                }

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
