<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgreementLogEditForm.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.AgreementLogEditForm" %>
<table>
    <tr>
        <td valign="top" align="right">
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbMod" DataValueField="Key" DataTextField="Value" Skin="Silk" Width="250px" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Date and Time:
        </td>
        <td valign="top">
            <telerik:RadDateTimePicker runat="server" ID="rdtpAgreementLogTime"  Skin="Silk" Width="250px" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Action:
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbActionAgreementLog" DataValueField="AgreementLogTypeID" DataTextField="Type" Skin="Silk" Width="250px" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Remarks:
        </td>
        <td valign="top">
            <telerik:RadTextBox runat="server" ID="rtbRemarksAgreementLog" Text="<%# log.Remarks %>" Skin="Silk" Width="500px" Height="200px" TextMode="MultiLine" />
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
