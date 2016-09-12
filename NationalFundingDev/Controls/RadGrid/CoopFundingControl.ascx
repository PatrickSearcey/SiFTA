<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoopFundingControl.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.CoopFundingControl" %>
<table>
    <tr>
        <td align="right" valign="top">
            Fiscal Year
        </td>
        <td align="left" valign="top">
            <telerik:RadNumericTextBox runat="server" ID="rntbFiscalYear" Type="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
            <asp:RequiredFieldValidator runat="server" ID="rf" Text="* Required" ForeColor="Red" ControlToValidate="rntbFiscalYear" ValidationGroup="vgCF" />
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            Mod:
        </td>
        <td align="left" valign="top">
            <telerik:RadComboBox runat="server" ID="rcbMod" >
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            Account:
        </td>
        <td align="left" valign="top">
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                <telerik:RadComboBox runat="server" ID="rcbAccount" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="ldsAccounts"
                                     DataTextField="AccountNumber" DataValueField="AccountNumber" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"  >
                    <ItemTemplate>
                        <b></b><%# ProcessMyDataItem(Eval("AccountNumber")) %></b> - 
                        <%# ProcessMyDataItem(Eval("AccountName")) %><br />
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvAccount" Text="* Required" ForeColor="Red" ControlToValidate="rcbAccount" ValidationGroup="vgCF" />

            </telerik:RadAjaxPanel>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            USGS:
        </td>
        <td align="left" valign="top">
            <telerik:RadNumericTextBox runat="server" ID="rntbUSGS" Type="Currency"  NumberFormat-DecimalDigits="2" />
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            Cooperator:
        </td>
        <td align="left" valign="top">
            <telerik:RadNumericTextBox runat="server" ID="rntbCooperator" Type="Currency"  NumberFormat-DecimalDigits="2" />
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            Status:
        </td>
        <td align="left" valign="top">
            <telerik:RadComboBox runat="server" ID="rcbStatus" >
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                    <telerik:RadComboBoxItem Text="DRAFT" Value="DRAFT" />
                    <telerik:RadComboBoxItem Text="LOW" Value="LOW" />
                    <telerik:RadComboBoxItem Text="HIGH" Value="HIGH" />
                    <telerik:RadComboBoxItem Text="FIRM" Value="FIRM" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            Remarks:
        </td>
        <td align="left" valign="top">
            <telerik:RadTextBox runat="server" ID="rtbRemarks" TextMode="MultiLine" Height="100px" />
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">

        </td>
        <td align="left" valign="top">
            <telerik:RadButton id="btnUpdate" text="Update" commandname="Update"  CausesValidation="true" ValidationGroup="vgCF" runat="server"  visible="false" Skin="Silk"  />
            <telerik:RadButton id="btnInsert" text="Insert" commandname="PerformInsert"  CausesValidation="true" ValidationGroup="vgCF"  runat="server"  visible="false" Skin="Silk"  />
            <telerik:RadButton id="btnCancel" text="Cancel" runat="server" causesvalidation="False" commandname="Cancel" Skin="Silk" />
        </td>
    </tr>
</table>
<asp:LinqDataSource ID="ldsAccounts" runat="server" OnSelecting="ldsAccounts_Selecting">
</asp:LinqDataSource>
