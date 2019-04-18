using helloserve.com.Domain.Syndication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace helloserve.com.Test.Domain.Syndication
{
    [TestClass]
    public class BlogSyndicationQueueTests
    {
        [TestMethod]
        public void Enqueue_Verify()
        {
            //arrange
            var expected = new Mock<IBlogSyndication>();
            var queue = new BlogSyndicationQueue();

            //act
            queue.Enqueue(expected.Object);
            var actual = queue.Dequeue();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Object, actual);
        }
    }
}
