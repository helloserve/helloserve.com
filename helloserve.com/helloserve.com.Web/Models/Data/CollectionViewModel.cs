using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models.Data
{
    public class CollectionViewModel : System.Collections.IEnumerable
    {
        public List<ContentDataModel> ListItems { get; set; }

        public ContentDataModel GetById(int id)
        {
            foreach (var item in ListItems)
            {
                if (item.IsId(id))
                    return item;
            }

            return new ContentDataModel();
        }

        public ContentDataModel GetByName(string name)
        {
            foreach (var item in ListItems)
            {
                if (item.GetUrlName == name)
                    return item;
            }

            return new ContentDataModel();
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return ListItems.GetEnumerator();
        }
    }
}