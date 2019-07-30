<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverEditForm.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.ReceiverEditForm" %>
<table>
    <tr>
        <td valign="top" align="right">Agreement ID:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbAgreementMod" Text="<%# rec.AgreementModID %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Fiscal Year:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbFiscalYear" Text="<%# rec.FY %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Mod Number:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbModNumber" Text="<%# rec.ModNumber %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Account Number:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbAccountNumber" Text="<%# rec.AccountNumber %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Customer Class:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbCustomerClass" Text="<%# rec.CustomerClass %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Status:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbStatus" Text="<%# rec.Status %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Match Pair:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbMatchPair" Text="<%# rec.MatchPair %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Program Element Code:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbProgramElementCode" Text="<%# rec.ProgramElementCode %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Funding Amount:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbFunding" Text="<%# rec.Funding %>" Skin="Silk" TextMode="SingleLine" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Remarks:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbRemarks" Text="<%# rec.Remarks %>" Skin="Silk" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td align="right"></td>
        <td>
            <telerik:RadButton ID="btnUpdate" Text="Update" CommandName="Update" CausesValidation="false" runat="server" Visible="false" Skin="Silk" />
            <telerik:RadButton ID="btnInsert" Text="Insert" CommandName="PerformInsert" CausesValidation="false" runat="server" Visible="false" Skin="Silk" />
            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" Skin="Silk" />
        </td>
    </tr>
</table>
