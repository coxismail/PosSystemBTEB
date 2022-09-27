using PosSystem.Models;
using PosSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{

    public partial class Inventory : System.Web.UI.Page
    {
        private readonly IInentoryRepository inrep;
        public Inventory()
        {
            inrep = new InventoryRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                ViewState["InventoryData"] = null;
                BindGrid(inrep.Balance());
                BindDropDown();
            }
        }

        private void BindDropDown()
        {
            var brlist = inrep.BrandList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
            
           brandDropDown.Items.AddRange(brlist);

            var catelist = inrep.CategoryList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
           categoryDropDown.Items.AddRange(catelist);

            var stores = inrep.StoreList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
            storeDropDown.Items.AddRange(stores);
        }

        private void BindGrid(IList<InventoryViewModel> data)
        {
            ViewState["InventoryData"] = data;
            InventoryGridView.DataSource = data;
            InventoryGridView.DataBind();
        }

        protected void InventoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InventoryGridView.PageIndex = e.NewPageIndex;
            BindGrid(ViewState["InventoryData"] as IList<InventoryViewModel>);
        }



        protected void LoadGridData_Click(object sender, EventArgs e)
        {
            List<int> cats = null;
            List<int> store = null;
            List<int> brands = null;
            var stores = storeDropDown.Items;
            var categs = categoryDropDown.Items ;
            var brans = brandDropDown.Items;
            foreach (ListItem item in categs)
            {
                if (item.Selected == true)
                {
                    if (cats == null)
                    {
                        cats = new List<int>();
                    }
                    cats.Add(Convert.ToInt32(item.Value));
                }
            }

            foreach (ListItem item in stores)
            {
                if (item.Selected == true)
                {
                    if (store == null)
                    {
                        store = new List<int>();
                    }
                    store.Add(Convert.ToInt32(item.Value));
                }
            }
            foreach (ListItem item in brans)
            {
                if (item.Selected == true)
                {
                    if (brands == null)
                    {
                        brands = new List<int>();
                    }
                    brands.Add(Convert.ToInt32(item.Value));
                }
            }
            LoadInventoryData(store, brands, cats);
        }

        private void LoadInventoryData(List<int> store, List<int> brands, List<int> cats)
        {
            var data = inrep.Balance();

             if (store != null && brands != null && cats != null)
            {
                 data = inrep.BalanceByAll(store,brands, cats);
            }

            else if (store != null && brands == null && cats == null)
            {
                data = inrep.BalanceByStore(store);
            }
            else if (store == null && brands != null && cats == null)
            {
                data = inrep.BalancebyBrand(brands);
            }
            else if (store == null && brands == null && cats != null)
            {
                data = inrep.BalanceByCategory(cats);
            }

            else if (store != null && brands != null && cats == null)
            {
                data = inrep.BalanceByStoreandBrand(store, brands);
            }
            else if (store != null && brands == null && cats != null)
            {
                data = inrep.BalanceByStoreandCat(store, cats);
            }
            else if (store == null && brands != null && cats != null)
            {
                data = inrep.BalanceByBrandandCategory(brands, cats);
            }
            else
            {
                data = inrep.Balance();
            }
            ViewState["InventoryData"] = data;
            BindGrid(data);
        }
    }
}