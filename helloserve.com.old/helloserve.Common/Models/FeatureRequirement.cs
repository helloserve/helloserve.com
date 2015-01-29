using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("FeatureRequirement")]
    public class FeatureRequirement : BaseEntity<FeatureRequirement>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int FeatureRequirementID { get; set; }
        public int RequirementID { get; set; }
        public int FeatureID { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return FeatureRequirementID == 0;
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
