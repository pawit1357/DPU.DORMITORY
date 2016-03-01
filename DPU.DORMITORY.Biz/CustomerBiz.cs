using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_CUSTOMER
    {
        #region "Property"
        public int BUILD_ID { get; set; }
        public CommandNameEnum RowState { get; set; }
        public bool? paymentStatus { get; set; }
        #endregion


        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                var result = from c in ctx.TB_CUSTOMER
                             join r in ctx.TB_ROOM on c.ROOM_ID equals r.ID
                             join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                             join ct in ctx.TB_M_CUSTOMER_TYPE on c.CUSTOMER_TYPE_ID equals ct.ID
                             join cp in ctx.TB_CUSTOMER_PROFILE on c.ID equals cp.CUS_ID
                             where c.STATUS == 0//Show only CheckIn Status
                             orderby c.ID, c.FIRSTNAME ascending
                             select new
                             {
                                 BUILD_ID = b.ID,
                                 BUILD_NAME = b.NAME,
                                 ROOM_NUMBER = r.NUMBER,
                                 c.STATUS,
                                 c.ID,
                                 c.CUSTOMER_NUMBER,
                                 c.FIRSTNAME,
                                 c.SURNAME,
                                 FIRST_LAST = c.FIRSTNAME+"  "+c.SURNAME,
                                 CUSTOMER_TYPE = ct.NAME,
                                 c.PERSONALID,
                                 c.CHECKIN_DATE,
                                 c.RESERV_DATE,
                                 cp.PHONE,
                                 c.ROOM_ID,
                                 c.STAY_ALONE
                             };

                if (this.ROOM_ID > 0)
                {
                    result = result.Where(x => x.ROOM_ID == this.ROOM_ID);
                }
                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == this.BUILD_ID);

                }
                if (!String.IsNullOrEmpty(this.FIRSTNAME))
                {
                    result = result.Where(x => x.FIRST_LAST.Contains(this.FIRSTNAME));

                }
                //if (this.STATUS > 0)
                //{
                //    result = result.Where(x => STATUS == (int)x.STATUS);
                //}
                return result.ToList();
            }
        }
        
    }

}
