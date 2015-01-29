using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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

        public string GetPostBrief()
        {
            Regex regex = new Regex(@"(?'open'\[img\])(?'link'http://.*)(?'close'\[/img\])");
            string post = Post.Substring(0, Math.Min(200, Post.Length));
            Match match = regex.Match(post);
            while (match.Success)
            {
                post = post.Replace(match.Groups[0].Value, "<img src=\"" + match.Groups["link"].Value + "\" width=\"100px\" height=\"auto\" />");
                match = regex.Match(post);
            }

            regex = new Regex(@"(?'open'\[img\])(?'link'http://.*)");
            match = regex.Match(post);
            if (match.Success)
            {
                post = post.Replace("[img]", "");
            }

            return post;
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
