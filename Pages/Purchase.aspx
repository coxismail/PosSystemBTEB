<%@ Page Title="Purchase Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="PosSystem.Pages.Puchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header d-flex justify-content-between">
            <div>
                Purchase Entry
           
            </div>
            <a href="/pages/PurchaseReport" class="btn btn-sm btn-defulat">Reports</a>
        </div>
        <div class="card-body">
            <asp:Label ID="MslLabel" Text="" CssClass="text-info d-block" runat="server"></asp:Label>
            <asp:FormView ID="AddPurchaseRecord" ValidateRequestMode="Enabled" InsertMethod="AddPurchaseRecord_InsertItem" DefaultMode="Insert" runat="server" ItemType="PosSystem.Models.Purchase">

                <InsertItemTemplate>
                    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                    <div>
                        <%--<asp:DynamicEntity runat="server" Mode="Insert"></asp:DynamicEntity>--%>
                        <div class="d-md-flex justify-content-md-between">
                            <div class="col-md-4 pl-md-0">
                                <asp:Label runat="server" CssClass="control-label">Supplier Name</asp:Label>
                                <asp:TextBox ID="TextBox1" CssClass="form-control input-sm" runat="server" Text='<%#Bind("Supplier") %>'></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <asp:Label runat="server" CssClass="control-label">Date</asp:Label>
                                <asp:TextBox ID="TextBox3" CssClass="form-control input-sm" TextMode="Date" runat="server" Text='<%#Bind("PurchaseDate") %>'></asp:TextBox>
                            </div>
                            <div class="col-md-4"></div>

                        </div>
                        <div class="d-md-flex justify-content-md-between">
                            <div class="col-md-4 pl-md-0">
                                <asp:Label runat="server" CssClass="control-label">Address</asp:Label>
                                <asp:TextBox ID="TextBox2" CssClass="form-control input-sm" runat="server" Text='<%#Bind("Address") %>'></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label runat="server" CssClass="control-label">Store</asp:Label>
                                <asp:DropDownList ID="storeDropDown" CausesValidation="true" CssClass="form-control input-sm chosen-select" SelectedValue='<%#Bind("StoreId") %>' runat="server">
                                    <asp:ListItem Text="--Select Store--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 pr-md-0">
                            </div>

                        </div>
                        <div class="clearifx col-md-8 pl-md-0">
                            <asp:Label runat="server" CssClass="control-label">Notes</asp:Label>
                            <asp:TextBox ID="TextBox4" TextMode="MultiLine" CssClass="form-control input-sm" runat="server" Text='<%#Bind("Notes") %>'></asp:TextBox>
                        </div>
                        <div class="clearfix pb-2">
                            <div class="col-md-8 shadow">
                                <hr />
                                <div class="my-2">
                                    <div class="d-flex justify-content-between align-items-end">
                                        <div class="col-md-6 pl-md-0">
                                            <asp:Label runat="server" CssClass="control-label">Product</asp:Label>
                                            <asp:DropDownList ID="ProductDropDownList" CssClass="chosen-select form-control input-sm" AutoPostBack="false" runat="server">
                                                <asp:ListItem Text="--Select Product--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" CssClass="control-label">Quantity</asp:Label>
                                            <asp:TextBox ID="QuantityTextBox" min="1" TextMode="Number" CssClass="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" CssClass="control-label">Rate</asp:Label>
                                            <asp:TextBox ID="RateTextBox" min="1" TextMode="Number" CssClass="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 pr-md-0 text-right">
                                            <asp:Button ID="AddtoCardBtn" OnClick="AddtoCardBtn_Click" runat="server" Text="Add to Cart" CssClass="btn btn-sm btn-success py-2" />
                                        </div>
                                    </div>
                                    <asp:Label ID="addactionMessage" CssClass="text-danger" Text="" runat="server"></asp:Label>
                                </div>
                                <div class="card">
                                    <div class="card-header">
                                        Cart
                                   
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-bordered" width="100%">
                                            <tbody>
                                                <%  foreach (var item in CartItems)
                                                    { %>
                                                <tr>
                                                    <td><% =item.Product.Title %> </td>
                                                    <td>Rate: <% =item.Rate %> </td>
                                                    <td>Qty: <% =item.Quantity %></td>
                                                    <td>Total: <% =item.Rate*item.Quantity %></td>
                                                </tr>
                                                <% } %>
                                            </tbody>

                                        </table>
                                        <div class="clearfix text-right">
                                            <asp:Button runat="server" ID="clearAllfromCart" CssClass="btn btn-sm btn-outline-primary" OnClick="clearAllfromCart_Click" Text="clear all" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-8 pr-md-0 text-right mt-3">
                                <asp:Button runat="server" CommandName="Insert" Text="Save Purchase Record" CssClass="btn btn-sm btn-primary" />
                            </div>
                        </div>



                    </div>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
