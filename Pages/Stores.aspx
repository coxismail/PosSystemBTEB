<%@ Page Title="Store" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stores.aspx.cs" Inherits="PosSystem.Pages.Stores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header d-flex justify-content-between">
            <div>Stores</div>
            <button type="button" class="btn btn-sm btn-primary" data-toggle="modal" data-target="#storedModal">Add New</button>
        </div>
        <div class="card-body">
            <asp:Label runat="server" CssClass="text-danger d-block" ID="errors"></asp:Label>
            <asp:Label CssClass="text-info" runat="server" ID="SaveMessage"></asp:Label>
            <asp:GridView CssClass="table table-bordered table-hover" ShowHeaderWhenEmpty="true" ID="storeGridView" AllowPaging="true" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="storeGridView_RowCancelingEdit" OnRowEditing="storeGridView_RowEditing" OnRowUpdating="storeGridView_RowUpdated" OnPageIndexChanging="storeGridView_PageIndexChanging">
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

                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label ID="labelAddress" runat="server" Text='<% #Eval("Address") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Address" CssClass="form-control" runat="server" Text='<% #Eval("Address") %>'></asp:TextBox>
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
    <!-- Modal -->
    <div class="modal fade <% = showModel %>" id="storedModal" tabindex="-1" role="dialog" aria-labelledby="storedModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">

            <div class="modal-content">
                <asp:FormView ID="addStore" runat="server" ItemType="PosSystem.Models.Store" DefaultMode="Insert" InsertMethod="addStore_InsertItem">
                    <InsertItemTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title" id="storedModalLabel">Add new Store</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                               <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                            <label for="newStore" class="control-label">Store Name :</label>
                            <asp:TextBox ID="newStore" CssClass="form-control" Text="<% #Bind('Name') %>" runat="server"></asp:TextBox>

                            <label for="storeAddress" class="control-label">Address :</label>
                            <asp:TextBox ID="storeAddress" CssClass="form-control" Text='<% #Bind("Address") %>' runat="server"></asp:TextBox>

                            <label for="storeDescription" class="control-label">Description :</label>
                            <asp:TextBox ID="storeDescription" CssClass="form-control" Text="<% #Bind('Description') %>" runat="server"></asp:TextBox>
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
