using DPU.DORMITORY.Biz;
using System;

namespace DPU.DORMITORY
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove(Constants.SESSION_MESSAGE);
            Session.Remove(Constants.SESSION_USER);
            Response.Redirect(Constants.LINK_LOGIN, true);
        }
    }
}