using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using DPU.DORMITORY.Utils;
using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class SearchFund : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_M_SPONSOR> rep;
        private Repository<TB_M_SERVICE> repService;
        public SearchFund()
        {
            rep = unitOfWork.Repository<TB_M_SPONSOR>();
            repService = unitOfWork.Repository<TB_M_SERVICE>();
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

        public TB_M_SPONSOR obj
        {
            get
            {
                TB_M_SPONSOR tmp = new TB_M_SPONSOR();
                tmp.NAME = txtName.Text;
                return tmp;
            }
        }

        private void initialPage()
        {
            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

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
            if (!Page.IsPostBack)
            {
                initialPage();
                
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_FUND);
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
                    Server.Transfer(Constants.LINK_FUND);
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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litServiceFor = (Literal)e.Row.FindControl("litServiceFor");

                //if (_litServiceFor != null)
                //{
                //    if (!String.IsNullOrEmpty(_litServiceFor.Text))
                //    {
                //        int sid = Convert.ToInt32(_litServiceFor.Text);
                //        TB_M_SERVICE _service = repService.Table.Where(x => x.ID == sid).FirstOrDefault();
                //        if (_service != null)
                //        {
                //            //_litServiceFor.Text = _service.NAME;
                //        }
                //    }
                //}
            }
        }

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