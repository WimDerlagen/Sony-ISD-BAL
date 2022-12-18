<%@ Control Language="C#" %>
<asp:UpdatePanel ID="PickerUpdater" runat="server">
<ContentTemplate>
    <asp:TextBox ID="Name" runat="server"></asp:TextBox>
    <asp:Button ID="Validate" runat="server" Text="Validate" />
    <br />
    <asp:DropDownList ID="Names" runat="server"></asp:DropDownList>
</ContentTemplate>
</asp:UpdatePanel>
