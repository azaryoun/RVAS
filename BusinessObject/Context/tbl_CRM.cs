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
    
    public partial class tbl_CRM
    {
        public int ID { get; set; }
        public Nullable<int> FK_OwnerUserID { get; set; }
        public Nullable<int> FK_CRMCategoryID { get; set; }
        public string Remarks { get; set; }
        public string ContactNo { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentMime { get; set; }
        public string RequestNo { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<System.DateTime> ResponseDate { get; set; }
        public string ResponseRemarks { get; set; }
    
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_CRMCategory tbl_CRMCategory { get; set; }
    }
}