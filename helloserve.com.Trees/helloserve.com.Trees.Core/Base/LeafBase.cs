using helloserve.com.Trees.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core
{
    public abstract class LeafBase<T> : ILeaf<T>
    {
        public List<ILeaf<T>> Leafs { get; set; }
        public abstract void Add(T item);

        protected abstract ILeaf<T> FindLeaf(T item);
    }
}
