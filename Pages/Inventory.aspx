<%@ Page Title="Inventory" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="PosSystem.Pages.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="card">
        <div class="card-header">
            Inventory
        </div>
        <div class="card-body">
            <div class="d-flex justify-content-between">
                <div class="col-md-3 pl-0">
                    <asp:ListBox SelectionMode="Multiple" cssClass="form-control chosen-select" ID="storeDropDown" runat="server">
                    </asp:ListBox>
                </div>
                <div class="col-md-3">
                    <asp:ListBox cssClass="form-control chosen-select" SelectionMode="Multiple" ID="brandDropDown" runat="server">
                    </asp:ListBox>
                </div>
                <div class="col-md-3">
                    <asp:ListBox SelectionMode="Multiple"  cssClass="form-control chosen-select" ID="categoryDropDown" runat="server">
                    </asp:ListBox>
                </div>
                <div class="col-md-3  pr-0">
                    <asp:Button runat="server" Text="Load Data" ID="LoadGridData" OnClick="LoadGridData_Click" CssClass="btn btn-sm btn-primary" />
                </div>
            </div>

            <hr />
            
            <asp:GridView CssClass="table table-bordered" runat="server" ID="InventoryGridView" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="InventoryGridView_PageIndexChanging" AutoGenerateColumns="false" AutoGenerateEditButton="false">
                <Columns>
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
                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>
