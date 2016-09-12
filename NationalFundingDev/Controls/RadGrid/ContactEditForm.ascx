<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactEditForm.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.ContactEditForm" %>
<table>
    <tr>
        <td align="right"><b>Name:</b></td>
        <td style="width:300px;"><telerik:RadComboBox runat="server" ID="rcbSalutation" Skin="Silk" Width="60px">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                    <telerik:RadComboBoxItem Text="Mr." Value="Mr." />
                    <telerik:RadComboBoxItem Text="Ms." Value="Ms." />
                    <telerik:RadComboBoxItem Text="Mrs." Value="Mrs." />
                    <telerik:RadComboBoxItem Text="Dr." Value="Dr." />
                </Items>
            </telerik:RadComboBox>
            <telerik:RadTextBox runat="server" ID="rtbFirstName" Skin="Silk" Text="<%# contact.FirstName %>" Width="100px" />
            <telerik:RadTextBox runat="server" ID="rtbMiddleInitial" Skin="Silk" Text="<%# contact.MiddleName %>" MaxLength="1" Width="20px" />
            <telerik:RadTextBox runat="server" ID="rtbLastName" Skin="Silk" Text="<%# contact.LastName %>" Width="100px" /></td>
    </tr>
    <tr>
        <td align="right"><b>Title:</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbTitle" Skin="Silk" Text="<%# contact.Title %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Work Phone:</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbPhoneWork" Skin="Silk" Text="<%# contact.PhoneWork %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Mobile Phone:</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbPhoneMobile" Skin="Silk" Text="<%# contact.PhoneMobile %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Fax:</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbPhoneFax" Skin="Silk" Text="<%# contact.PhoneFax %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Email:</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbEmail" Skin="Silk" Text="<%# contact.Email %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Remarks</b></td>
        <td><telerik:RadTextBox runat="server" ID="rtbRemarks" Skin="Silk" Text="<%# contact.Remarks %>" /></td>
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