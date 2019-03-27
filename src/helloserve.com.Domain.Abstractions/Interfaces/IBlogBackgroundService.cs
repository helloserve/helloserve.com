using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public interface IBlogBackgroundService : IHostedService, IDisposable
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
