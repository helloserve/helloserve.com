using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain.Syndication
{
    [TestClass]
    public class BlogSyndicationServiceTests
    {
        readonly Mock<IBlogSyndicationQueue> _blogSyndicationQueueMock = new Mock<IBlogSyndicationQueue>();
        readonly Mock<IBlogSyndicationFactory> _blogSyndicationFactoryMock = new Mock<IBlogSyndicationFactory>();
        readonly Mock<IOptionsMonitor<BlogSyndicationOptionCollection>> _blogSyndicationOptionsMock = new Mock<IOptionsMonitor<BlogSyndicationOptionCollection>>();
        readonly Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>();
        readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();

        public BlogSyndicationService Service => new BlogSyndicationService(_blogSyndicationQueueMock.Object, _blogSyndicationFactoryMock.Object, _blogSyndicationOptionsMock.Object, _loggerFactoryMock.Object);

        public BlogSyndicationServiceTests()
        {
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
        }

        [TestMethod]
        public async Task Syndicate_Verify()
        {
            //arrange
            string title = "Hello Test!";
            Blog blog = new Blog() { Title = title };
            Mock<IBlogSyndication> syndicationMock = new Mock<IBlogSyndication>();
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns(new BlogSyndicationOptionCollection()
                {
                    new BlogSyndicationOption() { Provider = "RSS" },
                    new BlogSyndicationOption() { Provider = "Twitter" }
                });
            _blogSyndicationFactoryMock.Setup(x => x.GetInstance(It.IsAny<string>()))
                .Returns(syndicationMock.Object);

            //act
            await Service.Syndicate(blog);

            //assert
            _blogSyndicationFactoryMock.Verify(x => x.GetInstance("RSS"));
            _blogSyndicationFactoryMock.Verify(x => x.GetInstance("Twitter"));
            _blogSyndicationQueueMock.Verify(x => x.Enqueue(syndicationMock.Object), Times.Exactly(2));
            syndicationMock.VerifySet(x => x.Blog = blog, Times.Exactly(2));
        }

        [TestMethod]
        public async Task Syndicate_NoConfig_Log()
        {
            //arrange
            string title = "Hello Test!";
            Blog blog = new Blog() { Title = title };
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns((BlogSyndicationOptionCollection)null);

            //act
            await Service.Syndicate(blog);

            //re-arrange
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns(new BlogSyndicationOptionCollection() { });

            //act
            await Service.Syndicate(blog);

            //assert
            _loggerMock.Verify(x => x.Log(LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Exactly(2));
        }
    }
}
