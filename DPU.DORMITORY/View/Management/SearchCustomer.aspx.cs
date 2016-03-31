using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using DPU.DORMITORY.Utils;
//using System.Linq.Dynamic;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class SearchCustomer : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_CUSTOMER> repCustomer;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;

        public SearchCustomer()
        {
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
        }

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
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

                tmp.BUILD_ID = String.IsNullOrEmpty(ddlBuild.SelectedValue) ? 0 : Convert.ToInt32(ddlBuild.SelectedValue);
                tmp.ROOM_ID = String.IsNullOrEmpty(ddlRoom.SelectedValue) ? 0 : Convert.ToInt32(ddlRoom.SelectedValue);
                tmp.FIRSTNAME = txtName.Text;
                return tmp;
            }
        }

        private void initialPage()
        {
            if (obj != null)
            {
                searchResult = obj.Search();
                int resultCount = CustomUtils.Count(searchResult);
                if (resultCount > 0)
                {
                    gvResult.DataSource = searchResult;
                    gvResult.DataBind();
                    gvResult.UseAccessibleHeader = true;
                    gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

            List<TB_M_BUILD> build = repBuild.Table.Where(x => userLogin.respoList.Contains(x.BUILD_ID.Value)).ToList();
            ddlBuild.DataSource = build;
            ddlBuild.DataBind();
            if (build != null && build.Count > 1)
            {
                ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }
            else
            {
                int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
                ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList().OrderBy(x=>x.NUMBER);
                ddlRoom.DataBind();
                ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
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


        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField _hCustomerStatus = (HiddenField)e.Row.FindControl("hCustomerStatus");
                LinkButton _btnInfo = (LinkButton)e.Row.FindControl("btnInfo");
                LinkButton _btnCheckOut = (LinkButton)e.Row.FindControl("btnCheckOut");
                LinkButton _btnChangeRoom = (LinkButton)e.Row.FindControl("btnChangeRoom");

                if (_hCustomerStatus != null)
                {
                    if (!String.IsNullOrEmpty(_hCustomerStatus.Value))
                    {
                        switch (_hCustomerStatus.Value)
                        {
                            case "กำลังศึกษาอยู่":
                                e.Row.ForeColor = System.Drawing.Color.Black;
                                _btnInfo.Visible = true;
                                _btnCheckOut.Visible = true;
                                _btnChangeRoom.Visible = true;
                                break;
                            default:
                                e.Row.ForeColor = System.Drawing.Color.Red;
                                _btnInfo.Visible = true;
                                _btnCheckOut.Visible = false;
                                _btnChangeRoom.Visible = false;
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchResult = obj.Search();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();

            if (gvResult.Rows.Count > 0)
            {
                gvResult.UseAccessibleHeader = true;
                gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlBuild.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            searchResult = obj.Search();
        }

        protected void ddlBuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlBuild.SelectedValue))
            {
                int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
                ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList().OrderBy(x => x.NUMBER);
                ddlRoom.DataBind();
                ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            }
        }

    }
}