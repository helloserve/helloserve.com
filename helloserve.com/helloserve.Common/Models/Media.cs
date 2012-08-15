using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Media")]
    public class Media : BaseEntity<Media>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int MediaID { get; internal set; }
        public int MediaType { get; set; }
        public string Location { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return MediaID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

    }
}
