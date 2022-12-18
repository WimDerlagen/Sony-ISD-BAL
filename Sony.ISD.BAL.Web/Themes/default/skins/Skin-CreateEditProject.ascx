<%@ Control Language="C#" %>

<table class="contentTable formTable">
    <tr>
        <td class="fCol">Project name</td>
        <td class="sCol"><asp:TextBox ID="ProjectName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Project description</td>
        <td><asp:TextBox ID="Description" runat="server" TextMode="MultiLine" CssClass="projdesc"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Project creator</td>
        <td><asp:Literal ID="Creator" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td>Status</td>
        <td><asp:Literal ID="Status" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td></td>
        <td><asp:Button ID="Save" runat="server" Text="Save" />&nbsp;&nbsp;
            <asp:Button ID="Cancel" runat="server" Text="Cancel" />
        </td>
    </tr>
</table>
