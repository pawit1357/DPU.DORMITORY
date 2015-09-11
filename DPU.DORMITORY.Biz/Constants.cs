using System;
using System.ComponentModel;
using System.Reflection;

namespace DPU.DORMITORY.Biz
{
    public class Constants
    {
        public const String APPNAME = "";
        public const String SESSION_USER = "UserData";
        public const String SESSION_MESSAGE = "Message";

        public const String PREVIOUS_PATH = "PreviousPath";
        public const String COMMAND_NAME = "CommandName";
        public const String PLEASE_SELECT = "Please Select";

        public const char CHAR_COMMA = ',';
        public const char CHAR_COLON = ':';
        public const String CHAR_DASH = "-";
        //
        public const String LINK_LOGIN = APPNAME + "/Login.aspx";
        public const String LINK_FORCE_CHANGE_PASSWORD = APPNAME + "/ForceChangePassword.aspx";
        //MASTER
        public const String LINK_RATE = APPNAME + "/View/Master/Rates.aspx";
        public const String LINK_SEARCH_RATE = APPNAME + "/View/Master/SearchRates.aspx";
        public const String LINK_ROOM = APPNAME + "/View/Master/Room.aspx";
        public const String LINK_SEARCH_ROOM = APPNAME + "/View/Master/SearchRoom.aspx";
        public const String LINK_BUILDING = APPNAME + "/View/Master/Building.aspx";
        public const String LINK_SEARCH_BUILDING = APPNAME + "/View/Master/SearchBuilding.aspx";
        public const String LINK_CUSTOMER_TYPE = APPNAME + "/View/Master/CustomerType.aspx";
        public const String LINK_SEARCH_CUSTOMER_TYPE = APPNAME + "/View/Master/SearchCustomerType.aspx";
        public const String LINK_ROOM_TYPE = APPNAME + "/View/Master/RoomType.aspx";
        public const String LINK_SEARCH_ROOM_TYPE = APPNAME + "/View/Master/SearchRoomType.aspx";
        public const String LINK_SERVICE = APPNAME + "/View/Master/Service.aspx";
        public const String LINK_SEARCH_SERVICE = APPNAME + "/View/Master/SearchService.aspx";
        public const String LINK_TYPE_RATE = APPNAME + "/View/Master/TypeRate.aspx";
        public const String LINK_SEARCH_TYPE_RATE = APPNAME + "/View/Master/SearchTypeRate.aspx";
        public const String LINK_NATION = APPNAME + "/View/Master/Nation.aspx";
        public const String LINK_SEARCH_NATION = APPNAME + "/View/Master/SearchNation.aspx";
        public const String LINK_SEARCH_FUND = APPNAME + "/View/Master/SearchFund.aspx";
        public const String LINK_FUND = APPNAME + "/View/Master/Fund.aspx";
        //MANAGEMENT
        public const String LINK_CHECK_IN = APPNAME + "/View/Management/CheckIn.aspx";
        public const String LINK_CHECK_OUT = APPNAME + "/View/Management/CheckOut.aspx";
        public const String LINK_MOVE_ROOM = APPNAME + "/View/Management/MoveRoom.aspx";
        public const String LINK_SEARCH_CUSTOMER = APPNAME + "/View/Management/SearchCustomer.aspx";
        public const String LINK_SEARCH_PAYMENT_HISTORY = APPNAME + "/View/Management/SearchPaymentHistory.aspx";
        public const String LINK_ROOM_DETAIL = APPNAME + "/View/Management/RoomDetail.aspx";
        public const String LINK_SEARCH_ROOM_FOR_RENT = APPNAME + "/View/Management/SearchRoomForRent.aspx";
        public const String LINK_DASHBOARD = APPNAME + "/Default.aspx";

        //ADMIN
        public const String LINK_ROLE = APPNAME + "/View/Admin/Role.aspx";
        public const String LINK_SEARCH_ROLE = APPNAME + "/View/Admin/SearchRole.aspx";
        public const String LINK_USER = APPNAME + "/View/Admin/User.aspx";
        public const String LINK_SEARCH_USER = APPNAME + "/View/Admin/SearchUser.aspx";
        //ACCOUNT
        public const String LINK_CREATE_INVOICE = APPNAME + "/View/Account/CreateInvoice.aspx";
        public const String LINK_POSGINT_2_SAP = APPNAME + "/View/Account/Posting2SAP.aspx";

        public const String CSS_BUTTON_SAVE = "btn green";
        public const String CSS_BUTTON_CANCEL = "btn default";
        public const String CSS_DISABLED_BUTTON_SAVE = "btn green disabled";
        public const String CSS_DISABLED_BUTTON_CANCEL = "btn default disabled";
        public const String CSS_DISABLED_BUTTON_CALCULATE = "btn blue disabled";
        public const String CSS_BUTTON_CALCULATE = "btn blue";

        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

    public enum CommandNameEnum : int
    {
        [Description("เพิ่ม")]
        Add = 1,
        [Description("แก้ไข")]
        Edit = 2,
        [Description("Delete")]
        Delete = 3,
        [Description("View")]
        View = 4,
        [Description("เข้าพัก")]
        CheckIn = 5,
        [Description("จองห้อง")]
        Reserv = 6,
        [Description("Select")]
        Select = 7,
        [Description("ย้ายออก")]
        CheckOut = 8,
        [Description("ย้ายห้อง")]
        MoveRoom = 9,
        [Description("พิมพ์ใบแจ้งหนี้")]
        PrintInvoice = 10,
        RepairRoom = 11,
        UndoRepair = 12,
        Send2SAP = 13,
        ViewLogs = 14,
        Payment = 15
    }

    public enum MenuRoleActionEnum : int
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
    }

    public enum RoomStatusEmum : int
    {
        Available = 1,
        UnAvailable = 2,
        Reservation = 3,
        RepairRoom = 4
    }

    public enum CustomerStatusEnum : int
    {
        CheckIn = 0,
        CheckOut = 1,
        MoveRoom = 2
    }

    public enum CustomerProfileEnum : int
    {
        RegisteredAddress = 1,
        AddressByCard = 2
    }

    public enum InvoiceStatusEmum : int
    {
        Open = 1,
        Cancel = 2
    }

    public enum InvoiceTypeEnum : int
    {
        ROOM = 1,
        CUSTOMER =2
    }
    public enum PayTypeEnum : int
    {
        SELF = 1,
        FUND =2
    }
}
