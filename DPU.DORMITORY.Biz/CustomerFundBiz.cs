using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_CUSTOMER_PAYER
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public int order { get; set; }
        public String SERVICE_NAME { get; set; }
        public String SPONSOR_NAME { get; set; }
        public String TERM_OF_PAYMENT_NAME { get; set; }
        public Decimal ROOM_RATE { get; set; }

        #endregion

        //ค้นหา Customer_Fund โดย join tb_m_service
        //public IEnumerable Search()
        //{
        //    using (DORMEntities ctx = new DORMEntities())
        //    {


                //payer.SPONSOR_ID = 0;
                //payer.SERVICE_ID = service.ID;
                //payer.SERVICE_NAME = service.NAME;
                //payer.SPONSOR_NAME = String.Format("{0}  {1}", txtName.Text, txtSurname.Text);
                //payer.TERM_OF_PAYMENT_ID = 2;
                //payer.TERM_OF_PAYMENT_NAME = "SHARE (%)";
                //payer.AMOUNT = 100;
                //payer.ROOM_ID = this.ROOM_ID;


                //var result = from c in ctx.TB_CUSTOMER
                //             join cp in ctx.TB_CUSTOMER_PAYER on c.ID equals cp.CUS_ID
                //             join sp in ctx.TB_M_SPONSOR on cp.SPONSOR_ID equals sp.ID
                //             join ss in ctx.TB_M_SERVICE on cp.SERVICE_ID equals ss.ID
                //             join tp in ctx.TB_M_TERM_OF_PAYMENT on cp.TERM_OF_PAYMENT_ID equals tp.ID
                //             select new
                //             {
                //                 SPONSOR_ID = 0;
                //                 SERVICE_ID = service.ID;
                //                 SERVICE_NAME = service.NAME;
                //                 SPONSOR_NAME = String.Format("{0}  {1}", txtName.Text, txtSurname.Text);
                //                 TERM_OF_PAYMENT_ID = 2;
                //                 TERM_OF_PAYMENT_NAME = "SHARE (%)";
                //                 AMOUNT = 100;
                //                 ROOM_ID = this.ROOM_ID;


                //             };

                //if (this.ROOM_ID > 0)
                //{
                //    result = result.Where(x => x.ROOM_ID == this.ROOM_ID);
                //}
                //if (this.STATUS > 0)
                //{
                //    result = result.Where(x => STATUS == (int)x.STATUS);
                //}
                //return result.ToList();
            //}
        //}

    }

}
