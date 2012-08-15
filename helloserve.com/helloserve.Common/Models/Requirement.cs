using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Requirement")]
    public class Requirement : BaseEntity<Requirement>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int RequirementID { get; internal set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public byte[] Icon { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return RequirementID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion
    }
}
