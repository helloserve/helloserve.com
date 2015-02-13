using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class FeatureViewModel
    {
        public string FeatureId;
        public string Title;
        public string Blurp;
        public string ImageUrl;

        public string GetUrlName {
            get { return Title.Replace(" ", "-").Substring(0, Math.Min(150, Title.Length)).Trim(); }
        }

        public static List<FeatureViewModel> MockList
        {
            get
            {
                return new List<FeatureViewModel>() {
                    new FeatureViewModel() {
                        Title = "Main Feature",
                        Blurp = "The main feature as selected by the editor. This feature doesn't necessarily have higher precendence.",
                        ImageUrl = "http://www.helloserve.com/Thumb/71/Stingray_FeatureHeader.png"
                    },
                    new FeatureViewModel() {
                        Title = "Secondary Feature",
                        Blurp = "The next in line of the main position.",
                        ImageUrl = "http://www.helloserve.com/Thumb/79/BadaChing_FeatureHeader.png"
                    },
                    new FeatureViewModel() {
                        Title = "Third Feature",
                        Blurp = "Some random stuff, nothing to get excited about.",
                        ImageUrl = "http://www.helloserve.com/Thumb/15/html5_FeatureHeader.jpg"
                    },
                    new FeatureViewModel() {
                        Title = "Feature Four",
                        Blurp = "Another project that's probably just dead, or never was to begin with.",
                        ImageUrl = "http://www.helloserve.com/Thumb/11/LudumDare_FeatureHeader.png"
                    },
                    new FeatureViewModel() {
                        Title = "The Fith Element",
                        Blurp = "The first project! The best project!",
                        ImageUrl = "http://www.helloserve.com/Thumb/8/TechDemo_FeatureHeader.jpg"
                    }
                };
            }
        }
    }
}