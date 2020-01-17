using helloserve.com.Adaptors;
using helloserve.com.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Adaptors
{
    [TestClass]
    public class ProjectServiceAdaptorTests : AdaptorTestsBase<IProjectServiceAdaptor>
    {
        readonly Mock<IProjectService> _serviceMock = new Mock<IProjectService>();

        protected override IServiceCollection Configure(IServiceCollection services)
        {
            return services
                .AddTransient(s => _serviceMock.Object);
        }

        [TestMethod]
        public async Task ReadAllActive_Verify()
        {
            //arrange

            //act
            await Adaptor.ReadAllActive();

            //assert
            _serviceMock.Verify(x => x.ReadAllActive());
        }
    }
}
