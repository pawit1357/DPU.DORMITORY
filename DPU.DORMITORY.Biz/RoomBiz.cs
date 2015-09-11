﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;

namespace DPU.DORMITORY.Biz.DataAccess
{
    [Serializable]
    public partial class TB_ROOM
    {

        public IEnumerable Search()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from r in ctx.TB_ROOM
                             join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                             join rt in ctx.TB_M_ROOM_TYPE on r.ROOM_TYPE_ID equals rt.ID
                             join rg in ctx.TB_RATES_GROUP on r.RATES_GROUP_ID equals rg.ID
                             orderby r.BUILD_ID, r.FLOOR, r.NUMBER ascending
                             select new
                             {
                                 ID = r.ID,
                                 r.BUILD_ID,
                                 BUILD = b.NAME,
                                 r.ROOM_TYPE_ID,
                                 ROOM_TYPE = rt.NAME,
                                 r.RATES_GROUP_ID,
                                 RATE_GROUP = rg.NAME,
                                 r.FLOOR,
                                 r.NUMBER,
                                 r.CUSTOMER_LIMIT
                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == this.BUILD_ID);
                }
                if (this.ROOM_TYPE_ID > 0)
                {
                    result = result.Where(x => x.ROOM_TYPE_ID == this.ROOM_TYPE_ID);
                }
                if (this.FLOOR > 0)
                {
                    result = result.Where(x => x.FLOOR == this.FLOOR);
                }
                if (!String.IsNullOrEmpty(this.NUMBER))
                {
                    result = result.Where(x => x.NUMBER.Equals(this.NUMBER));
                }
                return result.ToList();
            }
        }

        public IEnumerable SearchForRent()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from r in ctx.TB_ROOM
                             join b in ctx.TB_M_BUILD on r.BUILD_ID equals b.ID
                             join rt in ctx.TB_M_ROOM_TYPE on r.ROOM_TYPE_ID equals rt.ID
                             join rg in ctx.TB_RATES_GROUP on r.RATES_GROUP_ID equals rg.ID
                             join sub in
                                 (from c in ctx.TB_CUSTOMER
                                  where c.STATUS == 0
                                  group c by c.ROOM_ID into g
                                  select new
                                  {
                                      ROOM_ID = g.Key,
                                      CNT = (int?)g.Count(),
                                      AMNT = g.Sum(x => x.ID)
                                  }) on r.ID equals sub.ROOM_ID into subG
                             from personCount in subG.DefaultIfEmpty()
                             join sub1 in
                                 (from rr in ctx.TB_RATES_GROUP_DETAIL
                                  where rr.SERVICE_ID == 1
                                  select new
                                  {
                                      rr.RATES_GROUP_ID,
                                      AMOUNT = (int?)rr.AMOUNT
                                  }) on rg.ID equals sub1.RATES_GROUP_ID into sub1G
                             from roomRent in sub1G.DefaultIfEmpty()
                             from rentAmout in sub1G.DefaultIfEmpty()
                             orderby r.BUILD_ID, r.FLOOR, r.NUMBER ascending
                             select new
                             {
                                 ID = r.ID,
                                 r.BUILD_ID,
                                 BUILD = b.NAME,
                                 r.ROOM_TYPE_ID,
                                 ROOM_TYPE = rt.NAME,
                                 r.RATES_GROUP_ID,
                                 RATE_GROUP = rg.NAME,
                                 r.FLOOR,
                                 r.NUMBER,
                                 rg.INSURANCE_AMOUNT,
                                 personCount.CNT,
                                 rentAmout.AMOUNT,
                                 r.STATUS,
                                 r.CUSTOMER_LIMIT
                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.BUILD_ID > 0)
                {
                    result = result.Where(x => x.BUILD_ID == this.BUILD_ID);
                }
                if (this.ROOM_TYPE_ID > 0)
                {
                    result = result.Where(x => x.ROOM_TYPE_ID == this.ROOM_TYPE_ID);
                }
                if (this.FLOOR > 0)
                {
                    result = result.Where(x => x.FLOOR == this.FLOOR);
                }
                if (!String.IsNullOrEmpty(this.NUMBER))
                {
                    result = result.Where(x => x.NUMBER.Equals(this.NUMBER));
                }
                return result.ToList();
            }
        }

        public int GetPerson()
        {
            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from c in ctx.TB_CUSTOMER
                             where c.STATUS == 0
                             group c by c.ROOM_ID into g
                             select new
                             {
                                 ROOM_ID = g.Key,
                                 CNT = (int?)g.Count(),
                                 AMNT = g.Sum(x => x.ID)
                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ROOM_ID == this.ID);
                }

                return (result.FirstOrDefault() != null) ? (int)result.FirstOrDefault().CNT : 0;
            }
        }

        public Boolean hadStdNumInThisRoom()
        {
            Boolean hadStdNum = true;
            using (DORMEntities ctx = new DORMEntities())
            {
                var result = from c in ctx.TB_CUSTOMER
                             where c.STATUS == 0  //CheckIn status
                             && c.ROOM_ID == this.ID
                             select c;


                List<TB_CUSTOMER> listCus = result.ToList();
                foreach (TB_CUSTOMER _val in listCus)
                {
                    if ((bool)_val.HAS_STDNUM)
                    {
                        hadStdNum = false;
                        break;
                    }
                }
            }
            return hadStdNum;
        }

       

    }

}
