using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_M_SPONSOR
    {

        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                var result = from b in ctx.TB_M_SPONSOR select b;


                if (!String.IsNullOrEmpty(this.NAME))
                {
                    result = result.Where(x => x.NAME.Contains(this.NAME));
                }
     

                return result.ToList();
            }

        }
    }

}
