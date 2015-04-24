﻿using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class HomeViewModel : ProjectsViewModel
    {
        public ContentDataModel NewsItem { get; set; }

        public override void Load(object state = null)
        {
            base.Load();
            NewsItem = Model.News.GetLatest().AsDataModel();
        }
    }
}