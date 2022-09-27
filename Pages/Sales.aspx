<%@ Page Title="Sales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="PosSystem.Pages.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header d-flex justify-content-between">
            <div>Sales Entry</div>
            <a href="SalesReport.aspx" class="btn btn-sm btn-default">Report</a>
        </div>
        <div class="card-body">
            <asp:FormView ID="SalesFormView" InsertMethod="SalesFormView_InsertItem" runat="server" ItemType="PosSystem.Models.Sales" ValidateRequestMode="Enabled" DefaultMode="Insert">
                <InsertItemTemplate>
                    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                    <div>
                        <%--<asp:DynamicEntity runat="server" Mode="Insert"></asp:DynamicEntity>--%>
                        <div class="d-md-flex justify-content-md-start">
                            <div class="col-md-4 pl-md-0">
                                <asp:Label runat="server" CssClass="control-label">Customer Name</asp:Label>
                                <asp:TextBox ID="TextBox1" placeholder="Customer name" CssClass="form-control" runat="server" Text='<%#Bind("CustomerName") %>'></asp:TextBox>

                                <div class="my-2">
                                    <asp:Label runat="server" CssClass="control-label mt-2">Mobile</asp:Label>
                                    <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" Text='<%#Bind("Mobile") %>'></asp:TextBox>
                                </div>
                                <asp:Label runat="server" CssClass="control-label">Address</asp:Label>
                                <asp:TextBox ID="TextBox2" placeholder="Address" CssClass="form-control" runat="server" Text='<%#Bind("Address") %>'></asp:TextBox>

                            </div>

                            <div class="col-md-4 pr-md-0">

                                <asp:Label runat="server" CssClass="control-label">Date</asp:Label>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" TextMode="Date" runat="server" Text='<%#Bind("SalesDate") %>'></asp:TextBox>


                                <asp:Label runat="server" CssClass="control-label">Store</asp:Label>
                                <asp:DropDownList ID="storeDropDown" AutoPostBack="true" OnSelectedIndexChanged="storeDropDown_SelectedIndexChanged" CausesValidation="true" CssClass="form-control chosen-select" SelectedValue='<%#Bind("StoreId") %>' runat="server">
                                    <asp:ListItem Text="--Select Store--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label runat="server" CssClass="control-label">Notes</asp:Label>
                                <asp:TextBox ID="TextBox4" TextMode="MultiLine" CssClass="form-control" runat="server" Text='<%#Bind("Notes") %>'></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="clearfix pb-2">
                        <div class="col-md-8 shadow">
                            <hr />
                            <div class="my-2">
                                <div class="d-flex justify-content-between align-items-end">
                                    <div class="col-md-6 pl-md-0">
                                        <asp:Label runat="server" CssClass="control-label">Product</asp:Label>
                                        <asp:DropDownList ID="ProductDropDownList" CssClass="chosen-select form-control" AutoPostBack="false" runat="server">
                                            <asp:ListItem Text="--Select Product--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass="control-label">Quantity</asp:Label>
                                        <asp:TextBox ID="QuantityTextBox" min="1" max="100000" TextMode="Number" CssClass="form-control" runat="server" Text=''></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass="control-label">Rate</asp:Label>
                                        <asp:TextBox ID="RateTextBox" min="1" TextMode="Number" CssClass="form-control" runat="server" Text=''></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 pr-md-0 text-right">
                                        <asp:Button ID="AddtoCardBtn" OnClick="AddtoCardBtn_Click" runat="server" Text="Add to Cart" CssClass="btn btn-sm btn-success py-1" />
                                    </div>
                                </div>
                                <asp:Label ID="addactionMessage" CssClass="text-danger" Text="" runat="server"></asp:Label>
                            </div>
                            <div class="card pt-2">
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
                          <div class="col-md-8 text-right mt-3 pr-md-0">
                                <asp:Button runat="server" CommandName="Insert" Text="Save Sales Record" CssClass="btn btn-sm btn-primary" />
                            </div>
                    </div>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
