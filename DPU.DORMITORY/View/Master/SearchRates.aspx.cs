using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class SearchRates : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_RATES_GROUP> rateGroupRepo;
        private Repository<TB_RATES_GROUP_DETAIL> rateGroupDetailRepo;
        private Repository<TB_M_BUILD> repBuild;

        public SearchRates()
        {
            rateGroupRepo = unitOfWork.Repository<TB_RATES_GROUP>();
            rateGroupDetailRepo = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();

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
        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }
        public int PKID { get; set; }

        public TB_RATES_GROUP obj
        {
            get
            {
                TB_RATES_GROUP tmp = new TB_RATES_GROUP();
                tmp.BUILD_ID = String.IsNullOrEmpty(ddlBuild.SelectedValue) ? 0 : Convert.ToInt32(ddlBuild.SelectedValue);
                tmp.NAME = txtName.Text;
                return tmp;
            }
        }

        private void initialPage()
        {
            List<TB_M_BUILD> build = repBuild.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();
            ddlBuild.DataSource = build;
            ddlBuild.DataBind();
            if (build != null && build.Count > 1)
            {
                ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }

            bindingData();

        }
        private void bindingData() {
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
            Server.Transfer(Constants.LINK_RATE);
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
                    Server.Transfer(Constants.LINK_RATE);
                    break;
            }
        }

        protected void gvResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            var editModel = rateGroupRepo.GetById(this.PKID);
            if (editModel != null)
            {
                //Find Child
                List<TB_RATES_GROUP_DETAIL> listRateGroupDetail = rateGroupDetailRepo.Table.Where(x => x.RATES_GROUP_ID == this.PKID).ToList();
                foreach (TB_RATES_GROUP_DETAIL _val in listRateGroupDetail)
                {
                    rateGroupDetailRepo.Delete(_val);
                }
                rateGroupRepo .Delete(editModel);
                //
                initialPage();
            }

        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlBuild.SelectedIndex = 0;
            txtName.Text = string.Empty;
            bindingData();
        }
    }
}