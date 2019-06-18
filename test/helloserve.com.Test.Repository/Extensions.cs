using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace helloserve.com.Test.Repository
{
    public static class Extensions
    {
        //read: https://stackoverflow.com/questions/40476233/how-to-mock-an-async-repository-with-entity-framework-core
        public static Mock<DbSet<T>> AsDbSetMock<T>(this IEnumerable<T> collection)
            where T : class
        {
            var data = collection.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(data.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            mockSet.Setup(x => x.Add(It.IsAny<T>())).Callback<T>(p => { (collection as List<T>).Add(p); });
            mockSet.Setup(x => x.Remove(It.IsAny<T>())).Callback<T>(p => (collection as List<T>).Remove(p));

            //mockSet.Setup(x => x.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(p => collection.AddRange(p));

            //mockSet.Setup(x => x.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).Callback<T, CancellationToken>((p, t) => { (collection as List<T>).Add(p); })
            //    .ReturnsAsync<T, CancellationToken, DbSet<T>, EntityEntry<T>>((p, t) => null);
            //mockSet.Setup(x => x.AddRangeAsync(It.IsAny<IEnumerable<T>>(), It.IsAny<CancellationToken>())).Callback<IEnumerable<T>, CancellationToken>((p, t) => collection.AddRange(p))
            //    .Returns<T, CancellationToken>((p, t) => Task.FromResult(0));

            return mockSet;
        }
    }
}
