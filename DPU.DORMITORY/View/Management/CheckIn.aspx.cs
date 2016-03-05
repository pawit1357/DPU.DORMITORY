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
using MoreLinq;
using System.Transactions;
using Gen_Bapizarfi_01_Bapizcmi003;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class CheckIn : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_SPONSOR> repfund;
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_RATES_GROUP> repRateGroup;
        private Repository<TB_CUSTOMER_PAYER> repCustomerPayer;
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_CUSTOMER_PROFILE> repCustomerProfile;

        private Repository<TB_M_CUSTOMER_TYPE> repCustomerType;
        private Repository<TB_M_NATION> repNation;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_M_SERVICE> repService;
        private Repository<TB_M_SPONSOR> repSponsor;
        private Repository<TB_M_TERM_OF_PAYMENT> repTemOfPayment;
        private Repository<TB_M_COST_TYPE> repCostType;
        private Repository<TB_INVOICE> repInvoice;
        private Repository<TB_INVOICE_DETAIL> repInvoiceDetail;

        private Repository<TB_ROOM_METER> repMeter;


        private TB_CUSTOMER customer;
        private TB_CUSTOMER_PAYER customer_payer;
        private TB_ROOM room;
        private TB_RATES_GROUP_DETAIL rateGroupDetail;
        private SAPBiz sapBiz;

        public CheckIn()
        {
            repfund = unitOfWork.Repository<TB_M_SPONSOR>();
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repRateGroup = unitOfWork.Repository<TB_RATES_GROUP>();
            repCustomerPayer = unitOfWork.Repository<TB_CUSTOMER_PAYER>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repCustomerProfile = unitOfWork.Repository<TB_CUSTOMER_PROFILE>();
            repCustomerType = unitOfWork.Repository<TB_M_CUSTOMER_TYPE>();
            repNation = unitOfWork.Repository<TB_M_NATION>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repService = unitOfWork.Repository<TB_M_SERVICE>();
            repSponsor = unitOfWork.Repository<TB_M_SPONSOR>();
            repTemOfPayment = unitOfWork.Repository<TB_M_TERM_OF_PAYMENT>();
            repCostType = unitOfWork.Repository<TB_M_COST_TYPE>();
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
            repInvoiceDetail = unitOfWork.Repository<TB_INVOICE_DETAIL>();
            repMeter = unitOfWork.Repository<TB_ROOM_METER>();

            customer = new TB_CUSTOMER();
            customer_payer = new TB_CUSTOMER_PAYER();
            room = new TB_ROOM();
            rateGroupDetail = new TB_RATES_GROUP_DETAIL();
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

        public IEnumerable searchRoomResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchRoomResult"]; }
            set { Session[GetType().Name + "searchRoomResult"] = value; }
        }

        public int CUS_PK_ID
        {
            get { return (int)ViewState["CheckIn_CUS_PK_ID"]; }
            set { ViewState["CheckIn_CUS_PK_ID"] = value; }
        }

        public int ROOM_ID
        {
            get { return (int)ViewState["ROOM_ID"]; }
            set { ViewState["ROOM_ID"] = value; }
        }

        public List<TB_CUSTOMER_PAYER> cusPayers
        {
            get { return (List<TB_CUSTOMER_PAYER>)Session["cusPayers"]; }
            set { Session["cusPayers"] = value; }
        }
        public List<InvDetail> InvDetails
        {
            get { return (List<InvDetail>)ViewState["InvDetail"]; }
            set { ViewState["InvDetail"] = value; }
        }

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
        public string pActive04
        {
            get { return (string)ViewState["pActive04"]; }
            set { ViewState["pActive04"] = value; }
        }

        public TB_CUSTOMER objCustomer
        {
            get
            {
                TB_CUSTOMER _obj = new TB_CUSTOMER();
                _obj.ID = (CommandName == CommandNameEnum.Add || CommandName == CommandNameEnum.CheckIn) ? 0 : this.CUS_PK_ID;
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
                _obj.STAY_ALONE = cbSTayAlone.Checked;
                _obj.TB_CUSTOMER_PROFILE = objCustomerProfile;
                //_obj.tb_
                //_obj.tb = cusPayers;
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

        public TB_INVOICE objInvoice
        {
            get
            {
                TB_INVOICE tmp = new TB_INVOICE();
                tmp.CUS_ID = objCustomer.ID;
                //tmp.FilterPaymentStatus = false;
                tmp.SHOW_WITHOUT_SPONSOR = true;
                tmp.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);
                return tmp;
            }
        }

        private void initialPage()
        {

            if (Page.PreviousPage is SearchRoomForRent)
            {
                SearchRoomForRent prvPage = Page.PreviousPage as SearchRoomForRent;
                this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
                this.ROOM_ID = (prvPage == null) ? this.ROOM_ID : prvPage.ROOM_ID;
                //int BUILD_ID = (prvPage == null) ? this.BUILD_ID : prvPage.BUILD_ID;
                this.PreviousPath = Constants.LINK_SEARCH_ROOM_FOR_RENT;
                this.CUS_PK_ID = 0;
                objRoom = repRoom.Table.Where(x => x.ID == ROOM_ID).FirstOrDefault();
                if (objRoom != null)
                {
                    hRoomID.Value = objRoom.ID.ToString();
                    txtRoom.Text = objRoom.NUMBER.ToString();
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
                this.CUS_PK_ID = (prvPage == null) ? this.CUS_PK_ID : prvPage.PKID;
                this.PreviousPath = Constants.LINK_SEARCH_CUSTOMER;
            }

            ddlCustomerType.DataSource = repCustomerType.Table.ToList();
            ddlCustomerType.DataBind();

            ddlNation.DataSource = repNation.Table.ToList();
            ddlNation.DataBind();



            //bindingRoom();

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
                    idAc01.Visible = true;
                    idAc02.Visible = true;
                    idAc03.Visible = false;
                    idAc04.Visible = false;

                    pCustomerInfo.Enabled = true;
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Enabled = true;
                    //Set properties
                    pRoomInfo.Visible = true;
                    pMoveRoom.Visible = false;
                    spanPoup.Visible = false;
                    btnCheckCustomer.Visible = true;
                    rdHasStdNum.Enabled = true;
                    rdHasNotStdNum.Enabled = true;
                    txtCustomerID.Enabled = true;
                    btnSave.Visible = true;
                    lbDate.Text = Resources.MSG_CHECKIN_DATE;
                    this.cusPayers = new List<TB_CUSTOMER_PAYER>();
                    break;
                case CommandNameEnum.CheckOut:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set properties
                    fillInData();

                    gvPaymentHistory.DataSource = objInvoice.SearchPaymentHistory();
                    gvPaymentHistory.DataBind();
                    gvPaymentHistory.UseAccessibleHeader = true;
                    gvPaymentHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

                    idAc01.Visible = true;
                    idAc02.Visible = false;
                    idAc03.Visible = (gvPaymentHistory.Rows.Count > 0) ? true : false;
                    idAc04.Visible = true;

                    pCustomerInfo.Enabled = false;
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Enabled = true;
                    //Set properties
                    pRoomInfo.Visible = true;
                    pMoveRoom.Visible = false;
                    spanPoup.Visible = false;
                    rdHasStdNum.Enabled = false;
                    rdHasNotStdNum.Enabled = false;
                    txtCustomerID.Enabled = false;
                    btnCheckCustomer.Visible = false;
                    btnSave.Visible = false;
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

                    gvPaymentHistory.DataSource = objInvoice.SearchPaymentHistory();
                    gvPaymentHistory.DataBind();
                    gvPaymentHistory.UseAccessibleHeader = true;
                    gvPaymentHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

                    idAc01.Visible = true;
                    idAc02.Visible = false;
                    idAc03.Visible = (gvPaymentHistory.Rows.Count > 0) ? true : false;
                    idAc04.Visible = true;

                    pCustomerInfo.Enabled = false;
                    pRoomInfo.Enabled = false;
                    pMoveRoom.Enabled = true;
                    //Set properties
                    pRoomInfo.Visible = true;
                    pMoveRoom.Visible = true;
                    spanPoup.Visible = false;
                    rdHasStdNum.Enabled = false;
                    rdHasNotStdNum.Enabled = false;
                    txtCustomerID.Enabled = false;
                    btnCheckCustomer.Visible = false;
                    btnSave.Visible = false;
                    lbDate.Text = Resources.MSG_MOVEROOM_DATE;
                    break;
                case CommandNameEnum.Edit:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    fillInData();
                    gvPaymentHistory.DataSource = objInvoice.SearchPaymentHistory();
                    gvPaymentHistory.DataBind();
                    gvPaymentHistory.UseAccessibleHeader = true;
                    gvPaymentHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

                    idAc01.Visible = true;
                    idAc02.Visible = true;
                    idAc03.Visible = (gvPaymentHistory.Rows.Count > 0) ? true : false;
                    idAc04.Visible = false;

                    pCustomerInfo.Enabled = true;
                    pRoomInfo.Enabled = true;
                    pMoveRoom.Enabled = true;
                    //Set propertiess
                    pRoomInfo.Visible = false;
                    pMoveRoom.Visible = false;
                    spanPoup.Visible = false;
                    rdHasStdNum.Enabled = false;
                    rdHasNotStdNum.Enabled = false;
                    txtCustomerID.Enabled = false;
                    btnCheckCustomer.Visible = false;
                    btnSave.Visible = true;
                    break;
            }

            pStdInfo.Visible = true;
            ddlCustomerType.SelectedValue = "2";//Initial Fix Student
            pActive01 = "active";
            pActive02 = "";
            pActive03 = "";
            pActive04 = "";
        }

        private void bindingRoom()
        {


            //repRoom.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();
            room.respoList = userLogin.respoList;
            room.STATUS = Convert.ToInt32(RoomStatusEmum.Available);
            room.BUILD_ID = Convert.ToInt32(ddlBuild.SelectedValue);
            room.ID = Convert.ToInt32(ddlRoom.SelectedValue);
            searchRoomResult = room.SearchForRent();
            gvResult.DataSource = searchRoomResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void fillInData()
        {
            TB_CUSTOMER _obj = repCustomer.Table.Where(x => x.ID == this.CUS_PK_ID).FirstOrDefault();
            if (_obj != null)
            {
                this.ROOM_ID = _obj.ROOM_ID.Value;

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
                cbSTayAlone.Checked = (_obj.STAY_ALONE == null) ? false : (Boolean)_obj.STAY_ALONE;


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

                    //gvResult.DataSource = new TB_ROOM().Search();
                    //gvResult.DataBind();
                    //gvResult.UseAccessibleHeader = true;
                    //gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                int[] roomRentIDAndFurIDs = { 1, 2 };

                this.cusPayers = repCustomerPayer.Table.Where(x => x.CUS_ID == objCustomer.ID).ToList();
                foreach (TB_CUSTOMER_PAYER payer in this.cusPayers)
                {
                    payer.ROOM_RATE = payer.AMOUNT.Value;
                    if (payer.SPONSOR_ID == 0)
                    {
                        payer.SPONSOR_NAME = String.Format("{0}  {1}", txtName.Text, txtSurname.Text);
                    }
                    else
                    {
                        payer.SPONSOR_NAME = repSponsor.GetById(payer.SPONSOR_ID).NAME;
                    }

                    payer.SERVICE_NAME = repCostType.Table.Where(x => x.ID == payer.COST_TYPE_ID).FirstOrDefault().NAME;

                    payer.TERM_OF_PAYMENT_NAME = repTemOfPayment.GetById(payer.TERM_OF_PAYMENT_ID).NAME;
                    payer.ROOM_ID = this.ROOM_ID;
                }
                gvRespPayment.DataSource = cusPayers.OrderBy(x => x.order).OrderBy(x => x.COST_TYPE_ID);
                gvRespPayment.DataBind();
                if (this.cusPayers.Count == 0)
                {
                    generateTermOfPayment();
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
            //ZSTD_INFOTable resultx = sapBiz.getStudentInfo("570305010173");
            //if (resultx != null)
            //{
            //    ZSTD_INFO info = resultx[0];
            //    Console.WriteLine( );
            //}

            //Get Data Form SAP
            if (!isCustomerExist())
            {
                ZSTD_INFOTable result = sapBiz.getStudentInfo(txtCustomerID.Text);
                if (result != null)
                {
                    if (result[0] != null)
                    {
                        ZSTD_INFO info = result[0];
                        if (info != null)
                        {
                            txtName.Text = info.Name;
                            txtSurname.Text = info.Surname;
                            txtIDCard.Text = info.Identification;
                            txtFaculty.Text = info.Faculty;
                            txtMajor.Text = info.Major;
                            txtProTypeName.Text = info.Pro_Type_Name;
                            txtStatus.Text = info.Status;
                        }
                    }
                    else
                    {
                        MessageBox.Show(this.Page, Resources.MSG_NOT_FOUND_STD_INFO_IN_SAP);
                    }
                }
                else
                {
                    MessageBox.Show(this.Page, Resources.MSG_NOT_FOUND_STD_INFO_IN_SAP);
                }
                Console.WriteLine();
            }
            else
            {
                MessageBox.Show(this.Page, Resources.MSG_EXIST_CUSTOMER_NUMBER);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean saveStatus = true;

            using (TransactionScope tran = new TransactionScope())
            {
                switch (CommandName)
                {
                    case CommandNameEnum.CheckIn:
                        //Get Data For SAP
                        if (!isCustomerExist())
                        {
                            TB_CUSTOMER cus = repCustomer.Insert(objCustomer);
                            foreach (TB_CUSTOMER_PAYER _cp in cusPayers)
                            {
                                _cp.CUS_ID = cus.ID;
                                repCustomerPayer.Insert(_cp);
                            }
                        }
                        else
                        {
                            saveStatus = false;
                            MessageBox.Show(this.Page, Resources.MSG_EXIST_CUSTOMER_NUMBER);
                        }
                        break;
                    case CommandNameEnum.Edit:
                        TB_CUSTOMER _cus = repCustomer.Table.Where(x => x.ID == this.CUS_PK_ID).FirstOrDefault();
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
                            //_cus.TB_CUSTOMER_PAYER = CustomerFund;
                            _cus.PAYER = cbPayer.Checked;
                            _cus.STAY_ALONE = cbSTayAlone.Checked;
                            repCustomer.Update(_cus);
                        }
                        TB_CUSTOMER_PROFILE _profile = repCustomerProfile.Table.Where(x => x.CUS_ID == this.CUS_PK_ID).FirstOrDefault();
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

                        foreach (TB_CUSTOMER_PAYER _val in this.cusPayers)
                        {
                            TB_CUSTOMER_PAYER _cp = repCustomerPayer.Table.Where(x => x.ID.Equals(_val.ID)).FirstOrDefault();
                            if (_cp != null)
                            {
                                _cp.CUS_ID = _val.CUS_ID;
                                _cp.COST_TYPE_ID = _val.COST_TYPE_ID;
                                _cp.SPONSOR_ID = _val.SPONSOR_ID;
                                _cp.TERM_OF_PAYMENT_ID = _val.TERM_OF_PAYMENT_ID;
                                _cp.AMOUNT = _val.AMOUNT;
                                _cp.ROOM_ID = _val.ROOM_ID;
                                repCustomerPayer.Update(_cp);
                            }
                            else
                            {
                                _val.CUS_ID = this.CUS_PK_ID;
                                repCustomerPayer.Insert(_val);
                            }
                        }


                        break;
                    case CommandNameEnum.CheckOut:
                        //if (gvPaymentHistory.Rows.Count == 0)
                        //{
                        TB_CUSTOMER _cusCheckOut = repCustomer.Table.Where(x => x.ID == this.CUS_PK_ID).FirstOrDefault();
                        if (_cusCheckOut != null)
                        {
                            _cusCheckOut.CHECKOUT_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);// Convert.ToDateTime(txtChckInDate.Text);
                            _cusCheckOut.UPDATE_BY = userLogin.USER_ID;
                            _cusCheckOut.UPDATE_DATE = DateTime.Now;
                            _cusCheckOut.STATUS = Convert.ToInt32(CustomerStatusEnum.CheckOut);
                            repCustomer.Update(_cusCheckOut);
                        }
                        //ADD CUSTOMER INVOICE
                        if (this.InvDetails != null)
                        {
                            List<InvDetail> cusList = this.InvDetails.Where(x => x.SPONSOR_ID == 0).DistinctBy(x => x.CUS_ID).ToList();
                            if (cusList != null && cusList.Count > 0)
                            {
                                foreach (InvDetail _tmp in cusList)
                                {
                                    TB_INVOICE inv = new TB_INVOICE();
                                    inv.SPONSOR_ID = 0;
                                    inv.CUS_ID = _tmp.CUS_ID;
                                    inv.POSTING_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);
                                    inv.AMOUNT = this.InvDetails.Where(x => x.CUS_ID == _tmp.CUS_ID && x.SPONSOR_ID == 0).Sum(x => x.PAYMENT_AMOUNT).Value;
                                    inv.STAY_DAY = 0;
                                    inv.PAYMENT_STATUS = false;
                                    inv.UPDATE_BY = userLogin.USER_ID;
                                    inv.CREATE_DATE = DateTime.Now;
                                    inv.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);//Normal status is open
                                    List<TB_INVOICE_DETAIL> invoiceDetails = new List<TB_INVOICE_DETAIL>();
                                    foreach (InvDetail detail in this.InvDetails.Where(x => x.row_type != Convert.ToInt32(CommandNameEnum.GROUP) && x.CUS_ID == inv.CUS_ID))
                                    {
                                        TB_INVOICE_DETAIL invoice = new TB_INVOICE_DETAIL();
                                        invoice.COST_TYPE_ID = detail.COST_TYPE_ID;
                                        invoice.CUS_ID = detail.CUS_ID;
                                        invoice.SPONSOR_ID = detail.SPONSOR_ID;
                                        invoice.AMOUNT = (detail.SPONSOR_ID == 1) ? 0 : detail.PAYMENT_AMOUNT;
                                        invoice.REMARK = detail.REMARK;
                                        invoiceDetails.Add(invoice);
                                    }
                                    inv.TB_INVOICE_DETAIL = invoiceDetails;
                                    if (inv.AMOUNT > 0)
                                    {
                                        repInvoice.Insert(inv);
                                    }
                                }
                            }
                        }
                        //}
                        //else
                        //{
                        //    MessageBox.Show(this, Resources.MSG_PLASE_PAY_REMAIN);
                        //}
                        break;
                    case CommandNameEnum.MoveRoom:
                        if (String.IsNullOrEmpty(txtMoveTo.Text))
                        {
                            MessageBox.Show(this, Resources.MSG_REQUIRE_MOVE_TO_ROOM);
                        }
                        else //if (gvPaymentHistory.Rows.Count == 0)
                        {
                            TB_CUSTOMER _cusMove = repCustomer.Table.Where(x => x.ID == this.CUS_PK_ID).FirstOrDefault();
                            if (_cusMove != null)
                            {
                                _cusMove.ROOM_ID = Convert.ToInt32(hRoomID.Value);
                                _cusMove.MOVEROOM_DATE = CustomUtils.converFromDDMMYYYY(txtChckInDate.Text);// Convert.ToDateTime(txtChckInDate.Text);
                                _cusMove.UPDATE_BY = userLogin.USER_ID;
                                _cusMove.UPDATE_DATE = DateTime.Now;
                                //_cusMove.STATUS = Convert.ToInt32(CustomerStatusEnum.MoveRoom);
                                repCustomer.Update(_cusMove);
                            }
                            //repCustomer.Insert(objCustomer);


                            //foreach (TB_CUSTOMER_PAYER _val in this.cusPayers)
                            //{
                            //    repCustomerPayer.Insert(_val);

                            //TB_CUSTOMER_PAYER _cp = repCustomerPayer.Table.Where(x => x.ID.Equals(_val.ID)).FirstOrDefault();
                            //if (_cp != null)
                            //{
                            //    _cp.CUS_ID = _val.CUS_ID;
                            //    _cp.COST_TYPE_ID = _val.COST_TYPE_ID;
                            //    _cp.SPONSOR_ID = _val.SPONSOR_ID;
                            //    _cp.TERM_OF_PAYMENT_ID = _val.TERM_OF_PAYMENT_ID;
                            //    _cp.AMOUNT = _val.AMOUNT;
                            //    _cp.ROOM_ID = _val.ROOM_ID;
                            //    repCustomerPayer.Update(_cp);
                            //}
                            //else
                            //{
                            //    _val.CUS_ID = this.CUS_PK_ID;
                            //    repCustomerPayer.Insert(_val);
                            //}
                            //}
                        }
                        //else
                        //{
                        //    MessageBox.Show(this, Resources.MSG_PLASE_PAY_REMAIN);
                        //}
                        break;
                }
                #region "MESSAGE RESULT"
                if (saveStatus)
                {
                    String errorMessage = repCustomer.errorMessage;
                    if (!String.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(this, errorMessage);
                        tran.Dispose();
                    }
                    else
                    {
                        tran.Complete();
                        MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
                    }
                }

                #endregion
            }
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
                    bindingRoom();
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

        protected void btnMainData_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            switch (btn.ID)
            {
                case "btnMainData":
                    pActive01 = "active";
                    pActive02 = "";
                    pActive03 = "";
                    pActive04 = "";
                    switch (CommandName)
                    {
                        case CommandNameEnum.MoveRoom:
                        case CommandNameEnum.CheckOut:
                            btnSave.Visible = false;
                            break;
                        default:
                            btnSave.Visible = true;
                            break;
                    }
                    break;
                case "btnOwner":
                    pActive01 = "";
                    pActive02 = "active";
                    pActive03 = "";
                    pActive04 = "";
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.CheckIn:
                            generateTermOfPayment();
                            break;
                    }
                    switch (CommandName)
                    {
                        case CommandNameEnum.MoveRoom:
                        case CommandNameEnum.CheckOut:
                            btnSave.Visible = false;
                            break;
                        default:
                            btnSave.Visible = true;
                            break;
                    }
                    break;
                case "btnPaymentHistory":
                    pActive01 = "";
                    pActive02 = "";
                    pActive03 = "active";
                    pActive04 = "";
                    switch (CommandName)
                    {
                        case CommandNameEnum.MoveRoom:
                        case CommandNameEnum.CheckOut:
                            btnSave.Visible = false;
                            break;
                        default:
                            btnSave.Visible = true;
                            break;
                    }
                    break;
                case "btnInvoice":
                    pActive01 = "";
                    pActive02 = "";
                    pActive03 = "";
                    pActive04 = "active";

                    calculate();
                    switch (CommandName)
                    {
                        case CommandNameEnum.MoveRoom:
                        case CommandNameEnum.CheckOut:
                            btnSave.Visible = true;
                            break;
                        default:
                            btnSave.Visible = false;
                            break;
                    }
                    break;
            }



        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            DropDownList ddlService = (DropDownList)gvRespPayment.FooterRow.FindControl("ddlService");
            DropDownList ddlSponsor = (DropDownList)gvRespPayment.FooterRow.FindControl("ddlSponsor");
            DropDownList ddlTermOfPayment = (DropDownList)gvRespPayment.FooterRow.FindControl("ddlTermOfPayment");
            TextBox txtAmout = (TextBox)gvRespPayment.FooterRow.FindControl("txtAmout");

            Boolean isExist = cusPayers.Where(x => x.COST_TYPE_ID == Convert.ToInt32(ddlService.SelectedValue)).Any();
            if (!isExist)
            {
                if ((Convert.ToInt32(ddlService.SelectedValue) > 0) && (Convert.ToInt32(ddlSponsor.SelectedValue) > 0) && (Convert.ToInt32(ddlTermOfPayment.SelectedValue) > 0) && !String.IsNullOrEmpty(txtAmout.Text))
                {
                    TB_CUSTOMER_PAYER payer = new TB_CUSTOMER_PAYER();
                    payer.ID = cusPayers.Count + 1;
                    payer.SPONSOR_ID = Convert.ToInt32(ddlSponsor.SelectedValue);
                    payer.SPONSOR_NAME = ddlSponsor.SelectedItem.Text;
                    payer.COST_TYPE_ID = Convert.ToInt32(ddlService.SelectedValue);
                    payer.SERVICE_NAME = ddlService.SelectedItem.Text;
                    payer.SPONSOR_NAME = ddlSponsor.SelectedItem.Text;// String.Format("{0}  {1}", txtName.Text, txtSurname.Text);
                    payer.TERM_OF_PAYMENT_ID = 2;
                    payer.TERM_OF_PAYMENT_NAME = "SHARE (%)";
                    payer.AMOUNT = Convert.ToInt32(txtAmout.Text);
                    payer.ROOM_ID = this.ROOM_ID;
                    cusPayers.Add(payer);

                    gvRespPayment.DataSource = cusPayers.OrderBy(x => x.order).OrderBy(x => x.COST_TYPE_ID);
                    gvRespPayment.DataBind();
                    //Clear
                    ddlService.SelectedIndex = -1;
                    ddlSponsor.SelectedIndex = -1;
                    ddlTermOfPayment.SelectedIndex = -1;
                    txtAmout.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show(this.Page, "กรุณาตรวจสอบความถูกต้อง ประเภทค่าใช้จ่าย,ผู้สนับสนุน,รูปแบบการชำระ,จำนวนเงิน");
                }
            }
            else
            {
                MessageBox.Show(this.Page, "มีข้อมูลประเภทค่าใช้นี้อยู่แล้ว");

            }
            Console.WriteLine();
        }

        //
        #region "GRIDVIEW PAYMENTHISTORY"
        protected void gvRespPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //HiddenField spid = (HiddenField)e.Row.FindControl("hSPONSOR_ID");
                //Literal _litSponsor = (Literal)e.Row.FindControl("litSponsor");
                //if (spid != null && _litSponsor != null) { 
                //int sponsor_id = Convert.ToInt16(spid.Value);

                //TB_M_SPONSOR _sponsor = repSponsor.Table.Where(x => x.ID == sponsor_id).FirstOrDefault();
                //if (_sponsor != null)
                //{
                //    _litSponsor.Text = _sponsor.NAME;
                //}
                //}
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DropDownList ddlSponsor = (DropDownList)e.Row.FindControl("ddlSponsor");
                DropDownList ddlService = (DropDownList)e.Row.FindControl("ddlService");

                DropDownList ddlTermOfPayment = (DropDownList)e.Row.FindControl("ddlTermOfPayment");
                if (ddlService != null)
                {
                    ddlService.DataSource = repCostType.Table.ToList();
                    ddlService.DataBind();
                    ddlService.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
                }
                if (ddlSponsor != null)
                {
                    ddlSponsor.DataSource = repSponsor.Table.ToList();
                    ddlSponsor.DataBind();
                    ddlSponsor.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
                }
                if (ddlTermOfPayment != null)
                {
                    ddlTermOfPayment.DataSource = repTemOfPayment.Table.ToList();
                    ddlTermOfPayment.DataBind();
                    ddlTermOfPayment.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
                }
            }
        }

        protected void gvRespPayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRespPayment.EditIndex = -1;
            gvRespPayment.DataSource = this.cusPayers;
            gvRespPayment.DataBind();
        }

        protected void gvRespPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _deletePK = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            cusPayers.RemoveAll(x => x.ID == _deletePK);
            gvRespPayment.DataSource = cusPayers.OrderBy(x => x.order).OrderBy(x => x.COST_TYPE_ID);
            gvRespPayment.DataBind();
            Console.WriteLine();
        }

        protected void gvRespPayment_RowEditing(object sender, GridViewEditEventArgs e)
        {


            gvRespPayment.EditIndex = e.NewEditIndex;
            gvRespPayment.DataSource = cusPayers;
            gvRespPayment.DataBind();

            DropDownList ddlSponsor = (DropDownList)gvRespPayment.Rows[e.NewEditIndex].FindControl("ddlSponsor");
            DropDownList ddlTermOfPayment = (DropDownList)gvRespPayment.Rows[e.NewEditIndex].FindControl("ddlTermOfPayment");
            HiddenField hSPONSOR_ID = (HiddenField)gvRespPayment.Rows[e.NewEditIndex].FindControl("hSPONSOR_ID");
            HiddenField hTEAM_OF_PAYMENT_ID = (HiddenField)gvRespPayment.Rows[e.NewEditIndex].FindControl("hTEAM_OF_PAYMENT_ID");


            ddlSponsor.DataSource = repSponsor.Table.ToList();
            ddlSponsor.DataBind();

            ddlTermOfPayment.DataSource = repTemOfPayment.Table.ToList();
            ddlTermOfPayment.DataBind();

            ddlSponsor.SelectedValue = hSPONSOR_ID.Value;
            ddlTermOfPayment.SelectedValue = hTEAM_OF_PAYMENT_ID.Value;

        }

        protected void gvRespPayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            TextBox _txtAmout = (TextBox)gvRespPayment.Rows[e.RowIndex].FindControl("txtAmout");
            DropDownList _ddlSponsor = (DropDownList)gvRespPayment.Rows[e.RowIndex].FindControl("ddlSponsor");
            DropDownList _ddlTermOfPayment = (DropDownList)gvRespPayment.Rows[e.RowIndex].FindControl("ddlTermOfPayment");
            HiddenField _hSPONSOR_ID = (HiddenField)gvRespPayment.Rows[e.RowIndex].FindControl("hSPONSOR_ID");
            HiddenField _hTEAM_OF_PAYMENT_ID = (HiddenField)gvRespPayment.Rows[e.RowIndex].FindControl("hTEAM_OF_PAYMENT_ID");

            int _ID = Convert.ToInt32(gvRespPayment.DataKeys[e.RowIndex].Values[0].ToString());

            TB_CUSTOMER_PAYER tmp = this.cusPayers.Find(x => x.ID == _ID);
            if (tmp != null)
            {
                tmp.SPONSOR_ID = Convert.ToInt32(_ddlSponsor.SelectedValue);
                if (tmp.SPONSOR_ID > 0)
                {
                    tmp.SPONSOR_NAME = _ddlSponsor.SelectedItem.Text;
                }
                tmp.TERM_OF_PAYMENT_ID = Convert.ToInt32(_ddlTermOfPayment.SelectedValue);
                tmp.TERM_OF_PAYMENT_NAME = _ddlTermOfPayment.SelectedItem.Text;
                tmp.AMOUNT = String.IsNullOrEmpty(_txtAmout.Text) ? 0 : Convert.ToDecimal(_txtAmout.Text);
            }
            gvRespPayment.EditIndex = -1;
            gvRespPayment.DataSource = cusPayers;
            gvRespPayment.DataBind();
        }

        protected void gvRespPayment_DataBound(object sender, EventArgs e)
        {




        }

        protected void gvRespPayment_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion

        #region "GRIDVIEW PAYMENT"

        protected void gvPaymentHistory_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            //this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            //switch (cmd)
            //{
            //    case CommandNameEnum.PrintInvoice:
            //        print(0, 0, PKID);
            //        break;
            //}
        }
        protected void gvPaymentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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

        #region "GRIDVIEW INVOICE DETAIL"
        protected void gvInvoiceDetail_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvInvoiceDetail.EditIndex = e.NewEditIndex;
            gvInvoiceDetail.DataSource = this.InvDetails.OrderBy(x => x.ID); ;
            gvInvoiceDetail.DataBind();
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
            gvInvoiceDetail.DataSource = this.InvDetails.OrderBy(x => x.ID);
            gvInvoiceDetail.DataBind();
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
            gvInvoiceDetail.DataSource = this.InvDetails.OrderBy(x => x.ID); ;
            gvInvoiceDetail.DataBind();
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

        //
        private void generateTermOfPayment()
        {
            List<TB_CUSTOMER_PAYER> tmp = new List<TB_CUSTOMER_PAYER>();
            TB_ROOM room = repRoom.GetById(this.ROOM_ID);
            if (room != null)
            {
                int[] roomRentIDAndFurIDs = { 1, 2 };
                List<TB_RATES_GROUP_DETAIL> rateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == room.RATES_GROUP_ID && roomRentIDAndFurIDs.Contains(x.COST_TYPE_ID.Value)).ToList();
                foreach (TB_RATES_GROUP_DETAIL detail in rateGroup)
                {
                    TB_CUSTOMER_PAYER payer = new TB_CUSTOMER_PAYER();
                    payer.ID = detail.COST_TYPE_ID.Value;
                    payer.CUS_ID = this.CUS_PK_ID;
                    //payer.order = order + 1;
                    payer.COST_TYPE_ID = detail.COST_TYPE_ID;
                    payer.ROOM_RATE = detail.AMOUNT.Value;
                    payer.SPONSOR_ID = 0;
                    payer.SPONSOR_NAME = "จ่ายเอง";// String.Format("{0}  {1}", txtName.Text, txtSurname.Text);

                    payer.SERVICE_NAME = repCostType.Table.Where(x => x.ID == detail.COST_TYPE_ID).FirstOrDefault().NAME;

                    payer.TERM_OF_PAYMENT_ID = 1;
                    payer.TERM_OF_PAYMENT_NAME = "FIX AMOUNT";
                    payer.AMOUNT = detail.AMOUNT.Value;
                    payer.ROOM_ID = this.ROOM_ID;
                    tmp.Add(payer);
                }
            }

            this.cusPayers = tmp;
            gvRespPayment.DataSource = cusPayers.OrderBy(x => x.order).OrderBy(x => x.COST_TYPE_ID);
            gvRespPayment.DataBind();

        }

        //private void fillMeterData()
        //{
        //    txtPostingDate.Text = DateTime.Now.ToString("MM/yyyy");


        //    if (objRoom != null)
        //    {
        //        int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
        //        customers = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
        //        if (customers != null && customers.Count > 0)
        //        {

        //            Boolean notHasStdNum = customers.Where(x => x.HAS_STDNUM == false).Any();
        //            InvoiceType = (notHasStdNum) ? InvoiceTypeEnum.ROOM : InvoiceTypeEnum.CUSTOMER;

        //            #region "SET PREVIOS METER"

        //            DateTime postingDate = Convert.ToDateTime(txtPostingDate.Text);


        //            //Check Current Month
        //            List<TB_ROOM_METER> meters = repMeter.Table.Where(x => x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).ToList();
        //            if (meters != null && meters.Count > 0)
        //            {
        //                TB_ROOM_METER meterElec = meters.Where(x => x.METER_TYPE == 3).FirstOrDefault();
        //                if (meterElec != null)
        //                {
        //                    txtElecMeterStart.Text = meterElec.METER_START.Value.ToString();
        //                    txtElecMeterEnd.Text = meterElec.METER_END.Value.ToString();
        //                }
        //                TB_ROOM_METER meterWater = meters.Where(x => x.METER_TYPE == 4).FirstOrDefault();
        //                if (meterWater != null)
        //                {
        //                    txtWaterMeterStart.Text = meterWater.METER_START.Value.ToString();
        //                    txtWaterMeterEnd.Text = meterWater.METER_END.Value.ToString();
        //                }
        //            }
        //            else
        //            {
        //                //Check Prevoid Month
        //                postingDate = postingDate.AddMonths(-1);
        //                meters = repMeter.Table.Where(x => x.ROOM_ID == objRoom.ID && x.METER_DATE.Year == postingDate.Year && x.METER_DATE.Month == postingDate.Month).ToList();
        //                if (meters != null && meters.Count > 0)
        //                {
        //                    TB_ROOM_METER meterElec = meters.Where(x => x.METER_TYPE == 3).FirstOrDefault();
        //                    if (meterElec != null)
        //                    {
        //                        txtElecMeterStart.Text = meterElec.METER_END.Value.ToString();
        //                    }
        //                    else
        //                    {
        //                        txtElecMeterStart.Text = string.Empty;
        //                    }
        //                    TB_ROOM_METER meterWater = meters.Where(x => x.METER_TYPE == 4).FirstOrDefault();
        //                    if (meterWater != null)
        //                    {
        //                        txtWaterMeterStart.Text = meterWater.METER_END.Value.ToString();
        //                    }
        //                    else
        //                    {
        //                        txtWaterMeterStart.Text = string.Empty;
        //                    }
        //                }
        //            }
        //            #endregion



        //        }
        //        else
        //        {
        //            MessageBox.Show(this.Page, String.Format(Resources.MSG_NO_CUSTOMER_IN_ROOM, objRoom.NUMBER));
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(this.Page, Resources.MSG_NO_ROOM);
        //    }
        //}

        private void calculate()
        {

            List<InvDetail> tmps = new List<InvDetail>();


            if (objRoom != null)
            {
                int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                List<TB_CUSTOMER> listCus = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
                if (listCus != null && listCus.Count > 0)
                {

                    #region "Payment Detail"
                    int order = 1;
                    foreach (TB_CUSTOMER cus in listCus.Where(x => x.ID == this.CUS_PK_ID))
                    {
                        List<TB_CUSTOMER_PAYER> cusPays = repCustomerPayer.Table.Where(x => x.CUS_ID == cus.ID && x.SPONSOR_ID != 0).ToList();

                        List<TB_RATES_GROUP_DETAIL> details = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).ToList();
                        foreach (TB_RATES_GROUP_DETAIL _detail in details)
                        {
                            //TB_CUSTOMER_PAYER sponsor = cusPays.Where(x => x.COST_TYPE_ID == _detail.COST_TYPE_ID).FirstOrDefault();
                            //if (sponsor == null)
                            //{
                            InvDetail _tmp = new InvDetail();
                            _tmp.ID = order;
                            _tmp.SPONSOR_ID = 0;
                            _tmp.CUS_ID = cus.ID;
                            //_tmp.ROOM_ID = cus.ROOM_ID.Value;
                            //_tmp.BUILD_ID = _tmp.ROOM_ID;
                            //_tmp.ROOM_NUMBER = _tmp.ROOM_NUMBER;
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

                            //if (_detail.COST_TYPE_ID == 3)
                            //{
                            //    _tmp.RATE_UNIT = Convert.ToInt32(lbElecUnit.Text);
                            //    _tmp.PAYMENT_AMOUNT = _detail.AMOUNT * Convert.ToInt32(lbElecUnit.Text);
                            //}
                            //else if (_detail.COST_TYPE_ID == 4)
                            //{
                            //    _tmp.RATE_UNIT = Convert.ToInt32(lbWaterUnit.Text);
                            //    _tmp.PAYMENT_AMOUNT = _detail.AMOUNT * Convert.ToInt32(lbWaterUnit.Text);
                            //}
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
                                        TB_CUSTOMER _customer = listCus.Where(x => x.PAYER == true).First();
                                        if (_customer != null)
                                        {
                                            _tmp.PAYER_NAME = String.Format("{0} {1}", _customer.FIRSTNAME, _customer.SURNAME);
                                        }
                                        else
                                        {
                                            _tmp.PAYER_NAME = String.Format("{0} {1}", "ไม่ได้ระบุผู้ชำระ", "");

                                        }
                                        break;
                                }
                            }
                            _tmp.row_type = Convert.ToInt32(PayTypeEnum.SELF);
                            tmps.Add(_tmp);
                            order++;
                            //}
                        }

                    }

                }


                this.InvDetails = tmps;
                gvInvoiceDetail.DataSource = this.InvDetails.OrderBy(x => x.ID);
                gvInvoiceDetail.DataBind();
                //}
                    #endregion
            }
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRoom_Click(object sender, EventArgs e)
        {
            List<TB_M_BUILD> build = repBuild.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();
            ddlBuild.DataSource = build;
            ddlBuild.DataBind();
            if (build != null && build.Count > 1)
            {
                ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }
            else
            {
                int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
                ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList();
                ddlRoom.DataBind();
                ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }

            ModolPopupExtender.Show();
        }

        protected void ddlBuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlBuild.SelectedValue))
            {
                int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
                ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList();
                ddlRoom.DataBind();
                ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
                ModolPopupExtender.Show();
            }
        }

        protected void btnCancelSearch_Click(object sender, EventArgs e)
        {

            gvResult.DataSource = searchRoomResult;
            gvResult.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingRoom();
            ModolPopupExtender.Show();
        }
    }
}