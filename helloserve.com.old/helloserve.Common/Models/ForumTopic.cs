using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("ForumTopic")]
    public class ForumTopic : BaseEntity<ForumTopic>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ForumTopicID { get; set; }
        [Required]
        public int ForumCategoryID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool Sticky { get; set; }
        [Required]
        public bool Locked { get; set; }

        [NotMapped]
        public User User
        {
            get
            {
                User user = UserRepo.GetAll().Where(u => u.UserID == UserID).SingleOrDefault();
                return (user == null) ? new User() { UserID = -1, Username = "Unknown" } : user;
            }
        }

        public IQueryable<ForumPost> Posts()
        {
            return ForumRepo.GetPostsFor(ForumTopicID);
        }

        public IQueryable<ForumPost> Posts(int page, int pageTotal)
        {
            if (page == 0)
                return Posts().OrderBy(p => p.Date).Take(pageTotal);

            int offset = page * pageTotal;
            return Posts().OrderBy(p => p.Date).Skip(offset).Take(pageTotal);
        }

        public int PostCount()
        {
            return Posts().Count();
        }

        public void InitialPost(string post)
        {
            if (IsNew())
                Save();

            ForumRepo.CreatePost(ForumTopicID, UserID, post);
        }

        #region IENTITY

        public bool IsNew()
        {
            return ForumTopicID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

        public void Delete()
        {
            ForumRepo.DeletePosts(ForumTopicID);
            base.Delete(this);
        }
    }
}
