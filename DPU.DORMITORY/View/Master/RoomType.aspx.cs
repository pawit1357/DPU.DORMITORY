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
    public partial class RoomType : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_ROOM_TYPE> rep;

        public RoomType()
        {
            rep = unitOfWork.Repository<TB_M_ROOM_TYPE>();
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

        //public TB_M_SERVICE obj
        //{
        //    get { return (TB_M_SERVICE)Session[GetType().Name + "obj"]; }
        //    set { Session[GetType().Name + "obj"] = value; }
        //}


        public TB_M_ROOM_TYPE obj
        {
            get
            {
                TB_M_ROOM_TYPE _tmp = new TB_M_ROOM_TYPE();
                //_tmp.ID = Convert.ToInt16(txtID.Text);
                _tmp.NAME = txtName.Text;
                return _tmp;
            }
        }

        private void initialPage()
        {
            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

            SearchRoomType prvPage = Page.PreviousPage as SearchRoomType;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_ROOM_TYPE;
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
                    //txtID.ReadOnly = true;
                    break;
                case CommandNameEnum.View:
                    fillInData();
                    btnSave.CssClass = Constants.CSS_DISABLED_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    //Set ReadOnly
                    //txtID.ReadOnly = true;
                    txtName.ReadOnly = true;
                    break;
            }
        }

        private void fillInData()
        {
            TB_M_ROOM_TYPE _tmp = rep.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (_tmp != null)
            {
                //txtID.Text = _tmp.ID.ToString();
                txtName.Text = _tmp.NAME;
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
            //this.PKID = Convert.ToInt32(txtID.Text);
            String errorMessage = String.Empty;
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    //Boolean isExistPK = rep.Table.Where(x => x.ID == PKID).Any();
                    //if (!isExistPK)
                    //{
                        rep.Insert(obj);
                    //}
                    //else
                    //{
                    //    errorMessage = String.Format(Resources.MSG_DONT_INSERT_EXIST, txtID.Text);
                    //}
                    break;
                case CommandNameEnum.Edit:
                    TB_M_ROOM_TYPE editModel = rep.GetById(this.PKID);
                    editModel.NAME = txtName.Text;
                    rep.Update(editModel);
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