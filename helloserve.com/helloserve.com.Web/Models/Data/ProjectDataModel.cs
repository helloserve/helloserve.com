using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models.Data
{
    public class ProjectDataModel : ContentDataModel
    {
        public int FeatureId;
        public string Blurp;

        public override bool IsId(int id)
        {
            return base.IsId(id) || (FeatureId == id);
        }

        public static CollectionViewModel MockList
        {
            get
            {
                return new CollectionViewModel()
                {
                    ListItems = new List<ContentDataModel>() {
                    new ProjectDataModel() {
                        FeatureId = 1,
                        Title = "Main Feature",
                        Cut = "Blah Blah Blah",
                        CreatedDate = DateTime.Today,
                        Blurp = "The main feature as selected by the editor. This feature doesn't necessarily have higher precendence.",
                        Content = "Main Content!",
                        ImageUrl = "http://www.helloserve.com/Thumb/71/Stingray_FeatureHeader.png"
                    },
                    new ProjectDataModel() {
                        FeatureId = 2,
                        Title = "Secondary Feature",
                        Cut = "Blah Blah Blah Blah Blah",
                        CreatedDate = DateTime.Today,
                        Blurp = "The next in line of the main position.",
                        ImageUrl = "http://www.helloserve.com/Thumb/79/BadaChing_FeatureHeader.png"
                    },
                    new ProjectDataModel() {
                        FeatureId = 3,
                        Title = "Third Feature",
                        Cut = "Blah Blah Blah",
                        CreatedDate = DateTime.Today,
                        Blurp = "Some random stuff, nothing to get excited about.",
                        ImageUrl = "http://www.helloserve.com/Thumb/15/html5_FeatureHeader.jpg"
                    },
                    new ProjectDataModel() {
                        FeatureId = 4,
                        Title = "Feature Four",
                        Cut = "Blah Blah Blah",
                        CreatedDate = DateTime.Today,
                        Blurp = "Another project that's probably just dead, or never was to begin with.",
                        ImageUrl = "http://www.helloserve.com/Thumb/11/LudumDare_FeatureHeader.png"
                    },
                    new ProjectDataModel() {
                        FeatureId = 5,
                        Title = "The Fifth Element",
                        Cut = "So So So So So So So So",
                        CreatedDate = DateTime.Today,
                        Blurp = "The first project! The best project!",
                        ImageUrl = "http://www.helloserve.com/Thumb/8/TechDemo_FeatureHeader.jpg"
                    }
                }
                };
            }
        }
    }
}