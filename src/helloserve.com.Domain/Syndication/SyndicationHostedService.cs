using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public class SyndicationHostedService : BackgroundService
    {
        readonly IBlogSyndicationQueue _queue;
        readonly ILogger _logger;

        public SyndicationHostedService(IBlogSyndicationQueue queue, ILoggerFactory loggerFactory)
        {
            _queue = queue;
            _logger = loggerFactory.CreateLogger<SyndicationHostedService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IBlogSyndication syndicationItem = await _queue.DequeueAsync();
                _logger?.LogInformation($"Dequeue IBlogSyndication for title '{syndicationItem?.Blog?.Title}'");
                try
                {
                    await syndicationItem?.ProcessAsync();
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Error occured processing IBlogSyndication for title '{syndicationItem?.Blog?.Title}'");
                }
            }
        }
    }
}
