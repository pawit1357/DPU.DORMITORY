using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Utils;
using System;

namespace DPU.DORMITORY.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }

        public void CheckAuthenPage()
        {
            if (userLogin == null)
            {
                Response.Redirect(Constants.LINK_LOGIN);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            CheckAuthenPage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //USER _user = new USER();
            //_user.USER_ID = "admin";
            //_user.USER_PASSWORD = CustomUtils.EncodeMD5("1234");

            //Session.Add(Constants.SESSION_USER, _user.checkLogin());

            if (userLogin != null)
            {
                MENU_ROLE menuRoleBiz = new MENU_ROLE();
                MenuBiz menuBiz = new MenuBiz();
                //Generate Navigator
                litNavigator.Text = menuBiz.getNavigator(Request.PhysicalPath);
                litMenu.Text = menuBiz.getMenuByRole(menuRoleBiz.getMenuByRole((int)userLogin.ROLE_ID), Request.PhysicalPath);
                litUserData.Text = String.Format("{0}.{1} [{2}]", userLogin.TITLE_ID, userLogin.FIRST_NAME, userLogin.EMAIL_ADDRESS);
            }
        }
    }
}
