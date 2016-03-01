using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_RATES_GROUP
    {
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                var result = from rg in ctx.TB_RATES_GROUP
                             join b in ctx.TB_M_BUILD on rg.BUILD_ID equals b.ID
                             orderby rg.ID, rg.BUILD_ID, rg.NAME ascending
                             select new
                             {
                                 ID = rg.ID,
                                 rg.BUILD_ID,
                                 BUILD = b.NAME,
                                 rg.NAME,
                                 rg.DESCRIPTION,
                                 rg.INSURANCE_AMOUNT,

                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == this.BUILD_ID);
                }
                if (!string.IsNullOrEmpty(this.NAME))
                {
                    result = result.Where(x => x.NAME.Contains(this.NAME));
                }
                if (!string.IsNullOrEmpty(this.DESCRIPTION))
                {
                    result = result.Where(x => x.DESCRIPTION.Contains(this.DESCRIPTION));
                }
                if (this.INSURANCE_AMOUNT > 0)
                {
                    result = result.Where(x => x.INSURANCE_AMOUNT == this.INSURANCE_AMOUNT);
                }
                return result.ToList();
            }
        }
    }
}
