using PosSystem.Models;
using PosSystem.Repository;
using System;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{
    public partial class Transfer : System.Web.UI.Page
    {
        protected string showModel { get; set; }
        private readonly IInentoryRepository inven;
        public Transfer()
        {
            inven = new InventoryRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var store = inven.StoreListItems();
                fromStoreDropDown.Items.AddRange(store.ToArray());
            }
        }

        protected void fromStoreDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            trasferpageGridview.DataSource = null;
            var storeid = fromStoreDropDown.SelectedValue;
            if (storeid == "")
            {
                return;
            }
            var data = inven.BalanceBySingleStore(Convert.ToInt32(storeid)).Where(f=>f.BalanceQ > 0).ToList();
            trasferpageGridview.DataSource = data;
            trasferpageGridview.DataBind();
        }



        protected void trasferpageGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            trasferpageGridview.PageIndex = e.NewPageIndex;
            trasferpageGridview.DataBind();
        }

        public void TransferView_InsertItem()
        {
            var item = new PosSystem.Models.TransferViewModel();
            TryUpdateModel(item);
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (!inven.IsProductAvailable(item.FromStoreId, item.ProductId, item.Qty))
                    {
                        MslLabel.Text = "Quantity not available on this store";
                        return;
                    }

                    // check quantity is valid
                    var inp = new InCommingProducts();
                    inp.InQuantity = item.Qty;
                    inp.ProductId = item.ProductId;
                    inp.Amount = 00;
                    inp.StoreId = item.ToStoreId;
                    inp.Type = ProdTransType.Transfer;
                    inp.TransDate = DateTime.UtcNow.AddHours(6);

                    var oup = new OutGoingProducts();
                    oup.ProductId = item.ProductId;
                    oup.OutQuantity = item.Qty;
                    oup.Amount = 00;
                    oup.Type = ProdTransType.Transfer;
                    oup.StoreId = item.FromStoreId;
                    oup.TransDate = DateTime.UtcNow.AddHours(6);
                    inven.Transfer(inp, oup);

                    MslLabel.Text = "Successfully Transfered";
                    return;
                }
                MslLabel.Text = string.Join(", ", ModelState.Values.SelectMany(f => f.Errors).Select(s => s.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                MslLabel.Text = ex.Message;
                throw;
            }
          
            
        }

        protected void trasferpageGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            showModel = "showtheModel";
            var storeid = fromStoreDropDown.SelectedValue;
            TextBox ProId = trasferpageGridview.Rows[e.RowIndex].FindControl("pro_Id") as TextBox;
            var fromStoreFeild = TransferView.FindControl("fromstoreId") as TextBox;
            DropDownList tostroeDropDown = TransferView.FindControl("toStoreDropDown") as DropDownList;
            var qty = TransferView.FindControl("qtyTextBox") as TextBox;
            var ProductField = TransferView.FindControl("productId") as TextBox;

            // bind to view
            var items = inven.StoreListItems();
            var except = items.Where(f => f.Value != storeid).ToList();
            tostroeDropDown.Items.Clear();
            tostroeDropDown.Items.AddRange(except.ToArray());
            fromStoreFeild.Text = storeid;
            ProductField.Text = ProId.Text;

            var all = inven.BalanceBySingleStore(Convert.ToInt32(storeid));
            var bl = all.Where(f => f.ProductId == Convert.ToInt32(ProId.Text)).SingleOrDefault();
            qty.Text = bl.BalanceQ.ToString();
        }

    }
}