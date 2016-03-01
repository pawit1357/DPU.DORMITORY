using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_M_SERVICE
    {
        //#region "Property"
        //public CommandNameEnum RowState { get; set; }
        //public decimal Amount { get; set; }
        //#endregion



        //public IEnumerable Search()
        //{
        //    using (DORMEntities ctx = new DORMEntities())
        //    {
        //        var result = from s in ctx.TB_M_SERVICE
        //                     join b in ctx.TB_M_BUILD on s.BUILD_ID equals b.ID
        //                     join c in ctx.TB_M_COST_TYPE on s.COST_ID equals c.ID
        //                     where s.ID > 0
        //                     orderby s.BUILD_ID//, s.NAME ascending
        //                     select new
        //                     {
        //                         ID = s.ID,
        //                         s.BUILD_ID,
        //                         b.PROFIT_CTR,
        //                         BUILD_NAME = b.NAME,
        //                         c.NAME,
        //                         s.MAIN_TRANS,
        //                         s.SUB_TRANS,
        //                         s.GL_ACCOUNT,
        //                         //s.ACCOUNT_NAME
        //                     };

        //        if (this.ID > 0)
        //        {
        //            result = result.Where(x => x.ID == this.ID);
        //        }

        //        if (this.BUILD_ID > 0)
        //        {
        //            result = result.Where(x => x.BUILD_ID == this.BUILD_ID);
        //        }
        //        //if (!string.IsNullOrEmpty(this.NAME))
        //        //{
        //        //    result = result.Where(x => x.NAME.Contains(this.NAME));
        //        //}
        //        //if (!string.IsNullOrEmpty(this.DESCRIPTION))
        //        //{
        //        //    result = result.Where(x => x.DESCRIPTION.Contains(this.DESCRIPTION));
        //        //}
        //        //if (this.INSURANCE_AMOUNT > 0)
        //        //{
        //        //    result = result.Where(x => x.INSURANCE_AMOUNT == this.INSURANCE_AMOUNT);
        //        //}
        //        return result.ToList();
        //    }
        //}


    }

}
