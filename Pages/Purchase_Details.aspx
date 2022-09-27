<%@ Page Title="Purchase Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchase_Details.aspx.cs" Inherits="PosSystem.Pages.Purchase_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header d-flex justify-content-between">
          <div>  Details</div>
            <a href="PurchaseReport.aspx" class="btn btn-sm btn-default">Back to reports</a>
        </div>
        <div class="card-body">


            <asp:FormView runat="server" ItemType="PosSystem.Models.Purchase" ID="PurchaseView" SelectMethod="PurchaseView_GetItem">

                <ItemTemplate>
                    <div>
                        <label class="d-block">Date : <strong><%#:Item.PurchaseDate.ToString("dd-MM-yyyy")  %></strong></label>
                        <label class="d-block">Supplier: <strong><%#:Item.Supplier  %></label></strong>

                        <label class="d-block">Address: <strong><%#:Item.Address  %></strong></label>

                        <label class="d-block">Net Amount : <strong><%#:Item.NetAmount  %></strong></label>
                    </div>

                    <hr />

                    <div style="clear: both;">
                        <div class="col-md-7">
                            <h5 class="text-center">Items Details</h5>
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="text-center">
                                        <th>TItle </th>
                                        <th>Qty</th>
                                        <th>Rate</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% foreach (var item in purchase.Details)
                                        {  %>
                                    <tr>
                                        <td><% =item.Product.Title %> </td>
                                        <td style="text-align: center"><% =item.Quantity %> </td>
                                        <td style="text-align: right"><% =item.Rate %> </td>
                                        <td style="text-align: right"><% =item.Quantity*item.Rate %> </td>
                                    </tr>

                                    <% 
                                        }

                                    %>
                                </tbody>


                            </table>
                        </div>
                    </div>

                    <div class="clearfix text-center">
                        <%#:Item.Notes %>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
