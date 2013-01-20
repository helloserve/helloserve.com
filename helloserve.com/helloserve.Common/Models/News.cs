using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace helloserve.Common
{
    [Table("News")]
    public class News : BaseEntity<News>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int NewsID { get; set; }
        public int? FeatureID { get; set; }
        public string Title { get; set; }
        public string Cut { get; set; }
        public string Post { get; set; }
        public int? HeaderImageID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return NewsID == 0;
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

        public string GetCutBland()
        {
            Regex regex = new Regex(@"(?'open'<a.*>)(?'text'.*)(?'close'</a>)");
            string cut = Cut;
            Match match = regex.Match(cut);
            while (match.Success)
            {
                cut = cut.Replace(match.Groups[0].Value, match.Groups["text"].Value);
                match = regex.Match(cut);
            };

            return cut;
        }

        public void ImportCut()
        {
            string cut = Cut;
            cut = cut.Replace("&op&c", "").Replace("&o/p&c", "").Replace("&o", "<").Replace("&c", ">");;
            Cut = cut;
        }

        public void ImportPost()
        {
            string post = Post;

            post = post.Replace("&o", "<").Replace("&c", ">");

            Regex regex = new Regex(@"(?'open'\[image ')(?'name'.*)(?'close''\])");
            Match match = regex.Match(post);
            while (match.Success)
            {
                post = post.Replace(match.Groups[0].Value, string.Format("<img src=\"/Media/{0}\" alt=\"{0}\" />", match.Groups["name"].Value));
                match = regex.Match(post);
            }

            Post = post;
        }
    }
}
