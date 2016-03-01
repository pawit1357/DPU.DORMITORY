using CrystalDecisions.CrystalReports.Engine;
using System;

namespace DPU.DORMITORY.Biz
{

    public class ReportBiz
    {
        #region "Property"
        public DateTime date { get; set; }
        public int build_id { get; set; }
        public int room_id { get; set; }
        public int customer_id { get; set; }
        public String invoice { get; set; }
        public Boolean isPaid { get; set; }

        #endregion


        public ReportDocument getRpt01(int lang_id)
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportInvoice);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_CUSTOMER_ID", customer_id);
            _rpt.SetParameterValue("P_INVOICE_ID", 0);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            _rpt.SetParameterValue("P_BUILD", build_id);
            _rpt.SetParameterValue("P_NATION", lang_id);

            
            return _rpt;
        }

        public ReportDocument getRpt02(int lang_id)
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportRecieve);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_CUSTOMER_ID", customer_id);
            _rpt.SetParameterValue("P_INVOICE_ID", 0);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            _rpt.SetParameterValue("P_BUILD", build_id);
            _rpt.SetParameterValue("P_NATION", lang_id);

            return _rpt;
        }

        public ReportDocument getRpt03()
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportSummary_1);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_CUSTOMER_ID", customer_id);
            _rpt.SetParameterValue("P_INVOICE_ID", 0);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            _rpt.SetParameterValue("P_BUILD", build_id);
            return _rpt;
        }
        public ReportDocument getRpt04()
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportSummary);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_CUSTOMER_ID", customer_id);
            _rpt.SetParameterValue("P_INVOICE_ID", 0);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            return _rpt;
        }

        public ReportDocument getRpt05()
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportSummary_Elec_Water);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_BUILD", build_id);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            return _rpt;
        }
        public ReportDocument getRpt06()
        {
            ReportDocument _rpt = new ReportDocument();
            _rpt.Load(Configuration.PathReportSummary_2);
            _rpt.SetDatabaseLogon(Configuration.DbUserName, Configuration.DbPassword, Configuration.DbServiceIP, Configuration.DbCatalog);
            _rpt.SetParameterValue("P_ROOM_ID", room_id);
            _rpt.SetParameterValue("P_CUSTOMER_ID", customer_id);
            _rpt.SetParameterValue("P_INVOICE_ID", 0);
            _rpt.SetParameterValue("P_MONTH", date.Month);
            _rpt.SetParameterValue("P_YEAR", date.Year);
            _rpt.SetParameterValue("P_BUILD", build_id);
            return _rpt;
        }
    }
}
