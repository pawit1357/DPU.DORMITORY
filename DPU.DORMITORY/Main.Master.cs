using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPU.DORMITORY.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_RATES_GROUP> repRateGroup;
        #endregion
        public Main()
        {
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repRateGroup = unitOfWork.Repository<TB_RATES_GROUP>();
        }
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

                //Generate Alert
                renderAlert();
            }
        }


        private void renderAlert()
        {

            List<TB_RATES_GROUP> rgs = repRateGroup.Table.Where(x => DateTime.Now > x.END_DATE).ToList();

            if (rgs.Count > 0)
            {

                StringBuilder htmlNotification = new StringBuilder();
                htmlNotification.Append("<li class=\"dropdown dropdown-extended dropdown-notification\" id=\"header_notification_bar\">");
                htmlNotification.Append("<a href = \"javascript:;\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" data-hover=\"dropdown\" data-close-others=\"true\">");
                htmlNotification.Append("<i class=\"icon-bell\"></i>");
                htmlNotification.Append("<span class=\"badge badge-default\">" + rgs.Count + " </span>");
                htmlNotification.Append("</a>");
                htmlNotification.Append("<ul class=\"dropdown-menu\">");
                htmlNotification.Append("<li class=\"external\">");
                htmlNotification.Append("<h3>");
                htmlNotification.Append("<span class=\"bold\">" + rgs.Count + " pending</span> notifications</h3>");
                //htmlNotification.Append("<a href = \"#\" > view all</a>");
                htmlNotification.Append("</li>");
                htmlNotification.Append("<li>");
                htmlNotification.Append("<ul class=\"dropdown-menu-list scroller\" style=\"height: 250px;\" data-handle-color=\"#637283\">");
                #region "ALERT CONTENT"
                foreach (TB_RATES_GROUP log in rgs)
                {
                    htmlNotification.Append("<li>");
                    htmlNotification.Append("<a href = \"javascript:;\" >");
                    //htmlNotification.Append("<span class=\"time\">" + log.END_DATE.Value.ToShortDateString() + "</span>");
                    htmlNotification.Append("<span class=\"details\">");
                    htmlNotification.Append("<span class=\"label label-sm label-icon label-success\">");
                    htmlNotification.Append("<i class=\"fa fa-plus\"></i>");
                    htmlNotification.Append("</span>" + log.NAME + " หมดอายุแล้ว </span>");
                    htmlNotification.Append("</a>");
                    htmlNotification.Append("</li>");
                }


                #endregion
                htmlNotification.Append("</ul>");
                htmlNotification.Append("</li>");
                htmlNotification.Append("</ul>");
                htmlNotification.Append("</li>");

                litAlert.Text = htmlNotification.ToString();
            }

        }
    }
}
