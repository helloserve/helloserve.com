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
    
    public partial class FeatureRequirement
    {
        public int FeatureRequirementID { get; set; }
        public int RequirementID { get; set; }
        public int FeatureID { get; set; }
    
        public virtual Feature Feature { get; set; }
        public virtual Requirement Requirement { get; set; }
    }
}
