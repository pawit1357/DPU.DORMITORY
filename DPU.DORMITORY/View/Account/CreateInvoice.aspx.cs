using CrystalDecisions.CrystalReports.Engine;
using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;

namespace DPU.DORMITORY.Web.View.Account
{
    /* %%%%%%%%%%%%%%%%%%% TODO %%%%%%%%%%%%%%%%%%%
     * 1 เดือนทำรายการมากว่า 1 ครั้ง (ถ้ามีรายการชำระของเดือนอยู่แล้ว ให้ล้างข้อมูลออก)
     * 3. เพิ่มปุ่มล้างข้อมูล
     * 2. มิเตอร์น้ำต้องไม่บันทึกซ้ำ
     * 4. ยกเลิกรายการ
     */

    public partial class CreateInvoice : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_SPONSOR> repFund;
        //private Repository<TB_M_SERVICE> repService;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_INVOICE> repInvoice;
        private Repository<TB_INVOICE_DETAIL> repInvoiceDetail;
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_CUSTOMER_PROFILE> repCustomerProfile;
        //private Repository<TB_CUSTOMER_FUND> repCustomerFund;

        private Repository<TB_M_CUSTOMER_TYPE> repCustomerType;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_ROOM_METER> repMeter;
        private Repository<TB_M_COST_TYPE> repCostType;
        private Repository<TB_CUSTOMER_PAYER> repCustomerPayer;

        private Repository<TB_M_SPONSOR> repSponsor;
        private Repository<TB_RATES_GROUP> repRateGroup;
        private SAPBiz sapBiz;

        public CreateInvoice()
        {
            repFund = unitOfWork.Repository<TB_M_SPONSOR>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            //repService = unitOfWork.Repository<TB_M_SERVICE>();
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
            repInvoiceDetail = unitOfWork.Repository<TB_INVOICE_DETAIL>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repCustomerProfile = unitOfWork.Repository<TB_CUSTOMER_PROFILE>();
            //repCustomerFund = unitOfWork.Repository<TB_CUSTOMER_FUND>();
            repCustomerType = unitOfWork.Repository<TB_M_CUSTOMER_TYPE>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repMeter = unitOfWork.Repository<TB_ROOM_METER>();
            repCostType = unitOfWork.Repository<TB_M_COST_TYPE>();
            repCustomerPayer = unitOfWork.Repository<TB_CUSTOMER_PAYER>();
            repSponsor = unitOfWork.Repository<TB_M_SPONSOR>();
            repRateGroup = unitOfWork.Repository<TB_RATES_GROUP>();
            sapBiz = new SAPBiz();

        }

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public InvoiceTypeEnum InvoiceType
        {
            get { return (InvoiceTypeEnum)ViewState["InvoiceType"]; }
            set { ViewState["InvoiceType"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[Constants.PREVIOUS_PATH]; }
            set { ViewState[Constants.PREVIOUS_PATH] = value; }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchResult"]; }
            set { Session[GetType().Name + "searchResult"] = value; }
        }

        public TB_ROOM objRoom
        {
            get { return (TB_ROOM)ViewState["ObjRoom"]; }
            set { ViewState["ObjRoom"] = value; }
        }

        public List<TB_CUSTOMER> customers
        {
            get { return (List<TB_CUSTOMER>)ViewState["customers"]; }
            set { ViewState["customers"] = value; }
        }
        public List<InvDetail> InvDetails
        {
            get { return (List<InvDetail>)ViewState["InvDetail"]; }
            set { ViewState["InvDetail"] = value; }
        }

        public String selectedPayer
        {
            get { return (String)ViewState["selectedPayer"]; }
            set { ViewState["selectedPayer"] = value; }
        }

        //public List<InvDetail> InvdetailShow
        //{
        //    get { return this.InvDetails.Where(x => x.row_type != Convert.ToInt32(CommandNameEnum.ITEM)).ToList(); }
        //}
        public string pActive01
        {
            get { return (string)ViewState["pActive01"]; }
            set { ViewState["pActive01"] = value; }
        }
        public string pActive02
        {
            get { return (string)ViewState["pActive02"]; }
            set { ViewState["pActive02"] = value; }
        }
        public string pActive03
        {
            get { return (string)ViewState["pActive03"]; }
            set { ViewState["pActive03"] = value; }
        }
        public TB_INVOICE objInvoice
        {
            get
            {
                TB_INVOICE tmp = new TB_INVOICE();
                tmp.BUILD_ID = Convert.ToInt16(ddlBuild.SelectedValue);
                tmp.ROOM_NUMBER = txtRoom.Text;
                //tmp.FilterPaymentStatus = false;
                tmp.PAYMENT_STATUS = false;
                tmp.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);
                //tmp.OTHER = txtOther.Text;
                return tmp;
            }
        }

