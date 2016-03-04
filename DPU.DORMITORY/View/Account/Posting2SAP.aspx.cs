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
using System.Collections.Generic;
using Gen_Bapizarfi_01_Bapizcmi003;

namespace DPU.DORMITORY.Web.View.Account
{
    public partial class Posting2SAP : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Posting2SAP));

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_INVOICE> repInvoice;
        private Repository<TB_INVOICE_DETAIL> repInoviceDetail;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_M_SERVICE> repMService;
        private Repository<TB_CUSTOMER> repCustomer;

        public Posting2SAP()
        {
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
            repInoviceDetail = unitOfWork.Repository<TB_INVOICE_DETAIL>();
            repMService = unitOfWork.Repository<TB_M_SERVICE>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();

            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchResult"]; }
            set { Session[GetType().Name + "searchResult"] = value; }
        }
        public List<InvDetail> InvDetails
        {
            get { return (List<InvDetail>)Session[GetType().Name + "InvDetails"]; }
            set { Session[GetType().Name + "InvDetails"] = value; }
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

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }


        private void initialPage()
        {
            txtPostingDate.Text = DateTime.Now.ToString("MM/yyyy");
            List<TB_M_BUILD> build = repBuild.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();
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
            //int success = 0;
            //int fail = 0;
            //logger.Debug(":: BEGIN TRANSFER ::");
            //logger.Debug(String.Format(":: Start Time > {0}, for month > {1}, build > {2}, room > {2}", startDate, txtPostingDate.Text, ddlBuild.SelectedItem.Text, ddlRoom.SelectedItem.Text));

            for (int i = 0; i < gvResult.Rows.Count; i++)
            {
                int inv_id = Convert.ToInt32(gvResult.Rows[i].Cells[0].Text);
                TB_INVOICE invoice = repInvoice.Table.Where(x => x.ID == inv_id).FirstOrDefault();
                int building_id = Convert.ToInt32(gvResult.Rows[i].Cells[1].Text);
                String ba = gvResult.Rows[i].Cells[7].Text;
                String payer_name = gvResult.Rows[i].Cells[6].Text;
                String ref_num = gvResult.Rows[i].Cells[5].Text;

                //string id = "";
                string Ex_Doc = "";
                FKKKO im_header = new FKKKO();
                FKKOPTable im_items = new FKKOPTable();

                String conStr = String.Format("ASHOST={0} SYSNR={1} CLIENT={2} USER={3} PASSWD={4}",
                          Configuration.SAP_ASHOST, Configuration.SAP_SYSNR, Configuration.SAP_CLIENT, Configuration.SAP_USER, Configuration.SAP_PASSWD);
                SAPProxy1 proxy = new SAPProxy1(conStr);

                try
                {
                    //Add Header
                    im_header.Budat = DateTime.Now.ToString("yyyyMMdd");
                    im_header.Bldat = DateTime.Now.ToString("yyyyMMdd");
                    im_header.Opbel = ref_num;//Student Number (TEST id:000485520015)
                    //Add Item


                    List<TB_M_SERVICE> listService = repMService.Table.Where(x => x.BUILD_ID == building_id).ToList();
                    if (listService.Count > 0)
                    {
                        List<TB_INVOICE_DETAIL> listInv = repInoviceDetail.Table.Where(x => x.INVOICE_ID == inv_id).ToList();
                        foreach (TB_INVOICE_DETAIL invDetai in listInv)
                        {

                            TB_M_SERVICE service = listService.Where(x => x.ID == invDetai.COST_TYPE_ID.Value).FirstOrDefault();
                            if (service == null)
                            {
                                Console.WriteLine();
                            }
                            else
                            {
                                FKKOP item = new FKKOP();
                                item.Gsber = ba;//business Area (4000+DPU1 = 4001,4000+DPU2 = 4002)
                                item.Tvorg = service.SUB_TRANS + "";//sub tran
                                item.Optxt = payer_name + "[" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + "]";//Text
                                item.Betrh = Convert.ToDecimal(invDetai.AMOUNT);//Amount
                                item.Betrw = Convert.ToDecimal(invDetai.AMOUNT);//Amount
                                item.Budat = DateTime.Now.ToString("yyyyMMdd");//Posting Date วันที่ทำรายการ
                                item.Bldat = DateTime.Now.ToString("yyyyMMdd");//Document Date วันที่ทำรายการ
                                item.Faedn = DateTime.Now.ToString("yyyyMMdd");//Due Date 
                                item.Faeds = DateTime.Now.ToString("yyyyMMdd");//Due Date 
                                im_items.Add(item);
                            }
                        }

                        //Test Data
                        Console.WriteLine("---------HEADER---------");
                        Console.WriteLine("STD_ID: " + ref_num);
                        foreach (FKKOP fkkop in im_items)
                        {
                            //logger.Debug(fkkop.Gsber + "," + fkkop.Tvorg + "," + fkkop.Optxt + "," + fkkop.Betrh);
                        }

                        //Check Connection Status
                        try
                        {


                            proxy.Zarfi_01(im_header, out Ex_Doc, ref im_items);
                        }
                        catch (SAP.Connector.BapiException exxx)
                        {
                            Console.WriteLine();
                        }
                        proxy.Timeout = 5;
                        SAP.Connector.SAPConnectionPool.ReturnConnection(proxy.Connection);
                        proxy.Dispose();
                        //Update Doc No
                        invoice.SAP_DOCNO = Ex_Doc;
                        repInvoice.Update(invoice);

                        //logger.Debug(String.Format("DOC NO:{0}", Ex_Doc));
                        //success++;
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                    Console.WriteLine("---------END---------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    //System.Windows.Forms.MessageBox.Show(e.Message + "," + std_num);
                }

            }

            DateTime endDate = DateTime.Now;
            Double diff = endDate.Subtract(startDate).TotalSeconds;
            //logger.Debug(String.Format(":: End Time > {0},Diff time {1}", endDate, diff));
            //logger.Debug(String.Format(":: Total Records > {0},Success > {1},Fail > {2}", gvResult.Rows.Count, success, fail));
            //logger.Debug(":: END TRANSFER ::");

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
                //case CommandNameEnum.Send2SAP:
                //    break;
                case CommandNameEnum.ViewLogs:
                    break;
                case CommandNameEnum.EDIT_INVOICE:

                    TB_INVOICE inv = new TB_INVOICE();
                    inv.INV_ID = PKID;

                    this.InvDetails = inv.getPrepareInvoiceDataByInvID();
                    gvInvoiceDetail.DataSource = this.InvDetails;
                    gvInvoiceDetail.DataBind();

                    ModolPopupExtender.Show();
                    break;

            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        #endregion
        #region "Invoice Detail
        protected void gvInvoiceDetail_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvInvoiceDetail.EditIndex = e.NewEditIndex;
            gvInvoiceDetail.DataSource = this.InvDetails;
            gvInvoiceDetail.DataBind();
            //Show Popup
            ModolPopupExtender.Show();
        }

        protected void gvInvoiceDetail_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int pkid = Convert.ToInt32(gvInvoiceDetail.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtPAY_AMOUNT = (TextBox)gvInvoiceDetail.Rows[e.RowIndex].FindControl("txtPAY_AMOUNT");
            TextBox _txtREMARKT = (TextBox)gvInvoiceDetail.Rows[e.RowIndex].FindControl("txtREMARKT");

            InvDetail detail = this.InvDetails.Find(x => x.ID == pkid);
            if (detail != null)
            {
                detail.PAYMENT_AMOUNT = Convert.ToDecimal(_txtPAY_AMOUNT.Text);
                detail.REMARK = _txtREMARKT.Text;
            }
            gvInvoiceDetail.EditIndex = -1;
            gvInvoiceDetail.DataSource = this.InvDetails;
            gvInvoiceDetail.DataBind();
            //Show Popup
            ModolPopupExtender.Show();
        }

        protected void gvInvoiceDetail_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int _id = Convert.ToInt32(gvInvoiceDetail.DataKeys[e.Row.RowIndex].Values[0].ToString());
                InvDetail _inv = this.InvDetails.Find(x => x.ID == _id);
                if (_inv != null)
                {
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");

                    CommandNameEnum cmd = (CommandNameEnum)Enum.ToObject(typeof(CommandNameEnum), (int)_inv.row_type);
                    switch (cmd)
                    {
                        case CommandNameEnum.ITEM:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            if (btnEdit != null)
                            {
                                btnEdit.Visible = true;
                            }
                            break;
                        case CommandNameEnum.GROUP:
                            e.Row.ForeColor = System.Drawing.Color.Green;
                            if (btnEdit != null)
                            {
                                btnEdit.Visible = false;
                            }
                            break;
                        case CommandNameEnum.OTHER:
                            e.Row.ForeColor = System.Drawing.Color.Violet;
                            if (btnEdit != null)
                            {
                                btnEdit.Visible = false;
                            }
                            break;
                    }
                }
            }
        }

        protected void gvInvoiceDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInvoiceDetail.EditIndex = -1;
            gvInvoiceDetail.DataSource = this.InvDetails;
            gvInvoiceDetail.DataBind();
            //Show Popup
            ModolPopupExtender.Show();
        }

        protected void gvInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _deletePK = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            InvDetail _inv = this.InvDetails.Where(x => x.ID == _deletePK).FirstOrDefault();
            if (_inv != null)
            {
                _inv.row_type = Convert.ToInt32(CommandNameEnum.Delete);
                #region "MESSAGE RESULT"
                String errorMessage = repInvoice.errorMessage;
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(this, errorMessage);
                }
                else
                {
                    MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS);
                }
                #endregion
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


        protected void OK_Click(object sender, EventArgs e)
        {
            int inv_id = 0;
            //Edit Item
            foreach (InvDetail invDetail in this.InvDetails)
            {
                inv_id = invDetail.INV_ID;
                TB_INVOICE_DETAIL editModel = repInoviceDetail.GetById(invDetail.ID);
                if (editModel != null)
                {
                    editModel.AMOUNT = invDetail.PAYMENT_AMOUNT;
                    repInoviceDetail.Update(editModel);
                }
            }
            //Edit Parent
            TB_INVOICE editModelInvocie = repInvoice.GetById(inv_id);
            if (editModelInvocie != null)
            {
                editModelInvocie.AMOUNT = this.InvDetails.Sum(x => x.PAYMENT_AMOUNT).Value;
                editModelInvocie.UPDATE_BY = userLogin.USER_ID;
                editModelInvocie.UPDATE_DATE = DateTime.Now;
                repInvoice.Update(editModelInvocie);
            }

            searchResult = obj.preparePostingData();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
        }
    }
}