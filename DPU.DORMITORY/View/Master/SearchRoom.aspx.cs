﻿using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Master
{
    public partial class SearchRoom : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_ROOM> roomRepo;
        private Repository<TB_M_BUILD> repBuild;

        private Repository<TB_M_ROOM_TYPE> repRoomType;

        public SearchRoom()
        {
            roomRepo = unitOfWork.Repository<TB_ROOM>();
            repRoomType = unitOfWork.Repository<TB_M_ROOM_TYPE>();
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

        public int PKID { get; set; }

        public TB_ROOM obj
        {
            get
            {
                TB_ROOM tmp = new TB_ROOM();
                tmp.BUILD_ID = String.IsNullOrEmpty(ddlBuild.SelectedValue) ? 0 : Convert.ToInt32(ddlBuild.SelectedValue);
                tmp.ROOM_TYPE_ID = String.IsNullOrEmpty(ddlRoomType.SelectedValue) ? 0 : Convert.ToInt32(ddlRoomType.SelectedValue);
                tmp.NUMBER = txtName.Text;
                return tmp;
            }
        }
        private void initialPage()
        {

            ddlBuild.DataSource = repBuild.Table.ToList();
            ddlBuild.DataBind();
            ddlBuild.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlRoomType.DataSource = repRoomType.Table.ToList();
            ddlRoomType.DataBind();
            ddlRoomType.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            litPageTitle.Text = new MenuBiz().getCurrentMenuName(Request.PhysicalPath);

            if (!Page.IsPostBack)
            {
                initialPage();
                bindingData();
            }
        }

        private void bindingData()
        {
            searchResult = obj.Search();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_ROOM);
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
                    Server.Transfer(Constants.LINK_ROOM);
                    break;
            }
        }

        protected void gvResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            var editModel = roomRepo.GetById(this.PKID);
            if (editModel != null)
            {
                roomRepo.Delete(editModel);
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
            ddlRoomType.SelectedIndex = 0;
            txtName.Text = string.Empty;
            bindingData();
        }
    }
}