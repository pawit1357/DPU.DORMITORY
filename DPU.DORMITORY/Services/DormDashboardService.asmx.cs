using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DPU.DORMITORY.Services
{
    /// <summary>
    /// Summary description for DormDashboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DormDashboardService : System.Web.Services.WebService
    {

        //private UnitOfWork unitOfWork = new UnitOfWork();
        //private Repository<TB_ROOM> repRoom;

        //public DormDashboardService()
        //{
        //    repRoom = unitOfWork.Repository<TB_ROOM>();

        //}
                public class FruitEnity
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }


        [WebMethod]
        public List<FruitEnity> FruitAnalysis()
        {
            List<FruitEnity> fruitinfo = new List<FruitEnity>();
            DataSet ds = new DataSet();
            //Data Source=as095\SQLEXPRESS;Initial Catalog=DORM;User ID=dorm;Password=P@ssw0rd
            using (SqlConnection con = new SqlConnection(@"Data Source=as095\SQLEXPRESS;User Id=dorm;Password=P@ssw0rd;DataBase=DORM"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select build_id,count(build_id) icount from tb_room group by build_id";
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "FruitAnalysis");
                    }
                }
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["FruitAnalysis"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["FruitAnalysis"].Rows)
                        {
                            fruitinfo.Add(new FruitEnity { Name = dr["build_id"].ToString(), Value = Convert.ToInt32(dr["icount"]) });
                        }
                    }
                }
            }
            return fruitinfo;
        }
        
    }
}
