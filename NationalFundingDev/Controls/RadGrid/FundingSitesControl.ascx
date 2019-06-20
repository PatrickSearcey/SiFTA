<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundingSitesControl.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.FundingSitesControl" %>
<table>
    <tr>
        <td align="right">Mod:</td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbMod" />
        </td>
    </tr>
    <tr>
        <td align="right">Site Number:</td>
        <td>
            <telerik:RadTextBox runat="server" ID="rtbSiteNumber" Text="<%# siteFunding.SiteNumber  %>" /></td>
    </tr>
    <tr>
        <td align="right">Collection Code Category:</td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbCollectionCodeCategory" OnSelectedIndexChanged="rcbCollectionCodeCategory_SelectedIndexChanged" AutoPostBack="true">
                <Items>
                    <telerik:RadComboBoxItem Text="Atmospheric" Value="clim" />
                    <telerik:RadComboBoxItem Text="Ground Water" Value="gw" />
                    <telerik:RadComboBoxItem Text="Miscellaneous" Value="misc" />
                    <telerik:RadComboBoxItem Text="Surface Water" Value="sw" />
                    <telerik:RadComboBoxItem Text="Sediment" Value="sed" />
                    <telerik:RadComboBoxItem Text="Water Quality" Value="wq" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">Collection Code:</td>
        <td>
            <telerik:RadComboBox runat="server" ID="rcbCollectionCode" DataTextField="Name" DataValueField="CollectionCodeID" AppendDataBoundItems="true" DropDownAutoWidth="Enabled" MaxHeight="200px">
                <Items>
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right">Units:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbUnits" Type="Number" Value="<%# siteFunding.CollectionUnits  %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="UnitsToolTip">
                <img src="https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip4" TargetControlID="UnitsToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Enter the percent of the total cost of this item funded by the customer or program.
                    <br />
                    Example: If FPS is funding 50% of the streamflow data collection, enter .5
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Difficulty Factor:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbDifficultyFactor" Type="Number" Value="<%# siteFunding.DifficultyFactor  %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="DifficultyFactorToolTip">
                <img src="https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="rtt" TargetControlID="DifficultyFactorToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Difficulty Factor: Enter the percent effort required to perform the task as a decimal value between 0.1 and 10.
                    <br />
                    Example: If 50% additional effort is required to collect streamflow data at a station in a remote location, enter 1.5
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">Difficulty Factor Reason:</td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <telerik:RadTextBox runat="server" ID="rtbDifficultyFactorReason" Height="50px" Width="100%" TextMode="MultiLine" Text="<%# siteFunding.DifficultyFactorReason %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="DifficultyFactorReasonToolTip">
                <img src="https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip1" TargetControlID="DifficultyFactorReasonToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Difficulty Factor Reason: Explain the difficulty factor value.
                    <br />
                    Example: Helicopter access required.
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">USGS CMF Funds:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbUSGSCMFFunding" Type="Currency" Value="<%# siteFunding.FundingUSGSCMF  %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="FundingUSGSCMFToolTip">
                <img src="https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip2" TargetControlID="FundingUSGSCMFToolTip" ShowEvent="OnMouseOver"
                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="BlackMetroTouch">
                <div class="ToolTip" style="width: 200px;">
                    Enter USGS CMF funds applied to this agreement/mod. The amount entered is the +/- amount set in your agreement/mod letter. Remember, USGS funds can not be applied to programs such as NAWQA, FPS, GWRP, etc. The field will not be open for entry for these customers.
                </div>
            </telerik:RadToolTip>
        </td>
    </tr>
    <tr>
        <td align="right">Customer Funds:</td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="rntbCustomerFunding" Type="Currency" Value="<%# siteFunding.FundingCustomer %>" /></td>
        <td style="vertical-align: bottom;">
            <a runat="server" id="FundingCustomerToolTip">
                <img src="https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                    onmouseover="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                    onmouseout="this.src='https://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
            </a>
            <telerik:RadToolTip runat="server" ID="RadToolTip3" TargetControlID="FundingCustomerToolTip" ShowEvent="OnMouseOver"
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
            <telerik:RadNumericTextBox runat="server" ID="rntbOtherFunding" Type="Currency" Value="<%# siteFunding.FundingOther %>" /></td>
    </tr>

    <tr>
        <td align="right">Start Date:</td>
        <td>
            <telerik:RadDatePicker ID="rdpStartDate" runat="server" DateInput-Label="">
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td align="right">End Date:</td>
        <td>
            <telerik:RadDatePicker ID="rdpEndDate" runat="server" DateInput-Label="">
            </telerik:RadDatePicker>
        </td>
    </tr>

    <tr>
        <td align="left" colspan="2">Remarks:</td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <telerik:RadTextBox runat="server" ID="rtbRemarks" TextMode="MultiLine" Height="50px" Width="100%" Text="<%# siteFunding.Remarks  %>" />
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
