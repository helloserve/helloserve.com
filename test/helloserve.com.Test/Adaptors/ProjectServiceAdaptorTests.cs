using helloserve.com.Adaptors;
using helloserve.com.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
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
            _serviceMock.Setup(x => x.ReadAllActive())
                .ReturnsAsync(new List<Domain.Models.Project>()
                {
                    new Domain.Models.Project()
                });

            //act
            await Adaptor.ReadAllActive();

            //assert
            _serviceMock.Verify(x => x.ReadAllActive());
        }

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            string key = "key";
            _serviceMock.Setup(x => x.Read(key))
                .ReturnsAsync(new Domain.Models.Project()
                {
                    Key = key
                });

            //act
            await Adaptor.Read(key);

            //assert
            _serviceMock.Verify(x => x.Read(key));
        }
    }
}
