//using DPU.DORMITORY.Biz;
//using DPU.DORMITORY.Biz.DataAccess;
//using DPU.DORMITORY.Properties;
//using DPU.DORMITORY.Repositories;
//using DPU.DORMITORY.Utils;
//using System;
//using System.Linq;

//namespace DPU.DORMITORY.Web.View.Master
//{
//    public partial class Service : System.Web.UI.Page
//    {

//        #region "Property"
//        private UnitOfWork unitOfWork = new UnitOfWork();
//        private Repository<TB_M_SERVICE> rep;
//        private Repository<TB_M_BUILD> repBuild;
//        private Repository<TB_M_COST_TYPE> repCostType;

//        public Service()
//        {
//            rep = unitOfWork.Repository<TB_M_SERVICE>();
//            repBuild = unitOfWork.Repository<TB_M_BUILD>();
//            repCostType = unitOfWork.Repository<TB_M_COST_TYPE>();
//        }

//        public USER userLogin
//        {
//            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
//        }

//        public CommandNameEnum CommandName
//        {
//            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
//            set { ViewState[Constants.COMMAND_NAME] = value; }
//        }

//        public string PreviousPath
//        {
//            get { return (string)ViewState[Constants.PREVIOUS_PATH]; }
//            set { ViewState[Constants.PREVIOUS_PATH] = value; }
//        }

//        public int PKID
//        {
//            get { return (int)ViewState["PKID"]; }
//            set { ViewState["PKID"] = value; }
//        }

//        //public TB_M_SERVICE obj
//        //{
//        //    get { return (TB_M_SERVICE)Session[GetType().Name + "obj"]; }
//        //    set { Session[GetType().Name + "obj"] = value; }
//        //}


//        public TB_M_SERVICE obj
//        {
//            get
//            {
//                TB_M_SERVICE _tmp = new TB_M_SERVICE();
//                //_tmp.ID = Convert.ToInt16(txtID.Text);
//                _tmp.BUILD_ID = Convert.ToInt16(ddlBuildId.SelectedValue);
//                _tmp.COST_ID = Convert.ToInt16(ddlCostType.SelectedValue);
//                //_tmp.NAME = txtName.Text;
//                //_tmp.NAME_EN = txtNameEn.Text;
//                //_tmp.ACCOUNT_NAME = txtAccountName.Text;
//                _tmp.MAIN_TRANS = String.IsNullOrEmpty(txtMainTrans.Text) ? 0 : Convert.ToInt32(txtMainTrans.Text);
//                _tmp.SUB_TRANS = String.IsNullOrEmpty(txtSubTrans.Text) ? 0 : Convert.ToInt32(txtSubTrans.Text);
//                //_tmp.GL_ACCOUNT = String.IsNullOrEmpty(txtGlAccount.Text) ? 0 : Convert.ToInt32(txtGlAccount.Text);
//                return _tmp;
//            }
//        }

//        private void initialPage()
//        {
//            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

//            SearchService prvPage = Page.PreviousPage as SearchService;
//            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
//            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
//            this.PreviousPath = Constants.LINK_SEARCH_SERVICE;
//            lbCommandName.Text = CommandName.ToString();


//            ddlBuildId.DataSource = repBuild.Table.ToList();
//            ddlBuildId.DataBind();
//            ddlCostType.DataSource = repCostType.Table.ToList();
//            ddlCostType.DataBind();

//            switch (CommandName)
//            {
//                case CommandNameEnum.Add:
//                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
//                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
//                    break;
//                case CommandNameEnum.Edit:
//                    fillInData();
//                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
//                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
//                    //Set ReadOnly
//                    //txtID.ReadOnly = true;
//                    break;
//                case CommandNameEnum.View:
//                    fillInData();
//                    btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
//                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
//                    //Set ReadOnly
//                    //txtID.ReadOnly = true;
//                    //txtName.ReadOnly = true;
//                    break;
//            }
//        }

//        private void fillInData()
//        {
//            TB_M_SERVICE _tmp = rep.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
//            if (_tmp != null)
//            {
//                //txtID.Text = _tmp.ID.ToString();
//                //txtName.Text = _tmp.NAME;
//                //txtNameEn.Text = _tmp.NAME_EN;
//                ddlBuildId.SelectedValue = _tmp.BUILD_ID.ToString();
//                ddlCostType.SelectedValue = _tmp.COST_ID.ToString();
//                txtMainTrans.Text = _tmp.MAIN_TRANS.ToString();
//                txtSubTrans.Text = _tmp.SUB_TRANS.ToString();
//                //txtGlAccount.Text = _tmp.GL_ACCOUNT.ToString();
//                //txtAccountName.Text = _tmp.ACCOUNT_NAME;
//            }
//        }

//        private void removeSession()
//        {
//            //Session.Remove(GetType().Name + "obj");
//        }

//        #endregion


//        protected void Page_Load(object sender, EventArgs e)
//        {


//            if (!Page.IsPostBack)
//            {
//                initialPage();
//            }
//        }

//        protected void btnSave_Click(object sender, EventArgs e)
//        {
//            switch (CommandName)
//            {
//                case CommandNameEnum.Add:
//                    rep.Insert(obj);
//                    break;
//                case CommandNameEnum.Edit:
//                    TB_M_SERVICE editModel = rep.GetById(this.PKID);
//                    editModel.BUILD_ID = Convert.ToInt16(ddlBuildId.SelectedValue);
//                    editModel.COST_ID = Convert.ToInt16(ddlCostType.SelectedValue);

//                    //editModel.NAME = txtName.Text;
//                    //editModel.NAME_EN = txtNameEn.Text;
//                    //editModel.ACCOUNT_NAME = txtAccountName.Text;
//                    editModel.MAIN_TRANS = String.IsNullOrEmpty(txtMainTrans.Text) ? 0 : Convert.ToInt32(txtMainTrans.Text);
//                    editModel.SUB_TRANS = String.IsNullOrEmpty(txtSubTrans.Text) ? 0 : Convert.ToInt32(txtSubTrans.Text);
//                    //editModel.GL_ACCOUNT = String.IsNullOrEmpty(txtGlAccount.Text) ? 0 : Convert.ToInt32(txtGlAccount.Text);
//                    rep.Update(editModel);
//                    break;
//            }

//            #region "MESSAGE RESULT"
//            String errorMessage = rep.errorMessage;
//            if (!String.IsNullOrEmpty(errorMessage))
//            {
//                MessageBox.Show(this, errorMessage);
//            }
//            else
//            {
//                MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
//            }
//            #endregion
//        }

//        protected void btnCancel_Click(object sender, EventArgs e)
//        {

//            removeSession();
//            Response.Redirect(PreviousPath);
//        }

//    }
//}