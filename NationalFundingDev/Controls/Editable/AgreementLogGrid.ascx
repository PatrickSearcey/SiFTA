<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgreementLogGrid.ascx.cs" Inherits="NationalFundingDev.Controls.Editable.AgreementLogGrid" %>

<telerik:RadAjaxPanel runat="server" ID="rapAgreementLog">
<telerik:RadGrid runat="server" ID="rgAgreementLog" AllowSorting="true" 
                 OnNeedDataSource="rgAgreementLog_NeedDataSource" 
                 OnInsertCommand="rgAgreementLog_InsertCommand"
                 OnUpdateCommand="rgAgreementLog_UpdateCommand" 
                 OnDeleteCommand="rgAgreementLog_DeleteCommand" 
                 AutoGenerateColumns="false" >
    <MasterTableView DataKeyNames="AgreementModLogID" CommandItemSettings-AddNewRecordText="Add New Agreement Log" CommandItemSettings-ShowRefreshButton="true">
        <EditFormSettings UserControlName="~/Controls/RadGrid/AgreementLogEditForm.ascx" EditFormType="WebUserControl" />
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
            <telerik:GridBoundColumn HeaderText="" DataField="ModName" SortExpression="ModName" AllowSorting="true" />
            <telerik:GridBoundColumn HeaderText="Date" DataField="LoggedDate" SortExpression="LoggedDate" AllowSorting="true" DataFormatString="{0:d}" />
            <telerik:GridBoundColumn HeaderText="Log Type" DataField="Type" SortExpression="Type" AllowSorting="true" />
            <telerik:GridTemplateColumn HeaderText="Log By" DataField="CreatedBy" SortExpression="CreatedBy" AllowSorting="true" >
                <ItemTemplate>
                    <%# LoggedBy(Eval("Name"), Eval("CreatedBy")) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" AllowSorting="true" />
            <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this log?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
</telerik:RadAjaxPanel>