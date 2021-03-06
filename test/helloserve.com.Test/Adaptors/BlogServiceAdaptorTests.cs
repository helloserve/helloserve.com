﻿using helloserve.com.Adaptors;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Adaptors
{
    [TestClass]
    public class BlogServiceAdaptorTests : AdaptorTestsBase<IBlogServiceAdaptor>
    {
        readonly Mock<IBlogService> _serviceMock = new Mock<IBlogService>();

        protected override IServiceCollection Configure(IServiceCollection services)
        {
            return services
                .AddTransient(s => _serviceMock.Object);
        }

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
            BlogView result = await Adaptor.Read(title);

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
            string ownerKey = "owner";
            List<Blog> blogs = new List<Blog>()
            {
                new Blog(),
                new Blog(),
                new Blog()
            };
            _serviceMock.Setup(x => x.ReadAll(1, 3, ownerKey, authenticated))
                .ReturnsAsync(blogs);

            //act
            IEnumerable<BlogItemView> result = await Adaptor.ReadAll(1, 3, ownerKey, authenticated);

            //assert
            _serviceMock.Verify(x => x.ReadAll(1, 3, ownerKey, authenticated));
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
            BlogCreate result = await Adaptor.Edit(title);

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
            await Adaptor.Submit(blog);

            //assert
            _serviceMock.Verify(x => x.CreateUpdate(It.IsAny<Blog>()));
        }

        [TestMethod]
        public async Task Publish_Verify()
        {
            //arrange
            string title = "test title";

            //act
            await Adaptor.Publish(title);

            //assert
            _serviceMock.Verify(x => x.Publish(title, It.IsAny<IEnumerable<Domain.Syndication.Models.SyndicationText>>()));
        }

        [TestMethod]
        public async Task ReadLatest_Verify()
        {
            //arrange
            int count = 5;

            //act
            await Adaptor.ReadLatest(count);

            //assert
            _serviceMock.Verify(x => x.ReadLatest(count));
        }
    }
}
