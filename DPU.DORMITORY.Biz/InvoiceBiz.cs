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
        public int Count { get; set; }
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
        public Boolean SHOW_WITHOUT_SPONSOR { get; set; }
        //public Decimal COS_01 { get; set; }
        //public Decimal COS_02 { get; set; }
        //public Decimal COS_03 { get; set; }
        //public Decimal COS_04 { get; set; }
        //public Decimal COS_05 { get; set; }
        //public Decimal COS_06 { get; set; }
        //public Decimal COS_07 { get; set; }
        //public Decimal COS_08 { get; set; }
        //public Decimal COS_09 { get; set; }
        //public String PAYER_NAME { get { return String.Format("{0}  {1}", this.FIRSTNAME, this.SURNAME); } }

        #endregion

        public IEnumerable SearchPaymentHistory()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                //STATUS = 0 (CHECKIN),INVOICE_TYPE = 2 (CUSTOMER)
                var result = (from i in ctx.TB_INVOICE
                              join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
                              join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                              join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                              where c.STATUS == 0 && i.SPONSOR_ID == 0
                              select new
                              {
                                  BUILD_NAME = b.NAME,
                                  BID = r.BUILD_ID.Value,
                                  REF_ID = i.CUS_ID,
                                  REF_DESC = i.PAYER_NAME,
                                  i.ID,
                                  ROOM_NUMBER = r.NUMBER,
                                  i.POSTING_DATE,
                                  i.SAP_DOCNO,
                                  i.AMOUNT,
                                  c.FIRSTNAME,
                                  c.SURNAME,
                                  i.PAYMENT_STATUS,
                                  i.STATUS,
                                  i.SPONSOR_ID
                              })//Add sponsor
                              .Union(from i in ctx.TB_INVOICE
                                     join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
                                     join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                                     join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                                     join sp in ctx.TB_M_SPONSOR on i.SPONSOR_ID equals sp.ID
                                     where c.STATUS == 0 && i.SPONSOR_ID > 0
                                     select new
                                     {
                                         BUILD_NAME = b.NAME,
                                         BID = 0,
                                         REF_ID = i.CUS_ID,
                                         REF_DESC = i.PAYER_NAME,
                                         i.ID,
                                         ROOM_NUMBER = r.NUMBER,
                                         i.POSTING_DATE,
                                         i.SAP_DOCNO,
                                         i.AMOUNT,
                                         FIRSTNAME = sp.NAME,
                                         SURNAME = "",
                                         i.PAYMENT_STATUS,
                                         i.STATUS,
                                         i.SPONSOR_ID
                                     });

                if (HasDocumentNo)
                {
                    result = result.Where(x => !String.IsNullOrEmpty(x.SAP_DOCNO));
                }
                if (!String.IsNullOrEmpty(this.ROOM_NUMBER))
                {
                    result = result.Where(x => x.ROOM_NUMBER.Equals(this.RoomNumber));
                }
                if (!String.IsNullOrEmpty(this.FIRSTNAME))
                {
                    result = result.Where(x => x.FIRSTNAME.Equals(this.FIRSTNAME));
                }
                if (!String.IsNullOrEmpty(this.SAP_DOCNO))
                {
                    result = result.Where(x => x.SAP_DOCNO.Equals(this.SAP_DOCNO));
                }
                if (this.CUS_ID > 0)
                {
                    result = result.Where(x => x.REF_ID == this.CUS_ID);
                }
                //if (this.STATUS > 0)
                //{
                //    result = result.Where(x => x.PAYMENT_STATUS == this.STATUS);
                //}
                if (!FilterPaymentStatus)
                {
                    result = result.Where(x => x.PAYMENT_STATUS == this.PAYMENT_STATUS);
                }
      
                if (SHOW_WITHOUT_SPONSOR)
                {
                    result = result.Where(x => x.SPONSOR_ID == 0);

                }

                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BID == this.BUILD_ID);
                }
                if (this.ROOM_ID > 0)
                {
                    result = result.Where(x => x.BID == this.ROOM_ID);
                }
                Count = result.ToList().Count;
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
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                //STATUS = 0 (CHECKIN),INVOICE_TYPE = 2 (CUSTOMER)
                //var result = (from i in ctx.TB_INVOICE
                //              join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
                //              join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                //              join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                //              join invd in ctx.TB_INVOICE_DETAIL on i.ID equals invd.INVOICE_ID
                //              //join sv in ctx.TB_M_SERVICE on invd.SERVICE_ID equals sv.ID
                //              where c.STATUS == 0 //Only have student number
                //              && String.IsNullOrEmpty(i.SAP_DOCNO) && invd.AMOUNT > 0 && i.POSTING_DATE.Value.Year == this.POSTING_DATE.Value.Year && i.POSTING_DATE.Value.Month == this.POSTING_DATE.Value.Month
                //              select new
                //              {
                //                  BUILD_ID = b.ID,
                //                  ROOM_ID = r.ID,
                //                  i.ID,
                //                  i.POSTING_DATE,
                //                  b.COMPANY,
                //                  b.BA,
                //                  b.PROFIT_CTR,
                //                  //sv.MAIN_TRANS,
                //                  //sv.SUB_TRANS,
                //                  SERVICE = "",//sv.NAME,
                //                  PAY_BY = c.FIRSTNAME + " " + c.SURNAME,
                //                  BP_NO = c.CUSTOMER_NUMBER,
                //                  AMOUT = invd.AMOUNT
                //              }).Union(from i in ctx.TB_INVOICE
                //                       join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
                //                       join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                //                       join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                //                       join invd in ctx.TB_INVOICE_DETAIL on i.ID equals invd.INVOICE_ID
                //                       //join sv in ctx.TB_M_SERVICE on invd.SERVICE_ID equals sv.ID
                //                       where c.STATUS == 0 //Only have student number
                //                       && String.IsNullOrEmpty(i.SAP_DOCNO) && invd.AMOUNT > 0 && i.POSTING_DATE.Value.Year == this.POSTING_DATE.Value.Year && i.POSTING_DATE.Value.Month == this.POSTING_DATE.Value.Month
                //                       select new
                //                       {
                //                           BUILD_ID = b.ID,
                //                           ROOM_ID = r.ID,
                //                           i.ID,
                //                           i.POSTING_DATE,
                //                           b.COMPANY,
                //                           b.BA,
                //                           b.PROFIT_CTR,
                //                           //sv.MAIN_TRANS,
                //                           //sv.SUB_TRANS,
                //                           SERVICE = "",//sv.NAME,
                //                           PAY_BY = c.FIRSTNAME + " " + c.SURNAME,
                //                           BP_NO = c.CUSTOMER_NUMBER,
                //                           AMOUT = invd.AMOUNT
                //                       });

                var result = (from i in ctx.TB_INVOICE
                              join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
                              join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                              join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                              where c.STATUS == 0 //Only have student number
                              && String.IsNullOrEmpty(i.SAP_DOCNO) && i.AMOUNT > 0 && i.POSTING_DATE.Value.Year == this.POSTING_DATE.Value.Year && i.POSTING_DATE.Value.Month == this.POSTING_DATE.Value.Month
                              select new
                                                    {
                                                        //REF_NUM = (i.SPONSOR_ID==0)? i.PAYER_NAME:i.SPONSOR_ID+"",
                                                        i.ID,
                                                        BUILD_ID = b.ID,
                                                        ROOM_ID = r.ID,
                                                        b.NAME,
                                                        r.NUMBER,
                                                        i.POSTING_DATE,
                                                        i.AMOUNT,
                                                        i.PAYER_NAME,
                                                        c.CUSTOMER_NUMBER,
                                                        BID = b.ID,
                                                        b.BA
                                                    });

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

        //public IEnumerable getInvoiceDetail(int _id)
        //{
        //    using (DORMEntities ctx = new DORMEntities())
        //    {
        //        //ctx.Configuration.LazyLoadingEnabled = false;
        //        //ctx.Configuration.ProxyCreationEnabled = false;

        //        var result = (from i in ctx.TB_INVOICE
        //                      join idx in ctx.TB_INVOICE_DETAIL on i.ID equals idx.INVOICE_ID
        //                      join c in ctx.TB_CUSTOMER on i.CUS_ID equals c.ID
        //                      join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
        //                      join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
        //                      //join x in ctx.TB_M_SERVICE on ID equals idx.COST_TYPE_ID
        //                      where i.ID == _id //Only have student number
        //                      select new
        //                      {
        //                          b.BA,
        //                          //REF_NUM = (i.SPONSOR_ID == 0) ? c.CUSTOMER_NUMBER : i.SPONSOR_ID + "",
        //                          //i.ID,
        //                          //BUILD_ID = b.ID,
        //                          //ROOM_ID = r.ID,
        //                          //b.NAME,
        //                          //r.NUMBER,
        //                          //i.POSTING_DATE,
        //                          //i.AMOUNT,
        //                          //i.PAYER_NAME
        //                      });

        //        //if (BUILD_ID > 0)
        //        //{
        //        //    result = result.Where(x => x.BUILD_ID == BUILD_ID);
        //        //}
        //        //if (ROOM_ID > 0)
        //        //{
        //        //    result = result.Where(x => x.ROOM_ID == ROOM_ID);
        //        //}
        //        return result.ToList();
        //    }
        //}



        public List<InvDetail> getPrepareInvoiceData()
        {

            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from CUSTOMER in ctx.TB_CUSTOMER
                             join ROOM in ctx.TB_ROOM on CUSTOMER.ROOM_ID equals ROOM.ID
                             join BUILD in ctx.TB_M_BUILD on ROOM.BUILD_ID equals BUILD.ID
                             join CUSTOMER_PAYER in ctx.TB_CUSTOMER_PAYER on CUSTOMER.ID equals CUSTOMER_PAYER.CUS_ID
                             //join M_SERVICE in ctx.TB_M_SERVICE on CUSTOMER_PAYER.SERVICE_ID equals M_SERVICE.ID
                             join TERM_OF_PAYMENT in ctx.TB_M_TERM_OF_PAYMENT on CUSTOMER_PAYER.TERM_OF_PAYMENT_ID equals TERM_OF_PAYMENT.ID
                             join RATES_GROUP_DETAIL in ctx.TB_RATES_GROUP_DETAIL on ROOM.RATES_GROUP_ID equals RATES_GROUP_DETAIL.RATES_GROUP_ID
                             join SPONSOR in ctx.TB_M_SPONSOR on CUSTOMER_PAYER.SPONSOR_ID equals SPONSOR.ID
                             where CUSTOMER.STATUS == 0 //&& M_SERVICE.ID == RATES_GROUP_DETAIL.SERVICE_ID
                             orderby CUSTOMER.ID
                             select new InvDetail
                             {
                                 SPONSOR_ID = CUSTOMER_PAYER.SPONSOR_ID,
                                 CUS_ID = CUSTOMER.ID,
                                 BUILD_ID = BUILD.ID,
                                 ROOM_ID = ROOM.ID,
                                 TERM_OF_PAYMENT_ID = TERM_OF_PAYMENT.ID,
                                 ROOM_NUMBER = ROOM.NUMBER,
                                 //M_SERVICE_ID = M_SERVICE.ID,
                                 M_SERVICE_NAME = "",//M_SERVICE.NAME,
                                 CUSTOMER_NAME = CUSTOMER.FIRSTNAME + "  " + CUSTOMER.SURNAME,
                                 PAYER_NAME = (CUSTOMER_PAYER.SPONSOR_ID == 0) ? CUSTOMER.FIRSTNAME + "  " + CUSTOMER.SURNAME : SPONSOR.NAME,
                                 TERM_OF_PAYMENT_NAME = TERM_OF_PAYMENT.NAME,
                                 RATE_AMOUNT = RATES_GROUP_DETAIL.AMOUNT,
                                 RATE_UNIT = RATES_GROUP_DETAIL.UNIT,
                                 CUSTOMER_PAYER_AMOUNT = CUSTOMER_PAYER.AMOUNT,
                                 RATES_GROUP_DETAIL_AMOUNT = RATES_GROUP_DETAIL.AMOUNT,
                             };
                if (this.ROOM_ID > 0)
                {
                    result = result.Where(x => x.ROOM_ID == this.ROOM_ID);
                }
                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == this.BUILD_ID);
                }
                if (!String.IsNullOrEmpty(this.ROOM_NUMBER))
                {
                    result = result.Where(x => x.ROOM_NUMBER.Equals(this.ROOM_NUMBER));
                }
                return result.ToList();
            }
        }
    }

}
[Serializable]
public class InvDetail
{
    public String CUSTOMER_NAME { get; set; }
    public int ID { get; set; }
    public int? SPONSOR_ID { get; set; }
    public int? CUS_ID { get; set; }
    public int BUILD_ID { get; set; }
    public int ROOM_ID { get; set; }
    public String ROOM_NUMBER { get; set; }
    public int COST_TYPE_ID { get; set; }
    public String M_SERVICE_NAME { get; set; }
    public String PAYER_NAME { get; set; }
    public String TERM_OF_PAYMENT_NAME { get; set; }

