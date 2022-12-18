<%@ Page Language="C#" MasterPageFile="~/Themes/default/masters/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Dashboard_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="Dashboard" runat="server">
        <asp:View ID="CurrentActivities" runat="server"></asp:View>
        
    </asp:MultiView>
</asp:Content>

