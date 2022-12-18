<%@ Page Language="C#" MasterPageFile="~/Themes/default/masters/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Meetings_Overview_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <BAL:BreadCrumb ID="Crumbs" runat="server">
    </BAL:BreadCrumb>
    <br />
    <BAL:MeetingsOverview ID="Meetings" runat="server"></BAL:MeetingsOverview>
</asp:Content>

