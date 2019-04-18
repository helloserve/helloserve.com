using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain.Syndication
{
    [TestClass]
    public class SyndicationHostedServiceTests
    {
        [TestMethod]
        public async Task ExecuteAsync_Verify()
        {
            //arrange
            Mock<IBlogSyndicationQueue> queueMock = new Mock<IBlogSyndicationQueue>();
            Mock<ILoggerFactory> loggerFactoryMock = new Mock<ILoggerFactory>();
            SyndicationHostedService service = new SyndicationHostedService(queueMock.Object, loggerFactoryMock.Object);
            CancellationToken cancellationToken = new CancellationToken();

            //act
            await service.StartAsync(cancellationToken);

            //
            queueMock.Verify(x => x.Dequeue());
        }
    }
}
