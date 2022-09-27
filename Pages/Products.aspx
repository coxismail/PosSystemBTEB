<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="PosSystem.Pages.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-header d-flex justify-content-between align-items-center">
            <div>Products</div>
            <div class="btn-group">
                <a href="/pages/Sales" class="btn btn-sm btn-success">Sales</a>
                <a href="/pages/Purchase" class="btn btn-sm btn-warning">Purchase</a>
                <button type="button" class="btn btn-sm btn-primary" data-toggle="modal"  data-target="#productModal">Add New</button>

            </div>
        </div>
        <div class="card-body">
            <asp:Label runat="server" CssClass="text-danger d-block" ID="errors"></asp:Label>
            <asp:Label CssClass="text-info d-block" runat="server" ID="SaveMessage"></asp:Label>
            <asp:GridView CssClass="table table-bordered table-hover" ShowHeaderWhenEmpty="true" ID="productGridView" AllowPaging="true" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="productGridView_PageIndexChanging" OnRowCancelingEdit="productGridView_RowCancelingEdit" OnRowEditing="productGridView_RowEditing" OnRowUpdating="productGridView_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="labelId" runat="server" Text='<% #Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Id" CssClass="form-control" runat="server" ReadOnly="true" Text='<% #Eval("Id") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="160px">
                        <ItemTemplate>
                            <asp:Label ID="labelTitle" runat="server" Text='<% #Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Title" CssClass="form-control" runat="server" Text='<% #Eval("Title") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Code" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="labelCode" runat="server" Text='<% #Eval("Code") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Code" TextMode="Number" CssClass="form-control" runat="server" Text='<% #Eval("Code") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:Label ID="labelPrice" runat="server" Text='<% #Eval("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Price" TextMode="Number" CssClass="form-control" runat="server" Text='<% #Eval("Price") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="labelCategory" runat="server" Text='<% #Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="drop_Category" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Brand" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="labelBrand" runat="server" Text='<% #Eval("BrandName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="drop_Brand" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="labelDescription" runat="server" Text='<% #Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Description" CssClass="form-control" runat="server" Text='<% #Eval("Description") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btn_Edit" CssClass="btn btn-sm btn-info" runat="server" Text="Edit" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btn_Update" CssClass="btn btn-sm btn-success" runat="server" Text="Update" CommandName="Update" />
                            <asp:Button ID="btn_Cancel" CssClass="btn btn-sm btn-danger" runat="server" Text="Cancel" CommandName="Cancel" />
                        </EditItemTemplate>


                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

            </asp:GridView>
        </div>
    </div>






    <!-- Modal for Product -->
    <div class="modal fade <% = showModel %>" id="productModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:FormView ID="addProduct" runat="server"  ItemType="PosSystem.Models.Product" DefaultMode="Insert" InsertMethod="addProduct_InsertItem">
                    <InsertItemTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title" id="productModalLabel">Add new Product</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                            <label class="control-label">Product Title</label>
                            <asp:TextBox ID="pro_Title" CssClass="form-control" Text='<%#Bind("Title") %>' runat="server"></asp:TextBox>
                            <label class="control-label">Category</label>

                            <asp:DropDownList ID="categoryDropDown" CssClass="form-control chosen-select mt-2" SelectedValue='<%#Bind("CategoryId") %>' runat="server">
                                <asp:ListItem Text="Select Category" Value=""></asp:ListItem>
                            </asp:DropDownList>
                              <label class="control-label">Brand</label>
                            <asp:DropDownList ID="brandDropDown" CssClass="form-control chosen-select mt-2" SelectedValue='<%#Bind("BrandId") %>' runat="server">
                                <asp:ListItem Text="Select Brand" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="control-label mt-2">Unit Price</label>
                            <asp:TextBox ID="pro_PriceTextbox" TextMode="Number" min="1.00" CssClass="form-control" Text='<%#Bind("UnitPrice") %>' runat="server"></asp:TextBox>
                            <label class="control-label mt-2">Description</label>
                            <asp:TextBox ID="pro_DescriptionBox" CssClass="form-control" Text='<%#Bind("Description") %>' runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button CssClass="btn btn-sm btn-primary" CommandName="Insert" runat="server" Text="Save" />
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </div>
    </div>
</asp:Content>
