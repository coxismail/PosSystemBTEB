using PosSystem.Models;
using PosSystem.Repository;
using System;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace PosSystem.Pages
{
    public partial class Products : System.Web.UI.Page
    {
        private readonly IProductRepository productRepository;
        protected string showModel { get; set; }
        public Products()
        {
           productRepository = new ProductsRepository();
        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                showModel = "";
                BindDropDown();
            }
        }

        private void BindGrid()
        {
            var data = productRepository.List();
            productGridView.DataSource = data;
            productGridView.DataBind();
            SaveMessage.Text = string.Empty;
        }


        protected void productGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            productGridView.EditIndex = e.NewEditIndex;
           
            BindGrid();
           TextBox ProId = productGridView.Rows[e.NewEditIndex].FindControl("txt_Id") as TextBox;
            BindEditedDropDown(Convert.ToInt32(ProId.Text), e.NewEditIndex);

        }
        protected void productGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList br = productGridView.Rows[e.RowIndex].FindControl("drop_Brand") as DropDownList;
            DropDownList cr = productGridView.Rows[e.RowIndex].FindControl("drop_Category") as DropDownList;
            TextBox txt_Title = productGridView.Rows[e.RowIndex].FindControl("txt_Title") as TextBox;
            TextBox code = productGridView.Rows[e.RowIndex].FindControl("txt_Code") as TextBox;
            TextBox description = productGridView.Rows[e.RowIndex].FindControl("txt_Description") as TextBox;
            TextBox price = productGridView.Rows[e.RowIndex].FindControl("txt_Price") as TextBox;
            TextBox id = productGridView.Rows[e.RowIndex].FindControl("txt_Id") as TextBox;
            var UpdateProductModel = new Product()
            {
                BrandId = Convert.ToInt32(br.SelectedValue),
                CategoryId = Convert.ToInt32(cr.SelectedValue),
                Code = Convert.ToInt32(code.Text),
                Id = Convert.ToInt32(id.Text),
                Description = description.Text,
                Title = txt_Title.Text,
                UnitPrice = Convert.ToInt32(price.Text),
            };
            if (!ModelValidator.IsValid(UpdateProductModel))
            {
                errors.Text = string.Join(",", ModelValidator.Results.ToList());
                return;
            }
            try
            {
                if (productRepository.IsExist(UpdateProductModel))
                {
                    errors.Text = "Product Already Exist";
                }
                else
                {
                    productRepository.Update(UpdateProductModel);
                    SaveMessage.Text = "Successfully Updated";
                    productGridView.EditIndex = -1;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                errors.Text = ex.Message;
            }
        }
        protected void productGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void productGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            productGridView.EditIndex = -1;
            BindGrid();
        }

        private void BindDropDown()
        {
            var brlist = productRepository.BrandList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
           var brdro= (DropDownList)addProduct.Row.FindControl("brandDropDown");
            brdro.Items.AddRange( brlist);

            var catelist = productRepository.CategoryList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
            var catdrop = (DropDownList)addProduct.Row.FindControl("categoryDropDown");
            catdrop.Items.AddRange(catelist);
        }
        private void BindEditedDropDown(int Id, int rowIndex)
        {
            var pro = productRepository.GetSingle( Id);
            var brlist = productRepository.BrandList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();
            DropDownList br = productGridView.Rows[rowIndex].FindControl("drop_Brand") as DropDownList;
            DropDownList cr = productGridView.Rows[rowIndex].FindControl("drop_Category") as DropDownList;
            br.Items.AddRange(brlist);
            br.SelectedValue = pro.BrandId.ToString();
            var catelist = productRepository.CategoryList().Select(f => new ListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            }).ToArray();

            cr.Items.AddRange(catelist);
            cr.SelectedValue = pro.CategoryId.ToString();
        }

        public void addProduct_InsertItem()
        {
            showModel = "showtheModel";
            var item = new PosSystem.Models.Product();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                try
                {
                    if (productRepository.IsExist(item))
                    {
                        errors.Text = "Already Exist";
                      
                    }
                    else
                    {
                        productRepository.Create(item);
                        SaveMessage.Text = "Save Successfull";
                        BindGrid();
                        BindDropDown();
                        showModel = "";
                        
                    }
                }
                catch (Exception ex)
                {
                    errors.Text = ex.Message;
                }

            }
        }
    }
}