using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class Building : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_BUILD> rep;

        public Building()
        {
            rep = unitOfWork.Repository<TB_M_BUILD>();
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
            get { return (int)ViewState["PKID"]; }
            set { ViewState["PKID"] = value; }
        }

        public TB_M_BUILD obj
        {
            get
            {
                TB_M_BUILD _tmp = new TB_M_BUILD();
                _tmp.ID = Convert.ToInt16(txtID.Text);
                _tmp.NAME = txtName.Text;
                _tmp.DESCRIPTION = txtDescription.Text;
                _tmp.DESCRIPTION_EN = txtDescriptionEn.Text;
                _tmp.COMPANY = txtCompany.Text;
                _tmp.BA = txtBA.Text;
                _tmp.PROFIT_CTR = txtProfitCtr.Text;
                _tmp.TXT_NO = txtTaxNo.Text;
                _tmp.ADDRESS_LINE_1 = txtAddressLine1.Text;
                _tmp.ADDRESS_LINE_2 = txtAddressLine2.Text;
                _tmp.ADDRESS_LINE_1_EN = txtAddressLine1En.Text;
                _tmp.ADDRESS_LINE_2_EN = txtAddressLine2En.Text;
                return _tmp;
            }
        }

        private void initialPage()
        {
            SearchBuilding prvPage = Page.PreviousPage as SearchBuilding;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_BUILDING;

            lbCommandName.Text = CommandName.ToString();
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    break;
                case CommandNameEnum.Edit:
                    fillInData();
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set ReadOnly
                    txtID.ReadOnly = true;
                    break;
                case CommandNameEnum.View:
                    fillInData();
                    btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set ReadOnly
                    txtID.ReadOnly = true;
                    txtName.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtBA.ReadOnly = true;
                    break;
            }
        }

        private void fillInData()
        {
            TB_M_BUILD _tmp = rep.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (_tmp != null)
            {
                txtID.Text = _tmp.ID.ToString();
                txtName.Text = _tmp.NAME;
                txtDescription.Text = _tmp.DESCRIPTION;
                txtDescriptionEn.Text = _tmp.DESCRIPTION_EN;
                txtCompany.Text = _tmp.COMPANY;
                txtBA.Text = _tmp.BA;
                txtProfitCtr.Text = _tmp.PROFIT_CTR;
                txtTaxNo.Text = _tmp.TXT_NO;
                txtAddressLine1.Text = _tmp.ADDRESS_LINE_1;
                txtAddressLine2.Text = _tmp.ADDRESS_LINE_2;
                txtAddressLine1En.Text = _tmp.ADDRESS_LINE_1_EN;
                txtAddressLine2En.Text = _tmp.ADDRESS_LINE_2_EN;

            }
        }

        private void removeSession()
        {
            //Session.Remove(GetType().Name + "obj");
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
            this.PKID = Convert.ToInt32(txtID.Text);
            String errorMessage = String.Empty;
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    Boolean isExistPK = rep.Table.Where(x => x.ID == PKID).Any();
                    if (!isExistPK)
                    {
                        rep.Insert(obj);
                    }
                    else
                    {
                        errorMessage = String.Format(Resources.MSG_DONT_INSERT_EXIST, txtID.Text);
                    }
                    break;
                case CommandNameEnum.Edit:
                    TB_M_BUILD editModel = rep.GetById(this.PKID);
                    editModel.NAME = txtName.Text;
                    editModel.DESCRIPTION = txtDescription.Text;
                    editModel.DESCRIPTION_EN = txtDescriptionEn.Text;
                    editModel.COMPANY = txtCompany.Text;
                    editModel.BA = txtBA.Text;
                    editModel.PROFIT_CTR = txtProfitCtr.Text;
                    editModel.TXT_NO = txtTaxNo.Text;
                    editModel.ADDRESS_LINE_1 = txtAddressLine1.Text;
                    editModel.ADDRESS_LINE_2 = txtAddressLine2.Text;
                    editModel.ADDRESS_LINE_1_EN = txtAddressLine1En.Text;
                    editModel.ADDRESS_LINE_2_EN = txtAddressLine2En.Text;
                    rep.Update(obj);
                    break;
            }

            #region "MESSAGE RESULT"

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
            txtName.Text = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }

    }
}