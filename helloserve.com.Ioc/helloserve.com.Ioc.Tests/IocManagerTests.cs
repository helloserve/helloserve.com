using helloserve.com.Ioc.Tests.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Ioc.Tests
{
    [TestClass]
    public class IocManagerTests
    {
        Manager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new Implementation.Manager();
        }

        [TestMethod]
        public void InstantiateManager_Exists()
        {
            Assert.IsNotNull(_manager);
        }

        [TestMethod]
        public void RegisterType_CanGetInstance()
        {
            _manager.RegisterTypeInstance<IInterfaceA>(() => new ConcreteA());
            var instance = _manager.GetTypeInstance<IInterfaceA>();
            Assert.IsNotNull(instance as IInterfaceA);
            Assert.IsNotNull(instance as ConcreteA);
        }

        [TestMethod]
        public void NotRegisterType_GetsException()
        {
            bool exception = false;
            try
            {
                var instance = _manager.GetTypeInstance<IInterfaceA>();
            }
            catch
            {
                exception = true;
            }

            Assert.IsTrue(exception);
        }

        [TestMethod]
        public void RegisterType_ThenSubstituteType_GetCorrectInstance()
        {
            _manager.RegisterTypeInstance<IInterfaceA>(() => new ConcreteA());
            _manager.RegisterTypeInstance<IInterfaceA>(() => new ConcreteB());
            var instance = _manager.GetTypeInstance<IInterfaceA>();
            Assert.IsNotNull(instance as IInterfaceA);
            Assert.IsNotNull(instance as ConcreteB);
        }
    }
}
