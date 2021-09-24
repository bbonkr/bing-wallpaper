using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bing.Wallpaper.Mediator.Images.Commands;
using Bing.Wallpaper.Options;

using CronScheduler.Extensions.Scheduler;
using kr.bbon.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bing.Wallpaper.Jobs
{
    public class BingImageJob : IScheduledJob
    {
        const string TAG = "[JOB - BING TODAY IMAGE]";

        public BingImageJob(
            IServiceProvider provider
            )
        {
            this.provider = provider;
        }

        public string Name { get; } = nameof(BingImageJob);

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var scope= provider.CreateScope();
            var mediator= scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<BingImageJob>>();
            var collectorOptionsMonitor = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<CollectorOptions>>();
            var collectorOptions = collectorOptionsMonitor.CurrentValue ?? throw new ArgumentException(CollectorOptions.ExceptionMessage);

            // print options
            logger.LogInformation($"[{TAG}][{nameof(CollectorOptions)}]: ${collectorOptions.ToJson()}");

            var watch = new Stopwatch();
            var now = DateTimeOffset.UtcNow;
            string message = string.Empty;

            try
            {
                watch.Start();

                var command = new AddImageCommand();

                var result =await mediator.Send(command);

                watch.Stop();
                logger.LogInformation($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed} Completed. Count of collecting images is {result.CollectedCount:n0}");
            }
            catch (Exception ex)
            {
                watch.Stop();
                logger.LogWarning($"{TAG} {Name} @{DateTime.Now:yyyy-MM-dd HH:mm:ss} Elapsed:{watch.Elapsed} Incompleted ({ex.Message})");
            }
            finally
            {
            }
        }

        private readonly IServiceProvider provider;
    }
}
