using System;
using System.Configuration;

namespace DPU.DORMITORY.Biz
{
    public class Configuration
    {
        //public static String AppName
        //{
        //    get { return ConfigurationManager.AppSettings["APP_NAME"]; }
        //}

        public static String CompanyName
        {
            get { return ConfigurationManager.AppSettings["COMPANY_NAME"]; }
        }

        public static String AppTitle
        {
            get { return ConfigurationManager.AppSettings["APP_TITLE"]; }
        }
        public static String PathReportInvoice
        {
            get { return ConfigurationManager.AppSettings["PathReportInvoice"]; }
        }
        public static String PathReportRecieve
        {
            get { return ConfigurationManager.AppSettings["PathReportRecieve"]; }
        }
        public static String PathReportSummary
        {
            get { return ConfigurationManager.AppSettings["PathReportSummary"]; }
        }
        public static String PathReportSummary_1
        {
            get { return ConfigurationManager.AppSettings["PathReportSummary_1"]; }
        }
        public static String PathReportSummary_2
        {
            get { return ConfigurationManager.AppSettings["PathReportSummary_2"]; }
        }
        public static String PathReportSummary_Elec_Water
        {
            get { return ConfigurationManager.AppSettings["PathReportSummary_Elec_Water"]; }
        }


        public static String DbServiceIP
        {
            get { return ConfigurationManager.AppSettings["DbServiceIP"]; }
        }
        public static String DbUserName
        {
            get { return ConfigurationManager.AppSettings["DbUserName"]; }
        }
        public static String DbPassword
        {
            get { return ConfigurationManager.AppSettings["DbPassword"]; }
        }
        public static String DbCatalog
        {
            get { return ConfigurationManager.AppSettings["DbCatalog"]; }
        }

    }
}
