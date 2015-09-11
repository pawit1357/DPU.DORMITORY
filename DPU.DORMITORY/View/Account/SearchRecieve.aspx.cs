using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.View.Account
{
    public partial class SearchRecieve : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_INVOICE> repInvoice;
        public SearchRecieve()
        {
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
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
                tmp.HasDocumentNo = true;
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
                case CommandNameEnum.Payment:
                    TB_INVOICE _editInvoice = repInvoice.GetById(PKID);
                    if (_editInvoice != null)
                    {
                        _editInvoice.PAYMENT_STATUS = true;//1= Paymented
                        repInvoice.Update(_editInvoice);
                        #region "MESSAGE RESULT"
                        String errorMessage = repInvoice.errorMessage;
                        if (!String.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(this, errorMessage);
                           
                        }
                        #endregion
                        initialPage();
                    }
                    break;
            }
        }
        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litPaymentStatus = (Literal)e.Row.FindControl("litPaymentStatus");
                LinkButton _btnPay = (LinkButton)e.Row.FindControl("btnPay");

                
                if (_litPaymentStatus != null)
                {
                    if (!String.IsNullOrEmpty(_litPaymentStatus.Text))
                    {
                        switch (_litPaymentStatus.Text)
                        {
                            case "True":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_TRUE;
                                _btnPay.Visible = false;
                                e.Row.ForeColor = System.Drawing.Color.Green;
                                break;
                            case "False":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_FALSE;
                                _btnPay.Visible = true;
                                e.Row.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        #endregion
    }
}