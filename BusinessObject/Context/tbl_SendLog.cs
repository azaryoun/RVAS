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
    
    public partial class tbl_SendLog
    {
        public int ID { get; set; }
        public Nullable<int> FK_VASServiceID { get; set; }
        public Nullable<int> FK_VASMembershipSubscriberID { get; set; }
        public Nullable<System.DateTime> SendDateTime { get; set; }
        public string theText { get; set; }
        public string ReceiverMobile { get; set; }
        public Nullable<decimal> ServicePrice { get; set; }
        public Nullable<byte> theStatus { get; set; }
        public Nullable<int> FK_SendDateID { get; set; }
        public Nullable<int> SerialOrder { get; set; }
    
        public virtual tbl_Date tbl_Date { get; set; }
        public virtual tbl_VASService tbl_VASService { get; set; }
        public virtual tbl_VASServiceMembershipSubscriber tbl_VASServiceMembershipSubscriber { get; set; }
    }
}
