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
    
    public partial class tbl_VASServiceMembership
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_VASServiceMembership()
        {
            this.tbl_VASServiceMembership_NewsContent = new HashSet<tbl_VASServiceMembership_NewsContent>();
            this.tbl_VASServiceMembershipSubscriber = new HashSet<tbl_VASServiceMembershipSubscriber>();
        }
    
        public int FK_VASServiceID { get; set; }
        public string SubscriptionKey { get; set; }
        public string UnsubscriptionKey { get; set; }
        public Nullable<bool> IsNewsContent { get; set; }
    
        public virtual tbl_VASService tbl_VASService { get; set; }
        public virtual tbl_VASServiceMembershipSerialContentHeader tbl_VASServiceMembershipSerialContentHeader { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_VASServiceMembership_NewsContent> tbl_VASServiceMembership_NewsContent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_VASServiceMembershipSubscriber> tbl_VASServiceMembershipSubscriber { get; set; }
    }
}