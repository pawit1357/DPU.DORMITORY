//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DPU.DORMITORY.Biz.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_TRANSFER_REJECT_LOG
    {
        public int ID { get; set; }
        public Nullable<int> TRANSFER_LOG_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string ERROR_DESC { get; set; }
        public Nullable<System.DateTime> UPDATE_BY { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
    
        public virtual TB_TRANSFER_LOG TB_TRANSFER_LOG { get; set; }
    }
}
