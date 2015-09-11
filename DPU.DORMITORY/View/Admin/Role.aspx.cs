using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using DPU.DORMITORY.Properties;

namespace DPU.DORMITORY.Web.View.Admin
{
    public partial class Role : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MENU> repMenu;
        private Repository<MENU_ROLE> repMenuRole;
        private Repository<USERS_ROLE> repUserRole;
        public Role()
        {
            repUserRole = unitOfWork.Repository<USERS_ROLE>();
            repMenu = unitOfWork.Repository<MENU>();
            repMenuRole = unitOfWork.Repository<MENU_ROLE>();
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

        public int PKID
        {
            get { return (int)ViewState["Role_PKID"]; }
            set { ViewState["Role_PKID"] = value; }
        }

        public USERS_ROLE obj
        {
            get
            {
                USERS_ROLE tmp = new USERS_ROLE();
                tmp.NAME = txtName.Text;
                return tmp;
            }
        }

        private void initialPage()
        {

            SearchRole prvPage = Page.PreviousPage as SearchRole;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_ROLE;

            lbCommandName.Text = CommandName.ToString();

            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    txtName.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();

                    txtName.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();

                    txtName.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    break;
            }


            MENU_ROLE menuRoleBiz = new MENU_ROLE();
            MenuBiz menuBiz = new MenuBiz();

            List<MENU_ROLE> _menuRoles = menuRoleBiz.getRoleListByRoleId(PKID);
            menuBiz.getmenuByTree(ref this.tvPermission, _menuRoles);
        }

        private void fillinScreen()
        {
            USERS_ROLE role = repUserRole.Table.Where(x => x.ROLE_ID == this.PKID).FirstOrDefault();
            if (role != null)
            {
                txtName.Text = role.NAME;
            }

        }
        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
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
                    repUserRole.Insert(obj);
                    break;
                case CommandNameEnum.Edit:
                    USERS_ROLE _role = repUserRole.GetById(this.PKID);
                    if (_role != null)
                    {
                        _role.NAME = obj.NAME;
                        repUserRole.Update(_role);
                    }
                    break;
            }
            List<MENU_ROLE> _menuRoles = repMenuRole.Table.Where(x => x.ROLE_ID == this.PKID).ToList();
            foreach (MENU_ROLE menuRole in _menuRoles)
            {
                repMenuRole.Delete(menuRole);
            }
            List<MENU> menus = repMenu.Table.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER).ToList();
            foreach (MENU _menu in menus)
            {
                MENU_ROLE _menuRole = new MENU_ROLE();
                _menuRole.ROLE_ID = this.PKID;
                _menuRole.MENU_ID = Convert.ToInt32(_menu.MENU_ID);
                _menuRole.IS_REQUIRED_ACTION = false;
                _menuRole.IS_CREATE = isChecked(_menuRole.MENU_ID, CommandNameEnum.Add);
                _menuRole.IS_EDIT = isChecked(_menuRole.MENU_ID, CommandNameEnum.Edit);
                _menuRole.IS_DELETE = isChecked(_menuRole.MENU_ID, CommandNameEnum.Delete);
                _menuRole.UPDATE_BY = "SYSTEM";
                _menuRole.CREATE_DATE = DateTime.Now;
                _menuRole.UPDATE_DATE = DateTime.Now;
                _menuRole.RowState = CommandNameEnum.Add;
                repMenuRole.Insert(_menuRole);
            }
            menus = repMenu.Table.Where(x => x.PREVIOUS_MENU_ID != null).OrderBy(x => x.DISPLAY_ORDER).ToList();
            foreach (MENU _menu in menus)
            {
                MENU_ROLE _menuRole = new MENU_ROLE();
                _menuRole.ROLE_ID = this.PKID;
                _menuRole.MENU_ID = Convert.ToInt32(_menu.MENU_ID);
                _menuRole.IS_REQUIRED_ACTION = false;
                _menuRole.IS_CREATE = isChecked(_menuRole.MENU_ID, CommandNameEnum.Add);
                _menuRole.IS_EDIT = isChecked(_menuRole.MENU_ID, CommandNameEnum.Edit);
                _menuRole.IS_DELETE = isChecked(_menuRole.MENU_ID, CommandNameEnum.Delete);
                _menuRole.UPDATE_BY = "SYSTEM";
                _menuRole.CREATE_DATE = DateTime.Now;
                _menuRole.UPDATE_DATE = DateTime.Now;
                _menuRole.RowState = CommandNameEnum.Add;

                if ((bool)_menuRole.IS_CREATE || (bool)_menuRole.IS_EDIT || (bool)_menuRole.IS_DELETE)
                {
                    repMenuRole.Insert(_menuRole);
                }
               

            }

            #region "MESSAGE RESULT"
            String errorMessage = repMenuRole.errorMessage;
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

        public Boolean isChecked(int _menuID, CommandNameEnum cmd)
        {
            Boolean isChecked = false;
            foreach (TreeNode tn in tvPermission.CheckedNodes)
            {
                if (Convert.ToInt32(tn.Value) == _menuID && tn.Text.Equals(cmd.ToString()))
                {
                    isChecked = true;
                    break;
                }

            }
            return isChecked;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }
}