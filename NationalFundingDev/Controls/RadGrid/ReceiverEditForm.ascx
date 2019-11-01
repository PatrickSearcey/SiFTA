<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverEditForm.ascx.cs" Inherits="NationalFundingDev.Controls.RadGrid.ReceiverEditForm" %>
<table>
    <tr style="display: none;">
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
        <td valign="top" align="right">Mod:
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbMod" >
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Account:
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbAccount" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="ldsAccounts"
                                     DataTextField="AccountNumber" DataValueField="AccountNumber" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"  >
                <ItemTemplate>
                    <b></b><%# ProcessMyDataItem(Eval("AccountNumber")) %></b> - 
                    <%# ProcessMyDataItem(Eval("AccountName")) %><br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Customer Class:
        </td>
        <td valign="top">
            <telerik:RadDropDownList runat="server" ID="rddlCustomerClass" Skin="Silk">
                <Items>
                    <telerik:DropDownListItem Text="Direct (SIR)" Value="Direct (SIR)" />
                    <telerik:DropDownListItem Text="Reimbursable" Value="Reimbursable" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Status:
        </td>
        <td valign="top">
            <telerik:RadDropDownList runat="server" ID="rddlStatus" Skin="Silk">
                <Items>
                    <telerik:DropDownListItem Text="" Value="" />
                    <telerik:DropDownListItem Text="DRAFT" Value="DRAFT" />
                    <telerik:DropDownListItem Text="LOW" Value="LOW" />
                    <telerik:DropDownListItem Text="HIGH" Value="HIGH" />
                    <telerik:DropDownListItem Text="FIRM" Value="FIRM" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Match Pair:
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbMatchPair" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="rcbMPC"
                                     DataTextField="MatchPairCode" DataValueField="MatchPairCode" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"  >
                <ItemTemplate>
                    <b></b><%# ProcessMyDataItem(Eval("MatchPairCode")) %></b> - 
                    <%# ProcessMyDataItem(Eval("MatchPairCode")) %><br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">Program Element Code:
        </td>
        <td valign="top">
            <telerik:RadComboBox runat="server" ID="rcbProgramElementCode" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="rcbPEC"
                                     DataTextField="ProgramElementCode" DataValueField="ProgramElementCode" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"  >
                <ItemTemplate>
                    <b></b><%# ProcessMyDataItem(Eval("ProgramElementCode")) %></b> - 
                    <%# ProcessMyDataItem(Eval("ProgramElementCode")) %><br />
                </ItemTemplate>
            </telerik:RadComboBox>
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
<asp:LinqDataSource ID="ldsAccounts" runat="server" OnSelecting="ldsAccounts_Selecting">
</asp:LinqDataSource>
<asp:LinqDataSource ID="rcbMPC" runat="server" OnSelecting="rcbMPC_Selecting">
</asp:LinqDataSource>
<asp:LinqDataSource ID="rcbPEC" runat="server" OnSelecting="rcbPEC_Selecting">
</asp:LinqDataSource>