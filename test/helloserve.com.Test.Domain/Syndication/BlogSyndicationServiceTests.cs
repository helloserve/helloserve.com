using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using helloserve.com.Domain.Syndication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<SyndicationText> syndicationTexts = new List<SyndicationText>()
            {
                new SyndicationText() { Name = "Twitter", Text = "Hello #World!" },
                new SyndicationText() { Name = "Facebook", Text = "Today I hello worlded"}
            };
            BlogSyndicationOptionCollection syndicationConfig = new BlogSyndicationOptionCollection()
            {
                new BlogSyndicationOption() { Provider = "RSS" },
                new BlogSyndicationOption() { Provider = "Twitter" },
                new BlogSyndicationOption() { Provider = "Facebook" }
            };
            Mock<IBlogSyndication> syndicationMock = new Mock<IBlogSyndication>();
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns(syndicationConfig);
            _blogSyndicationFactoryMock.Setup(x => x.GetInstance(It.IsAny<string>()))
                .Returns(syndicationMock.Object);

            //act
            await Service.Syndicate(blog, syndicationTexts);

            //assert
            _blogSyndicationFactoryMock.Verify(x => x.GetInstance("Twitter"));
            _blogSyndicationFactoryMock.Verify(x => x.GetInstance("Facebook"));
            _blogSyndicationQueueMock.Verify(x => x.EnqueueAsync(syndicationMock.Object), Times.Exactly(2));
            syndicationMock.VerifySet(x => x.Blog = blog, Times.Exactly(2));
            syndicationMock.VerifySet(x => x.Config = syndicationConfig[1]);
            syndicationMock.VerifySet(x => x.Config = syndicationConfig[2]);
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
            await Service.Syndicate(blog, null);

            //re-arrange
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns(new BlogSyndicationOptionCollection() { });

            //act
            await Service.Syndicate(blog, null);

            //assert
            _loggerMock.Verify(x => x.Log(LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task Syndicate_MessedConfig_Log()
        {
            //arrange
            string title = "Hello Test!";
            Blog blog = new Blog() { Title = title };
            List<SyndicationText> syndicationTexts = new List<SyndicationText>()
            {
                new SyndicationText() { Name = "Twitter", Text = "Hello #World!" },
                new SyndicationText() { Name = "Instagram", Text = "Hello World! #program #software #development #blogging" },
                new SyndicationText() { Name = "Facebook", Text = "Today I hello worlded"}
            };
            BlogSyndicationOptionCollection syndicationConfig = new BlogSyndicationOptionCollection()
            {
                new BlogSyndicationOption() { Provider = "Twitter" },
                new BlogSyndicationOption() { Provider = "Twitter" },
                new BlogSyndicationOption() { Provider = "Facebook" }
            };
            Mock<IBlogSyndication> syndicationMock = new Mock<IBlogSyndication>();
            _blogSyndicationOptionsMock.SetupGet(x => x.CurrentValue)
                .Returns(syndicationConfig);
            _blogSyndicationFactoryMock.Setup(x => x.GetInstance(It.IsAny<string>()))
                .Returns(syndicationMock.Object);

            //act
            await Service.Syndicate(blog, syndicationTexts);

            //assert
            _blogSyndicationFactoryMock.Verify(x => x.GetInstance("Facebook"));
            _blogSyndicationQueueMock.Verify(x => x.EnqueueAsync(syndicationMock.Object));
            syndicationMock.VerifySet(x => x.Blog = blog);
            syndicationMock.VerifySet(x => x.Config = syndicationConfig[2]);

            _loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            var state = _loggerMock.Invocations[0].Arguments[2] as IReadOnlyList<KeyValuePair<string, object>>;
            Assert.IsTrue(_loggerMock.Invocations.Any(x => ((string)(x.Arguments[2] as IReadOnlyList<KeyValuePair<string, object>>)[0].Value).Contains("Instagram")));
            Assert.IsTrue(_loggerMock.Invocations.Any(x => ((string)(x.Arguments[2] as IReadOnlyList<KeyValuePair<string, object>>)[0].Value).Contains("Twitter")));
        }
    }
}
