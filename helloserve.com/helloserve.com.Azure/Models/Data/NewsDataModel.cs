using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Models.Data
{
    public class NewsDataModel : ContentDataModel
    {
        public int NewsId { get; set; }
        public int? ProjectId { get; set; }
        public bool IsPublished { get; set; }
        
        public ProjectDataModel Project { get; set; }

        public List<SelectListItem> Projects;

        public void LoadForEdit() {
            Projects = Model.Feature.GetAll().Select(m => new SelectListItem() { Text = m.Name, Value = m.FeatureID.ToString(), Selected = m.FeatureID == ProjectId }).ToList();
            Projects.Insert(0, new SelectListItem() { Text = string.Empty, Value = null, Selected = !ProjectId.HasValue });
        }

        public static CollectionViewModel MockList
        {
            get
            {
                return new CollectionViewModel()
                {
                    ListItems = new List<ContentDataModel>()
                {
                    new NewsDataModel() {
                        NewsId = 1,
                        Title = "Iraq, Ludhiana",
                        Cut = "Iraq is a village in the Ludhiana district of Punjab, India.[1] It is located in the Machhiwara block, around 30 km from the Ludhiana city.[2]",
                        Content = "<p>History[edit] The village gets its name from 'Irakh', the Arabic word for a wild bull breed. The animal was used by the Muslim villagers to cross a seasonal stream on the outskirts of the village. Over time, the pronunciation of the village's name gradually changed to 'Iraq'. The Muslim villagers migrated to Sialkot after the partition of India in 1947.[2] Demographics[edit] As of 2014, the village has around 800 inhabitants. Most of the villagers work as farmers or as workers in the spinning mills located around the Machiwara-Ludhiana Road.[2] References[edit] Jump up ^ 'Rural Housing Report for Financial year 2012-2013'. Government of India. Retrieved 2014-06-19. ^ Jump up to: a b c Shariq Majeed (2014-06-18). 'Punjab’s Iraq craves to be role model of peace'. The Times of India.</p>",
                        CreatedDate = DateTime.Today
                    },

                    new NewsDataModel() {
                        NewsId = 1,
                        Title = "Crap News Post",
                        Cut = "No I messed this one up.",
                        //Content = "<p>Yes, you <b>confusing</b> opinion with a lack of facts.</p>",
                        CreatedDate = new DateTime(2015, 2, 14, 15, 5, 0),
                        ImageUrl = "http://www.helloserve.com/Media/75/construction.jpg"
                    },

                    new NewsDataModel() {
                        NewsId = 1,
                        Title = "Oldest news post",
                        Cut = "Not relevant any more.",
                        //Content = "<p>Just go away now please</p>",
                        CreatedDate = new DateTime(2015, 2, 14, 15, 5, 0),
                        ImageUrl = "http://www.helloserve.com/Media/75/construction.jpg"
                    },
                }
                };
            }
        }

        public override string Controller
        {
            get
            {
                return "blog";
            }
        }

        public override void Save()
        {
            base.Save();

            Model.News.Save(this.AsModel());
        }
    }
}