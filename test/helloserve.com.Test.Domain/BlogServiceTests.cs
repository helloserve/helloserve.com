using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using helloserve.com.Domain.Syndication.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain
{
    [TestClass]
    public class BlogServiceTests
    {
        readonly IServiceCollection _services = new ServiceCollection();
        private IServiceProvider _serviceProvider;
        readonly Mock<IBlogDatabaseAdaptor> _dbAdaptorMock = new Mock<IBlogDatabaseAdaptor>();
        readonly Mock<IBlogSyndicationService> _blogSyndicationServiceMock = new Mock<IBlogSyndicationService>();

        public IBlogService Service => _serviceProvider.GetService<IBlogService>();

        [TestInitialize]
        public void Initialize()
        {
            _services
                .AddTransient(s => _dbAdaptorMock.Object)
                .AddTransient(s => _blogSyndicationServiceMock.Object)
                .AddDomainServices();

            _serviceProvider = _services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task Read_HasModel()
        {
            //arrange
            string title = "";
            _dbAdaptorMock.Setup(x => x.Read(title)).ReturnsAsync(new Blog());

            //act
            Blog result = await Service.Read(title);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Create_Verify()
        {
            //arrange
            Blog blog = new Blog() { Title = "Hello Test!" };

            //act
            await Service.Create(blog);

            //assert
            _dbAdaptorMock.Verify(x => x.Save(blog));
            Assert.AreEqual("hello_test", blog.Key);
        }

        [TestMethod]
        public async Task Create_NullDate_IsSet()
        {
            //arrange
            Blog blog = new Blog() { Title = "Hello Test!", PublishDate = null };

            //act
            await Service.Create(blog);

            //assert
            Assert.AreEqual(DateTime.Today, blog.PublishDate);
        }

        [TestMethod]
        public async Task Create_HasDate_NotSet()
        {
            //arrange
            DateTime expectedPublishDate = DateTime.Today.AddDays(-4);
            Blog blog = new Blog() { Title = "Hello Test!", PublishDate = expectedPublishDate };

            //act
            await Service.Create(blog);

            //assert
            Assert.AreEqual(expectedPublishDate, blog.PublishDate);
        }

        [TestMethod]
        public async Task Create_TitleIsNull_Throws()
        {
            //arrange
            Blog blog = new Blog();

            //act/assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Service.Create(blog));
        }

        [TestMethod]
        public async Task Publish_IsPublished_MarkedTrue()
        {
            //arrange
            string title = "hello_test";
            Blog blog = new Blog() { Title = "Hello Test!" };
            _dbAdaptorMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            await Service.Publish(title, null);

            //assert
            Assert.IsTrue(blog.IsPublished);
            _dbAdaptorMock.Verify(x => x.Save(blog));
        }

        [TestMethod]
        public async Task Publish_NoDate_IsSet()
        {
            //arrange
            string title = "hello_test";
            DateTime expectedDate = DateTime.Today;
            Blog blog = new Blog() { Title = "Hello Test!" };
            _dbAdaptorMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            await Service.Publish(title, null);

            //assert
            Assert.AreEqual(expectedDate, blog.PublishDate);
        }

        [TestMethod]
        public async Task Publish_DateSet_IsNotSet()
        {
            //arrange
            string title = "hello_test";
            DateTime expectedDate = DateTime.Today.AddDays(-2);
            Blog blog = new Blog() { Title = "Hello Test!", PublishDate = expectedDate };
            _dbAdaptorMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            await Service.Publish(title, null);

            //assert
            Assert.AreEqual(expectedDate, blog.PublishDate);
        }

        [TestMethod]
        public async Task Publish_EnqueueSyndications()
        {
            //arrange
            string title = "hello_test";
            Blog blog = new Blog() { Title = "Hello Test!" };
            List<SyndicationText> syndicationTexts = new List<SyndicationText>()
            {
                new SyndicationText() { Name = "Twitter", Text = "Hello #World!" },
                new SyndicationText() { Name = "Facebook", Text = "Today I write Hello World!" },
            };
            _dbAdaptorMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            await Service.Publish(title, syndicationTexts);

            //assert
            _blogSyndicationServiceMock.Verify(x => x.SyndicateAsync(blog, syndicationTexts));
        }
    }
}
