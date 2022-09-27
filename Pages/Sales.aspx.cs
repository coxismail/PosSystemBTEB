using PosSystem.Models;
using PosSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{
    public partial class Sales : System.Web.UI.Page
    {
        private readonly ISalesRepository srep;
        protected List<SalesDetails> CartItems { get; set; }
        public Sales()
        {
            srep = new SalesRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {

            CartItems = Session["CartItemSales"] == null ? new List<SalesDetails>() : Session["CartItemSales"] as List<SalesDetails>;
            if (!IsPostBack)
            {
                BindDropDown();
            }
        }

        private void BindDropDown()
        {
            var storeListData = srep.StoreList().ToArray();
            var productListData = srep.ProductsList().ToArray();
            var storeDropDown = (DropDownList)SalesFormView.Row.FindControl("storeDropDown");
            storeDropDown.Items.AddRange(storeListData);
            var productDropDown = (DropDownList)SalesFormView.Row.FindControl("ProductDropDownList");
            productDropDown.Items.AddRange(productListData);
        }

        protected void AddtoCardBtn_Click(object sender, EventArgs e)
        {
            CartItems = Session["CartItemSales"] as List<SalesDetails>;
            if (CartItems == null)
            {
                CartItems = new List<SalesDetails>();
            }

            var MsgLabel = (Label)SalesFormView.Row.FindControl("addactionMessage");
            MsgLabel.Text = "";
            var storeDropDown = (DropDownList)SalesFormView.Row.FindControl("storeDropDown");
            var prodropdown = (DropDownList)SalesFormView.Row.FindControl("ProductDropDownList");
            var qty = (TextBox)SalesFormView.Row.FindControl("QuantityTextBox");
            var rate = (TextBox)SalesFormView.Row.FindControl("RateTextBox");
            var storeId = storeDropDown.SelectedValue;
            var prodId = prodropdown.SelectedValue;


            if (prodId == null || prodId == "")
            {
                MsgLabel.Text = "Please Select Product";
                return;
            }

            if (storeId == null || storeId == "")
            {
                MsgLabel.Text = "Please Select Store First";
                return;
            }
            if (qty.Text == null || rate.Text == null || qty.Text == "" || rate.Text == "")
            {
                MsgLabel.Text = "Rate and Quantity can't be empty";
                return;
            }
            try
            {
                if (Convert.ToInt32(qty.Text) <= 0 || Convert.ToInt32(rate.Text) <= 0)
                {
                    MsgLabel.Text = "Quantity and rate must be greater than 0";
                    return;
                }
                var existqty = CartItems.Where(f => f.ProductId == Convert.ToInt32(prodId))?.ToList().Sum(f => f.Quantity) ?? 0;

                if (!srep.IsProductAvailable(Convert.ToInt32(storeId), Convert.ToInt32(prodId), existqty + Convert.ToInt32(qty.Text)))
                {
                    MsgLabel.Text = "Demanded Quantity is not available on selected store";
                    return;
                }

                var prod = srep.Product(Convert.ToInt32(prodId));
                var details = new SalesDetails()
                {
                    Product = prod,
                    ProductId = prod.Id,
                    Quantity = Convert.ToInt32(qty.Text),
                    Rate = Convert.ToInt32(rate.Text),
                    TotalAmount = Convert.ToInt32(rate.Text) * Convert.ToInt32(qty.Text)
                };
                CartItems.Add(details);
                Session["CartItemSales"] = CartItems;
            }
            catch (Exception ex)
            {
                MsgLabel.Text = ex.Message;
                return;
            }
        }

        protected void clearAllfromCart_Click(object sender, EventArgs e)
        {
            Session["CartItemSales"] = null;
            CartItems = new List<SalesDetails>();
        }

        public void SalesFormView_InsertItem()
        {
            CartItems = Session["CartItemSales"] as List<SalesDetails>;
            var MsgLabel = (Label)SalesFormView.Row.FindControl("addactionMessage");
            var item = new PosSystem.Models.Sales();
            TryUpdateModel(item);
            if (CartItems == null || CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Please Add item to cart");
                CartItems = new List<SalesDetails>();
                return;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    srep.Save(item, CartItems);
                    BindDropDown();
                    Session["CartItemSales"] = null;
                    CartItems = new List<SalesDetails>();
                    MsgLabel.Text = "Save Successfull";
                }
                catch (Exception ex)
                {
                    Session["CartItemSales"] = null;
                    MsgLabel.Text = ex.Message;
                }

            }
        }

        protected void storeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CartItemSales"] = null;
            CartItems = new List<SalesDetails>();
        }
    }
}