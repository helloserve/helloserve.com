using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Error")]
    public class Error : BaseEntity<Error>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ErrorID { get; set; }
        [Required]
        public DateTime ErrorDate { get; set; }
        [Required]
        public int ErrorType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return ErrorID == 0;
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
