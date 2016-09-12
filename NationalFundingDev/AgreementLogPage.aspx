<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgreementLogPage.aspx.cs" Inherits="NationalFundingDev.AgreementLogPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        h2 {
    color: #2dabc1;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="rsm"></telerik:RadScriptManager>
        <asp:Panel runat="server" ID="pnlAgreementLog">
        <h2>Add an Agreement Log</h2>
        <table>
            <tr>
                <td valign="top" align="right">Date and Time:
                </td>
                <td valign="top">
                    <telerik:RadDateTimePicker runat="server" ID="rdtpAgreementLogTime" Skin="Silk" Width="250px" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right">Action:
                </td>
                <td valign="top">
                    <telerik:RadComboBox runat="server" ID="rcbActionAgreementLog" Skin="Silk" Width="250px" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right">Remarks:
                </td>
                <td valign="top">
                    <telerik:RadTextBox runat="server" ID="rtbRemarksAgreementLog" Skin="Silk" Width="500px" Height="200px" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right"></td>
                <td valign="top">
                    <telerik:RadButton runat="server" ID="rbSubmitAgreementLog" Text="Add Agreement Log" Skin="Silk" OnClick="rbSubmitAgreementLog_Click" AutoPostBack="true" />
                    <telerik:RadButton runat="server" ID="RadButton3" Text="Close" OnClick="btnClose_Click" Skin="Silk" AutoPostBack="true" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlComplete" Visible="false">
            <h2>Agreement Log Successfully Added</h2>
            Thank you, your log has been added<br />
            <telerik:RadButton runat="server" ID="RadButton1" Text="Close" OnClick="btnClose_Click" Skin="Silk" AutoPostBack="true" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlError" Visible="false">
            Something went wrong please try again. If this issue persists contact support.<br />
            Reason for Error: <br />
            <asp:Literal runat="server" ID="ltlError" /><br />
            <telerik:RadButton runat="server" ID="rbBack" Text="Back" OnClick="rbBack_Click" Skin="Silk" AutoPostBack="true" />
            <telerik:RadButton runat="server" ID="RadButton2" Text="Close" OnClick="btnClose_Click" Skin="Silk" AutoPostBack="true" />
        </asp:Panel>
    </form>
</body>
</html>