        public List<TB_ROOM_METER> objRoomMeters
        {
            get
            {
                List<TB_ROOM_METER> tmp = new List<TB_ROOM_METER>();
                TB_ROOM_METER _objRoomMeter = new TB_ROOM_METER();
                _objRoomMeter.METER_DATE = Convert.ToDateTime(txtPostingDate.Text);
                _objRoomMeter.ROOM_ID = objRoom.ID;// Convert.ToInt32(hRoomID.Value);
                _objRoomMeter.METER_TYPE = 3;
                _objRoomMeter.METER_START = Convert.ToInt32(txtElecMeterStart.Text);
                _objRoomMeter.METER_END = Convert.ToInt32(txtElecMeterEnd.Text);
                _objRoomMeter.UPDATE_BY = userLogin.USER_ID;
                _objRoomMeter.UPDATE_DATE = DateTime.Now;
                tmp.Add(_objRoomMeter);
                _objRoomMeter = new TB_ROOM_METER();
                _objRoomMeter.METER_DATE = Convert.ToDateTime(txtPostingDate.Text);
                _objRoomMeter.ROOM_ID = objRoom.ID; //Convert.ToInt32(hRoomID.Value);
                _objRoomMeter.METER_TYPE = 4;
                _objRoomMeter.METER_START = Convert.ToInt32(txtWaterMeterStart.Text);
                _objRoomMeter.METER_END = Convert.ToInt32(txtWaterMeterEnd.Text);
                _objRoomMeter.UPDATE_BY = userLogin.USER_ID;
                _objRoomMeter.UPDATE_DATE = DateTime.Now;
                tmp.Add(_objRoomMeter);
                return tmp;
            }
        }

