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
    
    public partial class MENU_ROLE
    {
        public int ROLE_ID { get; set; }
        public int MENU_ID { get; set; }
        public Nullable<bool> IS_REQUIRED_ACTION { get; set; }
        public Nullable<bool> IS_CREATE { get; set; }
        public Nullable<bool> IS_EDIT { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public string UPDATE_BY { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public System.DateTime UPDATE_DATE { get; set; }
    
        public virtual MENU MENU { get; set; }
    }
}