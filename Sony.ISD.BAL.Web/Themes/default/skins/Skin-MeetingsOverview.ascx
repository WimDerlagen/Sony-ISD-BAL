<%@ Control Language="C#" %>

<asp:Button ID="New" runat="server" Text="New" />
<br /><br />
<WebToolkit:RepeaterPlusNone ID="Meetings" runat="server">
    <HeaderTemplate>
        <table class="contentTable meetingsTable">
            <tr>
                <th><asp:LinkButton ID="SortDate" runat="server" Text="Date"></asp:LinkButton></th>
                <th><asp:LinkButton ID="SortMeeting" runat="server" Text="Meeting"></asp:LinkButton></th>
                <th>Edit</th>
            </tr>
        
    </HeaderTemplate>
    
    <ItemTemplate>
        <tr>
            <td class="fCol"><asp:LinkButton ID="Date" runat="server"></asp:LinkButton></td>
            <td class="sCol"><asp:LinkButton ID="Meeting" runat="server"></asp:LinkButton></td>
            <td class="tCol"><asp:LinkButton ID="Edit" runat="server" Text="[edit]"></asp:LinkButton></td>
        </tr>
    </ItemTemplate>
    
    <AlternatingItemTemplate>
        <tr>
            <td class="fCol alt"><asp:LinkButton ID="Date" runat="server"></asp:LinkButton></td>        
            <td class="fCol alt"><asp:LinkButton ID="Meeting" runat="server"></asp:LinkButton></td>
            <td class="fCol alt"><asp:LinkButton ID="Edit" runat="server" Text="[edit]"></asp:LinkButton></td>
        </tr>
    </AlternatingItemTemplate>
    
    <FooterTemplate>
        </table>
    </FooterTemplate>
    
    <NoneTemplate>
        <tr>
            <td>There are no meetings</td>
        </tr>
    </NoneTemplate>
</WebToolkit:RepeaterPlusNone>

<WebToolkit:Pager ID="Pager" runat="server"></WebToolkit:Pager>