    public Decimal? RATE_AMOUNT { get; set; }
    public int? RATE_UNIT { get; set; }
    public Decimal? PAYMENT_AMOUNT { get; set; }
    public Decimal? TOTAL_AMOUNT { get; set; }
    public Decimal? CUSTOMER_PAYER_AMOUNT { get; set; }
    public Decimal? RATES_GROUP_DETAIL_AMOUNT { get; set; }
    public int? TERM_OF_PAYMENT_ID { get; set; }
    public String REMARK { get; set; }
    public int row_type { get; set; }
    public Boolean IS_ACTIVE { get; set; }
    public String TERM_DETAIL { get { return String.IsNullOrEmpty(TERM_OF_PAYMENT_NAME) ? "" : (PAYMENT_AMOUNT == null) ? string.Empty : String.Format("{0} = {1}", TERM_OF_PAYMENT_NAME, CUSTOMER_PAYER_AMOUNT.Value.ToString("N0")); } }
}

/*
 SELECT 
  M_SERVICE.ID,
  M_SERVICE.NAME,
  (CASE WHEN CUSTOMER_PAYER.SPONSOR_ID = 0 THEN  CUSTOMER.FIRSTNAME ELSE  SPONSOR.NAME END) PAYER_NAME, 
  -- TERM_OF_PAYMENT.ID,
  TERM_OF_PAYMENT.NAME,
  RATES_GROUP_DETAIL.UNIT,
  RATES_GROUP_DETAIL.AMOUNT RATE_AMOUNT,
  (CASE WHEN TERM_OF_PAYMENT.ID = 2	THEN  ((CUSTOMER_PAYER.AMOUNT/100)* RATES_GROUP_DETAIL.AMOUNT )-RATES_GROUP_DETAIL.AMOUNT
  ELSE  CUSTOMER_PAYER.AMOUNT-RATES_GROUP_DETAIL.AMOUNT  END) RESULT_AMOUT
  FROM TB_CUSTOMER CUSTOMER 
  LEFT JOIN TB_ROOM ROOM ON CUSTOMER.ROOM_ID = ROOM.ID
  LEFT JOIN TB_M_BUILD BUILD ON ROOM.BUILD_ID = BUILD.ID
  LEFT JOIN TB_CUSTOMER_PAYER CUSTOMER_PAYER ON CUSTOMER.ID = CUSTOMER_PAYER.CUS_ID
  LEFT JOIN TB_M_SERVICE M_SERVICE ON CUSTOMER_PAYER.SERVICE_ID = M_SERVICE.ID
  LEFT JOIN TB_M_TERM_OF_PAYMENT TERM_OF_PAYMENT ON CUSTOMER_PAYER.TERM_OF_PAYMENT_ID = TERM_OF_PAYMENT.ID
  LEFT JOIN TB_RATES_GROUP_DETAIL RATES_GROUP_DETAIL ON ROOM.RATES_GROUP_ID = RATES_GROUP_DETAIL.RATES_GROUP_ID
  LEFT JOIN TB_M_SPONSOR SPONSOR ON CUSTOMER_PAYER.SPONSOR_ID = SPONSOR.ID
WHERE CUSTOMER.STATUS = 0 -- สถานะยังพัก
  AND ROOM.ID=122
  AND M_SERVICE.ID = RATES_GROUP_DETAIL.SERVICE_ID
 */