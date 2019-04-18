using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            IBlogSyndication syndicationItem = _queue.Dequeue();
        }
    }
}
