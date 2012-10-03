using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class ForumCategoryDetailModel
    {
        public ForumCategory Category;
        public int? TopicCount;
        public ForumTopic LatestTopic;
    }

    public class ForumTopicDetailModel
    {
        public ForumTopic Topic;
        public int? PostCount;
        public ForumPost LatestPost;
    }

    public class ForumPostDetailModel
    {
        public Forum Forum;
        public ForumCategory Category;
        public ForumTopic Topic;
        public ForumPost Post;
    }
}
