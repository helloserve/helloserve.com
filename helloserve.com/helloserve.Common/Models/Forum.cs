using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Forum")]
    public class Forum : BaseEntity<Forum>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ForumID { get; internal set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public bool Internal { get; set; }

        [NotMapped]
        public IQueryable<ForumCategory> Categories
        {
            get { return ForumRepo.GetCategoriesFor(ForumID); }
        }

        public List<ForumCategoryDetailModel> GetCategoriesModel()
        {
            return ForumRepo.GetCategoriesModel(ForumID).OrderBy(c=>c.Category.SortOrder).ToList();
        }

        #region IENTITY

        public bool IsNew()
        {
            return ForumID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

        public void Delete()
        {
            ForumRepo.DeleteCategories(ForumID);
            base.Delete(this);
        }
    }
}
