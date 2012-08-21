using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("Feature")]
    public class Feature : BaseEntity<Feature>, IEntity
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int FeatureID { get; internal set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public string Subdomain { get; set; }
        public string CustomPage { get; set; }
        public string IndieDBLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public string Link
        {
            get
            {
                if (string.IsNullOrEmpty(Subdomain))
                    return FeatureID.ToString();
                else
                    return "http://" + Subdomain;
            }
        }

        [NotMapped]
        public string JSLink
        {
            get
            {
                if (string.IsNullOrEmpty(Subdomain))
                    return FeatureID.ToString();
                else
                    return "'http://" + Subdomain + "'";
            }
        }

        #region IENTITY

        public bool IsNew()
        {
            return FeatureID == 0;
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
