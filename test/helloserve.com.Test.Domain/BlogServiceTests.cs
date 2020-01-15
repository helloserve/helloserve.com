using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using helloserve.com.Domain.Syndication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .AddDomainServices()
                .AddSyndicationServices(new ConfigurationBuilder().Build())
                .AddTransient(s => _dbAdaptorMock.Object)
                .AddTransient(s => _blogSyndicationServiceMock.Object);

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
        public async Task CreateUpdate_Verify()
        {
            //arrange
            Blog blog = new Blog() { Title = "Hello Test!" };

            //act
            await Service.CreateUpdate(blog);

            //assert
            _dbAdaptorMock.Verify(x => x.Save(blog));
            Assert.AreEqual("hello-test", blog.Key);
        }

        [TestMethod]
        public async Task CreateUpdate_HasKey_Verify()
        {
            //arrange
            Blog blog = new Blog() { Key = "key", Title = "Hello Test!" };

            //act
            await Service.CreateUpdate(blog);

            //assert
            _dbAdaptorMock.Verify(x => x.Save(blog));
            Assert.AreEqual("key", blog.Key);
        }

        [TestMethod]
        public async Task CreateUpdate_NullDate_NotSet()
        {
            //arrange
            Blog blog = new Blog() { Title = "Hello Test!", PublishDate = null };

            //act
            await Service.CreateUpdate(blog);

            //assert
            Assert.IsNull(blog.PublishDate);
        }

        [TestMethod]
        public async Task CreateUpdate_HasDate_NotSet()
        {
            //arrange
            DateTime expectedPublishDate = DateTime.Today.AddDays(-4);
            Blog blog = new Blog() { Title = "Hello Test!", PublishDate = expectedPublishDate };

            //act
            await Service.CreateUpdate(blog);

            //assert
            Assert.AreEqual(expectedPublishDate, blog.PublishDate);
        }

        [TestMethod]
        public async Task CreateUpdate_TitleIsNull_Throws()
        {
            //arrange
            Blog blog = new Blog();

            //act/assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Service.CreateUpdate(blog));
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

        [TestMethod]
        public async Task ReadAll_Verify()
        {
            //arrange
            bool authenticated = true;
            string ownerKey = "owner";
            List<BlogListing> listing = new List<BlogListing>()
            {
                new BlogListing() { Key = "key1", Title = "title1" },
                new BlogListing() { Key = "key2", Title = "title2" },
                new BlogListing() { Key = "key3", Title = "title3" },
            };
            _dbAdaptorMock.Setup(x => x.ReadListings(1, 3, ownerKey, false))
                .ReturnsAsync(listing);

            //act
            IEnumerable<BlogListing> result = await Service.ReadAll(1, 3, ownerKey, authenticated);

            //assert
            _dbAdaptorMock.Verify(x => x.ReadListings(1, 3, ownerKey, false));
            Assert.AreEqual(result, listing);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task ReadAll_NoUser_Verify()
        {
            //arrange
            bool authenticated = false;
            string ownerKey = "owner";
            List<BlogListing> listing = new List<BlogListing>();

            //act
            IEnumerable<BlogListing> result = await Service.ReadAll(1, 3, ownerKey, authenticated);

            //assert
            _dbAdaptorMock.Verify(x => x.ReadListings(1, 3, ownerKey, true));
        }
    }
}
