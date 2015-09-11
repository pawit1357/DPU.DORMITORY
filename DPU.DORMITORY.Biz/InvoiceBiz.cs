using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_INVOICE
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public Boolean FilterPaymentStatus { get; set; }
        public Boolean HasDocumentNo { get; set; }
        public String RoomNumber { get; set; }
        #endregion


        public int ROOM_ID { get; set; }
        #region "COST"
        public int BUILD_ID { get; set; }
        public String BUILD_NAME { get; set; }
        public String BUILD_DESC { get; set; }
        public String BUILD_DESC_EN { get; set; }

        public String ROOM_NUMBER { get; set; }
        public String CUSTOMER_NUMBER { get; set; }
        public String RUNNING_NO { get; set; }
        //public String SAP_DOCNO { get; set; }
        //public String POSTING_DATE { get; set; }
        public String ITEM { get; set; }
        public String ITEM_EN { get; set; }
        //public String AMOUNT { get; set; }
        //public String OTHER { get; set; }
        public String FIRSTNAME { get; set; }
        public String SURNAME { get; set; }
        public String ELEC_METER { get; set; }
        public String WATER_METER { get; set; }

        public String ADDRESS_LINE_1 { get; set; }
        public String ADDRESS_LINE_2 { get; set; }
        public String ADDRESS_LINE_1_EN { get; set; }
        public String ADDRESS_LINE_2_EN { get; set; }
        public String NATION_ID { get; set; }
        public String NATION_CODE { get; set; }

        public Decimal COS_01 { get; set; }
        public Decimal COS_02 { get; set; }
        public Decimal COS_03 { get; set; }
        public Decimal COS_04 { get; set; }
        public Decimal COS_05 { get; set; }
        public Decimal COS_06 { get; set; }
        public Decimal COS_07 { get; set; }
        public Decimal COS_08 { get; set; }
        public Decimal COS_09 { get; set; }


        #endregion

        public IEnumerable SearchPaymentHistory()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                //STATUS = 0 (CHECKIN),INVOICE_TYPE = 2 (CUSTOMER)
                var result = (from i in ctx.TB_INVOICE
                              join c in ctx.TB_CUSTOMER on i.REF_ID equals c.ID
                              join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                              where c.STATUS == 0 && i.INVOICE_TYPE == 2
                              select new
                              {
                                  i.REF_ID,
                                  REF_DESC = c.CUSTOMER_NUMBER,
                                  i.ID,
                                  ROOM_NUMBER = r.NUMBER,
                                  i.POSTING_DATE,
                                  i.SAP_DOCNO,
                                  i.AMOUNT,
                                  c.FIRSTNAME,
                                  c.SURNAME,
                                  i.PAYMENT_STATUS,
                                  i.STATUS
                              }).Union(from i in ctx.TB_INVOICE
                                       join r in ctx.TB_ROOM on i.REF_ID equals r.ID
                                       where i.INVOICE_TYPE == 1
                                       select new
                                       {
                                           i.REF_ID,
                                           REF_DESC = r.NUMBER,
                                           i.ID,
                                           ROOM_NUMBER = r.NUMBER,
                                           i.POSTING_DATE,
                                           i.SAP_DOCNO,
                                           i.AMOUNT,
                                           FIRSTNAME = "",
                                           SURNAME = "",
                                           i.PAYMENT_STATUS,
                                           i.STATUS
                                       });

                if (HasDocumentNo)
                {
                    result = result.Where(x => !String.IsNullOrEmpty(x.SAP_DOCNO));
                }
                if (!String.IsNullOrEmpty(this.RoomNumber))
                {
                    result = result.Where(x => x.ROOM_NUMBER.Equals(this.RoomNumber));
                }
                if (this.REF_ID > 0)
                {
                    result = result.Where(x => x.REF_ID == this.REF_ID);
                }
                if (this.STATUS > 0)
                {
                    result = result.Where(x => x.STATUS == this.STATUS);
                }
                if (FilterPaymentStatus)
                {
                    result = result.Where(x => x.PAYMENT_STATUS == this.PAYMENT_STATUS);
                }
                return result.ToList();
            }
        }

        public IEnumerable preparePostingData()
        {
            //POSTING_DATE
            //COMPANY
            //BA
            //PROFIT_CTR
            //MAIN_TRANS
            //SUB_TRANS
            //SERVICE
            //PAY_BY
            //BP_NO
            //AMOUT
            using (DORMEntities ctx = new DORMEntities())
            {
                //STATUS = 0 (CHECKIN),INVOICE_TYPE = 2 (CUSTOMER)
                var result = from i in ctx.TB_INVOICE
                             join c in ctx.TB_CUSTOMER on i.REF_ID equals c.ID
                             join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                             join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                             join invd in ctx.TB_INVOICE_DETAIL on i.ID equals invd.INVOICE_ID
                             join sv in ctx.TB_M_SERVICE on invd.SERVICE_ID equals sv.ID
                             where c.STATUS == 0 && i.INVOICE_TYPE == 2 && invd.PAY_BY == 0 //Only have student number
                             && String.IsNullOrEmpty(i.SAP_DOCNO) && invd.AMOUNT > 0 && i.POSTING_DATE.Value.Year == this.POSTING_DATE.Value.Year && i.POSTING_DATE.Value.Month == this.POSTING_DATE.Value.Month
                             select new
                             {
                                 BUILD_ID = b.ID,
                                 ROOM_ID = r.ID,
                                 i.ID,
                                 i.POSTING_DATE,
                                 invd.COMPANY,
                                 invd.BA,
                                 invd.PROFIT_CTR,
                                 invd.MAIN_TRANS,
                                 invd.SUB_TRANS,
                                 SERVICE = sv.NAME,
                                 PAY_BY = c.FIRSTNAME + " " + c.SURNAME,
                                 BP_NO = c.CUSTOMER_NUMBER,
                                 AMOUT = invd.AMOUNT
                             };
                           
                if (BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == BUILD_ID);
                }
                if (ROOM_ID > 0)
                {
                    result = result.Where(x => x.ROOM_ID == ROOM_ID);
                }
                return result.ToList();
            }
        }

    }

}
