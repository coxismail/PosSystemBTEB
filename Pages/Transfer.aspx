<%@ Page Title="Transfer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="PosSystem.Pages.Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            Stock Transfer to another Store
        </div>
        <div class="card-body">
            <asp:Label ID="MslLabel" CssClass="text-danger d-block mb-2" runat="server" Text=""></asp:Label>
            <div class="col-md-8 offset-md-2">
                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="fromStoreDropDown" OnSelectedIndexChanged="fromStoreDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="--Select Store--" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>

            <hr />
            <asp:GridView CssClass="table table-bordered" OnRowDeleting="trasferpageGridview_RowDeleting" runat="server" ID="trasferpageGridview" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="trasferpageGridview_PageIndexChanging" AutoGenerateColumns="false" AutoGenerateEditButton="false">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none">
                        <ItemTemplate>
                            <asp:TextBox CssClass="d-none" ID="pro_Id" runat="server" Text='<%#Eval("ProductId") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("ProductTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Brand">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Brand") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bal. Qty">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("BalanceQ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button data-toggle="modal" data-target="#transferModel" ID="btn_transfer" CssClass="btn btn-sm btn-info" runat="server" Text="Transfer" CommandName="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>






    <!-- Modal for Product -->
    <div class="modal fade <% =showModel %>" id="transferModel" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="transferModelLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:FormView ID="TransferView" runat="server" ValidateRequestMode="Enabled" ItemType="PosSystem.Models.TransferViewModel" DefaultMode="Insert" InsertMethod="TransferView_InsertItem">
                    <InsertItemTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title" id="productModalLabel">Transfer Product</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                            <asp:TextBox ID="fromstoreId" TextMode="Number" CssClass="d-none"  ReadOnly="true"  Text='<%#Bind("FromStoreId") %>' runat="server"></asp:TextBox>
                            <asp:TextBox ID="productId" TextMode="Number" CssClass="d-none"  ReadOnly="true" Text='<%#Bind("ProductId") %>' runat="server"></asp:TextBox>
                           <label class="control-label">Transfer To Store</label>
                            <asp:DropDownList ID="toStoreDropDown" CssClass="form-control mt-2" SelectedValue='<%#Bind("ToStoreId") %>' runat="server">
                                <asp:ListItem Text="--Select Store--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="control-label mt-2">Quantity</label>
                            <asp:TextBox ID="qtyTextBox" placeholder="quantity ex 10" TextMode="Number" CssClass="form-control" Text='<%#Bind("Qty") %>' runat="server"></asp:TextBox>

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
