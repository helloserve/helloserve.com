using helloserve.com.Azure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Azure.Models
{
    public class ProjectsViewModel : BaseViewModel
    {
        public CollectionViewModel Projects { get; set; }

        public override void Load(object state = null)
        {
            Projects = Model.Feature.GetAll().ToCollectionView();
        }
    }
}