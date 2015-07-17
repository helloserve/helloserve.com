using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Base
{
    public abstract class TreeBase<T> : ITree<T>
    {
        public TreeBase()
        {
        }

        public ILeaf<T> Leaf { get; set; }
        public abstract void Add(T item);
        public abstract IList<T> Traverse(TreeTraverseMode mode, TreeTraverseOrder order);
    }
}
