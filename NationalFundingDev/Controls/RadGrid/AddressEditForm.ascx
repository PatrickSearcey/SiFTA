<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressEditForm.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.AddressEditForm" %>
<table>
    <tr>
        <td align="right"><b>Type:</b></td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbAddressType" Skin="Silk">
                <Items>
                    <telerik:RadComboBoxItem Text="Mailing" Value="Mailing" />
                    <telerik:RadComboBoxItem Text="Shipping" Value="Shipping" />
                    <telerik:RadComboBoxItem Text="Other" Value="Other" />
                </Items>
            </telerik:RadComboBox>    
        </td>
    </tr>
    <tr>
        <td align="right"><b>Address:</b></td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbStreetOne" Skin="Silk" Text="<%# address.StreetOne %>" /></td>
    </tr>
    <tr>
        <td align="right"></td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbStreetTwo" Skin="Silk" Text="<%# address.StreetTwo %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>City:</b></td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbCity" Skin="Silk" Text="<%# address.City %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>State:</b></td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbState" Skin="Silk" Text="<%# address.State %>" /></td>
    </tr>
    <tr>
        <td align="right"><b>Zip Code:</b></td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbZipCode" Skin="Silk" Text="<%# address.ZipCode %>" /></td>
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
