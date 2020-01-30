using helloserve.com.Domain.Models;
using helloserve.com.Mappers;
using helloserve.com.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace helloserve.com.Test.Mappers
{
    [TestClass]
    public class ProjectMapperTests
    {
        [TestMethod]
        public void ProjectConfig_Content()
        {
            //arrange
            Project project = new Project();

            //act
            ProjectView result = Config.Mapper.Map<ProjectView>(project);

            //assert
            Assert.AreEqual("article", result.Type);
        }
    }
}
