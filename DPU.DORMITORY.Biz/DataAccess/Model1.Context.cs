﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DORMEntities : DbContext
    {
        public DORMEntities()
            : base("name=DORMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MENU> MENUs { get; set; }
        public virtual DbSet<MENU_ROLE> MENU_ROLE { get; set; }
        public virtual DbSet<TB_CUSTOMER> TB_CUSTOMER { get; set; }
        public virtual DbSet<TB_CUSTOMER_FUND> TB_CUSTOMER_FUND { get; set; }
        public virtual DbSet<TB_CUSTOMER_PROFILE> TB_CUSTOMER_PROFILE { get; set; }
        public virtual DbSet<TB_INVOICE> TB_INVOICE { get; set; }
        public virtual DbSet<TB_INVOICE_DETAIL> TB_INVOICE_DETAIL { get; set; }
        public virtual DbSet<TB_M_BUILD> TB_M_BUILD { get; set; }
        public virtual DbSet<TB_M_CUSTOMER_TYPE> TB_M_CUSTOMER_TYPE { get; set; }
        public virtual DbSet<TB_M_FUND> TB_M_FUND { get; set; }
        public virtual DbSet<TB_M_NATION> TB_M_NATION { get; set; }
        public virtual DbSet<TB_M_ROOM_TYPE> TB_M_ROOM_TYPE { get; set; }
        public virtual DbSet<TB_M_SERVICE> TB_M_SERVICE { get; set; }
        public virtual DbSet<TB_M_TITLE> TB_M_TITLE { get; set; }
        public virtual DbSet<TB_RATES_GROUP> TB_RATES_GROUP { get; set; }
        public virtual DbSet<TB_RATES_GROUP_DETAIL> TB_RATES_GROUP_DETAIL { get; set; }
        public virtual DbSet<TB_ROOM> TB_ROOM { get; set; }
        public virtual DbSet<TB_ROOM_METER> TB_ROOM_METER { get; set; }
        public virtual DbSet<TB_TRANSFER_LOG> TB_TRANSFER_LOG { get; set; }
        public virtual DbSet<TB_TRANSFER_REJECT_LOG> TB_TRANSFER_REJECT_LOG { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<USERS_LOGGED> USERS_LOGGED { get; set; }
        public virtual DbSet<USERS_ROLE> USERS_ROLE { get; set; }
    }
}
