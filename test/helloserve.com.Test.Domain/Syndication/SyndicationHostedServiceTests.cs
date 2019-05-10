using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain.Syndication
{
    [TestClass]
    public class SyndicationHostedServiceTests
    {
        [TestMethod]
        public void ExecuteAsync_Verify_WithLoop()
        {
            //arrange
            var cancellationTokenSource = new CancellationTokenSource();
            var syndication = new Mock<IBlogSyndication>();
            var queueMock = new Mock<IBlogSyndicationQueue>();
            queueMock.Setup(x => x.DequeueAsync())
                .ReturnsAsync(syndication.Object);
            var loggerMock = new Mock<ILogger>();
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var service = new SyndicationHostedService(queueMock.Object, loggerFactoryMock.Object);

            //act
            Task.Run(() => service.StartAsync(cancellationTokenSource.Token));
            Thread.Sleep(1000);
            cancellationTokenSource.Cancel();

            //assert
            queueMock.Verify(x => x.DequeueAsync(), Times.AtLeastOnce());
            //loggerMock.Verify(x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.AtLeastOnce());
            syndication.Verify(x => x.ProcessAsync(), Times.AtLeastOnce());
        }

        [TestMethod]
        public void ExecuteAsync_Verify_WithException()
        {
            //arrange
            var cancellationTokenSource = new CancellationTokenSource();
            var syndication = new Mock<IBlogSyndication>();
            syndication.Setup(x => x.ProcessAsync())
                .ThrowsAsync(new Exception());
            var queueMock = new Mock<IBlogSyndicationQueue>();
            queueMock.Setup(x => x.DequeueAsync())
                .ReturnsAsync(syndication.Object);
            var loggerMock = new Mock<ILogger>();
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var service = new SyndicationHostedService(queueMock.Object, loggerFactoryMock.Object);

            //act
            Task.Run(() => service.StartAsync(cancellationTokenSource.Token));
            Thread.Sleep(1000);
            cancellationTokenSource.Cancel();

            //assert
            queueMock.Verify(x => x.DequeueAsync(), Times.AtLeastOnce());
            //loggerMock.Verify(x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.AtLeastOnce());
            syndication.Verify(x => x.ProcessAsync(), Times.AtLeastOnce());
            //loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.AtLeastOnce());
        }
    }
}
