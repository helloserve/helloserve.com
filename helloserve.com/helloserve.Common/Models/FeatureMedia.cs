using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    [Table("FeatureMedia")]
    public class FeatureMedia
    {
        [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int FeatureMediaID { get; internal set; }
        [Required]
        public int FeatureID { get; set; }
        [Required]
        public int MediaID { get; set; }
    }
}
