using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Downloadable")]
    public class Downloadable: BaseEntity<Downloadable>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int DownloadableID { get; set; }
        public int? FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return DownloadableID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion
    }
}
