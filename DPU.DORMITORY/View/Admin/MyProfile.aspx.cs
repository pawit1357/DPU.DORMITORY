using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.View.Admin
{
    public partial class MyProfile : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<USER> repUser;
        private Repository<USERS_ROLE> repUserRole;
        private Repository<TB_M_TITLE> repTitle;
        private Repository<TB_M_BUILD> repBuild;

        public MyProfile()
        {
            repUser = unitOfWork.Repository<USER>();
            repUserRole = unitOfWork.Repository<USERS_ROLE>();
            repTitle = unitOfWork.Repository<TB_M_TITLE>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
        }

        #region "Property"
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

        public String PKID
        {
            get { return (String)Session["PKID"]; }
            set { Session["PKID"] = value; }
        }

        private void removeSession()
        {
            Session.Remove("PKID");
        }

        public USER obj
        {
            get
            {
                USER user = new USER();
                user.USER_ID = txtUser.Text;
                user.ROLE_ID = Convert.ToInt32(ddlRole.SelectedValue);
                user.TITLE_ID = Convert.ToInt32(ddlTitle.SelectedValue);
                user.USER_PASSWORD = CustomUtils.EncodeMD5(txtPassword.Text);

                user.FIRST_NAME = txtFirstName.Text;
                user.LAST_NAME = txtLastName.Text;
                user.EMAIL_ADDRESS = txtEmail.Text;
                user.PHONE_NO = txtPhone.Text;

                user.UPDATE_BY = userLogin.USER_ID;
                user.LAST_SIGN_IN_DATE = DateTime.Now;
                user.CREATE_DATE = DateTime.Now;
                user.IS_ACTIVE = (bool)rdStatusA.Checked;

                StringBuilder sb = new StringBuilder();

                foreach (ListItem item in lstBuild.Items)
                {
                    if (item.Selected)
                    {
                        sb.Append(item.Value);
                        sb.Append(Constants.CHAR_COMMA);
                    }
                }
                if (sb.ToString().Length > 0)
                {
                    user.RESPONSIBLE_BUIDING = sb.ToString().Substring(0, sb.ToString().Length - 1);
                }

                user.IS_FORCE_CHANGE_PASSWORD = true;
                return user;
            }
        }

        private void initialPage()
        {

            this.CommandName = CommandNameEnum.Edit;
            lbCommandName.Text = CommandName.ToString();

            ddlRole.DataSource = repUserRole.Table.ToList();
            ddlRole.DataBind();

            ddlTitle.DataSource = repTitle.Table.ToList();
            ddlTitle.DataBind();

            lstBuild.DataSource = repBuild.Table.ToList();
            lstBuild.DataBind();

            switch (CommandName)
            {
                case CommandNameEnum.Edit:
                    fillinScreen();
                    txtUser.ReadOnly = true;
                    pBuildingOwner.Visible = false;
                    btnSave.CssClass = Constants.CSS_BUTTON_SAVE;
                    btnCancel.CssClass = Constants.CSS_BUTTON_CANCEL;
                    break;
            }
        }

        private void fillinScreen()
        {

            USER user = repUser.Table.Where(x => x.USER_ID.Equals(userLogin.USER_ID)).FirstOrDefault();
            if (user != null)
            {
                txtUser.Text = user.USER_ID;
                ddlRole.SelectedValue = user.ROLE_ID.ToString();
                ddlTitle.SelectedValue = user.TITLE_ID.ToString();
                txtPassword.Text = user.USER_PASSWORD;
                txtFirstName.Text = user.FIRST_NAME;
                txtLastName.Text = user.LAST_NAME;
                txtEmail.Text = user.EMAIL_ADDRESS;
                txtPhone.Text = user.PHONE_NO;
                rdStatusA.Checked = ((bool)user.IS_ACTIVE) ? true : false;

                if (!String.IsNullOrEmpty(user.RESPONSIBLE_BUIDING) && user.RESPONSIBLE_BUIDING.Length > 0)
                {
                    String[] repo = user.RESPONSIBLE_BUIDING.Split(Constants.CHAR_COMMA);
                    lstBuild.SelectionMode = ListSelectionMode.Multiple;
                    foreach (ListItem item in lstBuild.Items)
                    {
                        foreach (String r in repo)
                        {
                            if (item.Value == r)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
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
                    repUser.Insert(obj);
                    break;
                case CommandNameEnum.Edit:
                    USER user = repUser.Table.Where(x => x.USER_ID.Equals(obj.USER_ID)).FirstOrDefault();
                    if (user != null)
                    {
                        user.USER_ID = obj.USER_ID;
                        user.ROLE_ID = obj.ROLE_ID;
                        user.TITLE_ID = obj.TITLE_ID;
                        user.USER_PASSWORD = obj.USER_PASSWORD;
                        user.FIRST_NAME = obj.FIRST_NAME;
                        user.LAST_NAME = obj.LAST_NAME;
                        user.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                        user.PHONE_NO = obj.PHONE_NO;
                        user.IS_ACTIVE = obj.IS_ACTIVE;
                        user.UPDATE_BY = userLogin.USER_ID;
                        user.UPDATE_DATE = DateTime.Now;
                        user.RESPONSIBLE_BUIDING = obj.RESPONSIBLE_BUIDING;
                        repUser.Update(user);
                    }
                    break;
            }
            #region "MESSAGE RESULT"

            String errorMessage = repUser.errorMessage;
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
            Response.Redirect(Constants.LINK_SEARCH_ROOM_FOR_RENT);
        }
    }
}