using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class SearchCustomerType : System.Web.UI.Page
    {


        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_CUSTOMER_TYPE> rep;

        public SearchCustomerType()
        {
            rep = unitOfWork.Repository<TB_M_CUSTOMER_TYPE>();
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchResult"]; }
            set { Session[GetType().Name + "searchResult"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public TB_M_CUSTOMER_TYPE obj
        {
            get
            {
                TB_M_CUSTOMER_TYPE tmp = new TB_M_CUSTOMER_TYPE();
                tmp.NAME = txtName.Text;
                return tmp;
            }
        }

        private void initialPage()
        {
            BindingData();
        }

        private void BindingData()
        {
            searchResult = obj.Search();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

            if (!Page.IsPostBack)
            {
                initialPage();
                
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_CUSTOMER_TYPE);
        }

        #region "GRIDVIEW RESULT"
        protected void gvResult_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }

        protected void gvResult_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    Server.Transfer(Constants.LINK_CUSTOMER_TYPE);
                    break;
            }
        }

        protected void gvResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            var editModel = rep.GetById(this.PKID);
            if (editModel != null)
            {
                rep.Delete(editModel);
                #region "MESSAGE RESULT"
                String errorMessage = rep.errorMessage;
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(this, errorMessage);
                }
                #endregion
                BindingData();
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            BindingData();
        }
    }
}