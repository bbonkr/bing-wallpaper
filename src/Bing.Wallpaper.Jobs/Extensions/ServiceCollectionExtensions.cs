using Bing.Wallpaper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Jobs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBingImageCollectingJob(this IServiceCollection services, IConfiguration configuation)
        {
            var collectorOptions = new CollectorOptions();
            var collectionOptionsConfiguration = configuation.GetSection(CollectorOptions.Name);
            collectionOptionsConfiguration.Bind(collectorOptions);

            if (string.IsNullOrWhiteSpace(collectorOptions.Schedule))
            {
                Console.WriteLine("Please check your appsettings. 👁👁");
                Console.WriteLine(CollectorOptions.ExceptionMessage);

                throw new ArgumentException(CollectorOptions.ExceptionMessage, nameof(CollectorOptions.Schedule));
            }

            services.AddScheduler(builder =>
            {
                builder.Services
                .AddLogging()
                .AddDomainService(configuation)
                .Configure<CollectorOptions>(configuation.GetSection(CollectorOptions.Name))
                ;

                builder.AddJob<BingImageJob>(sectionName: "bing-image-collect-job", configure: options =>
                {
                    /*
                      * -------------------------------------------------------------------------------------------------------------
                      *                                        Allowed values    Allowed special characters   Comment
                      *
                      * ┌───────────── second (optional)       0-59              * , - /                      
                      * │ ┌───────────── minute                0-59              * , - /                      
                      * │ │ ┌───────────── hour                0-23              * , - /                      
                      * │ │ │ ┌───────────── day of month      1-31              * , - / L W ?                
                      * │ │ │ │ ┌───────────── month           1-12 or JAN-DEC   * , - /                      
                      * │ │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT   * , - / # L ?                Both 0 and 7 means SUN
                      * │ │ │ │ │ │
                      * * * * * * *                     
                      * -------------------------------------------------------------------------------------------------------------
                     */

                    options.CronSchedule = collectorOptions.Schedule;
                    options.CronTimeZone = TimeZoneInfo.Local.Id;
                    options.RunImmediately = false;
                });

                builder.UnobservedTaskExceptionHandler = (sender, e) => {
                    Console.WriteLine("Schedule Job got a exception", e.Exception);
                };
            });

            return services;
        }
    }
}
