using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Log")]
    public class Log : BaseEntity<Log>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }
        public int? UserID { get; set; }
        public int? FeatureID { get; set; }
        public int? NewsID { get; set; }
        public int? MediaID { get; set; }
        public int? DownloadID { get; set; }

        [Required]
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }

        public Log()
        {
            LogDate = DateTime.Now;
        }

        public User User
        {
            get
            {
                if (UserID.HasValue)
                    return UserRepo.GetByID(UserID.Value);

                return null;
            }
        }

        public Feature Feature
        {
            get {
                if (FeatureID.HasValue)
                    return FeatureRepo.GetByID(FeatureID.Value);

                return null;
            }
        }

        #region IENTITY

        public bool IsNew()
        {
            return LogID == 0;
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
