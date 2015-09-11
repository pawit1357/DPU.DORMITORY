using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class Rates : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_RATES_GROUP> repRateGroup;
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_M_SERVICE> repService;

        public Rates()
        {
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repService = unitOfWork.Repository<TB_M_SERVICE>();
            repRateGroup = unitOfWork.Repository<TB_RATES_GROUP>();
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

        public int PKID
        {
            get { return (int)ViewState["Rates_PKID"]; }
            set { ViewState["Rates_PKID"] = value; }
        }

        public List<TB_RATES_GROUP_DETAIL> listRateGroupDetal
        {
            get { return (List<TB_RATES_GROUP_DETAIL>)ViewState["Rates_listRateGroupDetal"]; }
            set { ViewState["Rates_listRateGroupDetal"] = value; }
        }

        //public List<TB_RATES_GROUP_DETAIL> listRateGroupDetalShow
        //{
        //    get { return listRateGroupDetal.FindAll(x => x.RowState != CommandNameEnum.Delete); }
        //}

        public TB_RATES_GROUP obj
        {
            get
            {
                TB_RATES_GROUP _obj = new TB_RATES_GROUP();
                _obj.BUILD_ID = Convert.ToInt32(ddlBuildId.SelectedValue);
                _obj.NAME = txtName.Text;
                _obj.DESCRIPTION = txtDescription.Text;
                _obj.INSURANCE_AMOUNT = Convert.ToDecimal(txtAmout.Text);
                _obj.START_DATE = CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
                _obj.END_DATE = CustomUtils.converFromDDMMYYYY(txtEndDate.Text);
                _obj.UPDATE_BY = userLogin.USER_ID;
                _obj.CREATE_DATE = DateTime.Now;
                _obj.UPDATE_DATE = DateTime.Now;
                _obj.TB_RATES_GROUP_DETAIL = listRateGroupDetal;
                _obj.RowState = CommandName;
                return _obj;
            }
        }

        private void initialPage()
        {
            SearchRates prvPage = Page.PreviousPage as SearchRates;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_RATE;

            ddlBuildId.DataSource = repBuild.Table.ToList();
            ddlBuildId.DataBind();



            //lbCommandName.Text = CommandName.ToString();
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;

                    listRateGroupDetal = new List<TB_RATES_GROUP_DETAIL>();
                    List<TB_M_SERVICE> services = repService.Table.ToList();
                    foreach (TB_M_SERVICE _service in services)
                    {
                        TB_RATES_GROUP_DETAIL detail = new TB_RATES_GROUP_DETAIL();
                        detail.ID = _service.ID;
                        //detail.RATES_GROUP_ID = this.PKID;
                        detail.SERVICE_ID = _service.ID;
                        detail.SERVICE_NAME = _service.NAME;
                        //detail.SUBTRAN = 0;
                        //detail.GL = 0;
                        //detail.AMOUNT = 0;
                        //detail.VAT = 0;
                        listRateGroupDetal.Add(detail);
                    }
                    gvResult.DataSource = listRateGroupDetal;
                    gvResult.DataBind();
                    break;
                case CommandNameEnum.Edit:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    fillInData();
                    //Set ReadOnly

                    break;
                case CommandNameEnum.View:
                    btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    fillInData();
                    //Set ReadOnly
                    break;
            }
        }

        private void fillInData()
        {
            TB_RATES_GROUP _obj = repRateGroup.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (_obj != null)
            {
                ddlBuildId.SelectedValue = _obj.BUILD_ID.Value.ToString();
                txtName.Text = _obj.NAME;
                txtDescription.Text = _obj.DESCRIPTION;
                txtAmout.Text = _obj.INSURANCE_AMOUNT.Value.ToString();
                txtStartDate.Text = _obj.START_DATE.Value.ToString("dd/MM/yyyy");
                txtEndDate.Text = _obj.END_DATE.Value.ToString("dd/MM/yyyy");
                List<TB_M_SERVICE> serviceList = repService.Table.ToList();

                listRateGroupDetal = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == _obj.ID).ToList();
                if (listRateGroupDetal != null)
                {
                    foreach (TB_RATES_GROUP_DETAIL val in listRateGroupDetal)
                    {
                        TB_M_SERVICE _service = serviceList.Where(x => x.ID == val.SERVICE_ID).FirstOrDefault();
                        if (_service != null)
                        {
                            val.SERVICE_NAME = _service.NAME;
                        }
                    }
                    gvResult.DataSource = listRateGroupDetal;
                    gvResult.DataBind();
                    gvResult.UseAccessibleHeader = true;
                    gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void removeSession()
        {
            //Session.Remove(GetType().Name + "listRateGroupDetal");
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    repRateGroup.Insert(obj);
                    break;
                case CommandNameEnum.Edit:
                    TB_RATES_GROUP editModel = repRateGroup.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
                    if (editModel != null)
                    {
                        editModel.BUILD_ID = obj.BUILD_ID;
                        editModel.NAME = obj.NAME;
                        editModel.DESCRIPTION = obj.DESCRIPTION;
                        editModel.INSURANCE_AMOUNT = obj.INSURANCE_AMOUNT;
                        editModel.START_DATE = obj.START_DATE;
                        editModel.END_DATE = obj.END_DATE;
                        editModel.UPDATE_BY = obj.UPDATE_BY;
                        editModel.UPDATE_DATE = DateTime.Now;
                        repRateGroup.Update(editModel);
                    }

                    foreach (TB_RATES_GROUP_DETAIL _detail in listRateGroupDetal)
                    {
                        TB_RATES_GROUP_DETAIL editGroupDetail = repRateGroupDetail.Table.Where(x => x.ID == _detail.ID).FirstOrDefault();
                        if (editGroupDetail != null)
                        {
                            editGroupDetail.MAINTRAN = _detail.MAINTRAN;
                            editGroupDetail.SUBTRAN = _detail.SUBTRAN;
                            editGroupDetail.GL = _detail.GL;
                            editGroupDetail.UNIT = _detail.UNIT;
                            editGroupDetail.AMOUNT = _detail.AMOUNT;
                            editGroupDetail.VAT = _detail.VAT;
                            repRateGroupDetail.Update(editGroupDetail);
                        }
                    }
                    break;
            }

            #region "MESSAGE RESULT"
            String errorMessage = repRateGroup.errorMessage;
            if (!String.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(this, errorMessage);
            }
            else
            {
                MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }
            #endregion
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }


        #region "GV RESULT"
        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Literal _litServiceID = (Literal)e.Row.FindControl("litServiceID");
            //    if (_litServiceID != null)
            //    {
            //        if (!String.IsNullOrEmpty(_litServiceID.Text))
            //        {
            //            TB_M_SERVICE mService = repService.GetById(Convert.ToInt32(_litServiceID.Text));
            //            if (mService != null)
            //            {
            //                _litServiceID.Text = mService.NAME;
            //            }
            //        }

            //    }
            //}
        }

        protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvResult.EditIndex = -1;
            //gvResult.DataSource = this.listRateGroupDetalShow;
            //gvResult.DataBind();
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int id = int.Parse(gvResult.DataKeys[e.RowIndex].Values[0].ToString());

            //TB_RATES_GROUP_DETAIL tmp = this.listRateGroupDetal.Where(x => x.RID == id).FirstOrDefault();
            //if (tmp != null)
            //{
            //    tmp.RowState = CommandNameEnum.Delete;
            //    gvResult.DataSource = listRateGroupDetalShow;
            //    gvResult.DataBind();
            //}
        }

        protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResult.EditIndex = e.NewEditIndex;

            gvResult.DataSource = this.listRateGroupDetal;
            gvResult.DataBind();
        }

        protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvResult.DataKeys[e.RowIndex].Values[0].ToString());
            //DropDownList ddlServiceID = (DropDownList)gvResult.Rows[e.RowIndex].FindControl("ddlServiceID");

            //bool isServiceExist = listRateGroupDetalShow.Where(x => x.SERVICE_ID == Convert.ToInt32(ddlServiceID.SelectedValue)).Any();
            //if (!isServiceExist)
            //{
                TextBox txtSubtran = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtSubtran");
                TextBox txtMainTran = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtMainTran");
                TextBox txtGL = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtGL");
                TextBox txtAMOUNT = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtAMOUNT");
                TextBox txtVAT = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtVAT");
                TextBox txtUnit = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtUnit");

                TB_RATES_GROUP_DETAIL tmp = this.listRateGroupDetal.Where(x => x.ID == _id).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MAINTRAN = !CustomUtils.isNumber(txtMainTran.Text) ? 0 : Convert.ToInt32(txtMainTran.Text);
                    tmp.SUBTRAN = !CustomUtils.isNumber(txtSubtran.Text) ? 0 : Convert.ToInt32(txtSubtran.Text);
                    tmp.GL = !CustomUtils.isNumber(txtGL.Text) ? 0 : Convert.ToInt32(txtGL.Text);
                    tmp.AMOUNT = !CustomUtils.isNumber(txtAMOUNT.Text) ? 0 : Convert.ToDecimal(txtAMOUNT.Text);
                    tmp.VAT = !CustomUtils.isNumber(txtVAT.Text) ? 0 : Convert.ToInt32(txtVAT.Text);
                    tmp.UNIT = !CustomUtils.isNumber(txtUnit.Text) ? 0 : Convert.ToInt32(txtUnit.Text);
                    tmp.RowState = CommandNameEnum.Edit;
                }

                gvResult.EditIndex = -1;
                gvResult.DataSource = this.listRateGroupDetal;
                gvResult.DataBind();
            //}
            //else
            //{
            //    MessageBox.Show(this.Page, String.Format(Resources.MSG_DONT_INSERT_EXIST, ddlServiceID.SelectedItem.Text));
            //}
        }

        protected void gvResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //TB_RATES_GROUP_DETAIL detail = new TB_RATES_GROUP_DETAIL();
            //detail.RID = CustomUtils.GetRandomPKID();
            ////detail.RATES_GROUP_ID = this.PKID;
            ////detail.SERVICE_ID = -1;
            ////detail.SUBTRAN = 0;
            ////detail.GL = 0;
            ////detail.AMOUNT = 0;
            ////detail.VAT = 0;
            //detail.RowState = CommandNameEnum.Add;
            //this.listRateGroupDetal.Add(detail);
            //gvResult.DataSource = listRateGroupDetalShow;
            //gvResult.DataBind();
        }

    }
}