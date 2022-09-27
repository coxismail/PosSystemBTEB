using PosSystem.Repository;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using PosSystem.Models;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class Brands : System.Web.UI.Page
    {
        private readonly BrandRepository brr = new BrandRepository();
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
            var data = brr.List();
            brandGridView.DataSource = data;
            brandGridView.DataBind();
        }


        protected void brandGridView_PageIndexChanged(object sender, EventArgs e)
        {
            brandGridView.PageIndex = 1;
            this.BindGrid();
        }



        protected void brandGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            brandGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }




        protected void brandGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            errors.Text = "";
            TextBox name = brandGridView.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            Label Id = brandGridView.Rows[e.RowIndex].FindControl("labelId") as Label;
            try
            {
                var nr = new Brand()
                {
                    Id = Convert.ToInt32(Id.Text),
                    Name = name.Text
                };
                if (!ModelValidator.IsValid(nr))
                {
                    errors.Text = string.Join(",", ModelValidator.Results.ToList());
                    return;
                }
                if (brr.IsExist(nr))
                {
                    brandGridView.EditIndex = -1;
                    errors.Text = "Already Exist";
                }
                else
                {
                    brr.Update(nr);
                    brandGridView.EditIndex = -1;
                }
            }
            catch (Exception ex)
            {
                errors.Text = ex.Message;
            }
            this.BindGrid();
        }

        protected void brandGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            errors.Text = string.Empty;
            brandGridView.EditIndex = -1;
            this.BindGrid();
        }

    
        public void addBrands_InsertItem()
        {
            showModel = "showtheModel";
            var item = new PosSystem.Models.Brand();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                if (brr.IsExist(item))
                {
                    SaveMessage.Text = "Brands Already Exist";
                    showModel = "";
                }
                else
                {
                    brr.Create(item);
                    SaveMessage.Text = "Successfully Saved";
                    showModel = "";
                    BindGrid();
                }

            }
        }
    }
}