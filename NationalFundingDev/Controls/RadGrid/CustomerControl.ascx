<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerControl.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.CustomerControl" %>
<style type="text/css">
    .ruBrowse
       {
           background-position: 0 -80px !important;
           width: 150px !important;
       }
</style>
<table cellpadding="3px">
    <tr>
        <td align="right">
            <telerik:RadBinaryImage runat="server" ID="imgCustomer" ImageUrl='<%# String.Format("https://sifta.water.usgs.gov/Services/REST/Customer/CustomerIcon.ashx?CustomerID={0}", customer.CustomerID) %>' 
                Width="100px" Height="100px" ResizeMode="Fit" ></telerik:RadBinaryImage>
        </td>
        <td valign="bottom">
            <telerik:RadAsyncUpload runat="server" ID="rauImage" MultipleFileSelection="Disabled" TargetFolder="~/icons/" AllowedFileExtensions="jpg,jpeg,png,gif,bmp" AllowedMimeTypes="image/gif,image/jpeg,image/pjpeg,image/png,image/x-png,image/bmp"
            DisableChunkUpload="true" Skin="Silk" MaxFileInputsCount="1" ToolTip="Upload Customer Icon" Localization-Select="Upload Customer Icon"  />
        </td>
    </tr>
    <tr>
        <td align="right">Customer Code:
        </td>
        <td>
            <telerik:RadTextBox ID="rtbCustomerCd" runat="server" EmptyMessage="Example: TX014" Text='<%# customer.Code  %>' EmptyMessageStyle-Font-Italic="true" Skin="Silk" Width="450px" />
        </td>
    </tr>
    <tr>
        <td align="right">FBMS Customer Number:
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbCustomerNo" Value='<%# customer.Number %>' Type="Number" EmptyMessage="Example: 6000000481" Skin="Silk">
                <NumberFormat AllowRounding="false" DecimalDigits="0" GroupSeparator="" />
            </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">Customer Name: 
        </td>
        <td>
            <telerik:RadTextBox ID="rtbCustomerName" runat="server" Skin="Silk" EmptyMessage="Example: City of Austin" EmptyMessageStyle-Font-Italic="true"
                Text='<%# customer.Name %>' Width="450px" />
            <asp:RequiredFieldValidator ID="rfvCustomerNm" runat="server" ErrorMessage="* Required" ForeColor="Red"
                ControlToValidate="rtbCustomerName" Font-Size="X-Small" ValidationGroup="vgCustomerControl" />
        </td>
    </tr>
    <tr>
        <td align="right">Customer Abbreviation:
        </td>
        <td>
            <telerik:RadTextBox ID="rtbCustomerAbbrev" runat="server" Skin="Silk" EmptyMessage="Example: Austin" EmptyMessageStyle-Font-Italic="true"
                Text='<%# customer.Abbreviation %>' Width="450px" />
        </td>
    </tr>
    <tr>
        <td align="right">Customer Url: 
        </td>
        <td>
            <telerik:RadTextBox ID="rtbCustomerUrl" runat="server" Skin="Silk" EmptyMessage="Example: http://www.austintexas.gov/" EmptyMessageStyle-Font-Italic="true"
                Text='<%# customer.URL %>' Width="450px" />
        </td>
    </tr>
    <tr>
        <td align="right">Remarks: 
        </td>
        <td>
            <telerik:RadTextBox ID="rtbRemarks" runat="server" Skin="Silk"
                Text='<%# customer.Remarks %>' Width="450px" />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Literal ID="ltCustomerTin" runat="server" Text="Tax Identification Number (TIN):" Visible="true" />
        </td>
        <td>
            <telerik:RadTextBox ID="rtbCustomerTin" runat="server" Skin="Silk" EmptyMessage="Example: 74-6000085" EmptyMessageStyle-Font-Italic="true"
                Text='<%# customer.TaxIdentificationNumber %>' Visible="true" Width="450px" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <telerik:RadButton ID="btnUpdate" Text="Update" CommandName="Update" CausesValidation="true" runat="server" Visible="false" Skin="Silk" ValidationGroup="vgCustomerControl" />
            <telerik:RadButton ID="btnInsert" Text="Insert" CommandName="PerformInsert" CausesValidation="true" runat="server" Visible="false" Skin="Silk" ValidationGroup="vgCustomerControl" />
            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" Skin="Silk" />
        </td>
    </tr>
</table>
<asp:LinqDataSource ID="ldsAgreementType" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutCustomerAgreementTypes"></asp:LinqDataSource>
<asp:LinqDataSource ID="ldsTags" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutTags"></asp:LinqDataSource>
