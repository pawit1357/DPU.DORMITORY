using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_M_BUILD
    {
        public String IdText { get { return this.ID.ToString(); } }




        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                var result = from b in ctx.TB_M_BUILD select b;


                if (!String.IsNullOrEmpty(this.NAME))
                {
                    result = result.Where(x => x.NAME.Contains(this.NAME));
                }
                if (!String.IsNullOrEmpty(this.DESCRIPTION))
                {
                    result = result.Where(x => x.DESCRIPTION.Contains(this.DESCRIPTION));
                }

                return result.ToList();
            }
        }
        


    }

}
