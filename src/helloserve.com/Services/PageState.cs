using System;
using System.Collections.Generic;

namespace helloserve.com
{
    public class PageState : IPageState
    {
        public PageState()
        {
            MetaCollection = new List<MetaCollection>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }
        public List<MetaCollection> MetaCollection { get; set; }

        public event EventHandler OnStateChange;

        public void StateChanged()
        {
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }

        public void Reset()
        {
            Title = string.Empty;
            Description = string.Empty;
            ImageUrl = string.Empty;
            Type = string.Empty;
            MetaCollection = new List<MetaCollection>();
            StateChanged();
        }
    }
}
