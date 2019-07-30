<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiverGrid.ascx.cs" Inherits="NationalFundingDev.Controls.Editable.ReceiverGrid" %>

<telerik:RadAjaxPanel runat="server" ID="rapReceiver">
<telerik:RadGrid runat="server" ID="rgReceiver" AllowSorting="true" 
                 OnNeedDataSource="rgReceiver_NeedDataSource" 
                 OnInsertCommand="rgReceiver_InsertCommand"
                 OnUpdateCommand="rgReceiver_UpdateCommand" 
                 OnDeleteCommand="rgReceiver_DeleteCommand" 
                 AutoGenerateColumns="false" >
    <MasterTableView DataKeyNames="RecID" CommandItemSettings-AddNewRecordText="Add New Receiver" CommandItemSettings-ShowRefreshButton="true">
        <EditFormSettings UserControlName="~/Controls/RadGrid/ReceiverEditForm.ascx" EditFormType="WebUserControl" />
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
            <telerik:GridBoundColumn HeaderText="AgreementModID" DataField="AgreementModID" SortExpression="AgreementModID" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FY" SortExpression="FY" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Mod Number" DataField="ModNumber" SortExpression="ModNumber" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" SortExpression="AccountNumber" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Customer Class" DataField="CustomerClass" SortExpression="CustomerClass" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" SortExpression="Status" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Match Pair" DataField="MatchPair" SortExpression="MatchPair" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Program Element Code" DataField="ProgramElementCode" SortExpression="ProgramElementCode" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Funding Amount" DataField="Funding" SortExpression="Funding" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" AllowSorting="true" />
            <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this log?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
</telerik:RadAjaxPanel>