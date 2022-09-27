using PosSystem.Repository;
using System;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class invoice : System.Web.UI.Page
    {
     private readonly   ISalesRepository salesRepository;
        protected Models.Sales sales { get; set; }
        public invoice()
        {
            salesRepository = new SalesRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public object invoiceView_GetItem()
        {
            var id = Request.QueryString["id"];
            if (id == null || id == "")
            {
                Response.Redirect("~/pages/SalesReport.aspx");
            }
            var found = salesRepository.Details(Convert.ToInt32(id));
            if (found == null)
            {
                Response.Redirect("~/pages/SalesReport.aspx");
            }
            sales = found;
            return found;
        
        }
    }
}