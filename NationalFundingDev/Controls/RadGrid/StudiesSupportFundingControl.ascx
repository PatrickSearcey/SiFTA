﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudiesSupportFundingControl.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.StudiesSupportFundingControl" %>
<table>
    <tr>
        <td align="right">Mod:</td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbMod" />
        </td>
    </tr>
    <tr>
        <td align="right">Type:</td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbType" DataTextField="Description" DataValueField="ResearchCodeID" AppendDataBoundItems="true">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">Basis Project Number:</td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbBasisProjectNumber" Text="<%# research.BasisProjectNumber %>" /></td>
    </tr>
    <tr>
        <td align="right">USGS CWP Funds:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbUSGSCWPFunding" Type="Currency" Value="<%# research.FundingUSGSCWP %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="FundingUSGSCWPToolTip">
                <img src="http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip4" TargetControlID="FundingUSGSCWPToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Enter USGS CWP funds applied to this agreement/mod. The amount entered is the +/- amount set in your agreement/mod letter. Remember, USGS funds can not be applied to programs such as NAWQA, NSIP, GWRP, etc. The field will not be open for entry for these customers.
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Customer Funds:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbCustomerFunding" Type="Currency" Value="<%# research.FundingCustomer %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="FundingCustomerToolTip">
                <img src="http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip1" TargetControlID="FundingCustomerToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Enter Customer funds applied to this agreement/mod. The amount entered is the +/- amount set in your agreement/mod letter
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Other Funds:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbOtherFunding" Type="Currency" Value="<%# research.FundingOther %>" /></td>
    </tr>
    <tr>
        <td align="right">Remarks</td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbRemarks" TextMode="MultiLine" Height="50px" Text="<%# research.Remarks %>" />
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
