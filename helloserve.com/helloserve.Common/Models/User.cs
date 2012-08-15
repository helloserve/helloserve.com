using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("User")]
    public class User : BaseEntity<User>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int UserID { get; internal set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public bool ReceiveUpdates { get; set; }
        public bool Administrator { get; set; }

        #region IENTITY

        public bool IsNew()
        {
            return UserID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion
    }
}
