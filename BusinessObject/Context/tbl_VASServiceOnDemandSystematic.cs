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
    
    public partial class tbl_VASServiceOnDemandSystematic
    {
        public int FK_VASServiceID { get; set; }
        public string WebServicePath { get; set; }
        public string WebServiceMethod { get; set; }
        public string WebMethodUsername { get; set; }
        public string WebMethodPassword { get; set; }
        public string WebMethodOtherParameter { get; set; }
    
        public virtual tbl_VASServiceOnDemand tbl_VASServiceOnDemand { get; set; }
    }
}
