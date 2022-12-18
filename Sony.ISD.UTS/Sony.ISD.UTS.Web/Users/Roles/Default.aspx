<%@ Page Language="C#" MasterPageFile="~/Themes/default/masters/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Users_Roles_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <Authentication:ManageRoles ID="Roles" runat="server"></Authentication:ManageRoles>
</asp:Content>

