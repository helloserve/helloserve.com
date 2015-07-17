using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Tests.Base
{
    public class BaseTests
    {
        public ITree<SimpleObject> Tree;

        public void Setup(ITree<SimpleObject> tree)
        {
            Tree = tree;
        }

        public void TearDown()
        {

        }
    }
}
