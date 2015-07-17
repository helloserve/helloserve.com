using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloserve.com.Trees.Core.Tests.Base;
using System.Collections.Generic;

namespace helloserve.com.Trees.Core.Tests
{
    [TestClass]
    public class BinaryTreeTests : BaseTests
    {
        [TestMethod]
        public void BinaryTree_AddOneItem()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });

            Assert.IsTrue(Tree.Leaf != null);
            Assert.IsTrue(Tree.Leaf.Leafs.Count == 0);
        }

        [TestMethod]
        public void BinaryTree_AddThreeItems_TraverseInOrder()
        {
            Setup(new BinaryTree<SimpleObject, string>(x => x.StringValue));

            Tree.Add(new SimpleObject() { StringValue = "Hello" });
            Tree.Add(new SimpleObject() { StringValue = "Alpha!" });
            Tree.Add(new SimpleObject() { StringValue = "Zulu" });

            IList<SimpleObject> items = Tree.Traverse(TreeTraverseMode.DepthFirst, TreeTraverseOrder.InOrder);

            Assert.IsTrue(items[0].StringValue == "Alpha!");
            Assert.IsTrue(items[1].StringValue == "Hello");
            Assert.IsTrue(items[2].StringValue == "Zulu");
        }
    }
}
