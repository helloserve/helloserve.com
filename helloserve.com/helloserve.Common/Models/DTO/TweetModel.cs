using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class Tweet
    {
        public string created_at { get; set; }
        public string text { get; set; }
    }

    public class TweetModel
    {
        public static TweetModel Tweets = new TweetModel();

        public DateTime LastPolled { get; set; }
        public List<Tweet> StoredTweets { get; set; }

        public TweetModel()
        {
            LastPolled = DateTime.Today;
            StoredTweets = new List<Tweet>();
        }

        public bool ShouldPoll()
        {
            return (DateTime.Now - LastPolled).TotalMinutes > 5.0D;
        }
    }
}
