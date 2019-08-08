using System;
using System.Collections.Generic;

namespace helloserve.com
{
    public interface IPageState
    {
        string Title { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        string Type { get; set; }
        List<MetaCollection> MetaCollection { get; }

        event EventHandler OnStateChange;
        void StateChanged();
        void Reset();
    }
}
