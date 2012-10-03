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
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }

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

        public void Delete()
        {
            base.Delete(this);
        }
    }
}
