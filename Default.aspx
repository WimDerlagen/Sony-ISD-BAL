<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            &nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal><br />
            &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>&nbsp;<br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Fill Dropdown" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Send Invite" />
            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Simple">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
