using helloserve.com.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace helloserve.com.Test.Mappers
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void ConfigTests_Valid()
        {
            Config.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