        private void bindPaymentHistory()
        {
            //IEnumerable paymentList = objInvoice.SearchPaymentHistory();
            //gvPaymentHistory.DataSource = paymentList;
            //gvPaymentHistory.DataBind();
            //gvPaymentHistory.UseAccessibleHeader = true;
            //gvPaymentHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void initialPage()
        {
            CommandName = new CommandNameEnum();
            InvDetails = new List<InvDetail>();
            List<TB_M_BUILD> build = repBuild.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();

            ddlBuild.DataSource = build;
            ddlBuild.DataBind();
            txtStayDay.Text = DateTime.Now.Day.ToString();

            btnCalculate.CssClass = Constants.CSS_BUTTON_CALCULATE;
            btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
            //btnPrintInvoice.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
            btnSave.Attributes.Add("onclick", "return confirm('ต้องการบันทึกข้อมูล?');");


            pActive01 = "active";
            pActive02 = "";
            pActive03 = "";

        }

        private void fillInData()
        {
            txtPostingDate.Text = DateTime.Now.ToString("MM/yyyy");

            int _buildID = Convert.ToInt16(ddlBuild.SelectedValue);
            objRoom = repRoom.Table.Where(x => x.BUILD_ID == _buildID && x.NUMBER.Equals(txtRoom.Text)).FirstOrDefault();
            if (objRoom != null)
            {
                int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                customers = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
                if (customers != null && customers.Count > 0)
                {

                    Boolean notHasStdNum = customers.Where(x => x.HAS_STDNUM == false).Any();
                    InvoiceType = (notHasStdNum) ? InvoiceTypeEnum.ROOM : InvoiceTypeEnum.CUSTOMER;

                    #region "SET PREVIOS METER"

                    DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text);


                    //Check Current Month
                    List<TB_ROOM_METER> meters = repMeter.Table.Where(x => x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).ToList();
                    if (meters != null && meters.Count > 0)
                    {
                        TB_ROOM_METER meterElec = meters.Where(x => x.METER_TYPE == 3).FirstOrDefault();
                        if (meterElec != null)
                        {
                            txtElecMeterStart.Text = meterElec.METER_START.Value.ToString();
                            txtElecMeterEnd.Text = meterElec.METER_END.Value.ToString();
                        }
                        TB_ROOM_METER meterWater = meters.Where(x => x.METER_TYPE == 4).FirstOrDefault();
                        if (meterWater != null)
                        {
                            txtWaterMeterStart.Text = meterWater.METER_START.Value.ToString();
                            txtWaterMeterEnd.Text = meterWater.METER_END.Value.ToString();
                        }
                    }
                    else
                    {
                        //Check Prevoid Month
                        postingDate = postingDate.AddMonths(-1);
                        meters = repMeter.Table.Where(x => x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).ToList();
                        if (meters != null && meters.Count > 0)
                        {
                            TB_ROOM_METER meterElec = meters.Where(x => x.METER_TYPE == 3).FirstOrDefault();
                            if (meterElec != null)
                            {
                                txtElecMeterStart.Text = meterElec.METER_END.Value.ToString();
                            }
                            else
                            {
                                txtElecMeterStart.Text = string.Empty;
                            }
                            TB_ROOM_METER meterWater = meters.Where(x => x.METER_TYPE == 4).FirstOrDefault();
                            if (meterWater != null)
                            {
                                txtWaterMeterStart.Text = meterWater.METER_END.Value.ToString();
                            }
                            else
                            {
                                txtWaterMeterStart.Text = string.Empty;
                            }
                        }
                    }
                    #endregion

                    #region "SET PAYMENT HISTORY"
                    bindPaymentHistory();
                    #endregion

                }
                else
                {
                    MessageBox.Show(this.Page, String.Format(Resources.MSG_NO_CUSTOMER_IN_ROOM, objRoom.NUMBER));
                }
            }
            else
            {
                MessageBox.Show(this.Page, Resources.MSG_NO_ROOM);
            }
        }

