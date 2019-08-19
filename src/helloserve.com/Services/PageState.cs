using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace helloserve.com
{
    public class PageState : IPageState
    {
        readonly DomainOptions _domainOptions;
        public PageState(IOptions<DomainOptions> domainOptions, IOptions<BlogSyndicationOptionCollection> syndicationOptions)
        {
            _domainOptions = domainOptions.Value;

            MetaCollection = syndicationOptions.Value?
                .Select(x => new MetaCollection()
                {
                    ProviderSource = x.Provider,
                    MetaTags = x.MetaTags.Select(t=>(t.Key.Replace("_", ":"), t.Value)).ToList()
                });
        }

        public string Title { get; set; }

        public string Description { get; set; }

        private string imageUrl;
        public string ImageUrl {
            get
            {
                return imageUrl;
            }
            set
            {
                imageUrl = $"{_domainOptions.Host}{value}";
            }
        }

        public string Type { get; set; }
        public IEnumerable<MetaCollection> MetaCollection { get; set; }

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
            StateChanged();
        }
    }
}
