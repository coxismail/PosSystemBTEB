<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="invoice.aspx.cs" Inherits="PosSystem.Pages.invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice View</title>
</head>

<body style="background-color: #00000078;">
    <form id="form1" runat="server">
        <div style="width: 17cm; height: 25cm; margin: 0px auto; background-color: white; box-shadow: 0px 0px 1px 1px white; padding: 2cm;">
            <asp:FormView runat="server" ItemType="PosSystem.Models.Sales" ID="invoiceView" SelectMethod="invoiceView_GetItem">

                <ItemTemplate>
                    <div style="clear:both;width:100%; margin-bottom:70px; text-align:right;">
                        <strong>Sales Invoice</strong> <br />
                        <label><%#:Item.SalesDate.ToString("dd-MM-yyyy ") %></label>
                    </div>
                    <div>
                       Customer: <strong><%#:Item.CustomerName  %></strong>
                    </div>
                    <div>
                        <label>Address: <%#:Item.Address  %></label>
                    </div>

                   <div>
                        <label>Mobile: <%#:Item.Mobile  %></label>
                   </div>

             
                    <div style="clear: both; margin-top: 30px;">
                         <div style="width:100%; border-bottom:1px dotted gray;"></div>
                        
                        <h5 style="text-align:center; clear:both; padding:2px 0px; margin:0px;"> Items Description</h5>
                       
                        <div style="width:100%; border-bottom:1px dotted gray;"></div>
                        <br />
                        <table style="width: 17cm" cellpadding="1" cellspacing="2" border="1">
                            <thead>
                                <tr>
                                    <th>TItle </th>
                                    <th>Qty</th>
                                    <th>Rate</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% foreach (var item in sales.Details)
                                    {  %>
                                <tr>
                                    <td><% =item.Product.Title %> </td>
                                    <td style="text-align: center"><% =item.Quantity %> </td>
                                    <td style="text-align: right"><% =item.Rate %> </td>
                                    <td style="text-align: right"><% =item.TotalAmount %> </td>
                                </tr>

                                <% 
                                    }

                                %>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="3">Total</td>
                                    <td style="text-align: right"><%=sales.Details.Sum(f=>f.TotalAmount) %></td>
                                </tr>
                            </tfoot>

                        </table>
                    </div>

                    <div>
                        <%#:Item.Notes %>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </form>

    <script>

        var print = window.print();
        // window.onafterprint = window.close();
        if (window.matchMedia) {
            var mediaQueryList = window.matchMedia('print');

            mediaQueryList.addListener(function (mql) {
                //alert($(mediaQueryList).html());
                if (mql.matches) {
                    console.log("before");
                } else {
                    window.close();
                }
            });
        }
    </script>
</body>
</html>