        private void removeSession()
        {
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnCheckRoom_Click(object sender, EventArgs e)
        {
            fillInData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text);

            if (customers != null && customers.Count > 0)
            {

                using (TransactionScope tran = new TransactionScope())
                {
                    #region "METER"
                    foreach (TB_ROOM_METER _val in objRoomMeters)
                    {
                        TB_ROOM_METER editMeter = repMeter.Table.Where(x => x.METER_TYPE == _val.METER_TYPE && x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).FirstOrDefault();
                        if (editMeter != null)
                        {
                            editMeter.METER_DATE = _val.METER_DATE;
                            editMeter.ROOM_ID = _val.ROOM_ID;
                            editMeter.METER_TYPE = _val.METER_TYPE;
                            editMeter.METER_START = _val.METER_START;
                            editMeter.METER_END = _val.METER_END;
                            editMeter.UPDATE_BY = userLogin.USER_ID;
                            editMeter.UPDATE_DATE = DateTime.Now;
                            repMeter.Update(editMeter);
                        }
                        else
                        {
                            repMeter.Insert(_val);
                        }
                    }
                    #endregion


                    //ADD CUSTOMER INVOICE
                    var invFund = this.InvDetails.Where(x => x.SPONSOR_ID > 0).GroupBy(u => u.SPONSOR_ID)
                                                             .Select(group => new { GroupID = group.Key, Customers = group.ToList() })
                                                             .ToList();
                    foreach (var item in invFund)
                    {
                        InvDetail detail = this.InvDetails.Where(x => x.SPONSOR_ID == item.GroupID && x.row_type == 2).FirstOrDefault();
                        TB_INVOICE inv = new TB_INVOICE();
                        inv.SPONSOR_ID = detail.SPONSOR_ID;
                        inv.CUS_ID = detail.CUS_ID;
                        inv.POSTING_DATE = postingDate;
                        inv.AMOUNT = this.InvDetails.Where(x => x.SPONSOR_ID == detail.SPONSOR_ID && x.row_type == 2).Sum(x => x.PAYMENT_AMOUNT).Value;
                        inv.STAY_DAY = 0;
                        inv.PAYMENT_STATUS = false;
                        inv.UPDATE_BY = userLogin.USER_ID;
                        inv.CREATE_DATE = DateTime.Now;
                        inv.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);//Normal status is open
                        inv.PAYER_NAME = detail.PAYER_NAME;
                        inv.IS_ACTIVE = detail.IS_ACTIVE;
                        List<TB_INVOICE_DETAIL> invoiceDetails = new List<TB_INVOICE_DETAIL>();
                        foreach (InvDetail _detail in this.InvDetails.Where(x => x.SPONSOR_ID == detail.SPONSOR_ID && x.row_type == 2))
                        {
                            TB_INVOICE_DETAIL invoice = new TB_INVOICE_DETAIL();
                            //invoice.INVOICE_ID = inv.ID;
                            invoice.COST_TYPE_ID = _detail.COST_TYPE_ID;
                            invoice.CUS_ID = _detail.CUS_ID;
                            invoice.SPONSOR_ID = _detail.SPONSOR_ID;
                            invoice.AMOUNT = _detail.PAYMENT_AMOUNT;
                            invoice.REMARK = _detail.REMARK;
                            invoiceDetails.Add(invoice);
                        }
                        inv.TB_INVOICE_DETAIL = invoiceDetails;
                        if (inv.AMOUNT > 0)
                        {
                            repInvoice.Insert(inv);
                        }
                    }
                    var invCus = this.InvDetails.Where(x => x.SPONSOR_ID == 0).GroupBy(u => u.CUS_ID)
                                                             .Select(group => new { GroupID = group.Key, Customers = group.ToList() })
                                                             .ToList();
                    int index = 1;
                    foreach (var item in invCus)
                    {
                        //item.Customers.FirstOrDefault().

                        InvDetail detail = this.InvDetails.Where(x => x.CUS_ID == item.GroupID && x.row_type == 1).FirstOrDefault();
                        TB_INVOICE inv = new TB_INVOICE();
                        inv.SPONSOR_ID = detail.SPONSOR_ID;
                        inv.CUS_ID = detail.CUS_ID;
                        inv.POSTING_DATE = postingDate;
                        inv.AMOUNT = this.InvDetails.Where(x => x.CUS_ID == detail.CUS_ID && x.row_type == 1).Sum(x => x.PAYMENT_AMOUNT).Value;
                        inv.STAY_DAY = 0;
                        inv.PAYMENT_STATUS = false;
                        inv.UPDATE_BY = userLogin.USER_ID;
                        inv.CREATE_DATE = DateTime.Now;
                        inv.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);//Normal status is open
                        inv.PAYER_NAME = detail.PAYER_NAME;

                        inv.IS_ACTIVE = (detail.PAYER_NAME.Split(',').Length == 2) ? (index == 1) ? true : false : true;
                        List<TB_INVOICE_DETAIL> invoiceDetails = new List<TB_INVOICE_DETAIL>();
                        foreach (InvDetail _detail in this.InvDetails.Where(x => x.CUS_ID == detail.CUS_ID && x.row_type == 1))
                        {
                            TB_INVOICE_DETAIL invoice = new TB_INVOICE_DETAIL();
                            //invoice.INVOICE_ID = inv.ID;
                            invoice.COST_TYPE_ID = _detail.COST_TYPE_ID;
                            invoice.CUS_ID = _detail.CUS_ID;
                            invoice.SPONSOR_ID = _detail.SPONSOR_ID;
                            invoice.AMOUNT = _detail.PAYMENT_AMOUNT;
                            invoice.REMARK = _detail.REMARK;
                            invoiceDetails.Add(invoice);
                        }
                        inv.TB_INVOICE_DETAIL = invoiceDetails;
                        if (inv.AMOUNT > 0)
                        {
                            repInvoice.Insert(inv);
                        }
                        index++;
                    }




                    #region "MESSAGE RESULT"
                    String errorMessage = repInvoice.errorMessage;
                    if (!String.IsNullOrEmpty(errorMessage))
                    {
                        tran.Dispose();
                        MessageBox.Show(this, errorMessage);
                    }
                    else
                    {
                        tran.Complete();

                        MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, Constants.LINK_CREATE_INVOICE);

                        //MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
                    }
                    #endregion

