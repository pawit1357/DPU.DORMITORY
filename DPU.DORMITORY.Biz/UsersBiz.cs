using DPU.DORMITORY.Biz.DataAccess;
using System;
using System.Collections;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class USER
    {
        public USER checkLogin()
        {
            using (var ctx = new DORMEntities())
            {
                return (from ur in ctx.USERS where ur.USER_ID == this.USER_ID && ur.USER_PASSWORD == this.USER_PASSWORD && ur.IS_ACTIVE == true select ur).FirstOrDefault();

            }
        }

        public IEnumerable SearchData()
        {
            using (DORMEntities ctx = new DORMEntities())
            {

                var result = from u in ctx.USERS
                             join ur in ctx.USERS_ROLE on u.ROLE_ID equals ur.ROLE_ID
                             join t in ctx.TB_M_TITLE on u.TITLE_ID equals t.ID
                             select new
                             {
                                 u.ROLE_ID,
                                 TITLE_NAME = t.NAME,
                                 u.USER_ID,
                                 u.USER_PASSWORD,
                                 ROLE_NAME = ur.NAME,
                                 u.FIRST_NAME,
                                 u.LAST_NAME,
                                 u.EMAIL_ADDRESS,
                                 u.LAST_SIGN_IN_DATE,
                                 u.CREATE_DATE,
                                 u.IS_ACTIVE,
                                 u.PHONE_NO
                             };
                if (!String.IsNullOrEmpty(this.USER_ID))
                {
                    result = result.Where(x => x.USER_ID == this.USER_ID);
                }

                if (!String.IsNullOrEmpty(this.USER_ID))
                {
                    result = result.Where(x => x.USER_ID == this.USER_ID);
                }
                if (!String.IsNullOrEmpty(this.USER_PASSWORD))
                {
                    result = result.Where(x => x.USER_PASSWORD == this.USER_PASSWORD);
                }
                return result.ToList();
            }
        }
    }
}
