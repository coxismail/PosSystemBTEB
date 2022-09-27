using Microsoft.Reporting.WebForms;
using PosSystem.Repository;
using System;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private ReportService rep;
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            rep = new ReportService();
            dashboardReport.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            string path = "/Reports/dashboardreport.rdlc";
            string fullpath = Server.MapPath(path);
            dashboardReport.LocalReport.ReportPath = fullpath;
            dashboardReport.LocalReport.DataSources.Clear();
            dashboardReport.LocalReport.DataSources.Add(new ReportDataSource("ss", rep.Sales()));
            dashboardReport.LocalReport.DataSources.Add(new ReportDataSource("ps", rep.Purchases()));
            dashboardReport.LocalReport.DataSources.Add(new ReportDataSource("customer", rep.Customers()));
            dashboardReport.LocalReport.DataSources.Add(new ReportDataSource("supplier", rep.Suppliers()));

        }
    }
}