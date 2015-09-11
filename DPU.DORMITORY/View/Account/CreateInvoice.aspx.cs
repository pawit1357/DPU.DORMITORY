using CrystalDecisions.CrystalReports.Engine;
using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        private Repository<TB_M_FUND> repFund;
        //private Repository<TB_M_SERVICE> repService;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_INVOICE> repInvoice;
        private Repository<TB_INVOICE_DETAIL> repInvoiceDetail;
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_CUSTOMER_PROFILE> repCustomerProfile;
        private Repository<TB_CUSTOMER_FUND> repCustomerFund;

        private Repository<TB_M_CUSTOMER_TYPE> repCustomerType;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_ROOM_METER> repMeter;

        public CreateInvoice()
        {
            repFund = unitOfWork.Repository<TB_M_FUND>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            //repService = unitOfWork.Repository<TB_M_SERVICE>();
            repInvoice = unitOfWork.Repository<TB_INVOICE>();
            repInvoiceDetail = unitOfWork.Repository<TB_INVOICE_DETAIL>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repCustomerProfile = unitOfWork.Repository<TB_CUSTOMER_PROFILE>();
            repCustomerFund = unitOfWork.Repository<TB_CUSTOMER_FUND>();
            repCustomerType = unitOfWork.Repository<TB_M_CUSTOMER_TYPE>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repMeter = unitOfWork.Repository<TB_ROOM_METER>();
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

        public TB_INVOICE objInvoice
        {
            //แสดงที่ยังไม่จ่ายตัง
            get
            {
                TB_INVOICE tmp = new TB_INVOICE();
                tmp.RoomNumber = txtRoom.Text;
                tmp.FilterPaymentStatus = false;
                tmp.PAYMENT_STATUS = false;
                tmp.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);
                tmp.OTHER = txtOther.Text;
                return tmp;
            }
        }

        public List<TB_INVOICE_DETAIL> objInvoiceDetail
        {
            get
            {
                List<TB_RATES_GROUP_DETAIL> listRateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).ToList();
                TB_M_BUILD build = repBuild.Table.Where(x => x.BUILD_ID == objRoom.BUILD_ID).FirstOrDefault();
                List<TB_INVOICE_DETAIL> invDets = new List<TB_INVOICE_DETAIL>();
                TB_INVOICE_DETAIL invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 1;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost01.Text) ? Convert.ToDecimal(txtCost01.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                TB_RATES_GROUP_DETAIL rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 2;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost02.Text) ? Convert.ToDecimal(txtCost02.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 3;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost03.Text) ? Convert.ToDecimal(txtCost03.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 4;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost04.Text) ? Convert.ToDecimal(txtCost04.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 5;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost05.Text) ? Convert.ToDecimal(txtCost05.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 6;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost06.Text) ? Convert.ToDecimal(txtCost06.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 7;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost07.Text) ? Convert.ToDecimal(txtCost07.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 8;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost08.Text) ? Convert.ToDecimal(txtCost08.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 9;
                invDet.AMOUNT = CustomUtils.isNumber(txtCost09.Text) ? Convert.ToDecimal(txtCost09.Text) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                return invDets;

            }
        }
        public List<TB_INVOICE_DETAIL> objInvoiceDetail1
        {
            get
            {
                List<TB_RATES_GROUP_DETAIL> listRateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).ToList();
                TB_M_BUILD build = repBuild.Table.Where(x => x.BUILD_ID == objRoom.BUILD_ID).FirstOrDefault();

                List<TB_INVOICE_DETAIL> invDets = new List<TB_INVOICE_DETAIL>();
                TB_INVOICE_DETAIL invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 1;
                invDet.PAY_BY = Convert.ToInt32(ddlPay01_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost01_1.Text) ? Convert.ToDecimal(txtCost01_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost01_1.Value) ? Convert.ToDecimal(hCost01_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                TB_RATES_GROUP_DETAIL rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 2;
                invDet.PAY_BY = Convert.ToInt32(ddlPay02_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost02_1.Text) ? Convert.ToDecimal(txtCost02_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost02_1.Value) ? Convert.ToDecimal(hCost02_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 3;
                invDet.PAY_BY = Convert.ToInt32(ddlPay03_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost03_1.Text) ? Convert.ToDecimal(txtCost03_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost03_1.Value) ? Convert.ToDecimal(hCost03_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 4;
                invDet.PAY_BY = Convert.ToInt32(ddlPay04_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost04_1.Text) ? Convert.ToDecimal(txtCost04_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost04_1.Value) ? Convert.ToDecimal(hCost04_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 5;
                invDet.PAY_BY = Convert.ToInt32(ddlPay05_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost05_1.Text) ? Convert.ToDecimal(txtCost05_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost05_1.Value) ? Convert.ToDecimal(hCost05_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 6;
                invDet.PAY_BY = Convert.ToInt32(ddlPay06_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost06_1.Text) ? Convert.ToDecimal(txtCost06_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost06_1.Value) ? Convert.ToDecimal(hCost06_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 7;
                invDet.PAY_BY = Convert.ToInt32(ddlPay07_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost07_1.Text) ? Convert.ToDecimal(txtCost07_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost07_1.Value) ? Convert.ToDecimal(hCost07_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 8;
                invDet.PAY_BY = Convert.ToInt32(ddlPay08_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost08_1.Text) ? Convert.ToDecimal(txtCost08_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost08_1.Value) ? Convert.ToDecimal(hCost08_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 9;
                invDet.PAY_BY = Convert.ToInt32(ddlPay09_1.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost09_1.Text) ? Convert.ToDecimal(txtCost09_1.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost09_1.Value) ? Convert.ToDecimal(hCost09_1.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                return invDets;

            }
        }
        public List<TB_INVOICE_DETAIL> objInvoiceDetail2
        {
            get
            {
                List<TB_RATES_GROUP_DETAIL> listRateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).ToList();
                TB_M_BUILD build = repBuild.Table.Where(x => x.BUILD_ID == objRoom.BUILD_ID).FirstOrDefault();

                List<TB_INVOICE_DETAIL> invDets = new List<TB_INVOICE_DETAIL>();
                TB_INVOICE_DETAIL invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 1;
                invDet.PAY_BY = Convert.ToInt32(ddlPay01_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost01_2.Text) ? Convert.ToDecimal(txtCost01_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost01_2.Value) ? Convert.ToDecimal(hCost01_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                TB_RATES_GROUP_DETAIL rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 2;
                invDet.PAY_BY = Convert.ToInt32(ddlPay02_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost02_2.Text) ? Convert.ToDecimal(txtCost02_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost02_2.Value) ? Convert.ToDecimal(hCost02_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 3;
                invDet.PAY_BY = Convert.ToInt32(ddlPay03_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost03_2.Text) ? Convert.ToDecimal(txtCost03_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost03_2.Value) ? Convert.ToDecimal(hCost03_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 4;
                invDet.PAY_BY = Convert.ToInt32(ddlPay04_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost04_2.Text) ? Convert.ToDecimal(txtCost04_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost04_2.Value) ? Convert.ToDecimal(hCost04_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 5;
                invDet.PAY_BY = Convert.ToInt32(ddlPay05_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost05_2.Text) ? Convert.ToDecimal(txtCost05_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost05_2.Value) ? Convert.ToDecimal(hCost05_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 6;
                invDet.PAY_BY = Convert.ToInt32(ddlPay06_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost06_2.Text) ? Convert.ToDecimal(txtCost06_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost06_2.Value) ? Convert.ToDecimal(hCost06_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 7;
                invDet.PAY_BY = Convert.ToInt32(ddlPay07_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost07_2.Text) ? Convert.ToDecimal(txtCost07_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost07_2.Value) ? Convert.ToDecimal(hCost07_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 8;
                invDet.PAY_BY = Convert.ToInt32(ddlPay08_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost08_2.Text) ? Convert.ToDecimal(txtCost08_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost08_2.Value) ? Convert.ToDecimal(hCost08_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                invDet = new TB_INVOICE_DETAIL();
                invDet.SERVICE_ID = 9;
                invDet.PAY_BY = Convert.ToInt32(ddlPay09_2.SelectedValue);
                invDet.AMOUNT = CustomUtils.isNumber(txtCost09_2.Text) ? Convert.ToDecimal(txtCost09_2.Text) : Convert.ToDecimal(0);
                invDet.PAY_AMOUT = CustomUtils.isNumber(hCost09_2.Value) ? Convert.ToDecimal(hCost09_2.Value) : Convert.ToDecimal(0);
                invDet.COMPANY = Convert.ToInt32(build.COMPANY);
                invDet.BA = Convert.ToInt32(build.BA);
                invDet.PROFIT_CTR = Convert.ToInt32(build.PROFIT_CTR);
                rgd = listRateGroup.Where(x => x.SERVICE_ID == invDet.SERVICE_ID).FirstOrDefault();
                if (rgd != null)
                {
                    invDet.MAIN_TRANS = rgd.MAINTRAN;
                    invDet.SUB_TRANS = rgd.SUBTRAN;
                }
                invDets.Add(invDet);
                return invDets;

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

            IEnumerable paymentList = objInvoice.SearchPaymentHistory();
            gvPaymentHistory.DataSource = paymentList;
            gvPaymentHistory.DataBind();
            gvPaymentHistory.UseAccessibleHeader = true;
            gvPaymentHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        private void initialPage()
        {
            ddlBuild.DataSource = repBuild.Table.ToList();
            ddlBuild.DataBind();
            txtStayDay.Text = DateTime.Now.Day.ToString();

            btnCalculate.CssClass = Constants.CSS_BUTTON_CALCULATE;
            btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
            btnPrintInvoice.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
            pPaymentInfo.Visible = false;
            btnSave.Attributes.Add("onclick", "return confirm('ต้องการบันทึกข้อมูล?');");

            th_cus_1.Visible = false;
            th_cus_2.Visible = false;
            th_cus_1_rate.Visible = false;
            th_cus_2_rate.Visible = false;

            td_cus_1_1.Visible = false;
            td_cus_1_2.Visible = false;
            td_cus_1_3.Visible = false;
            td_cus_1_4.Visible = false;
            td_cus_1_5.Visible = false;
            td_cus_1_6.Visible = false;
            td_cus_1_7.Visible = false;
            td_cus_1_8.Visible = false;
            td_cus_1_9.Visible = false;

            td_cus_2_1.Visible = false;
            td_cus_2_2.Visible = false;
            td_cus_2_3.Visible = false;
            td_cus_2_4.Visible = false;
            td_cus_2_5.Visible = false;
            td_cus_2_6.Visible = false;
            td_cus_2_7.Visible = false;
            td_cus_2_8.Visible = false;
            td_cus_2_9.Visible = false;


            td_cus_1_1_pay.Visible = false;
            td_cus_1_2_pay.Visible = false;
            td_cus_1_3_pay.Visible = false;
            td_cus_1_4_pay.Visible = false;
            td_cus_1_5_pay.Visible = false;
            td_cus_1_6_pay.Visible = false;
            td_cus_1_7_pay.Visible = false;
            td_cus_1_8_pay.Visible = false;
            td_cus_1_9_pay.Visible = false;

            td_cus_2_1_pay.Visible = false;
            td_cus_2_2_pay.Visible = false;
            td_cus_2_3_pay.Visible = false;
            td_cus_2_4_pay.Visible = false;
            td_cus_2_5_pay.Visible = false;
            td_cus_2_6_pay.Visible = false;
            td_cus_2_7_pay.Visible = false;
            td_cus_2_8_pay.Visible = false;
            td_cus_2_9_pay.Visible = false;

            td_amout_1.Visible = true;
            td_amout_2.Visible = false;
            td_amout_3.Visible = false;
            td_amout_bank_1.Visible = false;
            td_amout_bank_2.Visible = false;
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

                    pPaymentInfo.Visible = true;
                    th_cus_1.Visible = false;
                    td_cus_1_1.Visible = false;
                    td_cus_1_2.Visible = false;
                    td_cus_1_3.Visible = false;
                    td_cus_1_4.Visible = false;
                    td_cus_1_5.Visible = false;
                    td_cus_1_6.Visible = false;
                    td_cus_1_7.Visible = false;
                    td_cus_1_8.Visible = false;
                    td_cus_1_9.Visible = false;

                    th_cus_2.Visible = false;
                    td_cus_2_1.Visible = false;
                    td_cus_2_2.Visible = false;
                    td_cus_2_3.Visible = false;
                    td_cus_2_4.Visible = false;
                    td_cus_2_5.Visible = false;
                    td_cus_2_6.Visible = false;
                    td_cus_2_7.Visible = false;
                    td_cus_2_8.Visible = false;
                    td_cus_2_9.Visible = false;
                }
                else
                {
                    MessageBox.Show(this.Page, String.Format(Resources.MSG_NO_CUSTOMER_IN_ROOM, objRoom.NUMBER));
                }
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

                #region "INVOICE"
                switch (InvoiceType)
                {
                    case InvoiceTypeEnum.ROOM:
                        if (Convert.ToDecimal(txtAmout1.Text) > 0)
                        {
                            TB_INVOICE inv = new TB_INVOICE();
                            inv.REF_ID = objRoom.ID;
                            inv.POSTING_DATE = postingDate;
                            inv.AMOUNT = Convert.ToDecimal(txtAmout1.Text);
                            inv.STAY_DAY = Convert.ToInt32(txtStayDay.Text);
                            inv.PAYMENT_STATUS = false;
                            inv.UPDATE_BY = userLogin.USER_ID;
                            inv.CREATE_DATE = DateTime.Now;
                            inv.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);//Normal status is open
                            inv.INVOICE_TYPE = Convert.ToInt32(InvoiceTypeEnum.ROOM);
                            inv.TB_INVOICE_DETAIL = objInvoiceDetail;
                            inv.OTHER = txtOther.Text;
                            if (inv.AMOUNT > 0)
                            {
                                repInvoice.Insert(inv);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                        break;
                    case InvoiceTypeEnum.CUSTOMER:
                        int index = 1;

                        foreach (TB_CUSTOMER customer in customers)
                        {

                            TB_INVOICE inv1 = new TB_INVOICE();
                            inv1.REF_ID = customer.ID;
                            inv1.POSTING_DATE = postingDate;
                            inv1.STAY_DAY = Convert.ToInt32(txtStayDay.Text);
                            inv1.PAYMENT_STATUS = false;
                            inv1.UPDATE_BY = userLogin.USER_ID;
                            inv1.CREATE_DATE = DateTime.Now;
                            inv1.STATUS = Convert.ToInt32(InvoiceStatusEmum.Open);//Normal status is open
                            inv1.INVOICE_TYPE = Convert.ToInt32(InvoiceTypeEnum.CUSTOMER);
                            inv1.TB_INVOICE_DETAIL = (index == 1) ? objInvoiceDetail1 : objInvoiceDetail2;
                            inv1.AMOUNT = (Decimal)inv1.TB_INVOICE_DETAIL.Sum(x => x.AMOUNT);

                            inv1.OTHER = txtOther.Text;
                            if (inv1.AMOUNT > 0)
                            {
                                repInvoice.Insert(inv1);
                            }


                            index++;
                        }

                        break;
                }
                #endregion

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

            }
            #region "MESSAGE RESULT"
            String errorMessage = repInvoice.errorMessage;
            if (!String.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(this, errorMessage);
            }
            else
            {

                MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS);

                //MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }
            #endregion

            btnCalculate.CssClass = Constants.CSS_DISABLED_BUTTON_CALCULATE;
            btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
            btnPrintInvoice.CssClass = Constants.CSS_BUTTON_SAVE;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            calculate();
        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            print(0);
            MessageBox.Show(this, "พิมพ์เรียบร้อยแล้ว", Constants.LINK_CREATE_INVOICE);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCost01.Text = String.Empty;
            txtCost02.Text = String.Empty;
            txtCost03.Text = String.Empty;
            txtCost04.Text = String.Empty;
            txtCost05.Text = String.Empty;
            txtCost06.Text = String.Empty;
            txtCost07.Text = String.Empty;
            txtCost08.Text = String.Empty;
            txtCost09.Text = String.Empty;

            txtCost01_1.Text = String.Empty;
            txtCost02_1.Text = String.Empty;
            txtCost03_1.Text = String.Empty;
            txtCost04_1.Text = String.Empty;
            txtCost05_1.Text = String.Empty;
            txtCost06_1.Text = String.Empty;
            txtCost07_1.Text = String.Empty;
            txtCost08_1.Text = String.Empty;
            txtCost09_1.Text = String.Empty;

            txtCost01_2.Text = String.Empty;
            txtCost02_2.Text = String.Empty;
            txtCost03_2.Text = String.Empty;
            txtCost04_2.Text = String.Empty;
            txtCost05_2.Text = String.Empty;
            txtCost06_2.Text = String.Empty;
            txtCost07_2.Text = String.Empty;
            txtCost08_2.Text = String.Empty;
            txtCost09_2.Text = String.Empty;

            txtAmout1.Text = String.Empty;
            txtAmout2.Text = String.Empty;
            txtAmout3.Text = String.Empty;

            th_cus_1.Visible = false;
            th_cus_2.Visible = false;
            th_cus_1_rate.Visible = false;
            th_cus_2_rate.Visible = false;

            td_cus_1_1.Visible = false;
            td_cus_1_2.Visible = false;
            td_cus_1_3.Visible = false;
            td_cus_1_4.Visible = false;
            td_cus_1_5.Visible = false;
            td_cus_1_6.Visible = false;
            td_cus_1_7.Visible = false;
            td_cus_1_8.Visible = false;
            td_cus_1_9.Visible = false;

            td_cus_2_1.Visible = false;
            td_cus_2_2.Visible = false;
            td_cus_2_3.Visible = false;
            td_cus_2_4.Visible = false;
            td_cus_2_5.Visible = false;
            td_cus_2_6.Visible = false;
            td_cus_2_7.Visible = false;
            td_cus_2_8.Visible = false;
            td_cus_2_9.Visible = false;


            td_cus_1_1_pay.Visible = false;
            td_cus_1_2_pay.Visible = false;
            td_cus_1_3_pay.Visible = false;
            td_cus_1_4_pay.Visible = false;
            td_cus_1_5_pay.Visible = false;
            td_cus_1_6_pay.Visible = false;
            td_cus_1_7_pay.Visible = false;
            td_cus_1_8_pay.Visible = false;
            td_cus_1_9_pay.Visible = false;

            td_cus_2_1_pay.Visible = false;
            td_cus_2_2_pay.Visible = false;
            td_cus_2_3_pay.Visible = false;
            td_cus_2_4_pay.Visible = false;
            td_cus_2_5_pay.Visible = false;
            td_cus_2_6_pay.Visible = false;
            td_cus_2_7_pay.Visible = false;
            td_cus_2_8_pay.Visible = false;
            td_cus_2_9_pay.Visible = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.LINK_CREATE_INVOICE);
        }

        protected void txtPostingDate_TextChanged(object sender, EventArgs e)
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

        protected void gvPaymentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litPaymentStatus = (Literal)e.Row.FindControl("litPaymentStatus");
                //LinkButton _btnPay = (LinkButton)e.Row.FindControl("btnPay");


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
            crystalReport.PrintToPrinter(1, true, 0, 0);
        }

        protected void ddlPay01_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);
            switch (ddl.SelectedValue)
            {
                case "0":
                    int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                    List<TB_CUSTOMER> listCus = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
                    if (listCus != null && listCus.Count > 0)
                    {
                        int numOfCustomerInRoom = listCus.Count;
                        decimal cus01 = CustomUtils.isNumber(txtCost01.Text) ? Convert.ToDecimal(txtCost01.Text) : Convert.ToDecimal(0);
                        decimal cus02 = CustomUtils.isNumber(txtCost02.Text) ? Convert.ToDecimal(txtCost02.Text) : Convert.ToDecimal(0);
                        decimal cus03 = CustomUtils.isNumber(txtCost03.Text) ? Convert.ToDecimal(txtCost03.Text) : Convert.ToDecimal(0);
                        decimal cus04 = CustomUtils.isNumber(txtCost04.Text) ? Convert.ToDecimal(txtCost04.Text) : Convert.ToDecimal(0);
                        decimal cus05 = CustomUtils.isNumber(txtCost05.Text) ? Convert.ToDecimal(txtCost05.Text) : Convert.ToDecimal(0);
                        decimal cus06 = CustomUtils.isNumber(txtCost06.Text) ? Convert.ToDecimal(txtCost06.Text) : Convert.ToDecimal(0);
                        decimal cus07 = CustomUtils.isNumber(txtCost07.Text) ? Convert.ToDecimal(txtCost07.Text) : Convert.ToDecimal(0);
                        decimal cus08 = CustomUtils.isNumber(txtCost08.Text) ? Convert.ToDecimal(txtCost08.Text) : Convert.ToDecimal(0);
                        decimal cus09 = CustomUtils.isNumber(txtCost09.Text) ? Convert.ToDecimal(txtCost09.Text) : Convert.ToDecimal(0);


                        switch (ddl.ID)
                        {
                            case "ddlPay01_1":
                                txtCost01_1.Text = (cus01 / numOfCustomerInRoom).ToString();
                                hCost01_1.Value = "0";
                                break;
                            case "ddlPay02_1":
                                txtCost02_1.Text = (cus02 / numOfCustomerInRoom).ToString();
                                hCost02_1.Value = "0";
                                break;
                            case "ddlPay03_1":
                                txtCost03_1.Text = (cus03 / numOfCustomerInRoom).ToString();
                                hCost03_1.Value = "0";
                                break;
                            case "ddlPay04_1":
                                txtCost04_1.Text = (cus04 / numOfCustomerInRoom).ToString();
                                hCost04_1.Value = "0";
                                break;
                            case "ddlPay05_1":
                                txtCost05_1.Text = (cus05 / numOfCustomerInRoom).ToString();
                                hCost05_1.Value = "0";
                                break;
                            case "ddlPay06_1":
                                txtCost06_1.Text = (cus06 / numOfCustomerInRoom).ToString();
                                hCost06_1.Value = "0";
                                break;
                            case "ddlPay07_1":
                                txtCost07_1.Text = (cus07 / numOfCustomerInRoom).ToString();
                                hCost07_1.Value = "0";
                                break;
                            case "ddlPay08_1":
                                txtCost08_1.Text = (cus08 / numOfCustomerInRoom).ToString();
                                hCost08_1.Value = "0";
                                break;
                            case "ddlPay09_1":
                                txtCost09_1.Text = (cus09 / numOfCustomerInRoom).ToString();
                                hCost09_1.Value = "0";
                                break;

                            case "ddlPay01_2":
                                txtCost01_2.Text = (cus01 / numOfCustomerInRoom).ToString();
                                hCost01_2.Value = "0";
                                break;
                            case "ddlPay02_2":
                                txtCost02_2.Text = (cus02 / numOfCustomerInRoom).ToString();
                                hCost02_2.Value = "0";
                                break;
                            case "ddlPay03_2":
                                txtCost03_2.Text = (cus03 / numOfCustomerInRoom).ToString();
                                hCost03_2.Value = "0";
                                break;
                            case "ddlPay04_2":
                                txtCost04_2.Text = (cus04 / numOfCustomerInRoom).ToString();
                                hCost04_2.Value = "0";
                                break;
                            case "ddlPay05_2":
                                txtCost05_2.Text = (cus05 / numOfCustomerInRoom).ToString();
                                hCost05_2.Value = "0";
                                break;
                            case "ddlPay06_2":
                                txtCost06_2.Text = (cus06 / numOfCustomerInRoom).ToString();
                                hCost06_2.Value = "0";
                                break;
                            case "ddlPay07_2":
                                txtCost07_2.Text = (cus07 / numOfCustomerInRoom).ToString();
                                hCost07_2.Value = "0";
                                break;
                            case "ddlPay08_2":
                                txtCost08_2.Text = (cus08 / numOfCustomerInRoom).ToString();
                                hCost08_2.Value = "0";
                                break;
                            case "ddlPay09_2":
                                txtCost09_2.Text = (cus09 / numOfCustomerInRoom).ToString();
                                hCost09_2.Value = "0";
                                break;
                        }

                    }
                    break;
                default:
                    switch (ddl.ID)
                    {
                        case "ddlPay01_1": hCost01_1.Value = txtCost01_1.Text; txtCost01_1.Text = "0"; break;
                        case "ddlPay02_1": hCost02_1.Value = txtCost02_1.Text; txtCost02_1.Text = "0"; break;
                        case "ddlPay03_1": hCost03_1.Value = txtCost03_1.Text; txtCost03_1.Text = "0"; break;
                        case "ddlPay04_1": hCost04_1.Value = txtCost04_1.Text; txtCost04_1.Text = "0"; break;
                        case "ddlPay05_1": hCost05_1.Value = txtCost05_1.Text; txtCost05_1.Text = "0"; break;
                        case "ddlPay06_1": hCost06_1.Value = txtCost06_1.Text; txtCost06_1.Text = "0"; break;
                        case "ddlPay07_1": hCost07_1.Value = txtCost07_1.Text; txtCost07_1.Text = "0"; break;
                        case "ddlPay08_1": hCost08_1.Value = txtCost08_1.Text; txtCost08_1.Text = "0"; break;
                        case "ddlPay09_1": hCost09_1.Value = txtCost09_1.Text; txtCost09_1.Text = "0"; break;

                        case "ddlPay01_2": hCost01_2.Value = txtCost01_2.Text; txtCost01_2.Text = "0"; break;
                        case "ddlPay02_2": hCost02_2.Value = txtCost02_2.Text; txtCost02_2.Text = "0"; break;
                        case "ddlPay03_2": hCost03_2.Value = txtCost03_2.Text; txtCost03_2.Text = "0"; break;
                        case "ddlPay04_2": hCost04_2.Value = txtCost04_2.Text; txtCost04_2.Text = "0"; break;
                        case "ddlPay05_2": hCost05_2.Value = txtCost05_2.Text; txtCost05_2.Text = "0"; break;
                        case "ddlPay06_2": hCost06_2.Value = txtCost06_2.Text; txtCost06_2.Text = "0"; break;
                        case "ddlPay07_2": hCost07_2.Value = txtCost07_2.Text; txtCost07_2.Text = "0"; break;
                        case "ddlPay08_2": hCost08_2.Value = txtCost08_2.Text; txtCost08_2.Text = "0"; break;
                        case "ddlPay09_2": hCost09_2.Value = txtCost09_2.Text; txtCost09_2.Text = "0"; break;
                    }
                    break;
            }
            txtAmout2.Text = (
      Convert.ToDouble(String.IsNullOrEmpty(txtCost01_1.Text) ? "0" : txtCost01_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost02_1.Text) ? "0" : txtCost02_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost03_1.Text) ? "0" : txtCost03_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost04_1.Text) ? "0" : txtCost04_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost05_1.Text) ? "0" : txtCost05_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost06_1.Text) ? "0" : txtCost06_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost07_1.Text) ? "0" : txtCost07_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost08_1.Text) ? "0" : txtCost08_1.Text) +
      Convert.ToDouble(String.IsNullOrEmpty(txtCost09_1.Text) ? "0" : txtCost09_1.Text)).ToString();

            txtAmout3.Text = (
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost01_2.Text) ? "0" : txtCost01_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost02_2.Text) ? "0" : txtCost02_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost03_2.Text) ? "0" : txtCost03_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost04_2.Text) ? "0" : txtCost04_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost05_2.Text) ? "0" : txtCost05_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost06_2.Text) ? "0" : txtCost06_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost07_2.Text) ? "0" : txtCost07_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost08_2.Text) ? "0" : txtCost08_2.Text) +
                  Convert.ToDouble(String.IsNullOrEmpty(txtCost09_2.Text) ? "0" : txtCost09_2.Text)).ToString();
        }

        private void calculate()
        {
            int cusStatus = Convert.ToInt32(CustomerStatusEnum.CheckIn);
            List<TB_CUSTOMER> listCus = repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID && x.STATUS == cusStatus).ToList();
            if (listCus != null && listCus.Count > 0)
            {


                #region "SET PAYMENT DETAIL"
                List<TB_RATES_GROUP_DETAIL> invDetails = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == (int)objRoom.RATES_GROUP_ID).ToList();
                if (invDetails != null && invDetails.Count > 0)
                {

                    TB_RATES_GROUP_DETAIL invDetail = invDetails.Where(x => x.SERVICE_ID == 1).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost01.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 2).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost02.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 3).FirstOrDefault();
                    if (invDetail != null)
                    {
                        lbElecUnit.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 4).FirstOrDefault();
                    if (invDetail != null)
                    {
                        lbWaterUnit.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 5).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost05.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 6).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost06.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 7).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost07.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 8).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost08.Text = invDetail.AMOUNT.ToString();
                    }
                    invDetail = invDetails.Where(x => x.SERVICE_ID == 9).FirstOrDefault();
                    if (invDetail != null)
                    {
                        txtCost09.Text = invDetail.AMOUNT.ToString();
                    }
                }
                #endregion

                #region "SET WATER & ELECTRIC"
                decimal WaterMeterEnd = CustomUtils.isNumber(txtWaterMeterEnd.Text) ? Convert.ToDecimal(txtWaterMeterEnd.Text) : Convert.ToDecimal(0);
                decimal WaterMeterStart = CustomUtils.isNumber(txtWaterMeterStart.Text) ? Convert.ToDecimal(txtWaterMeterStart.Text) : Convert.ToDecimal(0);

                decimal ElecMeterEnd = CustomUtils.isNumber(txtElecMeterEnd.Text) ? Convert.ToDecimal(txtElecMeterEnd.Text) : Convert.ToDecimal(0);
                decimal ElecMeterStart = CustomUtils.isNumber(txtElecMeterStart.Text) ? Convert.ToDecimal(txtElecMeterStart.Text) : Convert.ToDecimal(0);

                txtCost04.Text = ((WaterMeterEnd - WaterMeterStart) * Convert.ToDecimal(lbWaterUnit.Text)).ToString();
                txtCost03.Text = ((ElecMeterEnd - ElecMeterStart) * Convert.ToDecimal(lbElecUnit.Text)).ToString();

                decimal cus01 = CustomUtils.isNumber(txtCost01.Text) ? Convert.ToDecimal(txtCost01.Text) : Convert.ToDecimal(0);
                decimal cus02 = CustomUtils.isNumber(txtCost02.Text) ? Convert.ToDecimal(txtCost02.Text) : Convert.ToDecimal(0);
                decimal cus03 = CustomUtils.isNumber(txtCost03.Text) ? Convert.ToDecimal(txtCost03.Text) : Convert.ToDecimal(0);
                decimal cus04 = CustomUtils.isNumber(txtCost04.Text) ? Convert.ToDecimal(txtCost04.Text) : Convert.ToDecimal(0);
                decimal cus05 = CustomUtils.isNumber(txtCost05.Text) ? Convert.ToDecimal(txtCost05.Text) : Convert.ToDecimal(0);
                decimal cus06 = CustomUtils.isNumber(txtCost06.Text) ? Convert.ToDecimal(txtCost06.Text) : Convert.ToDecimal(0);
                decimal cus07 = CustomUtils.isNumber(txtCost07.Text) ? Convert.ToDecimal(txtCost07.Text) : Convert.ToDecimal(0);
                decimal cus08 = CustomUtils.isNumber(txtCost08.Text) ? Convert.ToDecimal(txtCost08.Text) : Convert.ToDecimal(0);
                decimal cus09 = CustomUtils.isNumber(txtCost09.Text) ? Convert.ToDecimal(txtCost09.Text) : Convert.ToDecimal(0);

                switch (InvoiceType)
                {
                    case InvoiceTypeEnum.CUSTOMER:
                        #region "CRETE INVOICE BY CUSTOMER"
                        txtCost01.ReadOnly = true;
                        txtCost02.ReadOnly = true;
                        txtCost03.ReadOnly = true;
                        txtCost04.ReadOnly = true;
                        txtCost05.ReadOnly = true;
                        txtCost06.ReadOnly = true;
                        txtCost07.ReadOnly = true;
                        txtCost08.ReadOnly = true;
                        txtCost09.ReadOnly = true;

                        th_cus_1.Visible = true;
                        th_cus_2.Visible = true;
                        th_cus_1_rate.Visible = true;
                        th_cus_2_rate.Visible = true;

                        td_cus_1_1.Visible = true;
                        td_cus_1_2.Visible = true;
                        td_cus_1_3.Visible = true;
                        td_cus_1_4.Visible = true;
                        td_cus_1_5.Visible = true;
                        td_cus_1_6.Visible = true;
                        td_cus_1_7.Visible = true;
                        td_cus_1_8.Visible = true;
                        td_cus_1_9.Visible = true;

                        td_cus_2_1.Visible = true;
                        td_cus_2_2.Visible = true;
                        td_cus_2_3.Visible = true;
                        td_cus_2_4.Visible = true;
                        td_cus_2_5.Visible = true;
                        td_cus_2_6.Visible = true;
                        td_cus_2_7.Visible = true;
                        td_cus_2_8.Visible = true;
                        td_cus_2_9.Visible = true;

                        td_cus_1_1_pay.Visible = true;
                        td_cus_1_2_pay.Visible = true;
                        td_cus_1_3_pay.Visible = true;
                        td_cus_1_4_pay.Visible = true;
                        td_cus_1_5_pay.Visible = true;
                        td_cus_1_6_pay.Visible = true;
                        td_cus_1_7_pay.Visible = true;
                        td_cus_1_8_pay.Visible = true;
                        td_cus_1_9_pay.Visible = true;

                        td_cus_2_1_pay.Visible = true;
                        td_cus_2_2_pay.Visible = true;
                        td_cus_2_3_pay.Visible = true;
                        td_cus_2_4_pay.Visible = true;
                        td_cus_2_5_pay.Visible = true;
                        td_cus_2_6_pay.Visible = true;
                        td_cus_2_7_pay.Visible = true;
                        td_cus_2_8_pay.Visible = true;
                        td_cus_2_9_pay.Visible = true;




                        List<TB_M_FUND> listFound = repFund.Table.ToList();

                        int _PKID = 0;
                        int numOfCustomerInRoom = listCus.Count;
                        TB_CUSTOMER_FUND cf = new TB_CUSTOMER_FUND();
                        switch (numOfCustomerInRoom)
                        {
                            case 1:

                                txtCost01_1.Text = (cus01 / numOfCustomerInRoom).ToString();
                                txtCost02_1.Text = (cus02 / numOfCustomerInRoom).ToString();
                                txtCost03_1.Text = (cus03 / numOfCustomerInRoom).ToString();
                                txtCost04_1.Text = (cus04 / numOfCustomerInRoom).ToString();
                                txtCost05_1.Text = (cus05 / numOfCustomerInRoom).ToString();
                                txtCost06_1.Text = (cus06 / numOfCustomerInRoom).ToString();
                                txtCost07_1.Text = (cus07 / numOfCustomerInRoom).ToString();
                                txtCost08_1.Text = (cus08 / numOfCustomerInRoom).ToString();
                                txtCost09_1.Text = (cus09 / numOfCustomerInRoom).ToString();

                                #region "FUND CUS01"
                                _PKID = Convert.ToInt32(listCus[0].ID);
                                ddlPay01_1.DataSource = listFound;
                                ddlPay01_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 1 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay01_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost01_1.Value = txtCost01_1.Text;
                                    txtCost01_1.Text = "0";
                                }
                                ddlPay02_1.DataSource = listFound;
                                ddlPay02_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 2 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay02_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost02_1.Value = txtCost02_1.Text;
                                    txtCost02_1.Text = "0";
                                }
                                ddlPay03_1.DataSource = listFound;
                                ddlPay03_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 3 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay03_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost03_1.Value = txtCost03_1.Text;
                                    txtCost03_1.Text = "0";
                                }
                                ddlPay04_1.DataSource = listFound;
                                ddlPay04_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 4 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay04_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost04_1.Value = txtCost04_1.Text;
                                    txtCost04_1.Text = "0";

                                }
                                ddlPay05_1.DataSource = listFound;
                                ddlPay05_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 5 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay05_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost05_1.Value = txtCost05_1.Text;
                                    txtCost05_1.Text = "0";

                                }
                                ddlPay06_1.DataSource = listFound;
                                ddlPay06_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 6 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay06_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost06_1.Value = txtCost06_1.Text;
                                    txtCost06_1.Text = "0";

                                }
                                ddlPay07_1.DataSource = listFound;
                                ddlPay07_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 7 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay07_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost07_1.Value = txtCost07_1.Text;
                                    txtCost07_1.Text = "0";

                                }
                                ddlPay08_1.DataSource = listFound;
                                ddlPay08_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 8 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay08_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost08_1.Value = txtCost08_1.Text;
                                    txtCost08_1.Text = "0";

                                }
                                ddlPay09_1.DataSource = listFound;
                                ddlPay09_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 9 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay09_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost09_1.Value = txtCost09_1.Text;
                                    txtCost09_1.Text = "0";

                                }
                                #endregion
                                td_amout_1.Visible = true;
                                td_amout_2.Visible = true;
                                td_amout_3.Visible = false;
                                td_amout_bank_1.Visible = true;
                                td_amout_bank_2.Visible = false;


                                txtAmout2.Text = (
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost01_1.Text) ? "0" : txtCost01_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost02_1.Text) ? "0" : txtCost02_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost03_1.Text) ? "0" : txtCost03_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost04_1.Text) ? "0" : txtCost04_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost05_1.Text) ? "0" : txtCost05_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost06_1.Text) ? "0" : txtCost06_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost07_1.Text) ? "0" : txtCost07_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost08_1.Text) ? "0" : txtCost08_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost09_1.Text) ? "0" : txtCost09_1.Text)).ToString();
                                lbCus01.Text = String.Format("1. {0}  {1}", listCus[0].FIRSTNAME, listCus[0].SURNAME);
                                break;
                            case 2:
                                txtCost01_1.Text = (cus01 / numOfCustomerInRoom).ToString();
                                txtCost02_1.Text = (cus02 / numOfCustomerInRoom).ToString();
                                txtCost03_1.Text = (cus03 / numOfCustomerInRoom).ToString();
                                txtCost04_1.Text = (cus04 / numOfCustomerInRoom).ToString();
                                txtCost05_1.Text = (cus05 / numOfCustomerInRoom).ToString();
                                txtCost06_1.Text = (cus06 / numOfCustomerInRoom).ToString();
                                txtCost07_1.Text = (cus07 / numOfCustomerInRoom).ToString();
                                txtCost08_1.Text = (cus08 / numOfCustomerInRoom).ToString();
                                txtCost09_1.Text = (cus09 / numOfCustomerInRoom).ToString();

                                txtCost01_2.Text = (cus01 / numOfCustomerInRoom).ToString();
                                txtCost02_2.Text = (cus02 / numOfCustomerInRoom).ToString();
                                txtCost03_2.Text = (cus03 / numOfCustomerInRoom).ToString();
                                txtCost04_2.Text = (cus04 / numOfCustomerInRoom).ToString();
                                txtCost05_2.Text = (cus05 / numOfCustomerInRoom).ToString();
                                txtCost06_2.Text = (cus06 / numOfCustomerInRoom).ToString();
                                txtCost07_2.Text = (cus07 / numOfCustomerInRoom).ToString();
                                txtCost08_2.Text = (cus08 / numOfCustomerInRoom).ToString();
                                txtCost09_2.Text = (cus09 / numOfCustomerInRoom).ToString();

                                #region "FUND CUS01"
                                _PKID = Convert.ToInt32(listCus[0].ID);
                                ddlPay01_1.DataSource = listFound;
                                ddlPay01_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 1 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay01_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost01_1.Value = txtCost01_1.Text;
                                    txtCost01_1.Text = "0";
                                }
                                ddlPay02_1.DataSource = listFound;
                                ddlPay02_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 2 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay02_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost02_1.Value = txtCost02_1.Text;
                                    txtCost02_1.Text = "0";
                                }
                                ddlPay03_1.DataSource = listFound;
                                ddlPay03_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 3 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay03_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost03_1.Value = txtCost03_1.Text;
                                    txtCost03_1.Text = "0";
                                }
                                ddlPay04_1.DataSource = listFound;
                                ddlPay04_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 4 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay04_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost04_1.Value = txtCost04_1.Text;
                                    txtCost04_1.Text = "0";

                                }
                                ddlPay05_1.DataSource = listFound;
                                ddlPay05_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 5 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay05_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost05_1.Value = txtCost05_1.Text;
                                    txtCost05_1.Text = "0";

                                }
                                ddlPay06_1.DataSource = listFound;
                                ddlPay06_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 6 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay06_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost06_1.Value = txtCost06_1.Text;
                                    txtCost06_1.Text = "0";

                                }
                                ddlPay07_1.DataSource = listFound;
                                ddlPay07_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 7 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay07_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost07_1.Value = txtCost07_1.Text;
                                    txtCost07_1.Text = "0";

                                }
                                ddlPay08_1.DataSource = listFound;
                                ddlPay08_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 8 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay08_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost08_1.Value = txtCost08_1.Text;
                                    txtCost08_1.Text = "0";

                                }
                                ddlPay09_1.DataSource = listFound;
                                ddlPay09_1.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 9 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay09_1.SelectedValue = cf.FUND_ID.ToString();
                                    hCost09_1.Value = txtCost09_1.Text;
                                    txtCost09_1.Text = "0";

                                }
                                #endregion
                                #region "FUND CUS02"
                                _PKID = Convert.ToInt32(listCus[1].ID);
                                ddlPay01_2.DataSource = listFound;
                                ddlPay01_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 1 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay01_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost01_2.Value = txtCost01_2.Text;
                                    txtCost01_2.Text = "0";
                                }
                                ddlPay02_2.DataSource = listFound;
                                ddlPay02_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 2 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay02_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost02_2.Value = txtCost02_2.Text;
                                    txtCost02_2.Text = "0";
                                }
                                ddlPay03_2.DataSource = listFound;
                                ddlPay03_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 3 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay03_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost03_2.Value = txtCost03_2.Text;
                                    txtCost03_2.Text = "0";
                                }
                                ddlPay04_2.DataSource = listFound;
                                ddlPay04_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 4 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay04_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost04_2.Value = txtCost04_2.Text;
                                    txtCost04_2.Text = "0";
                                }
                                ddlPay05_2.DataSource = listFound;
                                ddlPay05_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 5 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay05_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost05_2.Value = txtCost05_2.Text;
                                    txtCost05_2.Text = "0";
                                }
                                ddlPay06_2.DataSource = listFound;
                                ddlPay06_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 6 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay06_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost06_2.Value = txtCost06_2.Text;
                                    txtCost06_2.Text = "0";
                                }
                                ddlPay07_2.DataSource = listFound;
                                ddlPay07_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 7 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay07_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost07_2.Value = txtCost07_2.Text;
                                    txtCost07_2.Text = "0";
                                }
                                ddlPay08_2.DataSource = listFound;
                                ddlPay08_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 8 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay08_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost08_2.Value = txtCost08_2.Text;
                                    txtCost08_2.Text = "0";
                                }
                                ddlPay09_2.DataSource = listFound;
                                ddlPay09_2.DataBind();
                                cf = repCustomerFund.Table.Where(x => x.FOR_SERVICE_ID == 9 && x.CUS_ID == _PKID).FirstOrDefault();
                                if (cf != null)
                                {
                                    ddlPay09_2.SelectedValue = cf.FUND_ID.ToString();
                                    hCost09_2.Value = txtCost09_2.Text;
                                    txtCost09_2.Text = "0";
                                }
                                #endregion
                                td_amout_1.Visible = true;
                                td_amout_2.Visible = true;
                                td_amout_3.Visible = true;
                                td_amout_bank_1.Visible = true;
                                td_amout_bank_2.Visible = true;
                                txtAmout2.Text = (
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost01_1.Text) ? "0" : txtCost01_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost02_1.Text) ? "0" : txtCost02_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost03_1.Text) ? "0" : txtCost03_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost04_1.Text) ? "0" : txtCost04_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost05_1.Text) ? "0" : txtCost05_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost06_1.Text) ? "0" : txtCost06_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost07_1.Text) ? "0" : txtCost07_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost08_1.Text) ? "0" : txtCost08_1.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost09_1.Text) ? "0" : txtCost09_1.Text)).ToString();

                                txtAmout3.Text = (
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost01_2.Text) ? "0" : txtCost01_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost02_2.Text) ? "0" : txtCost02_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost03_2.Text) ? "0" : txtCost03_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost04_2.Text) ? "0" : txtCost04_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost05_2.Text) ? "0" : txtCost05_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost06_2.Text) ? "0" : txtCost06_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost07_2.Text) ? "0" : txtCost07_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost08_2.Text) ? "0" : txtCost08_2.Text) +
                                      Convert.ToDouble(String.IsNullOrEmpty(txtCost09_2.Text) ? "0" : txtCost09_2.Text)).ToString();

                                lbCus01.Text = String.Format("1. {0}  {1}", listCus[0].FIRSTNAME, listCus[0].SURNAME);
                                lbCus02.Text = String.Format("2. {0}  {1}", listCus[1].FIRSTNAME, listCus[1].SURNAME);
                                break;
                        }
                        #endregion
                        break;
                    case InvoiceTypeEnum.ROOM:
                        #region "CRETE INVOICE BY ROOM"
                        txtCost01.ReadOnly = false;
                        txtCost02.ReadOnly = false;
                        txtCost03.ReadOnly = false;
                        txtCost04.ReadOnly = false;
                        txtCost05.ReadOnly = false;
                        txtCost06.ReadOnly = false;
                        txtCost07.ReadOnly = false;
                        txtCost08.ReadOnly = false;
                        txtCost09.ReadOnly = false;

                        th_cus_1.Visible = false;
                        th_cus_2.Visible = false;
                        th_cus_1_rate.Visible = false;
                        th_cus_2_rate.Visible = false;

                        td_cus_1_1.Visible = false;
                        td_cus_1_2.Visible = false;
                        td_cus_1_3.Visible = false;
                        td_cus_1_4.Visible = false;
                        td_cus_1_5.Visible = false;
                        td_cus_1_6.Visible = false;
                        td_cus_1_7.Visible = false;
                        td_cus_1_8.Visible = false;
                        td_cus_1_9.Visible = false;

                        td_cus_2_1.Visible = false;
                        td_cus_2_2.Visible = false;
                        td_cus_2_3.Visible = false;
                        td_cus_2_4.Visible = false;
                        td_cus_2_5.Visible = false;
                        td_cus_2_6.Visible = false;
                        td_cus_2_7.Visible = false;
                        td_cus_2_8.Visible = false;
                        td_cus_2_9.Visible = false;


                        td_cus_1_1_pay.Visible = false;
                        td_cus_1_2_pay.Visible = false;
                        td_cus_1_3_pay.Visible = false;
                        td_cus_1_4_pay.Visible = false;
                        td_cus_1_5_pay.Visible = false;
                        td_cus_1_6_pay.Visible = false;
                        td_cus_1_7_pay.Visible = false;
                        td_cus_1_8_pay.Visible = false;
                        td_cus_1_9_pay.Visible = false;

                        td_cus_2_1_pay.Visible = false;
                        td_cus_2_2_pay.Visible = false;
                        td_cus_2_3_pay.Visible = false;
                        td_cus_2_4_pay.Visible = false;
                        td_cus_2_5_pay.Visible = false;
                        td_cus_2_6_pay.Visible = false;
                        td_cus_2_7_pay.Visible = false;
                        td_cus_2_8_pay.Visible = false;
                        td_cus_2_9_pay.Visible = false;
                        td_amout_1.Visible = true;
                        td_amout_2.Visible = false;
                        td_amout_3.Visible = false;
                        td_amout_bank_1.Visible = false;
                        td_amout_bank_2.Visible = false;
                        //SUMMARY
                        txtAmout1.Text = (
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost01.Text) ? "0" : txtCost01.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost02.Text) ? "0" : txtCost02.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost03.Text) ? "0" : txtCost03.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost04.Text) ? "0" : txtCost04.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost05.Text) ? "0" : txtCost05.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost06.Text) ? "0" : txtCost06.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost07.Text) ? "0" : txtCost07.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost08.Text) ? "0" : txtCost08.Text) +
                              Convert.ToDouble(String.IsNullOrEmpty(txtCost09.Text) ? "0" : txtCost09.Text)).ToString();


                        #endregion
                        break;
                }


                #endregion



            }


            //BUTTON STATUS
            btnCalculate.CssClass = Constants.CSS_BUTTON_CALCULATE;
            btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
            btnPrintInvoice.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
        }

    }
}