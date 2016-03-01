using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class USERS_ROLE
    {

        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                var result = from b in ctx.USERS_ROLE select b;


                if (!String.IsNullOrEmpty(this.NAME))
                {
                    result = result.Where(x => x.NAME.Contains(this.NAME));
                }
         

                return result.ToList();
            }

        }
    }

}
