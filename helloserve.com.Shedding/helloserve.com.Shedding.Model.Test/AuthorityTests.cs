using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace helloserve.com.Shedding.Model.Test
{
    [TestClass]
    public class AuthorityTests
    {
        [TestMethod]
        public void GetCurrentStage()
        {
            AuthorityModel model = new AuthorityModel();
            SheddingStage? stage = model.GetCurrentStage();

            Assert.IsTrue(stage != null);
        }
    }
}
