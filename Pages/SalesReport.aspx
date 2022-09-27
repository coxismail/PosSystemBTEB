<%@ Page Title="Sales Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesReport.aspx.cs" Inherits="PosSystem.Pages.SalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% int i = 0; %>
    <div class="card">
        <div class="card-header">
            Sales Report
        </div>
        <div class="card-body">
            <table class="table table-bordered table-hover" width="100%">
                <thead>
                    <tr>
                        <th>Sl</th>
                        <th>Customer</th>
                        <th>Mobile</th>
                        <th>Date</th>
                        <th>T. Amount</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var item in salesReportdata)
                        {
                            i += 1;
                    %>
                    <tr>
                        <td><% =i %></td>
                        <td><% =item.CustomerName %></td>
                        <td><% =item.Mobile %></td>
                        <td><% =item.SalesDate.ToString("dd-MM-yyyy") %></td>
                        <td><% =item.Details.Sum(f=>f.TotalAmount) %></td>
                        <td>
                            <a href="/pages/invoice.aspx?id=<% =item.Id %>" target="_blank" class="btn btn-sm btn-primary">Print</a>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
