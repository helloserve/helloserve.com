using helloserve.com.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Domain
{
    [TestClass]
    public class ProjectServiceTests : ServiceTestsBase<IProjectService>
    {
        readonly Mock<IProjectDatabaseAdaptor> _repositoryMock = new Mock<IProjectDatabaseAdaptor>();

        protected override IServiceCollection Configure(IServiceCollection services)
        {
            return services
                .AddTransient(s => _repositoryMock.Object);
        }

        [TestMethod]
        public async Task ReadAllActive_Verify()
        {
            //arrange

            //act
            await Service.ReadAllActive();

            //assert
            _repositoryMock.Verify(x => x.ReadAll());
        }

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            string key = "key";

            //act
            await Service.Read(key);

            //assert
            _repositoryMock.Verify(x => x.Read(key));
        }
    }
}
