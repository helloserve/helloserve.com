using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Trees.Core.Interfaces
{
    public interface ILeaf<T>
    {
        List<ILeaf<T>> Leafs { get; set; }
        void Add(T item);
    }
}
