using System;

namespace helloserve.com
{
    public class PageState : IPageState
    {
        private string title;
        public string Title { get; set; }

        public string Description { get; set; }

        public event EventHandler OnStateChange;

        public void StateChanged()
        {
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }

        public void Reset()
        {
            Title = string.Empty;
            Description = string.Empty;
            StateChanged();
        }
    }
}
