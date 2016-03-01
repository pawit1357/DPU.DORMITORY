using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_RATES_GROUP_DETAIL
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public String SERVICE_NAME { get; set; }
        #endregion

        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                var result = from rgd in ctx.TB_RATES_GROUP_DETAIL
                             join s in ctx.TB_M_COST_TYPE on rgd.COST_TYPE_ID equals s.ID
                             orderby s.ID//, s.NAME ascending
                             select new
                             {
                                 rgd.RATES_GROUP_ID,
                                 rgd.ID,
                                 SERVICE_NAME = s.NAME,
                                 rgd.AMOUNT,
                                 rgd.VAT,
                             };

                if (this.RATES_GROUP_ID > 0)
                {
                    result = result.Where(x => x.RATES_GROUP_ID == this.RATES_GROUP_ID);
                }
                return result.ToList();
            }
        }

        public List<TB_RATES_GROUP_DETAIL> GetData()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                var result = from rgd in ctx.TB_RATES_GROUP_DETAIL 
                             select rgd;

                if (this.RATES_GROUP_ID > 0)
                {
                    result = result.Where(x => x.RATES_GROUP_ID == this.RATES_GROUP_ID);
                }
                if (this.COST_TYPE_ID > 0)
                {
                    result = result.Where(x => x.COST_TYPE_ID == this.COST_TYPE_ID);
                }
                return result.ToList();
            }
        }

    }

}
