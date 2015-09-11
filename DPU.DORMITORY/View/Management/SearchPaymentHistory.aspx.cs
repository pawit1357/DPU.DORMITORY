using CrystalDecisions.CrystalReports.Engine;
using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class SearchPaymentHistory : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_CUSTOMER> repCustomer;

        public SearchPaymentHistory()
        {
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchResult"]; }
            set { Session[GetType().Name + "searchResult"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public TB_CUSTOMER obj
        {
            get
            {
 
                TB_CUSTOMER tmp = new TB_CUSTOMER();
                tmp.STATUS = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                return tmp;
            }
        }

        public TB_ROOM objRoom
        {
            get
            {
                TB_ROOM tmp = new TB_ROOM();
                return tmp;
            }
        }
        public TB_INVOICE objInvoice
        {
            get
            {
                TB_INVOICE tmp = new TB_INVOICE();
                //tmp.RoomNumber = txtRoom.Text;
                tmp.FilterPaymentStatus = false;
                //tmp.PAYMENT_STATUS = false;
                tmp.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);
                return tmp;
            }
        }
        private void initialPage()
        {
            if (objRoom != null)
            {
                gvResult.DataSource = objInvoice.SearchPaymentHistory();
                gvResult.DataBind();
                gvResult.UseAccessibleHeader = true;
                gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }


        #region "GRIDVIEW RESULT"


        protected void gvResult_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            switch (cmd)
            {
                case CommandNameEnum.PrintInvoice:
                    print(0, 0, PKID);
                    break;
            }
        }
        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litPaymentStatus = (Literal)e.Row.FindControl("litPaymentStatus");
                LinkButton _btnPrintRecieve = (LinkButton)e.Row.FindControl("btnPrintRecieve");
                
                if (_litPaymentStatus != null)
                {
                    if (!String.IsNullOrEmpty(_litPaymentStatus.Text))
                    {
                        switch (_litPaymentStatus.Text)
                        {
                            case "True":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_TRUE;
                                _btnPrintRecieve.Visible = true;
                                e.Row.ForeColor = System.Drawing.Color.Green;
                                break;
                            case "False":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_FALSE;
                                _btnPrintRecieve.Visible = false;
                                e.Row.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        #endregion


        private void print(int _roomId, int _customerId, int _invoiceId)
        {
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Configuration.PathReportRecieve);
            crystalReport.SetDatabaseLogon
                (Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            crystalReport.SetParameterValue("P_ROOM_ID", _roomId);
            crystalReport.SetParameterValue("P_CUSTOMER_ID", _customerId);
            crystalReport.SetParameterValue("P_INVOICE_ID", _invoiceId);


            crystalReport.PrintToPrinter(1, true, 0, 0);
        }
    }
}