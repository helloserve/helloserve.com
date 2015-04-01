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

        public override void Load()
        {
            base.Load();

            BlogPosts = Model.News.GetAll().ToCollectionView();            
        }
    }
}