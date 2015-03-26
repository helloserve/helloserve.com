using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class ProjectsViewModel : BaseViewModel
    {
        public CollectionViewModel Projects { get; set; }

        public override void Load()
        {
            Projects = ProjectDataModel.MockList;
        }
    }
}