                    btnCalculate.CssClass = Constants.CSS_DISABLED_BUTTON_CALCULATE;
                    btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
                    //btnPrintInvoice.CssClass = Constants.CSS_BUTTON_SAVE;


                }
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            pActive01 = "";
            pActive02 = "active";
            pActive03 = "";

            int elecUnit = Convert.ToInt32(String.IsNullOrEmpty(txtElecMeterEnd.Text) ? "0" : txtElecMeterEnd.Text) - Convert.ToInt32(String.IsNullOrEmpty(txtElecMeterStart.Text) ? "0" : txtElecMeterStart.Text); ;
            int waterUnit = Convert.ToInt32(String.IsNullOrEmpty(txtWaterMeterEnd.Text) ? "0" : txtWaterMeterEnd.Text) - Convert.ToInt32(String.IsNullOrEmpty(txtWaterMeterStart.Text) ? "0" : txtWaterMeterStart.Text);
            if (elecUnit >= 0 && waterUnit >= 0)
            {
                calculate();
            }


        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            print(0);
            MessageBox.Show(this, "พิมพ์เรียบร้อยแล้ว", Constants.LINK_CREATE_INVOICE);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.LINK_CREATE_INVOICE);
        }

        protected void txtPostingDate_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRoom.Text) && objRoom != null)
            {
                #region "SET PREVIOS METER"
                DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text).AddDays(-1);

                List<TB_ROOM_METER> meters = repMeter.Table.Where(x => x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).ToList();
                if (meters != null && meters.Count > 0)
                {
                    TB_ROOM_METER meterElec = meters.Where(x => x.METER_TYPE == 3).FirstOrDefault();
                    if (meterElec != null)
                    {
                        txtElecMeterStart.Text = meterElec.METER_END.Value.ToString();
                    }
                    else
                    {
                        txtElecMeterStart.Text = string.Empty;
                    }
                    TB_ROOM_METER meterWater = meters.Where(x => x.METER_TYPE == 4).FirstOrDefault();
                    if (meterWater != null)
                    {
                        txtWaterMeterStart.Text = meterWater.METER_END.Value.ToString();
                    }
                    else
                    {
                        txtWaterMeterStart.Text = string.Empty;
                    }
                }
                #endregion
            }
        }

        protected void gvPaymentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litPaymentStatus = (Literal)e.Row.FindControl("litPaymentStatus");
                if (_litPaymentStatus != null)
                {
                    if (!String.IsNullOrEmpty(_litPaymentStatus.Text))
                    {
                        switch (_litPaymentStatus.Text)
                        {
                            case "True":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_TRUE;
                                //_btnPay.Visible = false;
                                e.Row.ForeColor = System.Drawing.Color.Green;
                                break;
                            case "False":
                                _litPaymentStatus.Text = Resources.MSG_PAYMENT_FALSE;
                                //_btnPay.Visible = true;
                                e.Row.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        protected void gvPaymentHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int invoice_id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            switch (cmd)
            {
                case CommandNameEnum.PrintInvoice:
                    print(invoice_id);
                    break;
                case CommandNameEnum.Delete:
                    TB_INVOICE _editInvoice = repInvoice.GetById(invoice_id);
                    if (_editInvoice != null)
                    {
                        _editInvoice.STATUS = Convert.ToInt32(InvoiceStatusEmum.Cancel);
                        repInvoice.Update(_editInvoice);
                        #region "MESSAGE RESULT"
                        String errorMessage = repInvoice.errorMessage;
                        if (!String.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(this, errorMessage);
                        }
                        #endregion
                        #region "SET PAYMENT HISTORY"
                        bindPaymentHistory();
                        #endregion
                    }
                    break;
            }
        }

        protected void gvPaymentHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void print(int inv_id)
        {
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Configuration.PathReportInvoice);
            crystalReport.SetDatabaseLogon
                (Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);

            TB_ROOM _room = repRoom.Table.Where(x => x.NUMBER.Equals(txtRoom.Text)).FirstOrDefault();
            DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text);


            crystalReport.SetParameterValue("P_ROOM_ID", _room.ID);
            crystalReport.SetParameterValue("P_CUSTOMER_ID", 0);
            crystalReport.SetParameterValue("P_INVOICE_ID", inv_id);
            crystalReport.SetParameterValue("P_MONTH", postingDate.Month);
            crystalReport.SetParameterValue("P_YEAR", postingDate.Year);
            crystalReport.SetParameterValue("P_BUILD", ddlBuild.SelectedValue);

            crystalReport.PrintToPrinter(1, true, 0, 0);
        }


        private void calculate()
        {
            DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text);

            List<InvDetail> tmps = new List<InvDetail>();
            List<int> pkIds = new List<int>();

            if (objRoom != null)
            {
                int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                List<TB_CUSTOMER> listCus = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
                if (listCus != null && listCus.Count > 0)
                {
                    #region "SET WATER & ELECTRIC"
                    decimal WaterMeterEnd = CustomUtils.isNumber(txtWaterMeterEnd.Text) ? Convert.ToDecimal(txtWaterMeterEnd.Text) : Convert.ToDecimal(0);
                    decimal WaterMeterStart = CustomUtils.isNumber(txtWaterMeterStart.Text) ? Convert.ToDecimal(txtWaterMeterStart.Text) : Convert.ToDecimal(0);

                    decimal ElecMeterEnd = CustomUtils.isNumber(txtElecMeterEnd.Text) ? Convert.ToDecimal(txtElecMeterEnd.Text) : Convert.ToDecimal(0);
                    decimal ElecMeterStart = CustomUtils.isNumber(txtElecMeterStart.Text) ? Convert.ToDecimal(txtElecMeterStart.Text) : Convert.ToDecimal(0);

                    lbWaterUnit.Text = (WaterMeterEnd - WaterMeterStart) + "";
                    lbElecUnit.Text = (ElecMeterEnd - ElecMeterStart) + "";
                    #endregion

                    #region "Payment Detail"
                    int order = 1;
                    int rowIndex = 1;
                    foreach (TB_CUSTOMER cus in listCus)
                    {
                        Boolean existInvoice = repInvoice.Table.Where(x => x.POSTING_DATE.Value.Year == postingDate.Year && x.POSTING_DATE.Value.Month == postingDate.Month && x.CUS_ID == cus.ID).Any();
                        if (!existInvoice)
                        {
                            List<TB_CUSTOMER_PAYER> cusPays = repCustomerPayer.Table.Where(x => x.CUS_ID == cus.ID && x.SPONSOR_ID != 0).ToList();

                            List<TB_RATES_GROUP_DETAIL> details = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).ToList();
                            foreach (TB_RATES_GROUP_DETAIL _detail in details)
                            {
                                TB_CUSTOMER_PAYER sponsor = cusPays.Where(x => x.COST_TYPE_ID == _detail.COST_TYPE_ID).FirstOrDefault();
                                if (sponsor == null)
                                {
                                    InvDetail _tmp = new InvDetail();
                                    _tmp.ID = order;
                                    _tmp.SPONSOR_ID = 0;
                                    _tmp.CUS_ID = cus.ID;

                                    TB_M_COST_TYPE cusType = repCostType.Table.Where(x => x.ID == _detail.COST_TYPE_ID).FirstOrDefault();
                                    if (cusType != null)
                                    {
                                        _tmp.M_SERVICE_NAME = cusType.NAME;
                                        _tmp.COST_TYPE_ID = cusType.ID;
                                    }

                                    _tmp.PAYER_NAME = String.Format("{0} {1}", cus.FIRSTNAME, cus.SURNAME);
                                    _tmp.RATE_UNIT = _detail.UNIT;
                                    _tmp.RATE_AMOUNT = _detail.AMOUNT;
                                    _tmp.PAYMENT_AMOUNT = _tmp.RATE_AMOUNT * _tmp.RATE_UNIT;

                                    if (_detail.COST_TYPE_ID == 3)
                                    {
                                        _tmp.RATE_UNIT = Convert.ToInt32(lbElecUnit.Text);
                                        _tmp.PAYMENT_AMOUNT = _detail.AMOUNT * Convert.ToInt32(lbElecUnit.Text);
                                    }
                                    else if (_detail.COST_TYPE_ID == 4)
                                    {
                                        _tmp.RATE_UNIT = Convert.ToInt32(lbWaterUnit.Text);
                                        _tmp.PAYMENT_AMOUNT = _detail.AMOUNT * Convert.ToInt32(lbWaterUnit.Text);
                                    }
                                    _tmp.row_type = Convert.ToInt32(PayTypeEnum.SELF);

                                    TB_RATES_GROUP _rate = repRateGroup.Table.Where(x => x.ID == _detail.RATES_GROUP_ID).FirstOrDefault();
                                    if (_rate != null)
                                    {
                                        CalculateInvoiceEnum cmd = (CalculateInvoiceEnum)Enum.ToObject(typeof(CalculateInvoiceEnum), (int)_rate.CALCULATE_INVOICE_TYPE);
                                        switch (cmd)
                                        {
                                            case CalculateInvoiceEnum.ByPerson:
                                                _tmp.PAYMENT_AMOUNT = (_tmp.PAYMENT_AMOUNT == null) ? 0 : _tmp.PAYMENT_AMOUNT.Value / listCus.Count;

                                                break;
                                            case CalculateInvoiceEnum.ByRoom:

                                                //Find Payer
                                                TB_CUSTOMER _customer = listCus.Where(x => x.PAYER == true).FirstOrDefault();
                                                if (_customer != null)
                                                {
                                                    _tmp.PAYER_NAME = String.Format("{0} {1}", _customer.FIRSTNAME, _customer.SURNAME);
                                                }
                                                else
                                                {
                                                    _tmp.PAYER_NAME = (listCus.Count == 2) ? (listCus[0].FIRSTNAME + " " + listCus[0].SURNAME + "," + listCus[1].FIRSTNAME + " " + listCus[1].SURNAME) : String.Empty;
                                                }
                                                break;
                                        }
                                    }
                                    _tmp.IS_ACTIVE = (_tmp.CUS_ID.Value == listCus[0].ID) ? true : false;
                                    tmps.Add(_tmp);
                                    order++;
                                }
                                else
                                {
                                    //Sponsor
                                    InvDetail _tmp = new InvDetail();
                                    _tmp.ID = order;
                                    _tmp.SPONSOR_ID = sponsor.SPONSOR_ID;
                                    _tmp.CUS_ID = sponsor.CUS_ID;
                                    TB_M_COST_TYPE cusType = repCostType.Table.Where(x => x.ID == _detail.COST_TYPE_ID).FirstOrDefault();
                                    if (cusType != null)
                                    {
                                        _tmp.M_SERVICE_NAME = cusType.NAME;
                                        _tmp.COST_TYPE_ID = cusType.ID;
                                    }
                                    TB_M_SPONSOR _sponsor = repSponsor.Table.Where(x => x.ID == sponsor.SPONSOR_ID).FirstOrDefault();
                                    if (_sponsor != null)
                                    {
                                        _tmp.PAYER_NAME = _sponsor.NAME;
                                    }
                                    _tmp.RATE_UNIT = _detail.UNIT;
                                    _tmp.RATE_AMOUNT = sponsor.AMOUNT;
                                    _tmp.PAYMENT_AMOUNT = _tmp.RATE_AMOUNT * _tmp.RATE_UNIT;
                                    _tmp.IS_ACTIVE = true;
                                    _tmp.row_type = Convert.ToInt32(PayTypeEnum.FUND);
                                    if (!pkIds.Contains(sponsor.SPONSOR_ID.Value))
                                    {
                                        tmps.Add(_tmp);
                                        pkIds.Add(sponsor.SPONSOR_ID.Value);
                                    }
                                    else
                                    {
                                        InvDetail invTmp = tmps.Where(x => x.SPONSOR_ID.Value == sponsor.SPONSOR_ID.Value).FirstOrDefault();
                                        if (invTmp != null)
                                        {
                                            invTmp.PAYMENT_AMOUNT = invTmp.PAYMENT_AMOUNT + _tmp.PAYMENT_AMOUNT;
                                        }
                                    }

                                    order++;
                                }
                            }

                            rowIndex++;
                        }
                        else
                        {
                            Console.WriteLine("มีการตั้งหนี้ของเดือนนี้ไปแล้ว");
                        }

                    }
                }


                this.InvDetails = tmps;
                DataTable dt = new DataTable();
                dt = PivotTable.GetInversedDataTable(this.InvDetails.Where(x => x.IS_ACTIVE = true).ToDataTable(), "M_SERVICE_NAME", "PAYER_NAME", "PAYMENT_AMOUNT", "-", false);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //GridView1.Columns[1].HeaderText = String.Empty;//
                //}
                #endregion
            }



            //BUTTON STATUS
            btnCalculate.CssClass = Constants.CSS_BUTTON_CALCULATE;
            btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
            btnSave.Visible = GridView1.Rows.Count > 0;
            //btnPrintInvoice.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
        }

        protected void btnMainData_Click(object sender, EventArgs e)
        {
            pActive01 = "active";
            pActive02 = "";
            pActive03 = "";
        }

        protected void btnPamentDetail_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPostingDate.Text))
            {
                int elecUnit = Convert.ToInt32(String.IsNullOrEmpty(txtElecMeterEnd.Text) ? "0" : txtElecMeterEnd.Text) - Convert.ToInt32(String.IsNullOrEmpty(txtElecMeterStart.Text) ? "0" : txtElecMeterStart.Text); ;
                int waterUnit = Convert.ToInt32(String.IsNullOrEmpty(txtWaterMeterEnd.Text) ? "0" : txtWaterMeterEnd.Text) - Convert.ToInt32(String.IsNullOrEmpty(txtWaterMeterStart.Text) ? "0" : txtWaterMeterStart.Text);
                if (elecUnit >= 0 && waterUnit >= 0)
                {
                    pActive01 = "";
                    pActive02 = "active";
                    pActive03 = "";
                }
            }
        }

        protected void btnOwner_Click(object sender, EventArgs e)
        {
            pActive01 = "";
            pActive02 = "";
            pActive03 = "active";
        }

        #region "Invoice Detail
        protected void gvInvoiceDetail_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvInvoiceDetail.EditIndex = e.NewEditIndex;
            gvInvoiceDetail.DataSource = this.InvDetails.Where(x => x.PAYER_NAME.Equals(this.selectedPayer)).OrderBy(x => x.ID);
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
            gvInvoiceDetail.DataSource = this.InvDetails.Where(x => x.PAYER_NAME.Equals(this.selectedPayer)).OrderBy(x => x.ID);
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
            gvInvoiceDetail.DataSource = this.InvDetails.Where(x => x.PAYER_NAME.Equals(this.selectedPayer)).OrderBy(x => x.ID);
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
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            this.selectedPayer = GridView1.Rows[index].Cells[1].Text;
            gvInvoiceDetail.DataSource = this.InvDetails.Where(x => x.PAYER_NAME.Equals(this.selectedPayer)).OrderBy(x => x.ID);
            gvInvoiceDetail.DataBind();

            ModolPopupExtender.Show();
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = PivotTable.GetInversedDataTable(this.InvDetails.ToDataTable(), "M_SERVICE_NAME", "PAYER_NAME", "PAYMENT_AMOUNT", "-", false);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }
}