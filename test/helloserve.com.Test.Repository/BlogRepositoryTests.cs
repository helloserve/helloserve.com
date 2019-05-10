using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
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
        readonly IServiceCollection _services = new ServiceCollection();
        private IServiceProvider _serviceProvider;        
        readonly Mock<IhelloserveContext> _contextMock = new Mock<IhelloserveContext>();

        public IBlogDatabaseAdaptor Repository => _serviceProvider.GetService<IBlogDatabaseAdaptor>();

        [TestInitialize]
        public void Initialize()
        {
            _services
                .AddTransient(s => _contextMock.Object)
                .AddRepositories();

            _serviceProvider = _services.BuildServiceProvider();
        }

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
            _contextMock.SetupGet(x => x.Blogs)
                .Returns(blogs.AsDbSetMock().Object);

            //act
            Blog result = await Repository.Read(title);

            //assert
            Assert.IsNotNull(result);
            _contextMock.Verify(x => x.Blogs);
            Assert.AreEqual(title, result.Key);
        }
    }
}
