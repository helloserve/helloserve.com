using System;

namespace helloserve.com
{
    public class PageState
    {
        public static PageState Current { get; internal set; }

        static PageState()
        {
            Current = PageState.Default;
        }

        public static void Reset()
        {
            PageState prev = Current;
            Current = PageState.Default;
            Current.OnStateChange = prev.OnStateChange;
            Current.StateChanged();
        }

        static PageState Default => new PageState()
        {            
            Title = "helloserve.com",
            Description = string.Empty
        };

        private string title;
        public string Title {
            get
            {
                return title;
            }
            set
            {
                title = value;
                StateChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                StateChanged();
            }
        }

        public event EventHandler OnStateChange;

        private void StateChanged()
        {
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
