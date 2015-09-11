using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class SearchCustomer : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_CUSTOMER> repCustomer;

        public SearchCustomer()
        {
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
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

        public TB_CUSTOMER obj
        {
            get
            {
                TB_CUSTOMER tmp = new TB_CUSTOMER();
                tmp.STATUS  = Convert.ToInt32(CustomerStatusEnum.CheckIn);
                return tmp;
            }
        }

        private void initialPage()
        {
            if (obj != null)
            {
                gvResult.DataSource = obj.Search();
                gvResult.DataBind();
                gvResult.UseAccessibleHeader = true;
                gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }


        #region "GRIDVIEW RESULT"


        protected void gvResult_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.CheckOut:
                case CommandNameEnum.MoveRoom:
                    Server.Transfer(Constants.LINK_CHECK_IN);
                    break;
            }
        }
        #endregion
    }
}