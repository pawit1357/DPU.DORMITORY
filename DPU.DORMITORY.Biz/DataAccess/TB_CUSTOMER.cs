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
    
    public partial class TB_CUSTOMER
    {
        public int ID { get; set; }
        public string CUSTOMER_NUMBER { get; set; }
        public Nullable<int> ROOM_ID { get; set; }
        public Nullable<int> CUSTOMER_TYPE_ID { get; set; }
        public string FIRSTNAME { get; set; }
        public string SURNAME { get; set; }
        public string PERSONALID { get; set; }
        public Nullable<System.DateTime> CHECKIN_DATE { get; set; }
        public Nullable<System.DateTime> RESERV_DATE { get; set; }
        public Nullable<System.DateTime> CHECKOUT_DATE { get; set; }
        public Nullable<System.DateTime> MOVEROOM_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> STATUS { get; set; }
        public string STD_FACULTY { get; set; }
        public string STD_MAJOR { get; set; }
        public string STD_PRO_TYPE_NAME { get; set; }
        public string STD_STATUS { get; set; }
        public Nullable<bool> HAS_STDNUM { get; set; }
        public Nullable<bool> PAYER { get; set; }
        public Nullable<bool> STAY_ALONE { get; set; }
    
        public virtual TB_CUSTOMER_PROFILE TB_CUSTOMER_PROFILE { get; set; }
        public virtual TB_M_CUSTOMER_TYPE TB_M_CUSTOMER_TYPE { get; set; }
    }
}