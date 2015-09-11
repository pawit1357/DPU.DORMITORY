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

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class CheckIn : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_FUND> repfund;
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_RATES_GROUP> repRateGroup;
        private Repository<TB_CUSTOMER_FUND> repCustomerFund;
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_CUSTOMER_PROFILE> repCustomerProfile;

        private Repository<TB_M_CUSTOMER_TYPE> repCustomerType;
        private Repository<TB_M_NATION> repNation;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;

        public CheckIn()
        {
            repfund = unitOfWork.Repository<TB_M_FUND>();
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repRateGroup = unitOfWork.Repository<TB_RATES_GROUP>();
            repCustomerFund = unitOfWork.Repository<TB_CUSTOMER_FUND>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repCustomerProfile = unitOfWork.Repository<TB_CUSTOMER_PROFILE>();
            repCustomerType = unitOfWork.Repository<TB_M_CUSTOMER_TYPE>();
            repNation = unitOfWork.Repository<TB_M_NATION>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
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

        public int PKID
        {
            get { return (int)ViewState["CheckIn_PKID"]; }
            set { ViewState["CheckIn_PKID"] = value; }
        }

        public List<TB_CUSTOMER_FUND> CustomerFund
        {
            get
            {
                List<TB_CUSTOMER_FUND> listCusFunds = new List<TB_CUSTOMER_FUND>();
                foreach (GridViewRow row in gvFund.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        HiddenField hID = (row.Cells[0].FindControl("hID") as HiddenField);
                        HiddenField hForServiceID = (row.Cells[0].FindControl("hForServiceID") as HiddenField);
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                        if (chkRow.Checked)
                        {
                            TB_CUSTOMER_FUND tmp = new TB_CUSTOMER_FUND();
                            tmp.FUND_ID = Convert.ToInt32(hID.Value);
                            tmp.FOR_SERVICE_ID = Convert.ToInt32(hForServiceID.Value);
                            listCusFunds.Add(tmp);
                        }
                    }
                }
                return listCusFunds;
            }
        }
        public TB_CUSTOMER objCustomer
        {
            get
            {
                TB_CUSTOMER _obj = new TB_CUSTOMER();
                _obj.ID = (CommandName == CommandNameEnum.Add || CommandName == CommandNameEnum.CheckIn) ? 0 : this.PKID;
                _obj.CUSTOMER_NUMBER = txtCustomerID.Text;
                _obj.ROOM_ID = Convert.ToInt32(hRoomID.Value);
                _obj.CUSTOMER_TYPE_ID = Convert.ToInt32(ddlCustomerType.SelectedValue);
                _obj.FIRSTNAME = txtName.Text;
                _obj.SURNAME = txtSurname.Text;
                _obj.PERSONALID = txtIDCard.Text;
                _obj.CHECKIN_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);// Convert.ToDateTime(txtChckInDate.Text);
                _obj.RESERV_DATE = (CommandName == CommandNameEnum.Reserv) ? Convert.ToDateTime(txtChckInDate.Text) : DateTime.MinValue;
                _obj.UPDATE_BY = userLogin.USER_ID;
                _obj.CREATE_DATE = DateTime.Now;
                _obj.STATUS = Convert.ToInt32(CustomerStatusEnum.CheckIn);

                _obj.STD_FACULTY = txtFaculty.Text;
                _obj.STD_MAJOR = txtMajor.Text;
                _obj.STD_PRO_TYPE_NAME = txtProTypeName.Text;
                _obj.STD_STATUS = txtStatus.Text;
                _obj.HAS_STDNUM = rdHasStdNum.Checked;
                _obj.TB_CUSTOMER_PROFILE = objCustomerProfile;
                _obj.TB_CUSTOMER_FUND = CustomerFund;
                _obj.PAYER = cbPayer.Checked;
                _obj.RowState = CommandName;
                return _obj;
            }
        }

        public TB_CUSTOMER_PROFILE objCustomerProfile
        {
            get
            {
                TB_CUSTOMER_PROFILE _obj = new TB_CUSTOMER_PROFILE();
                _obj.ADDR = txtAddress.Text;
                _obj.ROAD = txtRoad.Text;
                _obj.SOI = txtSoi.Text;
                _obj.TAMBON = txtTambon.Text;// Convert.ToInt32(ddlTambon.SelectedValue);
                _obj.AMPHUR = txtAmphur.Text;// Convert.ToInt32(ddlAmphur.SelectedValue);
                _obj.PROVINCE = txtPronvice.Text;// Convert.ToInt32(ddlProvince.SelectValue);
                _obj.ZIPCODE = txtZipcode.Text;
                _obj.PHONE = txtPhone.Text;
                _obj.NATION_ID = Convert.ToInt32(ddlNation.SelectedValue);
                _obj.PROFILE_TYPE = Convert.ToInt32(CustomerProfileEnum.RegisteredAddress);
                _obj.RowState = CommandName;
                return _obj;
            }
        }


        public TB_ROOM objRoom
        {
            get { return (TB_ROOM)Session["objRoom"]; }
            set { Session["objRoom"] = value; }
        }

        private void initialPage()
        {

            if (Page.PreviousPage is SearchRoomForRent)
            {
                SearchRoomForRent prvPage = Page.PreviousPage as SearchRoomForRent;
                this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
                int roomId = (prvPage == null) ? this.PKID : prvPage.PKID;
                this.PreviousPath = Constants.LINK_SEARCH_ROOM_FOR_RENT;

                objRoom = repRoom.Table.Where(x => x.ID == roomId).FirstOrDefault();
                if (objRoom != null)
                {
                    hRoomID.Value = objRoom.ID.ToString();
                    txtRoom.Text = objRoom.NUMBER.ToString();
                    gvResult.DataSource = new TB_ROOM().Search();
                    gvResult.DataBind();
                    gvResult.UseAccessibleHeader = true;
                    gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                TB_RATES_GROUP_DETAIL rateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).FirstOrDefault();
                if (rateGroup != null)
                {
                    gvRoomInfo.DataSource = rateGroup.Search();
                    gvRoomInfo.DataBind();
                    gvRoomInfo.UseAccessibleHeader = true;
                    gvRoomInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            if (Page.PreviousPage is SearchCustomer)
            {
                SearchCustomer prvPage = Page.PreviousPage as SearchCustomer;
                this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
                this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
                this.PreviousPath = Constants.LINK_SEARCH_CUSTOMER;
            }

            ddlCustomerType.DataSource = repCustomerType.Table.ToList();
            ddlCustomerType.DataBind();

            ddlNation.DataSource = repNation.Table.ToList();
            ddlNation.DataBind();

            gvFund.DataSource = repfund.Table.ToList();
            gvFund.DataBind();

            pStdInfo.Visible = false;
            txtChckInDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbCommandName.Text = Constants.GetEnumDescription(CommandName);





            switch (CommandName)
            {
                case CommandNameEnum.Add:
                case CommandNameEnum.CheckIn:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set properties
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Visible = false;
                    lbDate.Text = Resources.MSG_CHECKIN_DATE;
                    break;
                case CommandNameEnum.CheckOut:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set properties
                    fillInData();
                    pCustomerInfo.Enabled = false;
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Visible = false;
                    lbDate.Text = Resources.MSG_CHECKOUT_DATE;
                    txtRoom.ReadOnly = true;
                    aPopupRoom.Attributes["class"] = "btn green disabled";
                    btnCheckCustomer.CssClass = "btn green disabled";
                    break;
                case CommandNameEnum.MoveRoom:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set properties
                    fillInData();
                    pCustomerInfo.Enabled = false;
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Visible = true;
                    lbDate.Text = Resources.MSG_MOVEROOM_DATE;
                    break;
                case CommandNameEnum.Edit:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    fillInData();
                    //Set properties
                    pRoomInfo.Visible = false;
                    pMoveRoom.Visible = false;
                    break;
            }
            pStdInfo.Visible = true;
            ddlCustomerType.SelectedValue = "2";//Initial Fix Student
        }

        private void fillInData()
        {
            TB_CUSTOMER _obj = repCustomer.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (_obj != null)
            {
                txtCustomerID.Text = _obj.CUSTOMER_NUMBER.ToString();
                txtRoom.Text = _obj.ROOM_ID.ToString();
                ddlCustomerType.SelectedValue = _obj.CUSTOMER_TYPE_ID.ToString();
                txtName.Text = _obj.FIRSTNAME;
                txtSurname.Text = _obj.SURNAME;
                txtIDCard.Text = _obj.PERSONALID;

                txtFaculty.Text = _obj.STD_FACULTY;
                txtMajor.Text = _obj.STD_MAJOR;
                txtProTypeName.Text = _obj.STD_PRO_TYPE_NAME;
                txtStatus.Text = _obj.STD_STATUS;
                cbPayer.Checked = (_obj.PAYER == null) ? false : (Boolean)_obj.PAYER;

                TB_CUSTOMER_PROFILE _objProfile = repCustomerProfile.Table.Where(x => x.CUS_ID == _obj.ID).FirstOrDefault();
                if (_obj != null)
                {
                    txtAddress.Text = _objProfile.ADDR;
                    txtRoad.Text = _objProfile.ROAD;
                    txtSoi.Text = _objProfile.SOI;
                    txtTambon.Text = _objProfile.TAMBON;
                    txtAmphur.Text = _objProfile.AMPHUR;
                    txtPronvice.Text = _objProfile.PROVINCE;
                    txtZipcode.Text = _objProfile.ZIPCODE;
                    txtPhone.Text = _objProfile.PHONE;
                    ddlNation.SelectedValue = _objProfile.NATION_ID.ToString();
                }
                objRoom = repRoom.Table.Where(x => x.ID == _obj.ROOM_ID).FirstOrDefault();
                if (objRoom != null)
                {
                    txtRoom.Text = objRoom.NUMBER.ToString();
                    hRoomID.Value = objRoom.ID.ToString();

                    gvResult.DataSource = new TB_ROOM().Search();
                    gvResult.DataBind();
                    gvResult.UseAccessibleHeader = true;
                    gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                List<TB_M_FUND> listfund = repfund.Table.ToList();
                if (listfund != null && listfund.Count > 0)
                {
                    gvFund.DataSource = listfund;
                    gvFund.DataBind();

                    List<int> pkIds = new List<int>();
                    List<TB_CUSTOMER_FUND> listCusFund = repCustomerFund.Table.Where(x => x.CUS_ID == _obj.ID).ToList();
                    foreach (TB_CUSTOMER_FUND _tmp in listCusFund)
                    {
                        pkIds.Add(Convert.ToInt32(_tmp.FUND_ID));
                    }
                    foreach (GridViewRow row in gvFund.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            HiddenField hID = (row.Cells[0].FindControl("hID") as HiddenField);
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                            int id = Convert.ToInt32(hID.Value);
                            if (pkIds.Contains(id))
                            {
                                chkRow.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        private void removeSession()
        {
        }

        private Boolean isCustomerExist()
        {
            Boolean isExist = false;

            int stayStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
            TB_CUSTOMER _cus = repCustomer.Table.Where(x => x.STATUS == stayStatus && x.CUSTOMER_NUMBER.Equals(txtCustomerID.Text)).FirstOrDefault();
            if (_cus != null)
            {
                isExist = true;
            }
            return isExist;
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnCheckCustomer_Click(object sender, EventArgs e)
        {
            //Get Data For SAP
            if (!isCustomerExist())
            {

            }
            else
            {
                MessageBox.Show(this.Page, Resources.MSG_EXIST_CUSTOMER_NUMBER);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean saveStatus = true;
            switch (CommandName)
            {
                case CommandNameEnum.CheckIn:
                    //Get Data For SAP
                    if (!isCustomerExist())
                    {
                        repCustomer.Insert(objCustomer);
                    }
                    else
                    {
                        saveStatus = false;
                        MessageBox.Show(this.Page, Resources.MSG_EXIST_CUSTOMER_NUMBER);
                    }
                    break;
                case CommandNameEnum.Edit:
                    //Delete Old fund
                    foreach (TB_CUSTOMER_FUND _cusFund in repCustomerFund.Table.Where(x=>x.CUS_ID == this.PKID).ToList())
                    {
                        repCustomerFund.Delete(_cusFund);
                    }
                    TB_CUSTOMER _cus = repCustomer.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
                    if (_cus != null)
                    {
                        _cus.CUSTOMER_NUMBER = objCustomer.CUSTOMER_NUMBER;
                        _cus.ROOM_ID = objCustomer.ROOM_ID;
                        _cus.CUSTOMER_TYPE_ID = objCustomer.CUSTOMER_TYPE_ID;
                        _cus.FIRSTNAME = objCustomer.FIRSTNAME;
                        _cus.SURNAME = objCustomer.SURNAME;
                        _cus.PERSONALID = objCustomer.PERSONALID;
                        _cus.CHECKIN_DATE = objCustomer.CHECKIN_DATE;
                        _cus.UPDATE_BY = userLogin.USER_ID;
                        _cus.UPDATE_DATE = DateTime.Now;

                        _cus.STD_FACULTY = objCustomer.STD_FACULTY;
                        _cus.STD_MAJOR = objCustomer.STD_MAJOR;
                        _cus.STD_PRO_TYPE_NAME = objCustomer.STD_PRO_TYPE_NAME;
                        _cus.STD_STATUS = objCustomer.STD_STATUS;
                        _cus.TB_CUSTOMER_FUND = CustomerFund;
                        _cus.PAYER = cbPayer.Checked;
                        repCustomer.Update(_cus);
                    }
                    TB_CUSTOMER_PROFILE _profile = repCustomerProfile.Table.Where(x => x.CUS_ID == this.PKID).FirstOrDefault();
                    if (_profile != null)
                    {
                        _profile.ADDR = objCustomerProfile.ADDR;
                        _profile.ROAD = objCustomerProfile.ROAD;
                        _profile.SOI = objCustomerProfile.SOI;
                        _profile.TAMBON = objCustomerProfile.TAMBON;
                        _profile.AMPHUR = objCustomerProfile.AMPHUR;
                        _profile.PROVINCE = objCustomerProfile.PROVINCE;
                        _profile.ZIPCODE = objCustomerProfile.ZIPCODE;
                        _profile.PHONE = objCustomerProfile.PHONE;
                        _profile.NATION_ID = objCustomerProfile.NATION_ID;
                        _profile.PROFILE_TYPE = objCustomerProfile.PROFILE_TYPE;
                        repCustomerProfile.Update(_profile);
                    }
                    break;
                case CommandNameEnum.CheckOut:
                    TB_CUSTOMER _cusCheckOut = repCustomer.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
                    if (_cusCheckOut != null)
                    {
                        _cusCheckOut.CHECKOUT_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);// Convert.ToDateTime(txtChckInDate.Text);
                        _cusCheckOut.UPDATE_BY = userLogin.USER_ID;
                        _cusCheckOut.UPDATE_DATE = DateTime.Now;
                        _cusCheckOut.STATUS = Convert.ToInt32(CustomerStatusEnum.CheckOut);
                        repCustomer.Update(_cusCheckOut);
                    }
                    break;
                case CommandNameEnum.MoveRoom:
                    TB_CUSTOMER _cusMove = repCustomer.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
                    if (_cusMove != null)
                    {
                        _cusMove.MOVEROOM_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);// Convert.ToDateTime(txtChckInDate.Text);
                        _cusMove.UPDATE_BY = userLogin.USER_ID;
                        _cusMove.UPDATE_DATE = DateTime.Now;
                        _cusMove.STATUS = Convert.ToInt32(CustomerStatusEnum.MoveRoom);
                        repCustomer.Update(_cusMove);
                    }
                    repCustomer.Insert(objCustomer);
                    break;
            }

            #region "MESSAGE RESULT"
            if (saveStatus)
            {
                String errorMessage = repCustomer.errorMessage;
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(this, errorMessage);
                }
                else
                {
                    MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
                }
            }

            #endregion
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            Console.WriteLine();

            switch (cmd)
            {
                case CommandNameEnum.Select:
                    int roomID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    TB_ROOM _room = repRoom.Table.Where(x => x.ID == roomID).FirstOrDefault();
                    if (_room != null)
                    {
                        switch (CommandName)
                        {
                            case CommandNameEnum.MoveRoom:
                                txtMoveTo.Text = _room.NUMBER.ToString();
                                break;
                            default:
                                txtRoom.Text = _room.NUMBER.ToString();
                                break;
                        }

                        hRoomID.Value = _room.ID.ToString();
                    }

                    break;
            }
        }


        protected void rdHasNotStdNum_CheckedChanged(object sender, EventArgs e)
        {
            if (rdHasStdNum.Checked)
            {
                pStdInfo.Visible = true;
                btnCheckCustomer.Visible = true;
                txtCustomerID.ReadOnly = false;
                txtCustomerID.Text = String.Empty;
            }
            else
            {
                txtCustomerID.Text = String.Format("TMP_{0}", DateTime.Now.ToString("yyyyMMddHHmm"));
                txtCustomerID.ReadOnly = true;
                pStdInfo.Visible = false;
                btnCheckCustomer.Visible = false;
            }
        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {

        }



    }
}