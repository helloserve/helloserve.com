using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain
{
    [TestClass]
    public class BlogServiceTests
    {
        readonly Mock<IBlogDatabaseAdaptor> _dbAdaptorMock = new Mock<IBlogDatabaseAdaptor>();
        public BlogService Service => new BlogService(_dbAdaptorMock.Object);

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
            _dbAdaptorMock.Verify(x => x.Create(blog));
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
    }
}
