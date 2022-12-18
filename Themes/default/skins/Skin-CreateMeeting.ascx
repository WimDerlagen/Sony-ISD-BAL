<%@ Control Language="C#" %>

<table class="contentTable formTable">
    <tr>
        <td class="fCol">Meeting name</td>
        <td class="sCol"><asp:TextBox ID="MeetingName" CssClass="textBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Meeting initiator</td>
        <td><asp:Literal ID="Creator" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td>Date</td>
        <td><BAL:DateBox ID="Date" runat="server"></BAL:DateBox></td>
    </tr>
    <tr>
        <td>From</td>
        <td><WebToolkit:TimeDropDown ID="FromTime" runat="server"></WebToolkit:TimeDropDown></td>
    </tr>
    <tr>
        <td>Till</td>
        <td><WebToolkit:TimeDropDown ID="TillTime" runat="server"></WebToolkit:TimeDropDown></td>
    </tr>
    <tr>
        <td></td>
        <td><asp:Button ID="Save" runat="server" Text="Save" />&nbsp;&nbsp;
            <asp:Button ID="Cancel" runat="server" Text="Cancel" />
        </td>
    </tr>
</table>

