using PosSystem.Repository;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using PosSystem.Models;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class Stores : System.Web.UI.Page
    {
        private readonly StoreRepository srr = new StoreRepository();
        protected string showModel { get; set; }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var data = srr.List();
            storeGridView.DataSource = data;
            storeGridView.DataBind();
        }

        protected void storeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
           storeGridView. EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void storeGridView_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {

            errors.Text = "";
            TextBox name = storeGridView.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            Label Id = storeGridView.Rows[e.RowIndex].FindControl("labelId") as Label;
            TextBox address = storeGridView.Rows[e.RowIndex].FindControl("txt_Address") as TextBox;
            TextBox description = storeGridView.Rows[e.RowIndex].FindControl("txt_Description") as TextBox;
            try
            {
                var nr = new Store()
                {
                    Id = Convert.ToInt32(Id.Text),
                    Name = name.Text,
                    Address = address.Text,
                    Description = description.Text
                };
                if (!ModelValidator.IsValid(nr))
                {
                    errors.Text = string.Join("\n ", ModelValidator.Results.ToList());
                    return;
                }
                if (srr.IsExist(nr))
                {
                    storeGridView.EditIndex = -1;
                }
                else
                {
                    srr.Update(nr);
                    storeGridView.EditIndex = -1;
                }
            }
            catch (Exception ex)
            {
                errors.Text = ex.Message;
            }
            this.BindGrid();
        }

        protected void storeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            errors.Text = string.Empty;
            storeGridView.EditIndex = -1;
            BindGrid();
        }




        protected void storeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            storeGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        public void addStore_InsertItem()
        {
            showModel = "showtheModel";
            var item = new PosSystem.Models.Store();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                try
                {
                    if (srr.IsExist(item))
                    {
                        SaveMessage.Text = "Store Already Exist";
                    }
                    else
                    {
                        srr.Create(item);
                        SaveMessage.Text = "Successfully Saved";
                        showModel = "";
                    }

                    this.BindGrid();
                }
                catch (Exception ex)
                {
                    errors.Text = ex.Message;
                    this.BindGrid();
                }

            }
        }
    }
}