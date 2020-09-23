using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Web;

namespace Bing.Wallpaper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                logger.Error(ex, "Stopped webapp because of exception");
                throw ex;
            }
            finally
            {
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //.ConfigureLogging(logging => {
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            //})
            .UseNLog()
            ;
    }
}
