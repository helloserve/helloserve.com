using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("SellingPoint")]
    public class SellingPoint : BaseEntity<SellingPoint>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int SellingPointID { get; set; }
        public int? FeatureID { get; set; }
        public string Description { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return SellingPointID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion
    }
}
