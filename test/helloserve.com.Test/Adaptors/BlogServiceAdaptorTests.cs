using helloserve.com.Adaptors;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Adaptors
{
    [TestClass]
    public class BlogServiceAdaptorTests
    {
        [TestMethod]
        public async Task GetBlog_Verify()
        {
            //arrange
            string title = "test-title";
            string content = "content";
            Blog blog = new Blog() { Title = title, Content = content };
            Mock<IBlogService> serviceMock = new Mock<IBlogService>();
            serviceMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);
            IBlogServiceAdaptor adaptor = new BlogServiceAdaptor(serviceMock.Object);

            //act
            Models.BlogView result = await adaptor.Read(title);

            //assert
            serviceMock.Verify(x => x.Read(title));
            Assert.AreEqual(blog.Title, result.Title);
            Assert.AreEqual("<p>content</p>\n", result.Content);
        }
    }
}
