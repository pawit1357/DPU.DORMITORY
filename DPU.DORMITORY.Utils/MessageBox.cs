using System;
using System.Web.UI;

namespace DPU.DORMITORY.Utils
{
    public static class MessageBox
    {
        public static void Show(this Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
        public static void Show(this Page Page, String Message, String Location)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');window.location.href='" + Location + "';</script>"
            );
        }
    }
}
