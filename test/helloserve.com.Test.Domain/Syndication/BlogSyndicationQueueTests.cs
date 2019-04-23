using helloserve.com.Domain.Syndication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain.Syndication
{
    [TestClass]
    public class BlogSyndicationQueueTests
    {
        [TestMethod]
        public async Task EnqueueAsync_DequeueAsync_Verify()
        {
            //arrange
            var expected = new Mock<IBlogSyndication>();
            var queue = new BlogSyndicationQueue();

            //act
            await queue.EnqueueAsync(expected.Object);
            var actual = await queue.DequeueAsync();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Object, actual);
        }
    }
}
