//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessObject.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_VASServiceMembershipSerialContentHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_VASServiceMembershipSerialContentHeader()
        {
            this.tbl_VASServiceMembershipSerialContentFooter = new HashSet<tbl_VASServiceMembershipSerialContentFooter>();
        }
    
        public int FK_VASServiceID { get; set; }
        public string theName { get; set; }
        public Nullable<System.DateTime> StratFrom { get; set; }
        public Nullable<System.DateTime> EndAt { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> STime { get; set; }
    
        public virtual tbl_VASServiceMembership tbl_VASServiceMembership { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_VASServiceMembershipSerialContentFooter> tbl_VASServiceMembershipSerialContentFooter { get; set; }
    }
}
