using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("News")]
    public class News : BaseEntity<News>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int NewsID { get; internal set; }
        public int? FeatureID { get; set; }
        public string Title { get; set; }
        public string Cut { get; set; }
        public string Post { get; set; }
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

    }
}
