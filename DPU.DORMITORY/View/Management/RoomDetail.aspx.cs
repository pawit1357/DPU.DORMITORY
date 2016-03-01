using DPU.DORMITORY.Biz;
using DPU.DORMITORY.Biz.DataAccess;
using DPU.DORMITORY.Repositories;
using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPU.DORMITORY.Web.View.Management
{
    public partial class RoomDetail : System.Web.UI.Page
    {

        #region "Property"
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TB_RATES_GROUP_DETAIL> repRateGroupDetail;
        private Repository<TB_ROOM> repRoom;
        private Repository<TB_M_BUILD> repBuild;
        private Repository<TB_CUSTOMER> repCustomer;

        public RoomDetail()
        {
            repRateGroupDetail = unitOfWork.Repository<TB_RATES_GROUP_DETAIL>();
            repRoom = unitOfWork.Repository<TB_ROOM>();
            repBuild = unitOfWork.Repository<TB_M_BUILD>();
            repCustomer = unitOfWork.Repository<TB_CUSTOMER>();
        }

        public USER userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (USER)Session[Constants.SESSION_USER] : null); }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[ Constants.PREVIOUS_PATH]; }
            set { ViewState[Constants.PREVIOUS_PATH] = value; }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchResult"]; }
            set { Session[GetType().Name + "searchResult"] = value; }
        }

        public int PKID
        {
            get { return (int)ViewState["CheckIn_PKID"]; }
            set { ViewState["CheckIn_PKID"] = value; }
        }

        public TB_ROOM objRoom
        {
            get { return (TB_ROOM)ViewState["objRoom"]; }
            set { ViewState["objRoom"] = value; }
        }

        private void initialPage()
        {
            SearchRoomForRent prvPage = Page.PreviousPage as SearchRoomForRent;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.ROOM_ID;
            this.PreviousPath = Constants.LINK_SEARCH_ROOM_FOR_RENT;

            objRoom = repRoom.Table.Where(x => x.ID == this.PKID).FirstOrDefault();
            if (objRoom != null)
            {
                TB_M_BUILD _build = repBuild.Table.Where(x => x.ID == objRoom.BUILD_ID).FirstOrDefault();
                if (_build != null)
                {
                    txtBuild.Text = _build.NAME;
                }
                txtRoom.Text = objRoom.NUMBER.ToString();
                TB_RATES_GROUP_DETAIL rateGroup = repRateGroupDetail.Table.Where(x => x.RATES_GROUP_ID == objRoom.RATES_GROUP_ID).FirstOrDefault();
                if (rateGroup != null)
                {
                    gvResult.DataSource = rateGroup.Search();
                    gvResult.DataBind();
                    //gvResult.UseAccessibleHeader = true;
                    //gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                TB_CUSTOMER _customer = new TB_CUSTOMER();// repCustomer.Table.Where(x => x.ROOM_ID == objRoom.ID).FirstOrDefault();
                _customer.ROOM_ID = objRoom.ID;
                if (_customer != null)
                {
                    gvCustomer.DataSource = _customer.Search();
                    gvCustomer.DataBind();
                    //gvCustomer.UseAccessibleHeader = true;
                    //gvCustomer.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }


        private void removeSession()
        {
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }
}