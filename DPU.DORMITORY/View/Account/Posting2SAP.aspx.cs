using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DPU.DORMITORY.Properties;

namespace DPU.DORMITORY.Web.View.Account
{
    public partial class Posting2SAP : System.Web.UI.Page
    {

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Posting2SAP));

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_INVOICE> repInvoice;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;

        public Posting2SAP()
        {
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
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

        public TB_INVOICE obj
        {
            get
            {
                TB_INVOICE tmp = new TB_INVOICE();
                tmp.BUILD_ID = Convert.ToInt32(ddlBuild.SelectedValue);
                tmp.ROOM_ID = Convert.ToInt32(ddlRoom.SelectedValue);
                tmp.POSTING_DATE = CustomUtils.converFromDDMMYYYY(txtPostingDate.Text);
                return tmp;
            }
        }

        private void initialPage()
        {
            txtPostingDate.Text = DateTime.Now.ToString("MM/yyyy");
            ddlBuild.DataSource = repBuild.Table.ToList();
            ddlBuild.DataBind();
            ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlRoom.DataSource = repRoom.Table.ToList();
            ddlRoom.DataBind();
            ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            searchResult = obj.preparePostingData();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;

            btnTransfer.Attributes.Add("onclick", "return confirm('ต้องการที่จะโอนข้อมูลไปยัง SAP ?');");
            pMainPage.Visible = true;
            pTransferResult.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchResult = obj.preparePostingData();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now;
            int success = 0;
            int fail = 0;
            logger.Debug(":: BEGIN TRANSFER ::");
            logger.Debug(String.Format(":: Start Time > {0}, for month > {1}, build > {2}, room > {2}", startDate, txtPostingDate.Text, ddlBuild.SelectedItem.Text, ddlRoom.SelectedItem.Text));

            for (int i = 0; i < gvResult.Rows.Count; i++)
            {
                String POSTING_DATE = gvResult.Rows[i].Cells[0].Text;
                String COMPANY = gvResult.Rows[i].Cells[1].Text;
                String BA = gvResult.Rows[i].Cells[2].Text;
                String PROFIT_CTR = gvResult.Rows[i].Cells[3].Text;
                String MAIN_TRANS = gvResult.Rows[i].Cells[4].Text;
                String SUB_TRANS = gvResult.Rows[i].Cells[5].Text;
                String SERVICE = gvResult.Rows[i].Cells[6].Text;
                String PAY_BY = gvResult.Rows[i].Cells[7].Text;
                String BP_NO = gvResult.Rows[i].Cells[8].Text;
                String AMOUT = gvResult.Rows[i].Cells[9].Text;

                logger.Debug(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}"
                                , i
                                , POSTING_DATE
                                , COMPANY
                                , BA
                                , PROFIT_CTR
                                , MAIN_TRANS
                                , SUB_TRANS
                                , SERVICE
                                , PAY_BY
                                , BP_NO
                                , AMOUT));
                success++;
            }

            DateTime endDate = DateTime.Now;
            Double diff = endDate.Subtract(startDate).TotalSeconds;
            logger.Debug(String.Format(":: End Time > {0},Diff time {1}", endDate, diff));
            logger.Debug(String.Format(":: Total Records > {0},Success > {1},Fail > {2}", gvResult.Rows.Count, success, fail));
            logger.Debug(":: END TRANSFER ::");

            //#region "MESSAGE RESULT"
            //String errorMessage = repInvoice.errorMessage;
            //if (!String.IsNullOrEmpty(errorMessage))
            //{
            //    MessageBox.Show(this, errorMessage);
            //}
            //else
            //{

            //    MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS);

            //    //MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            //}
            //#endregion

            Console.WriteLine();
        }

        #region "GRIDVIEW RESULT"

        protected void gvResult_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            switch (cmd)
            {
                case CommandNameEnum.View:
                    Console.WriteLine();
                    break;
                case CommandNameEnum.Send2SAP:
                    break;
                case CommandNameEnum.ViewLogs:
                    break;

            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Literal _litPaymentStatus = (Literal)e.Row.FindControl("litPaymentStatus");
                //if (_litPaymentStatus != null)
                //{
                //    if (!String.IsNullOrEmpty(_litPaymentStatus.Text))
                //    {
                //        switch (_litPaymentStatus.Text)
                //        {
                //            case "True":
                //                _litPaymentStatus.Text = Resources.MSG_PAYMENT_TRUE;
                //                break;
                //            case "False":
                //                _litPaymentStatus.Text = Resources.MSG_PAYMENT_FALSE;
                //                break;
                //        }
                //    }
                //}
            }
        }
        #endregion

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

    }
}