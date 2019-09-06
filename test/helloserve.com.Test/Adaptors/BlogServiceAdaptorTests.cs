using helloserve.com.Adaptors;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Models;
using Microsoft.AspNetCore.Components;
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
        IBlogServiceAdaptor adaptor => new BlogServiceAdaptor(_serviceMock.Object);

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
            BlogView result = await adaptor.Read(title);

            //assert
            _serviceMock.Verify(x => x.Read(title));
            Assert.AreEqual(blog.Title, result.Title);
            Assert.AreEqual("<p>content</p>\n", result.Content);
        }

        [TestMethod]
        public async Task ReadAll_Verify()
        {
            //arrange
            bool authenticated = true;
            List<Blog> blogs = new List<Blog>()
            {
                new Blog(),
                new Blog(),
                new Blog()
            };
            _serviceMock.Setup(x => x.ReadAll(1, 3, authenticated))
                .ReturnsAsync(blogs);

            //act
            IEnumerable<BlogItemView> result = await adaptor.ReadAll(1, 3, authenticated);

            //assert
            _serviceMock.Verify(x => x.ReadAll(1, 3, authenticated));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task Edit_Verify()
        {
            //arrange
            string title = "title";
            Blog blog = new Blog()
            {
                Title = "Title",
                Key = title
            };
            _serviceMock.Setup(x => x.Read(title))
                .ReturnsAsync(blog);

            //act
            BlogCreate result = await adaptor.Edit(title);

            //assert
            Assert.IsNotNull(result);
            _serviceMock.Verify(x => x.Read(title));
        }

        [TestMethod]
        public async Task Submit_Verify()
        {
            //arrange
            BlogCreate blog = new BlogCreate();

            //act
            await adaptor.Submit(blog);

            //assert
            _serviceMock.Verify(x => x.CreateUpdate(It.IsAny<Blog>()));
        }

        [TestMethod]
        public async Task Publish_Verify()
        {
            //arrange
            string title = "test title";

            //act
            await adaptor.Publish(title);

            //assert
            _serviceMock.Verify(x => x.Publish(title, It.IsAny<IEnumerable<Domain.Syndication.Models.SyndicationText>>()));
        }
    }
}
