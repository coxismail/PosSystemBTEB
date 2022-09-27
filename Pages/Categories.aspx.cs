using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PosSystem.Repository;
using PosSystem.Models;
using System.Security.Permissions;

namespace PosSystem.Pages
{
    public partial class Categories : System.Web.UI.Page
    {
    private readonly  CategoryRepository crr = new CategoryRepository();
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
            var data = crr.List();
            categoryGridView.DataSource = data;
            categoryGridView.DataBind();
        }

        protected void categoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            categoryGridView.PageIndex = e.NewPageIndex;
            categoryGridView.DataBind();
        }

        protected void categoryGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
          categoryGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void categoryGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            errors.Text = string.Empty;
            categoryGridView.EditIndex = -1;
            BindGrid();
        }

        protected void categoryGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            errors.Text = "";
            TextBox name = categoryGridView.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            Label Id = categoryGridView.Rows[e.RowIndex].FindControl("labelId") as Label;

            try
            {
                var nr = new Category()
                {
                    Id = Convert.ToInt32(Id.Text),
                    Name = name.Text
                };
                if (!ModelValidator.IsValid(nr))
                {
                    errors.Text = string.Join(",", ModelValidator.Results.ToList());
                    return;
                }
                if (crr.IsExist(nr))
                {
                    categoryGridView.EditIndex = -1;
                }
                else
                {
                    crr.Update(nr);
                    categoryGridView.EditIndex = -1;
                }
            }
            catch (Exception ex)
            {
                errors.Text = ex.Message;
            }
            this.BindGrid();

        }


        public void addCategory_InsertItem()
        {
            showModel = "showtheModel";
            var item = new PosSystem.Models.Category();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                try
                {
                    if (crr.IsExist(item))
                    {
                        SaveMessage.Text = "Category Already Exist";
                        showModel = "";
                    }
                    else
                    {
                        crr.Create(item);
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