//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GSA.UnliquidatedObligations.BusinessLayer.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserUser
    {
        public int UserUserId { get; set; }
        public string ParentUserId { get; set; }
        public string ChildUserId { get; set; }
        public Nullable<int> RegionId { get; set; }
    
        public virtual AspNetUser ChildUser { get; set; }
        public virtual AspNetUser ParentUser { get; set; }
        public virtual Region Region { get; set; }
    }
}
