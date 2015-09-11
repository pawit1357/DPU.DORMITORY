using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DPU.DORMITORY.Services
{
    /// <summary>
    /// Summary description for DashboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DashboardService : System.Web.Services.WebService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_ROOM> repRoom;

        public DashboardService()
        {
            repRoom = unitOfWork.Repository<TB_ROOM>();

        }

        [WebMethod]
        public List<TB_ROOM> GetRoomStatus()
        {
            
            return repRoom.Table.ToList();
        }
    }
}
