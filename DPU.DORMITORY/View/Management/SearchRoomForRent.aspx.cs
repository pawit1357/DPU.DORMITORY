using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DPU.DORMITORY.Utils;
using DPU.DORMITORY.Properties;
using System.Collections.Generic;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class SearchRoomForRent : System.Web.UI.Page
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }

        public SearchRoomForRent()
        {
            repRoom = unitOfWork.Repository<TB_ROOM>();
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
        public int ROOM_ID
        {
            get { return (int)Session[GetType().Name + "ROOM_ID"]; }
            set { Session[GetType().Name + "ROOM_ID"] = value; }
        }

        public TB_ROOM obj
        {
            get
            {
                TB_ROOM tmp = new TB_ROOM();
                tmp.BUILD_ID = String.IsNullOrEmpty(ddlBuild.SelectedValue)? 0: Convert.ToInt32(ddlBuild.SelectedValue);
                //tmp.ID = String.IsNullOrEmpty(ddlRoom.SelectedValue) ? 0 : Convert.ToInt32(ddlRoom.SelectedValue);
                tmp.NUMBER = txtRoomNum.Text;
                tmp.STATUS = String.IsNullOrEmpty(ddlStatus.SelectedValue) ? 0 : Convert.ToInt32(ddlStatus.SelectedValue);
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
            //else
            //{
            //    int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
            //    ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList().OrderBy(x=>x.NUMBER);
            //    ddlRoom.DataBind();
            //    ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            //}



            foreach (RoomStatusEmum r in Enum.GetValues(typeof(RoomStatusEmum)))
            {
                ListItem item = new ListItem(Constants.GetEnumDescription(r) , Convert.ToInt32(r)+"");
                ddlStatus.Items.Add(item);
            }
            ddlStatus.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            //
            //pSearchResult.Visible = false;
            
          
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            if (gvResult.Rows.Count > 0)
            {
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
            //if (searchResult != null)
            //{
            //    gvResult.DataSource = searchResult;
            //    gvResult.DataBind();
            //    gvResult.UseAccessibleHeader = true;
            //    gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_ROOM);
        }

        #region "GRIDVIEW RESULT"


        protected void gvResult_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            this.ROOM_ID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            int customerInRoom = (!String.IsNullOrEmpty(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1])) ? int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]) : 0;
            TB_ROOM _room = repRoom.Table.Where(x => x.ID == this.ROOM_ID).FirstOrDefault();
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    Server.Transfer(Constants.LINK_ROOM_DETAIL);
                    break;
                case CommandNameEnum.CheckIn:
                    //CheckCustomerInRoom
                    if (customerInRoom >= _room.CUSTOMER_LIMIT)
                    {
                        MessageBox.Show(this.Page, String.Format(Resources.MSG_STAY_OVER_LIMIT, _room.CUSTOMER_LIMIT));
                    }
                    else
                    {
                        Server.Transfer(Constants.LINK_CHECK_IN);
                    }
                    break;
                case CommandNameEnum.Reserv:
                    if (_room != null)
                    {
                        _room.STATUS = Convert.ToInt32(RoomStatusEmum.Reservation);
                        repRoom.Update(_room);
                        //Refresh
                        gvResult.DataSource = obj.SearchForRent();
                        gvResult.DataBind();
                        gvResult.UseAccessibleHeader = true;
                        gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    break;
                case CommandNameEnum.RepairRoom:
                    if (_room != null)
                    {
                        _room.STATUS = Convert.ToInt32(RoomStatusEmum.RepairRoom);
                        repRoom.Update(_room);
                        //Refresh
                        gvResult.DataSource = obj.SearchForRent();
                        gvResult.DataBind();
                        gvResult.UseAccessibleHeader = true;
                        gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    break;
                case CommandNameEnum.UndoRepair:
                    if (_room != null)
                    {
                        _room.STATUS = Convert.ToInt32(RoomStatusEmum.Available);
                        repRoom.Update(_room);
                        //Refresh
                        gvResult.DataSource = obj.SearchForRent();
                        gvResult.DataBind();
                        gvResult.UseAccessibleHeader = true;
                        gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    break;
            }
        }


        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnView = (LinkButton)e.Row.FindControl("btnView");
                LinkButton btnCheckIn = (LinkButton)e.Row.FindControl("btnCheckIn");
                //LinkButton btnReserv = (LinkButton)e.Row.FindControl("btnReserv");
                LinkButton btnRepairRoom = (LinkButton)e.Row.FindControl("btnRepairRoom");
                LinkButton btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                Literal _litRoomStatus = (Literal)e.Row.FindControl("litRoomStatus");
                Literal _litCustomerLimit = (Literal)e.Row.FindControl("litCustomerLimit");

                HiddenField _hCnt = (HiddenField)e.Row.FindControl("hCnt");
                if (btnView != null && btnCheckIn != null && btnRepairRoom != null && btnUndo != null)
                {
                    int current = !CustomUtils.isNumber(_hCnt.Value) ? 0 : Convert.ToInt32(_hCnt.Value);
                    int limit = !CustomUtils.isNumber(_litCustomerLimit.Text) ? 0 : Convert.ToInt32(_litCustomerLimit.Text);
                    Boolean isAllowCheckIn = (current < limit) ? true : false;
                    if (!String.IsNullOrEmpty(_litRoomStatus.Text) && isAllowCheckIn)
                    {
                        RoomStatusEmum status = (RoomStatusEmum)Enum.Parse(typeof(RoomStatusEmum), _litRoomStatus.Text, true);
                        switch (status)
                        {
                            case RoomStatusEmum.Available:
                                _litRoomStatus.Text = " <span class=\"label label-sm label-success\">ห้องว่าง </span>";
                                btnView.Visible = true;
                                btnCheckIn.Visible = isAllowCheckIn;
                                //btnReserv.Visible = true;
                                btnRepairRoom.Visible = true;
                                btnUndo.Visible = false;
                                break;
                            case RoomStatusEmum.UnAvailable:
                                _litRoomStatus.Text = " <span class=\"label label-sm label-warning\">ห้องไม่ว่าง </span>";
                                btnView.Visible = true;
                                btnCheckIn.Visible = false;
                                //btnReserv.Visible = true;
                                btnRepairRoom.Visible = true;
                                btnUndo.Visible = false;
                                break;
                            //case RoomStatusEmum.Reservation:
                            //    _litRoomStatus.Text = " <span class=\"label label-sm label-info\">จอง </span>";
                            //    btnView.Visible = true;
                            //    btnCheckIn.Visible = true;
                            //    btnReserv.Visible = false;
                            //    btnRepairRoom.Visible = false;
                            //    btnUndo.Visible = true;
                            //    break;
                            case RoomStatusEmum.RepairRoom:
                                _litRoomStatus.Text = " <span class=\"label label-sm label-info\">ทำความสะอาด </span>";
                                btnView.Visible = true;
                                btnCheckIn.Visible = false;
                                //btnReserv.Visible = false;
                                btnRepairRoom.Visible = false;
                                btnUndo.Visible = true;
                                break;

                        }
                    }
                    else
                    {
                        _litRoomStatus.Text = " <span class=\"label label-sm label-warning\">ห้องไม่ว่าง </span>";
                        btnView.Visible = true;
                        btnCheckIn.Visible = false;
                        //btnReserv.Visible = true;
                        btnRepairRoom.Visible = true;
                        btnUndo.Visible = false;

                        //if (!String.IsNullOrEmpty(_hCnt.Value))
                        //{
                        //    if (Convert.ToInt32(_hCnt.Value) > 0)
                        //    {
                        //        _litRoomStatus.Text = " <span class=\"label label-sm label-warning\">ห้องไม่ว่าง </span>";
                        //    }
                        //}
                        //else
                        //{
                        //    _litRoomStatus.Text = " <span class=\"label label-sm label-success\">ห้องว่าง </span>";
                        //}
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchResult = obj.SearchForRent();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();

            if (gvResult.Rows.Count > 0)
            {
                gvResult.UseAccessibleHeader = true;
                gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                pSearchResult.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlBuild.SelectedIndex = 0;
            //ddlRoom.SelectedIndex = 0;
            searchResult = obj.SearchForRent();
        }

        protected void ddlBuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult = obj.SearchForRent();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();

            if (gvResult.Rows.Count > 0)
            {
                gvResult.UseAccessibleHeader = true;
                gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                pSearchResult.Visible = true;
            }
            //if (!String.IsNullOrEmpty(ddlBuild.SelectedValue))
            //{
            //    int buildId = Convert.ToInt32(ddlBuild.SelectedValue);
            //    ddlRoom.DataSource = repRoom.Table.Where(x => x.BUILD_ID == buildId).ToList().OrderBy(x => x.NUMBER);
            //    ddlRoom.DataBind();
            //    ddlRoom.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            //}
        }


    }
}