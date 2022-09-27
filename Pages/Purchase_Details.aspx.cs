using PosSystem.Repository;
using System;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class Purchase_Details : System.Web.UI.Page
    {
        private readonly IPurchaseRepository purepo;
        protected  Models.Purchase purchase { get; set; }
        public Purchase_Details()
        {
            purepo = new PurchaseRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public PosSystem.Models.Purchase PurchaseView_GetItem()
        {
            var id = Request.QueryString["id"];
            if (id == null || id == "")
            {
                Response.Redirect("~/pages/purchaseReport.aspx");
            }
            var found = purepo.Details(Convert.ToInt32(id));
            if (found == null)
            {
                Response.Redirect("~/pages/purchaseReport.aspx");
            }
            purchase = found;
            return found;
        }
    }
}