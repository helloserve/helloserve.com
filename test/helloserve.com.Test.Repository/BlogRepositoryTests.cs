using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Repository
{
    [TestClass]
    public class BlogRepositoryTests : RepositoryTests<IBlogDatabaseAdaptor>
    {
        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_Read_Verify");
            string title = "title";
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1" },
                new Database.Entities.Blog() { Key = title },
                new Database.Entities.Blog() { Key = "key2" }
            };
            using (var context = new helloserveContext(Options))
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
            ArrangeDatabase("BlogRepository_ReadListing_Verify");
            string key = "key";
            string title = "title";
            DateTime publishDate = new DateTime(2019, 6, 18, 10, 47, 0);
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1", Title = "title1", IsPublished = true, PublishDate = publishDate },
                new Database.Entities.Blog() { Key = key, Title = title, IsPublished = true, PublishDate = DateTime.Today },
                new Database.Entities.Blog() { Key = "key2", Title = "title2" },
                new Database.Entities.Blog() { Key = "key3", Title = "title3", IsPublished = true },
                new Database.Entities.Blog() { Key = "key4", Title = "title4", IsPublished = true },
                new Database.Entities.Blog() { Key = "key5", Title = "title5", IsPublished = true },
                new Database.Entities.Blog() { Key = "key6", Title = "title6", IsPublished = true },
                new Database.Entities.Blog() { Key = "key7", Title = "title7", IsPublished = true },
                new Database.Entities.Blog() { Key = "key8", Title = "title8", IsPublished = true },
                new Database.Entities.Blog() { Key = "key9", Title = "title9", IsPublished = true },
                new Database.Entities.Blog() { Key = "key10", Title = "title10", IsPublished = true }
            };
            using (var context = Context)
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            IEnumerable<BlogListing> listings = await Repository.ReadListings(1, 3);

            //assert
            Assert.IsTrue(listings.Count() == 3);
            Assert.IsTrue(listings.Any(x => x.Key == key));
            Assert.IsTrue(listings.Any(x => x.Key == "key1"));
            Assert.IsTrue(listings.Any(x => x.Key == "key3"));
            Assert.IsTrue(listings.Any(x => x.Title == title));
            Assert.IsTrue(listings.Any(x => x.PublishDate == publishDate));
            Assert.IsTrue(listings.Any(x => x.PublishDate == null));

            //act page 2
            listings = await Repository.ReadListings(2, 3);

            //assert
            Assert.IsTrue(listings.Count() == 3);
            Assert.IsTrue(listings.Any(x => x.Key == "key4"));
            Assert.IsTrue(listings.Any(x => x.Key == "key5"));
            Assert.IsTrue(listings.Any(x => x.Key == "key6"));

            //act page 3
            listings = await Repository.ReadListings(3, 3);

            //assert
            Assert.IsTrue(listings.Count() == 3);
            Assert.IsTrue(listings.Any(x => x.Key == "key7"));
            Assert.IsTrue(listings.Any(x => x.Key == "key8"));
            Assert.IsTrue(listings.Any(x => x.Key == "key9"));

            //act page 4
            listings = await Repository.ReadListings(4, 3);

            //assert
            Assert.IsTrue(listings.Count() == 1);
            Assert.IsTrue(listings.Any(x => x.Key == "key10"));
        }

        [TestMethod]
        public async Task ReadListing_ForOwner_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_ReadListing_ForOwner_Verify");
            string key = "key";
            string title = "title";
            string ownerKey = "owner";
            DateTime publishDate = new DateTime(2019, 6, 18, 10, 47, 0);
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1", Title = "title1", IsPublished = true, PublishDate = publishDate },
                new Database.Entities.Blog() { Key = key, Title = title, IsPublished = true, PublishDate = DateTime.Today },
                new Database.Entities.Blog() { Key = "key2", Title = "title2" }
            };
            var blogOwners = new List<Database.Entities.BlogOwner>()
            {
                new Database.Entities.BlogOwner() { BlogKey = key, OwnerType = "Project", OwnerKey = ownerKey }
            };
            using (var context = Context)
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.BlogOwners.AddRangeAsync(blogOwners);
                await context.SaveChangesAsync();
            }

            //act
            IEnumerable<BlogListing> listings = await Repository.ReadListings(1, 3, blogOwnerKey: ownerKey);
            //assert
            Assert.IsTrue(listings.Count() == 1);
            Assert.IsTrue(listings.Any(x => x.Key == key));
        }

        [TestMethod]
        public async Task ReadListing__IncludeUnpublished_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_ReadListing_IncludeUnpublished_Verify");
            string key = "key";
            string title = "title";
            DateTime publishDate = new DateTime(2019, 6, 18, 10, 47, 0);
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1", Title = "title1", IsPublished = true, PublishDate = publishDate },
                new Database.Entities.Blog() { Key = key, Title = title, IsPublished = true, PublishDate = DateTime.Today },
                new Database.Entities.Blog() { Key = "key2", Title = "title2" },
                new Database.Entities.Blog() { Key = "key3", Title = "title3", IsPublished = true },
                new Database.Entities.Blog() { Key = "key4", Title = "title4", IsPublished = true },
                new Database.Entities.Blog() { Key = "key5", Title = "title5", IsPublished = true },
                new Database.Entities.Blog() { Key = "key6", Title = "title6" },
                new Database.Entities.Blog() { Key = "key7", Title = "title7", IsPublished = true },
                new Database.Entities.Blog() { Key = "key8", Title = "title8", IsPublished = true },
                new Database.Entities.Blog() { Key = "key9", Title = "title9", IsPublished = true },
                new Database.Entities.Blog() { Key = "key10", Title = "title10", IsPublished = true }
            };
            using (var context = Context)
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            IEnumerable<BlogListing> listings = await Repository.ReadListings(1, 3, publishedOnly: false);

            //assert
            Assert.IsTrue(listings.Count() == 5);
            Assert.IsTrue(listings.First().Key == "key2");
            Assert.IsTrue(listings.Skip(1).First().Key == "key6");
            Assert.IsTrue(listings.Any(x => x.Key == key));
            Assert.IsTrue(listings.Any(x => x.Key == "key1"));
            Assert.IsTrue(listings.Any(x => x.Key == "key3"));
            Assert.IsTrue(listings.Any(x => x.Title == title));
            Assert.IsTrue(listings.Any(x => x.PublishDate == publishDate));
            Assert.IsTrue(listings.Any(x => x.PublishDate == null));

            //act page 2
            listings = await Repository.ReadListings(2, 3, publishedOnly: false);

            //assert
            Assert.IsTrue(listings.Count() == 5);
            Assert.IsTrue(listings.First().Key == "key2");
            Assert.IsTrue(listings.Skip(1).First().Key == "key6");
            Assert.IsTrue(listings.Any(x => x.Key == "key4"));
            Assert.IsTrue(listings.Any(x => x.Key == "key5"));
            Assert.IsTrue(listings.Any(x => x.Key == "key7"));

            //act page 3
            listings = await Repository.ReadListings(3, 3, publishedOnly: false);

            //assert
            Assert.IsTrue(listings.Count() == 5);
            Assert.IsTrue(listings.First().Key == "key2");
            Assert.IsTrue(listings.Skip(1).First().Key == "key6");
            Assert.IsTrue(listings.Any(x => x.Key == "key8"));
            Assert.IsTrue(listings.Any(x => x.Key == "key9"));
            Assert.IsTrue(listings.Any(x => x.Key == "key10"));

            //act page 4
            listings = await Repository.ReadListings(4, 3, publishedOnly: false);

            //assert
            Assert.IsTrue(listings.Count() == 2);
            Assert.IsTrue(listings.First().Key == "key2");
            Assert.IsTrue(listings.Skip(1).First().Key == "key6");
        }

        [TestMethod]
        public async Task Save_Create_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_Save_Create_Verify");
            Blog blog = new Blog()
            {
                Title = "This is a title!",
                Key = "this_is_a_title",
                Content = "content"
            };

            //act
            await Repository.Save(blog);

            //assert
            using var context = Context;
            Assert.IsTrue(context.Blogs.Any(x => x.Key == blog.Key));
        }

        [TestMethod]
        public async Task Save_Update_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_Save_Update_Verify");
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
            using (var context = Context)
            {
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            //act
            await Repository.Save(blog);

            //assert
            using (var context = Context)
            {
                Assert.IsTrue(context.Blogs.Any(x => x.Key == blog.Key && x.Content == blog.Content && x.Title == blog.Title));
            }
        }

        [TestMethod]
        public async Task Read_Latest_Verify()
        {
            //arrange
            ArrangeDatabase("BlogRepository_Read_Latest_Verify");
            var blogs = new List<Database.Entities.Blog>()
            {
                new Database.Entities.Blog() { Key = "key1", IsPublished = true, PublishDate = DateTime.Now },
                new Database.Entities.Blog() { Key = "key2", IsPublished = false, PublishDate = null },
                new Database.Entities.Blog() { Key = "key3", IsPublished = false, PublishDate = DateTime.Now.AddDays(-2) },
                new Database.Entities.Blog() { Key = "key4", IsPublished = true, PublishDate = DateTime.Now.AddDays(-4) },
                new Database.Entities.Blog() { Key = "key5", IsPublished = true, PublishDate = DateTime.Now.AddDays(-5) }
            };
            using (var context = Context)
            {
                context.Blogs.AddRange(blogs);
                await context.SaveChangesAsync();
            }

            //act
            var result = await Repository.ReadLatest(2);

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(x => x.Key == "key1"));
            Assert.IsTrue(result.Any(x => x.Key == "key4"));
            Assert.IsFalse(result.Any(x => x.Key == "key2"));
            Assert.IsFalse(result.Any(x => x.Key == "key3"));
            Assert.IsFalse(result.Any(x => x.Key == "key5"));
        }
    }
}
