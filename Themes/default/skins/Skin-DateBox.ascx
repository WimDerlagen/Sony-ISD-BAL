<%@ Control Language="C#" %>
<asp:TextBox ID="Date" runat="server"></asp:TextBox>
<asp:RegularExpressionValidator ID="DateValidator" ErrorMessage="Datum formaat is niet correct.Gebruik het formaat dd/mm/yyyy" runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="Date" Display="None"></asp:RegularExpressionValidator>
<ajax:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="Date" runat="server" Mask="99/99/9999"></ajax:MaskedEditExtender>
<ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="DateValidator" Width="250px"></ajax:ValidatorCalloutExtender>
