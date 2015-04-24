using System;
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
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }

        public virtual string Controller
        {
            get { return ""; }
        }

        public virtual string UrlName
        {
            get
            {
                string cleaned = Title.Replace(" ", "-").Replace("?", "").Replace("!", "").Replace(",", "").Replace(".", "");
                return cleaned.Substring(0, Math.Min(150, cleaned.Length)).Trim().ToLower();
            }
        }

        public virtual bool IsUrlName(string name)
        {
            return UrlName == name;
        }

        public virtual bool IsId(int id)
        {
            return false;
        }

        public virtual void Save()
        {

        }
    }
}