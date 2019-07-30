using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Repository
{
    [TestClass]
    public class BlogRepositoryTests
    {
        readonly IServiceCollection _services = new ServiceCollection();
        private IServiceProvider _serviceProvider;        
        private DbContextOptions<helloserveContext> _options;

        public IBlogDatabaseAdaptor Repository => _serviceProvider.GetService<IBlogDatabaseAdaptor>();

        [TestInitialize]
        public void Initialize()
        {
            _services.AddTransient(sp => new helloserveContext(_options)); //sets up for injection
            _services.AddRepositories();

            _serviceProvider = _services.BuildServiceProvider();
        }

        private void ArrangeDatabase(string name)
        {
            _options = new DbContextOptionsBuilder<helloserveContext>()
                .UseInMemoryDatabase(name)
                .Options;
        }

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            ArrangeDatabase("Read_Verify");
            string title = "title";
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1" },
                new Database.Entities.Blog() { Key = title },
                new Database.Entities.Blog() { Key = "key2" }
            };
            using (var context = new helloserveContext(_options))
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            Blog result = await Repository.Read(title);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(title, result.Key);
        }

        [TestMethod]
        public async Task ReadListing_Verify()
        {
            //arrange
            ArrangeDatabase("ReadListing_Verify");
            string key = "key";
            string title = "title";
            DateTime publishDate = new DateTime(2019, 6, 18, 10, 47, 0);
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1", Title = "title1", PublishDate = publishDate },
                new Database.Entities.Blog() { Key = key, Title = title, PublishDate = DateTime.Today },
                new Database.Entities.Blog() { Key = "key2", Title = "title2" }
            };
            using (var context = new helloserveContext(_options))
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            IEnumerable<BlogListing> listings = await Repository.ReadListings();

            //arrange
            Assert.IsTrue(listings.Count() == 3);
            Assert.IsTrue(listings.Any(x => x.Key == key));
            Assert.IsTrue(listings.Any(x => x.Title == title));
            Assert.IsTrue(listings.Any(x => x.PublishDate == publishDate));
            Assert.IsTrue(listings.Any(x => x.PublishDate == null));
        }

        [TestMethod]
        public async Task Save_Create_Verify()
        {
            //arrange
            ArrangeDatabase("Save_Create_Verify");
            Blog blog = new Blog()
            {
                Title = "This is a title!",
                Key = "this_is_a_title",
                Content = "content"
            };

            //act
            await Repository.Save(blog);

            //assert
            using (var context = new helloserveContext(_options))
            {
                Assert.IsTrue(context.Blogs.Any(x => x.Key == blog.Key));
            }
        }

        [TestMethod]
        public async Task Save_Update_Verify()
        {
            //arrange
            ArrangeDatabase("Save_Update_Verify");
            Blog blog = new Blog()
            {
                Title = "This is a title!",
                Key = "this_is_a_title",
                Content = "content"
            };
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = blog.Key, Title = "title1", Content = "before" },
                new Database.Entities.Blog() { Key = "key2", Title = "title2" }
            };
            using (var context = new helloserveContext(_options))
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            await Repository.Save(blog);

            //assert
            using (var context = new helloserveContext(_options))
            {
                Assert.IsTrue(context.Blogs.Any(x => x.Key == blog.Key && x.Content == blog.Content && x.Title == blog.Title));
            }
        }
    }
}
