using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("ForumCategory")]
    public class ForumCategory : BaseEntity<ForumCategory>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ForumCategoryID { get; set; }
        [Required]
        public int ForumID { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }       
        public int SortOrder { get; set; }

        [NotMapped]
        public IQueryable<ForumTopic> Topics
        {
            get { return ForumRepo.GetTopicsFor(ForumCategoryID); }
        }

        public List<ForumTopicDetailModel> GetTopicsModel()
        {
            return ForumRepo.GetTopicsModel(ForumCategoryID).OrderBy(t=>t.Topic.Sticky).ThenBy(t=>t.Topic.Date).ToList();
        }

        #region IENTITY

        public bool IsNew()
        {
            return ForumCategoryID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

        public void Delete()
        {
            ForumRepo.DeleteTopics(ForumCategoryID);
            base.Delete(this);
        }
    }
}
