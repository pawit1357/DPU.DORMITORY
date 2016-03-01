using CrystalDecisions.CrystalReports.Engine;
using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.View.Report
{
    public partial class SearchRpt03 : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_ROOM> repRoom;

        public ReportDocument SearchResult
        {
            get { return (ReportDocument)Session["SearchResult"]; }
            set { Session["SearchResult"] = value; }
        }

        public ReportBiz objReport
        {
            //แสดงที่ยังไม่จ่ายตัง
            get
            {
                ReportBiz tmp = new ReportBiz();
                tmp.build_id = Convert.ToInt32(ddlBuild.SelectedValue);
                tmp.room_id = Convert.ToInt32(ddlRoom.SelectedValue);
                tmp.date = Utils.CustomUtils.converFromDDMMYYYY(txtDate.Text);
                return tmp;
            }
        }
        public SearchRpt03()
        {
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
        }

        private void initialPage()
        {

            ddlBuild.DataSource = repBuild.Table.ToList();
            ddlBuild.DataBind();
            ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlRoom.DataSource = repRoom.Table.ToList();
            ddlRoom.DataBind();
            ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            txtDate.Text = DateTime.Now.ToString("MM/yyyy");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
            else
            {
                BindingReport();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetReportDocument();
            BindingReport();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }


        protected void ddlBuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlBuild.SelectedValue))
            {
                int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
                ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList();
                ddlRoom.DataBind();
                ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }
        }


        private void SetReportDocument()
        {
            SearchResult = objReport.getRpt03();
        }

        private void BindingReport()
        {
            CrystalReportViewer1.ReportSource = SearchResult;
        }

    }
}