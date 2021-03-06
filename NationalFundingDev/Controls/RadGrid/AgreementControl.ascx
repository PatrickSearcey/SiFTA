﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgreementControl.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.AgreementControl" %>
<table>
    <tr>
        <td align="right">
            Agreement Number:
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbPurchaseOrderNumber" Skin="Silk" Text="<%# agreement.PurchaseOrderNumber %>" />
        </td> 
        <td style="vertical-align:bottom;">
            <a runat="server" id="AgreementToolTip" >
                <img src="https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif" height="15" width="15" 
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="rtt" TargetControlID="AgreementToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width:200px;">
                    Agreement number used in associated financial systems. If using the copy feature, please remove the words COPY_ from the Agreement entry
                </div>
            </telerik:RadToolTip>
            <asp:RequiredFieldValidator runat="server" ID="rfvAgreementNumber" ValidationGroup="vgAgreement" ControlToValidate="rtbPurchaseOrderNumber" Text="* Required" ForeColor="Red" />
        </td>
    </tr>
    <tr>
        <td align="right">Sales Order Number:
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbSalesDocument" Skin="Silk" Text="<%# agreement.SalesDocument %>" />
        </td>
        <td style="vertical-align:bottom;">
            <a runat="server" id="SalesOrderNumberToolTip" >
                <img src="https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif" height="15" width="15" 
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip2" TargetControlID="SalesOrderNumberToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width:200px;">
                    Auto-populated from FBMS
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Start Date:
        </td>
        <td>
            <telerik:RadDatePicker runat="server" ID="rdpStartDate" Skin="Silk" SelectedDate="<%# mod.StartDate %>" />
        </td>
    </tr>
    <tr>
        <td align="right">End Date:
        </td>
        <td>
            <telerik:RadDatePicker runat="server" ID="rdpEndDate" Skin="Silk" SelectedDate="<%# mod.EndDate %>" />
        </td>
    </tr>
    <tr>
        <td align="right">USGS Signed:
        </td>
        <td>
            <telerik:RadDatePicker runat="server" ID="rdpUSGSSigned" Skin="Silk" SelectedDate="<%# mod.SignUSGSDate %>" />
        </td>
    </tr>
    <tr>
        <td align="right">Customer Signed:
        </td>
        <td>
            <telerik:RadDatePicker runat="server" ID="rdpCustomerSigned" Skin="Silk" SelectedDate="<%# mod.SignCustomerDate %>" />
        </td>
    </tr>
    <tr>
        <td align="right">Agreement Type:
        </td>
        <td>
            <telerik:RadComboBox ID="rcbAType" runat="server" Skin="Silk" Width="160px">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">Billing Type:
        </td>
        <td>
            <telerik:RadComboBox ID="rcbFundsType" runat="server" Skin="Silk" Width="160px">
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    <telerik:RadComboBoxItem Value="F" Text="Fixed" />
                    <telerik:RadComboBoxItem Value="A" Text="Actual" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">Billing Cycle:
        </td>
        <td>
            <telerik:RadComboBox ID="rcbBillingCycle" runat="server" Skin="Silk" Width="160px">
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    <telerik:RadComboBoxItem Value="Monthly" Text="Monthly" />
                    <telerik:RadComboBoxItem Value="Quarterly" Text="Quarterly" />
                    <telerik:RadComboBoxItem Value="Annually" Text="Annually" />
                    <telerik:RadComboBoxItem Value="Semi-Annually" Text="Semi-Annual" />
                    <telerik:RadComboBoxItem Value="Advance" Text="Advance" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">USGS CMF Funding:
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbUSGSFunding" Type="Currency" NumberFormat-DecimalDigits="2" Skin="Silk" Value="<%# mod.FundingUSGSCMF %>" />
        </td>
        <td style="vertical-align:bottom;">
            <a runat="server" id="USGSFundingToolTip" >
                <img src="https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif" height="15" width="15" 
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip3" TargetControlID="USGSFundingToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width:200px;">
                    Please enter USGS funds applied to this agreement. The number entered is the number sent in your agreement letter.<br /><br />
                    <b>Note:</b><br />
                    This number may not match exactly in other accounting systems due to some historical accounting practices or the total found on the Customer total found on the details page. Values should be similar or should be explained in the agreement documents.
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Customer Funding:
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbCustomerFunding" Type="Currency" NumberFormat-DecimalDigits="2" Skin="Silk" Value="<%# mod.FundingCustomer %>" />
        </td>
        <td style="vertical-align:bottom; float:left;">
            <a runat="server" id="CustomerFundingToolTip" >
                <img src="https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif" height="15" width="15" 
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/Images/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip4" TargetControlID="CustomerFundingToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width:200px;">
                    Please enter Customer funds applied to this agreement. The number entered is the number sent in your agreement letter<br /><br />
                    <b>Note:</b><br />
                    This number may not match exactly in other accounting systems due to some historical accounting practices or the total found on the Customer total found on the details page. Values should be similar or should be explained in the agreement documents.
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">In Kind Funding:
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbOtherFunding" Type="Currency" NumberFormat-DecimalDigits="2" Skin="Silk" Value="<%# mod.FundingOther %>" />
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">In Kind Funding Reason: 
        </td>
        <td colspan="2">
            <telerik:RadTextBox runat="server" ID="rtbOtherFundingReason" Skin="Silk" TextMode="MultiLine" Height="50px" Text="<%# mod.FundingOtherReason %>" />
        </td>
    </tr>
    </table>
<table>
    <tr>
        <td style="width:150px;"></td>
        <td>
            <telerik:RadButton ID="btnUpdate" Text="Update" CommandName="Update"  runat="server" Visible="false" Skin="Silk" ValidationGroup="vgAgreement" />
            <telerik:RadButton ID="btnInsert" Text="Insert" CommandName="PerformInsert"  runat="server" Visible="false" Skin="Silk" ValidationGroup="vgAgreement" />
            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" Skin="Silk" />
        </td>
    </tr>
</table>
<asp:LinqDataSource ID="ldsTags" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutTags"></asp:LinqDataSource>