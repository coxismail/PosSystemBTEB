using PosSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{
    public partial class PurchaseReport : System.Web.UI.Page
    {
        private readonly IPurchaseRepository pur;
        public PurchaseReport()
        {
            pur = new PurchaseRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var query = pur.Reports();
            var data = query.Select(f => new
            {
                Supplier = f.Supplier,
                Date = f.PurchaseDate.ToString("dd-MM-yyyy"),
                Address = f.Address,
                Id = f.Id,
                Notes = f.Notes,
                NetAmount = f.NetAmount
            }).ToList();
            purchaseReport.DataSource = data;
            purchaseReport.DataBind();
        }
        protected void purchaseReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            purchaseReport.PageIndex = e.NewPageIndex;
            purchaseReport.DataBind();
        }

        // it will not delete anyting it will show details instead of delete
        protected void purchaseReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = purchaseReport.Rows[e.RowIndex].FindControl("Idtextbox") as Label;
            if (id != null && id.Text != "")
            {
                Response.Redirect("/pages/Purchase_Details.aspx?id="+id.Text);
            }

        }
    }
}