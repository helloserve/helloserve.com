using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class BlogEditModel : BaseViewModel
    {
        public CollectionViewModel BlogPosts { get; set; }

        public override void Load(object state = null)
        {
            base.Load();

            BlogPosts = Model.News.GetAll(isPublished: null).ToCollectionView();
        }
    }
}