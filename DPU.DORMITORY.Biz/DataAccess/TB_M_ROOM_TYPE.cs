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
    
    public partial class TB_M_ROOM_TYPE
    {
        public TB_M_ROOM_TYPE()
        {
            this.TB_RATES_GROUP = new HashSet<TB_RATES_GROUP>();
            this.TB_ROOM = new HashSet<TB_ROOM>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<TB_RATES_GROUP> TB_RATES_GROUP { get; set; }
        public virtual ICollection<TB_ROOM> TB_ROOM { get; set; }
    }
}