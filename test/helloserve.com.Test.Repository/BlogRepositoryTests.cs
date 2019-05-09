using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Test.Repository
{
    [TestClass]
    public class BlogRepositoryTests
    {
        readonly Mock<IhelloserveContext> _contextMock = new Mock<IhelloserveContext>();

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            string title = "title";
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1" },
                new Database.Entities.Blog() { Key = title },
                new Database.Entities.Blog() { Key = "key2" }
            };
            IBlogDatabaseAdaptor adaptor = new BlogRepository(_contextMock.Object);
            _contextMock.SetupGet(x => x.Blogs)
                .Returns(blogs.AsDbSetMock().Object);

            //act
            Blog result = await adaptor.Read(title);

            //assert
            Assert.IsNotNull(result);
            _contextMock.Verify(x => x.Blogs);
            Assert.AreEqual(title, result.Key);
        }
    }
}
