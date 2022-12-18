<%@ Control Language="C#" %>

<table cellpadding="0" cellspacing="0" class="attendeeTable">
    <tr>
        <td>Present</td>
        <td>Name</td>
        <td>Initials</td>
        <td>Department</td>
        <td>Phone</td>
    </tr>
    <tr>
        <td><asp:CheckBox ID="Present" runat="server" /></td>
        <td><asp:Literal ID="Name" runat="server"></asp:Literal></td>
        <td><asp:Literal ID="Initials" runat="server"></asp:Literal></td>
        <td><asp:Literal id="Department" runat="Server"></asp:Literal></td>
        <td><asp:Literal ID="Phone" runat="Server"></asp:Literal></td>
    </tr>
</table>

<BAL:DomainPicker ID="Piocker" runat="server"></BAL:DomainPicker>