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
    public partial class Room : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_M_ROOM_TYPE> repRoomType;
        private Repository<TB_RATES_GROUP> repRateGroup;

        public Room()
        {
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repRoomType = unitOfWork.Repository<TB_M_ROOM_TYPE>();
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
            get { return (int)ViewState["PKID"]; }
            set { ViewState["PKID"] = value; }
        }

        public TB_ROOM obj
        {
            get
            {
                
                TB_ROOM _tmp = new TB_ROOM();
                _tmp.BUILD_ID = Convert.ToInt32(ddlBuildId.SelectedValue);
                _tmp.FLOOR = Convert.ToInt32(txtFloor.Text);
                _tmp.NUMBER = txtRoomNumber.Text;
                _tmp.ROOM_TYPE_ID = Convert.ToInt32(ddlRoomType.SelectedValue);
                _tmp.RATES_GROUP_ID = Convert.ToInt32(ddlRateGroupId.SelectedValue);
                _tmp.CUSTOMER_LIMIT = Convert.ToInt32(txtCustomerLimit.Text);
                _tmp.STATUS = Convert.ToInt32(RoomStatusEmum.Available);
                //_tmp.SPLIT_INV_BY_PERSON = cbSplitInvByPerson.Checked;
                return _tmp;
            }
        }

        private void initialPage()
        {
            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

            SearchRoom prvPage = Page.PreviousPage as SearchRoom;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_ROOM;

            ddlBuildId.DataSource = repBuild.Table.ToList();
            ddlBuildId.DataBind();
            ddlRoomType.DataSource = repRoomType.Table.ToList();
            ddlRoomType.DataBind();
            ddlRateGroupId.DataSource = repRateGroup.Table.ToList();
            ddlRateGroupId.DataBind();

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
                    //ddlBuildId.ReadOnly = true;
                    txtFloor.ReadOnly = true;
                    txtRoomNumber.ReadOnly = true;
                    //txtRoomTypeId.ReadOnly = true;
                    //txtRateGroupId.ReadOnly = true;
                    break;
            }
        }

        private void fillInData()
        {
            TB_ROOM _tmp = repRoom.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (_tmp != null)
            {
                ddlBuildId.SelectedValue = _tmp.BUILD_ID.ToString();
                txtFloor.Text = _tmp.FLOOR.ToString();
                txtRoomNumber.Text = _tmp.NUMBER.ToString();
                ddlRoomType.SelectedValue = _tmp.ROOM_TYPE_ID.ToString();
                ddlRateGroupId.SelectedValue = _tmp.RATES_GROUP_ID.ToString();
                txtCustomerLimit.Text = _tmp.CUSTOMER_LIMIT.ToString();
                //cbSplitInvByPerson.Checked = (_tmp.SPLIT_INV_BY_PERSON == null) ? false : (Boolean)_tmp.SPLIT_INV_BY_PERSON;

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
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    repRoom.Insert(obj);
                    break;
                case CommandNameEnum.Edit:
                    TB_ROOM editModel = repRoom.GetById(this.PKID);
                    editModel.BUILD_ID = Convert.ToInt32(ddlBuildId.SelectedValue);
                    editModel.FLOOR = Convert.ToInt32(txtFloor.Text);
                    editModel.NUMBER = txtRoomNumber.Text;
                    editModel.ROOM_TYPE_ID = Convert.ToInt32(ddlRoomType.SelectedValue);
                    editModel.RATES_GROUP_ID = Convert.ToInt32(ddlRateGroupId.SelectedValue);
                    editModel.CUSTOMER_LIMIT = Convert.ToInt32(txtCustomerLimit.Text);
                    //editModel.SPLIT_INV_BY_PERSON = cbSplitInvByPerson.Checked;
                    repRoom.Update(obj);
                    break;
            }

            #region "MESSAGE RESULT"
            String errorMessage = repRoom.errorMessage;
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

    }
}