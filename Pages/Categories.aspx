<%@ Page Title="Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="PosSystem.Pages.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-header d-flex justify-content-between">
            <div>Categories</div>
            <button type="button" class="btn btn-sm btn-primary" data-toggle="modal" data-target="#categoryModal">Add New</button>
        </div>
        <div class="card-body">
            <asp:Label runat="server" CssClass="text-danger" ID="errors"></asp:Label>
            <br />
            <asp:Label CssClass="text-info" runat="server" ID="SaveMessage"></asp:Label>
            <asp:GridView CssClass="table table-bordered table-hover" ShowHeaderWhenEmpty="true" ID="categoryGridView" AllowPaging="true" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="categoryGridView_PageIndexChanging" OnRowCancelingEdit="categoryGridView_RowCancelingEdit" OnRowEditing="categoryGridView_RowEditing" OnRowUpdating="categoryGridView_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="labelId" runat="server" Text='<% #Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="labelName" runat="server" Text='<% #Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Name" CssClass="form-control" runat="server" Text='<% #Eval("Name") %>'></asp:TextBox>
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
    <!-- Modal -->
    <div class="modal fade <% = showModel %>" id="categoryModal" tabindex="-1" role="dialog" aria-labelledby="categoryModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:FormView ID="addCategory" runat="server" ItemType="PosSystem.Models.Category" DefaultMode="Insert" InsertMethod="addCategory_InsertItem">
                    <InsertItemTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title" id="categoryModalLabel">Add new Category</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                            <label for="newCategory" class="control-label">Category Name :</label>
                            <asp:TextBox ID="newCategory" CssClass="form-control" Text="<% #Bind('Name') %>" runat="server"></asp:TextBox>
                           

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
