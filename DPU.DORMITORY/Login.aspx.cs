using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DPU.DORMITORY.Web
{
    public partial class Login : System.Web.UI.Page
    {

        protected String Message
        {
            get { return (String)Session[Constants.SESSION_MESSAGE]; }
            set { Session[Constants.SESSION_MESSAGE] = value; }
        }

        public USER objUser
        {
            get
            {
                USER user = new USER();
                user.USER_ID = txtUserName.Text;
                user.USER_PASSWORD = CustomUtils.EncodeMD5(txtPassword.Text);
                return user;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtUserName.Text) || String.IsNullOrEmpty(txtPassword.Text))
                {
                    Message = "<div class=\"alert alert-danger\"><button class=\"close\" data-close=\"alert\"></button><span>Enter any username and password. </span></div>";
                }
                else
                {
                    USER user = objUser.checkLogin();
                    if (user != null)
                    {
                        Session.Add(Constants.SESSION_USER, user);
                        if (!Convert.ToBoolean(user.IS_FORCE_CHANGE_PASSWORD))
                        {
                            //removeSession();
                            Response.Redirect(Constants.LINK_DASHBOARD);
                        }
                        else
                        {
                            Response.Redirect(Constants.LINK_FORCE_CHANGE_PASSWORD);
                        }
                    }
                    else
                    {
                        Message = "<div class=\"alert alert-danger\"><button class=\"close\" data-close=\"alert\"></button><span>Please verify your username or password. </span></div>";
                    }
                }

            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
                Message = "<div class=\"alert alert-danger\"><button class=\"close\" data-close=\"alert\"></button><span>" + errorMessage + " </span></div>";
            }
        }

    }
}