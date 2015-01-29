using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("FeatureMedia")]
    public class FeatureMedia : BaseEntity<FeatureMedia>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int FeatureMediaID { get; set; }
        [Required]
        public int FeatureID { get; set; }
        [Required]
        public int MediaID { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return FeatureMediaID == 0;
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
