using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("ForumPost")]
    public class ForumPost : BaseEntity<ForumPost>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ForumPostID { get; set; }
        [Required]
        public int ForumTopicID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, MaxLength()]
        public string Post { get; set; }

        [NotMapped]
        public User User
        {
            get
            {
                User user = UserRepo.GetAll().Where(u => u.UserID == UserID).SingleOrDefault();
                return (user == null) ? new User() { UserID = -1, Username = "Unknown" } : user;
            }
        }

        #region IENTITY

        public bool IsNew()
        {
            return ForumPostID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

        public void Delete()
        {
            base.Delete(this);
        }
    }
}
