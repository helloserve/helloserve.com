using helloserve.com.Adaptors;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Adaptors
{
    [TestClass]
    public class BlogServiceAdaptorTests
    {
        readonly Mock<IBlogService> _serviceMock = new Mock<IBlogService>();
        IBlogServiceAdaptor _adaptor => new BlogServiceAdaptor(_serviceMock.Object);

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            string title = "test-title";
            string content = "content";
            Blog blog = new Blog() { Title = title, Content = content };
            _serviceMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            BlogView result = await _adaptor.Read(title);

            //assert
            _serviceMock.Verify(x => x.Read(title));
            Assert.AreEqual(blog.Title, result.Title);
            Assert.AreEqual("<p>content</p>\n", result.Content);
        }

        [TestMethod]
        public async Task ReadAll_Verify()
        {
            //arrange
            List<Blog> blogs = new List<Blog>()
            {
                new Blog(),
                new Blog(),
                new Blog()
            };
            _serviceMock.Setup(x => x.ReadAll())
                .ReturnsAsync(blogs);

            //act
            IEnumerable<BlogItemView> result = await _adaptor.ReadAll();

            //assert
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task Submit_Verify()
        {
            //arrange
            BlogCreate blog = new BlogCreate();

            //act
            await _adaptor.Submit(blog);

            //assert
            _serviceMock.Verify(x => x.Create(It.IsAny<Blog>()));
        }
    }
}
