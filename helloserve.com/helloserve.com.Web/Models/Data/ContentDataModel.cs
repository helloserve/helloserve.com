﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models.Data
{
    public class ContentDataModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Cut { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual string GetUrlName
        {
            get { return Title.Replace(" ", "-").Substring(0, Math.Min(150, Title.Length)).Trim(); }
        }

        public virtual bool IsUrlName(string name)
        {
            return GetUrlName == name;
        }

        public virtual bool IsId(int id)
        {
            return false;
        }
    }
}