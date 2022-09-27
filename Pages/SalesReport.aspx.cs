using PosSystem.Repository;
using System;
using System.Collections.Generic;
using PosSystem.Models;
using System.Web.UI.WebControls;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class SalesReport : System.Web.UI.Page
    {
        protected List<Models.Sales> salesReportdata { get; set; }
        private readonly ISalesRepository sr;
        public SalesReport()
        {
            sr = new SalesRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                salesReportdata = sr.Reports();
            }
        }

        protected void printSalesReport_Click(object sender, EventArgs e)
        {

            var btn = (Button)sender;
          var args =  btn.CommandArgument;
            
        }
    }
}