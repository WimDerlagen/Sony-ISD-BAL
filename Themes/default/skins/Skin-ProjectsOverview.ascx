<%@ Control Language="C#" %>

<asp:Button ID="New" runat="server" Text="New" />
<br /><br />
<WebToolkit:RepeaterPlusNone ID="Projects" runat="server">
    <HeaderTemplate>
        <table class="contentTable projectsTable">
            <tr>
                <th>Project</th>
                <th>Edit</th>
            </tr>
        
    </HeaderTemplate>
    
    <ItemTemplate>
        <tr>
            <td class="fCol"><asp:LinkButton ID="Project" runat="server"></asp:LinkButton></td>
            <td class="sCol"><asp:LinkButton ID="Edit" runat="server" Text="[edit]"></asp:LinkButton></td>
        </tr>
    </ItemTemplate>
    
    <AlternatingItemTemplate>
        <tr>
            <td><asp:LinkButton ID="Project" runat="server"></asp:LinkButton></td>
            <td><asp:LinkButton ID="Edit" runat="server" Text="[edit]"></asp:LinkButton></td>
        </tr>
    </AlternatingItemTemplate>
    
    <FooterTemplate>
        </table>
    </FooterTemplate>
    
    <NoneTemplate>
        <tr>
            <td>There are no projects</td>
        </tr>
    </NoneTemplate>
</WebToolkit:RepeaterPlusNone>

<WebToolkit:Pager ID="Pager" runat="server"></WebToolkit:Pager>
