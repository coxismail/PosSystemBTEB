<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PosSystem.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

<div class="d-flex flex-column align-itmes-center justify-content-center" style="position:fixed; width:100%; height:100vh; background-color:dimgray; z-index:9999; top:0px;left:0px;">
   
    <div class="d-flex justify-content-center">
        <div style="width:350px;" class="bg-primary d-flex flex-column align-items-center justify-content-center">
            <h4 style="border-bottom: 4px double green; color:white; font-style:italic;">Login to POS</h4>
        </div>
        <div style="width:350px;">
     <div class="d-flex flex-column justify-content-center px-3" style=" width:350px; height:430px; position:relative; z-index:5; background-color:seagreen;">
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
        <div class="clearfix">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label text-light">Email</asp:Label>
            <div class="">
                <asp:TextBox runat="server" placeholder="Your eamil address" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="clearfix">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label text-light">Password</asp:Label>
            <div class="">
                <asp:TextBox runat="server" ID="Password" placeholder="password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="clearfix">
            <div class="">
                <div class="checkbox">
                    <asp:CheckBox runat="server" ID="RememberMe" />
                    <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                </div>
            </div>
        </div>
        <div class="clearfix">
            <div class="text-center">
                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
        </div>
    </div>
 
  </div>
</asp:Content>
