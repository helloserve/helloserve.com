using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Interfaces
{
    public interface ITree<T>
    {
        ILeaf<T> Leaf { get; set; }
        void Add(T item);
        IList<T> Traverse(TreeTraverseMode mode, TreeTraverseOrder order);
    }
}
