using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class BlogViewModel : BaseViewModel
    {
        public CollectionViewModel BlogPosts { get; set; }

        public override void Load(object state = null)
        {
            base.Load(state);

            BlogPosts = Model.News.GetAll(isPublished: (bool?)state).ToCollectionView();
        }
    }
}