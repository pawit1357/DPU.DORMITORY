using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Properties;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Admin
{
    public partial class SearchRole : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();

        private Repository<USERS_ROLE> repRole;

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchRole"]; }
            set { Session[GetType().Name + "SearchRole"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public USERS_ROLE obj
        {
            get
            {
                USERS_ROLE tmp = new USERS_ROLE();
                return tmp;
            }
        }
        #endregion

        public SearchRole()
        {
            repRole = unitOfWork.Repository<USERS_ROLE>();
        }

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

        private void bindingData()
        {
            searchResult = repRole.Table.ToList();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchRole");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    Server.Transfer(Constants.LINK_ROLE);
                    break;
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_ROLE);
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);
            USERS_ROLE deleteRole = repRole.GetById(this.PKID);
            if (deleteRole != null)
            {
                repRole.Delete(deleteRole);
                #region "MESSAGE RESULT"
                String errorMessage = repRole.errorMessage;
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(this, errorMessage);
                }
                else
                {
                    bindingData();
                }
                #endregion
            }
        }

        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }
    }
}