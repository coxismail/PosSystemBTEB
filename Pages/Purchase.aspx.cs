using PosSystem.Models;
using PosSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{
    public partial class Puchase : System.Web.UI.Page
    {
        private readonly IPurchaseRepository purep;
        protected List<Models.PurchaseDetails> CartItems { get; set; }
        public Puchase()
        {
            purep = new PurchaseRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CartItems = Session["CartItems"] == null?  new List<Models.PurchaseDetails>(): Session["CartItems"] as List<Models.PurchaseDetails>;
                BindDropDown();
            }
            
        }

        private void BindDropDown()
        {
            var storeListData = purep.StoreList().ToArray();
            var productListData = purep.ProductsList().ToArray();
            var storeDropDown = (DropDownList)AddPurchaseRecord.Row.FindControl("storeDropDown");
            storeDropDown.Items.AddRange(storeListData);
            var productDropDown = (DropDownList)AddPurchaseRecord.Row.FindControl("ProductDropDownList");
            productDropDown.Items.AddRange(productListData);
        }



        public void AddPurchaseRecord_InsertItem()
        {
            CartItems = Session["CartItems"] as List<Models.PurchaseDetails>;
          //  var MsgLabel = (Label)AddPurchaseRecord.Row.FindControl("addactionMessage");
            var item = new PosSystem.Models.Purchase();
            TryUpdateModel(item);
            if (CartItems== null || CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Please Add item to cart");
                CartItems = new List<PurchaseDetails>();
                return;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    item.NetAmount = CartItems.Sum(f => f.Rate * f.Quantity);
                    purep.Save(item, CartItems);
                    MslLabel.Text = "Save Successfull";
                    BindDropDown();
                    Session["CartItems"] = null;
                    CartItems = new List<PurchaseDetails>();
                   
                }
                catch (Exception ex)
                {
                    Session["CartItems"] = null;
                    MslLabel.Text = ex.Message;
                }
            }
        }

        protected void AddtoCardBtn_Click(object sender, EventArgs e)
        {
            CartItems = Session["CartItems"] as List<PurchaseDetails>;
            if (CartItems == null)
            {
                CartItems = new List<PurchaseDetails>();
            }

            var MsgLabel = (Label)AddPurchaseRecord.Row.FindControl("addactionMessage");
            MsgLabel.Text = "";
           var storeDropDown = (DropDownList)AddPurchaseRecord.Row.FindControl("storeDropDown");
            var prodropdown = (DropDownList)AddPurchaseRecord.Row.FindControl("ProductDropDownList");
            var qty = (TextBox)AddPurchaseRecord.Row.FindControl("QuantityTextBox");
            var rate = (TextBox)AddPurchaseRecord.Row.FindControl("RateTextBox");
         //   var storeId = storeDropDown.SelectedValue;
            var prodId = prodropdown.SelectedValue;


            if (prodId == null || prodId == "")
            {
                MsgLabel.Text = "Please Select Product";
                return;
            }
            if (qty.Text == null || rate.Text == null || qty.Text == "" || rate.Text == "")
            {
                MsgLabel.Text = "Rate and Quantity can't be empty";
                return;
            }
            try
            {
                if (Convert.ToInt32(qty.Text) <= 0)
                {
                    MsgLabel.Text = "Quantity must be greater than 0";
                    return;
                }
                var prod = purep.Product(Convert.ToInt32(prodId));
                var details = new Models.PurchaseDetails()
                {
                    Product = prod,
                    Quantity = Convert.ToInt32(qty.Text),
                    Rate = Convert.ToInt32(rate.Text)
                };
                CartItems.Add(details);
                Session["CartItems"] = CartItems;
            }
            catch (Exception ex)
            {
                MsgLabel.Text = ex.Message;
                return;
            }

        }

        protected void clearAllfromCart_Click(object sender, EventArgs e)
        {
            Session["CartItems"] = null;
            CartItems = new List<PurchaseDetails>();
        }
    }
}