<%@ Control Language="C#" %>

<div id="createMeeting">

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
        <td>Subject</td>
        <td><asp:TextBox ID="Subject" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Location</td>
        <td><asp:TextBox ID="Location" runat="server"></asp:TextBox></td>
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

<table class="recurranceTable formTable" runat="server" visible="false">
    <tr>
        <td>Occurs</td>
        <td><asp:RadioButtonList ID="Occurance" runat="server">
            <asp:ListItem ID="Once" Text="Once" Value="Once" Selected="True"></asp:ListItem>
            <asp:ListItem ID="Recurring" Text="Recurring" Value="Recurring"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr id="RowPattern" runat="server">
        <td colspan="2">
            <table cellpadding="0" cellspacing="0">
                <tr>    
                    <td>
                        <asp:RadioButtonList ID="RecursionInterval" runat="server">
                            <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                            <asp:ListItem Text="Weekly" Value="Weekly" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                            <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table id="DayRecursion" runat="server" visible="false" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:RadioButton ID="EveryXDay" runat="server" /> Every <asp:TextBox ID="EveryXDayValue" runat="server"></asp:TextBox>
                    <asp:RadioButton ID="EveryWeekDay" runat="server" Text="Every week day" />
                </td>
            </tr>
            </table>
            <table id="WeekRecursion" runat="server"  visible="false" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Recur every <asp:TextBox ID="RecusionCount" runat="server"></asp:TextBox> week(s) on:<br />
                    <asp:CheckBox ID="Monday" runat="server" Text="Monday" />&nbsp;
                    <asp:CheckBox ID="TuesDay" runat="server" Text="TuesDay" />&nbsp;
                    <asp:CheckBox ID="Wednesday" runat="server" Text="Wednesday" />&nbsp;
                    <asp:CheckBox ID="Thursday" runat="server" Text="Thursday" />&nbsp;
                    <asp:CheckBox ID="Friday" runat="server" Text="Friday" />&nbsp;
                    <asp:CheckBox ID="Saturday" runat="server" Text="Saturday" />&nbsp;
                    <asp:CheckBox ID="Sunday" runat="server" Text="Sunday" />&nbsp;
                </td>
            </tr>
            </table>
            <table id="MonthRecursion" runat="server"  visible="false" cellpadding="0" cellspacing="0">
            <tr>
                <td></td>
            </tr>
            </table>
            
            <table id="YearRecursion" runat="server"  visible="false" cellpadding="0" cellspacing="0">
            <tr>
                <td></td>
            </tr>
            </table>
            
            
        </td>
    </tr>
</table>

</div>

