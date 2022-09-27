<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PosSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Simple Point of Sale System</h1>
        <p class="lead">Asp.Net -> web form technology are used in behind of this system</p>
        <p><a href="/pages/stores" class="btn btn-primary btn-lg">Get Started &raquo;</a></p>
    </div>

    <div class="d-md-flex">
        <div class="col-md-4">
            <h2>Stores</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="/pages/stores">go to page &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Brands</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="/pages/brands">Go to pages &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Categories</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="/pages/categories">go to page &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
