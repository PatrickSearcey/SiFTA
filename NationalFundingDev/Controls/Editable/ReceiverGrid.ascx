<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverGrid.ascx.cs" Inherits="NationalFundingDev.Controls.Editable.ReceiverGrid" %>

<telerik:RadAjaxPanel runat="server" ID="rapReceiver">
    
<div runat="server" id="mpcDiv" style="border-radius:4px; border: lightgray 1px solid; padding: 10px;">
    <asp:ImageButton runat="server" Height="12px" Width="12px" ImageUrl="~/Images/editPencil.png" OnClick="PencilEdit" />
    <span>&nbsp;&nbsp;Default Match Pair: </span>
    <telerik:RadComboBox runat="server" ID="rcbMatchPair" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="rcbMPC"
                DataTextField="MatchPair" DataValueField="MatchPair" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"
                OnSelectedIndexChanged="rcbMatchPair_SelectedIndexChanged" AutoPostBack="true" Enabled="false" >
        <ItemTemplate>
            <b></b><%# ProcessMyDataItem(Eval("MatchPair")) %></b><br />
        </ItemTemplate>
    </telerik:RadComboBox>&nbsp;&nbsp;
    <span>Default Program Element Code: </span>
    <telerik:RadComboBox runat="server" ID="rcbProgramElementCode" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="rcbPEC"
                DataTextField="ProgramElementCode" DataValueField="ProgramElementCode" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"
                OnSelectedIndexChanged="rcbProgramElementCode_SelectedIndexChanged" AutoPostBack="true" Enabled="false" >
        <ItemTemplate>
            <b></b><%# ProcessMyDataItem(Eval("ProgramElementCode")) %></b><br />
        </ItemTemplate>
    </telerik:RadComboBox>
</div>
<br />

<telerik:RadGrid runat="server" ID="rgReceiver" AllowSorting="true" 
                 OnNeedDataSource="rgReceiver_NeedDataSource" 
                 OnInsertCommand="rgReceiver_InsertCommand"
                 OnUpdateCommand="rgReceiver_UpdateCommand" 
                 OnDeleteCommand="rgReceiver_DeleteCommand" 
                 OnItemDataBound="rgReceiver_ItemDataBound"
                 AutoGenerateColumns="false" PageSize="50" >
    <MasterTableView DataKeyNames="AFSID" CommandItemSettings-AddNewRecordText="Add New Fund Source" CommandItemSettings-ShowRefreshButton="true" ShowGroupFooter="true" >
        <EditFormSettings UserControlName="~/Controls/RadGrid/ReceiverEditForm.ascx" EditFormType="WebUserControl" />
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
            <telerik:GridBoundColumn HeaderText="AgreementID" DataField="AgreementID" SortExpression="AgreementID" Visible="false" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FundSourceFY" SortExpression="FY" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" SortExpression="AccountNumber" AllowSorting="true" />
            <telerik:GridTemplateColumn HeaderText="Account Name" SortExpression="AccountName" AllowSorting="true">
                <ItemTemplate>
                    <%# ProcessItem(Eval("AccountNumber")) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Customer Class" DataField="CustomerClass" SortExpression="CustomerClass" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="FundStatus" SortExpression="Status" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Match Pair" DataField="MatchPair" SortExpression="MatchPair" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Program Element Code" DataField="ProgramElementCode" SortExpression="ProgramElementCode" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Funding Amount" DataField="Funding" SortExpression="Funding" AllowSorting="true" DataFormatString="{0:c0}" />
            <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" AllowSorting="true" />
            <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this log?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

<div runat="server" id="totalsDiv" style="border-radius:4px; border: lightgray 1px solid; padding: 10px;">
    <table style="width:100%;table-layout:auto;empty-cells:show;border-collapse:collapse;">
        <tr style="background-color: #bd8f04; color: white; font-weight: bold">
            <td align="right" style="padding:3px;"></td>
            <td style="padding:3px;">Planned Total (From Fund Sources)</td>
            <td align="right" style="padding:3px;">Direct(SIR) Total:</td>
            <td align="right" style="padding:3px;" runat="server" id="dirTd"></td>
            <td align="right" style="padding:3px;">Reimbursable Total:</td>
            <td align="right" style="padding:3px;" runat="server" id="reimTd"></td>
            <td align="right" style="padding:3px;">Grand Total:</td>
            <td align="right" style="padding:3px;" runat="server" id="totalsTd"></td>
        </tr>
        <tr style="background-color: black; color: white; font-weight: bold">
            <td align="right" style="padding:3px;"></td>
            <td style="padding:3px;">Funding Total (From Agreement Overview)</td>
            <td align="right" style="padding:3px;">USGS CMF:</td>
            <td align="right" style="padding:3px;" runat="server" id="cmfTd"></td>
            <td align="right" style="padding:3px;">Customer:</td>
            <td align="right" style="padding:3px;" runat="server" id="custTd"></td>
            <td align="right" style="padding:3px;">Grand Total:</td>
            <td align="right" style="padding:3px;" runat="server" id="aogtTd"></td>
        </tr>
        <tr style="font-weight: bold">
            <td align="right" style="padding:3px;"></td>
            <td style="padding:3px;">Difference</td>
            <td align="right" style="padding:3px;"></td>
            <td align="right" style="padding:3px;" runat="server" id="diff1Td"></td>
            <td align="right" style="padding:3px;"></td>
            <td align="right" style="padding:3px;" runat="server" id="diff2Td"></td>
            <td align="right" style="padding:3px;"></td>
            <td align="right" style="padding:3px;" runat="server" id="diff3Td"></td>
        </tr>
    </table>
</div>

</telerik:RadAjaxPanel>
<asp:LinqDataSource ID="rcbMPC" runat="server" OnSelecting="rcbMPC_Selecting">
</asp:LinqDataSource>
<asp:LinqDataSource ID="rcbPEC" runat="server" OnSelecting="rcbPEC_Selecting">
</asp:LinqDataSource>