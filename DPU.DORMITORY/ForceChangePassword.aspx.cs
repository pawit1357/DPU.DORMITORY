using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;

namespace DPU.DORMITORY
{
    public partial class ForceChangePassword : System.Web.UI.Page
    {

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }

        private UnitOfWork unitOfWork = new UnitOfWork();

        private Repository<USER> repUser;

        public ForceChangePassword()
        {
            repUser = unitOfWork.Repository<USER>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Text = userLogin.USER_ID;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.LINK_LOGIN);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            USER _user = repUser.GetById(userLogin.USER_ID);
            if (_user != null)
            {
                _user.USER_PASSWORD = CustomUtils.EncodeMD5(txtPassword.Text);
                _user.IS_FORCE_CHANGE_PASSWORD = false;
                repUser.Update(_user);
            }

            #region "MESSAGE RESULT"
            String errorMessage = repUser.errorMessage;
            if (!String.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(this, errorMessage);
            }
            else
            {
                MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, Constants.LINK_LOGIN);
            }
            #endregion
        }
    }
}