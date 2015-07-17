using helloserve.com.Trees.Core.Base;
using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    public class BinaryTree<T, TProperty> : TreeBase<T>, ITree<T>
        where TProperty : IComparable
    {
        private Expression<Func<T, TProperty>> _expression;

        public BinaryTree(Expression<Func<T, TProperty>> expression)
        {
            _expression = expression;
            Leaf = new BinaryLeaf<T, TProperty>(_expression);
        }

        public override void Add(T item)
        {
            Leaf.Add(item);
        }

        public override IList<T> Traverse(TreeTraverseMode mode, TreeTraverseOrder order)
        {
            IList<T> collection = new List<T>();
            if (mode == TreeTraverseMode.DepthFirst)
                return (Leaf as BinaryLeaf<T, TProperty>).TraverseDepthFirst(order, collection);
            else
                return (Leaf as BinaryLeaf<T, TProperty>).TraverseBreathFirst(collection);
        }
    }
}
