using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_CUSTOMER_FUND
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        //ค้นหา Customer_Fund โดย join tb_m_service
        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from c in ctx.TB_CUSTOMER
                             join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                             join ct in ctx.TB_M_CUSTOMER_TYPE on c.CUSTOMER_TYPE_ID equals ct.ID
                             join cp in ctx.TB_CUSTOMER_PROFILE on c.ID equals cp.CUS_ID
                             where c.STATUS == 0//Show only CheckIn Status
                             orderby c.ID, c.FIRSTNAME ascending
                             select new
                             {
                                 c.STATUS,
                                 c.ID,
                                 c.CUSTOMER_NUMBER,
                                 c.FIRSTNAME,
                                 c.SURNAME,
                                 CUSTOMER_TYPE = ct.NAME,
                                 c.PERSONALID,
                                 c.CHECKIN_DATE,
                                 c.RESERV_DATE,
                                 cp.PHONE,
                                 c.ROOM_ID
                             };

                //if (this.ROOM_ID > 0)
                //{
                //    result = result.Where(x => x.ROOM_ID == this.ROOM_ID);
                //}
                //if (this.STATUS > 0)
                //{
                //    result = result.Where(x => STATUS == (int)x.STATUS);
                //}
                return result.ToList();
            }
        }
        
    }

}
