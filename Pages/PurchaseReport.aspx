<%@ Page Title="Purchase Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseReport.aspx.cs" Inherits="PosSystem.Pages.PurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-header">
            Purchase Report
        </div>
        <div class="card-body">

            <asp:GridView runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ID="purchaseReport"
                OnRowDeleting="purchaseReport_RowDeleting" AllowPaging="true" OnPageIndexChanging="purchaseReport_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                        <ItemTemplate>
                            <asp:Label ID="Idtextbox" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="50">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Net Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Delete" CssClass="btn btn-sm btn-primary" Text="Details"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
