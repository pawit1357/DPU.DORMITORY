using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_M_NATION
    {
        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                var result = from b in ctx.TB_M_NATION select b;


                if (!String.IsNullOrEmpty(this.NAME))
                {
                    result = result.Where(x => x.NAME.Contains(this.NAME));
                }
                if (!String.IsNullOrEmpty(this.NAME_EN))
                {
                    result = result.Where(x => x.NAME_EN.Contains(this.NAME_EN));
                }

                return result.ToList();
            }

        }
    }

}
