using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Admin
{
    public partial class SearchUser : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<USER> repUser;

        public SearchUser()
        {
            repUser = unitOfWork.Repository<USER>();
        }

        #region "Property"
        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchUser"]; }
            set { Session[GetType().Name + "SearchUser"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchUser");
        }

        public String PKID { get; set; }

        public USER obj
        {
            get
            {
                USER tmp = new USER();
                //tmp.username = txtUserName.Text;
                //tmp.userProfile = new user_profile { phone = txtPhone.Text };
                return tmp;
            }
        }

        #endregion

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

        private void bindingData()
        {
            searchResult = obj.SearchData();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_USER);
        }
        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    this.PKID = e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0];
                    Server.Transfer(Constants.LINK_USER);
                    break;
            }
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0];

            USER user = repUser.GetById(this.PKID);
            if (user != null)
            {
                repUser.Delete(user);
                bindingData();
            }
        }

    }
}