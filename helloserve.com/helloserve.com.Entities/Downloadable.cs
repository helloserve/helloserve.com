//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace helloserve.com.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Downloadable
    {
        public int DownloadableID { get; set; }
        public Nullable<int> FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Feature Feature { get; set; }
    }
}
