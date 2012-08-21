using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("RelatedLink")]
    public class RelatedLink : BaseEntity<RelatedLink>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int RelatedLinkID { get; set; }
        [Required]
        public int FeatureID { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return RelatedLinkID == 0;
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
