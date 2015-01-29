using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public interface IEntity
    {
        int GetID();
        bool IsNew();
        void Save(bool skipValidation = false);
    }
}
