<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverGrid.ascx.cs" Inherits="NationalFundingDev.Controls.Editable.ReceiverGrid" %>

<telerik:RadAjaxPanel runat="server" ID="rapReceiver">
    
<div runat="server" id="mpcDiv" style="border-radius:4px; border: lightgray 1px solid; padding: 10px;">
    <span>Default Match Pair Code: </span>
    <telerik:RadComboBox runat="server" ID="rcbMatchPair" Filter="Contains" AllowCustomText="false" MarkFirstMatch="true" HighlightTemplatedItems="true" DataSourceID="rcbMPC"
                DataTextField="MatchPairCode" DataValueField="MatchPairCode" ItemsPerRequest="5" Height="150px"  DropDownAutoWidth="Enabled"
                OnSelectedIndexChanged="rcbMatchPair_SelectedIndexChanged" AutoPostBack="true" >
        <ItemTemplate>
            <b></b><%# ProcessMyDataItem(Eval("MatchPairCode")) %></b><br />
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
                 AutoGenerateColumns="false" >
    <MasterTableView DataKeyNames="RecID" CommandItemSettings-AddNewRecordText="Add New Receiver" CommandItemSettings-ShowRefreshButton="true" ShowGroupFooter="true" ShowFooter="true">
        <FooterStyle BackColor="Black" ForeColor="White" />
        <EditFormSettings UserControlName="~/Controls/RadGrid/ReceiverEditForm.ascx" EditFormType="WebUserControl" />
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
            <telerik:GridBoundColumn HeaderText="AgreementID" DataField="AgreementID" SortExpression="AgreementID" Visible="false" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FY" SortExpression="FY" AllowSorting="true" />
            <telerik:GridTemplateColumn HeaderText="Mod" DataField="ModNumber">
                <ItemTemplate>
                    <%# Eval("ModNumber").ToString() == "0" ? "" : Eval("ModNumber") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" SortExpression="AccountNumber" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Customer Class" DataField="CustomerClass" SortExpression="CustomerClass" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" SortExpression="Status" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Match Pair" DataField="MatchPair" SortExpression="MatchPair" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Program Element Code" DataField="ProgramElementCode" SortExpression="ProgramElementCode" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Funding Amount" DataField="Funding" SortExpression="Funding" AllowSorting="true" DataFormatString="{0:c0}" />
            <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" AllowSorting="true" />
            <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this log?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
</telerik:RadAjaxPanel>
<asp:LinqDataSource ID="rcbMPC" runat="server" OnSelecting="rcbMPC_Selecting">
</asp:LinqDataSource>