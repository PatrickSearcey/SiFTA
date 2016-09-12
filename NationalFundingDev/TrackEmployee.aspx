<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackEmployee.aspx.cs" Inherits="NationalFundingDev.TrackEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager runat="server" ID="rsm">
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel runat="server" ID="rap">
        <table>
            <tr>
                <td>
                    <b>Employee ID:</b>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="rtbID" EmptyMessage="Example: jdoe" Width="250px" />
                </td>
                <td>
                    <telerik:RadButton runat="server" ID="rbValidate" Text="Validate" OnClick="rbValidate_Click" />
                </td>
            </tr>
        </table>
            <asp:Panel runat="server" ID="pnlInfo" Visible="false">
                <asp:Literal runat="server" ID="ltlID" /> <asp:Literal runat="server" ID="ltlName" />  <telerik:RadButton runat="server" ID="btnTrack" Text="Track" Skin="Default" OnClick="btnTrack_Click" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlWrongInfo" Visible="false">
                <b style="color:red;">Active Directory does not recognize this employee id. </b>
            </asp:Panel>
        </telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
