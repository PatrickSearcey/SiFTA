<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="CoopFunding.aspx.cs" Inherits="NationalFundingDev.Reports.CoopFunding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Cooperative Funding
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
    <style type="text/css">
        h4
        {
            padding:1px;
            margin:1px;
            font-family:Arial,Helvetica,sans-serif;
            color:#555555;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>
    <telerik:RadAjaxManager runat="server" ID="ram">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCooperativeFunding" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbFormat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCooperativeFunding" LoadingPanelID="ralpSilk" />
                    <telerik:AjaxUpdatedControl ControlID="rlbColumnSelection" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlbColumnSelection">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFormat" />
                    <telerik:AjaxUpdatedControl ControlID="rlbColumnSelection" LoadingPanelID="ralpSilk" />
                    <telerik:AjaxUpdatedControl ControlID="rgCooperativeFunding" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rlbOfficeFilters">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCooperativeFunding" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCooperativeFunding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCooperativeFunding" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpSilk" Skin="MetroTouch" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphInformation" runat="server">
    <h2><asp:Literal runat="server" ID="ltlTitle" /></h2>
    <a href='<%= String.Format("../../Center.aspx?OrgCode={0}", center.OrgCode) %>' style="color: orange;">Center Home</a> >> Account Funding
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSidePanel" runat="server">
    <hr />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cphBody" runat="server">
    Test
    <table style="height:100%; width:100%; min-height:500px; min-width:800px;">
        <tr>
            <td valign="top" align="left" style="width:200px; min-width:200px;">
                <hr />
                <h4>Quick Download</h4>
                <telerik:RadComboBox runat="server" ID="rcbQuickDownloadOptions" Width="100%" >
                    <Items>
                        <telerik:RadComboBoxItem Text="All Years" Value="All" />
                        <telerik:RadComboBoxItem Text="Recent & Future" Value="Recent&Future" />
                    </Items>
                </telerik:RadComboBox><br /><br />
                <telerik:RadButton Width="100%" runat="server" ID="rbDownloadAllCurrentAndFuture" OnClick="rbDownloadAllCurrentAndFuture_Click" Text="Download Excel" />
                <hr />
                <h4>Fiscal Year</h4>
                <telerik:RadComboBox runat="server" ID="rcbYear" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rcbYear_SelectedIndexChanged" />
                <hr />
                <h4>Format</h4>
                <telerik:RadComboBox runat="server" ID="rcbFormat" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rcbFormat_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="Custom" Value="Custom" />
                        <telerik:RadComboBoxItem Text="Total Program (Bottom Line)" Value="TotalBottom" Selected="true" />
                        <telerik:RadComboBoxItem Text="Total Program (Basic)" Value="TotalBasic"  />
                        <telerik:RadComboBoxItem Text="Proposed" Value="Proposed" />
                        <telerik:RadComboBoxItem Text="Coop Available Balance" Value="CoopBalance" />
                    </Items>
                </telerik:RadComboBox>
                <hr />
                <h4>Column Selection</h4>
                <telerik:RadListBox runat="server" ID="rlbColumnSelection" Width="100%" AutoPostBack="true" CheckBoxes="true" ShowCheckAll="true" OnCheckAllCheck="rlbColumnSelection_CheckAllCheck" OnItemCheck="rlbColumnSelection_ItemCheck">
                    <Items>
                        <telerik:RadListBoxItem Text="Fiscal Year" Value="FiscalYear" />
                        <telerik:RadListBoxItem Text="Office" Value="Office" />
                        <telerik:RadListBoxItem Text="Customer Info" Value="CustomerInfo" />
                        <telerik:RadListBoxItem Text="Agreement Type" Value="AgreementType" />
                        <telerik:RadListBoxItem Text="Agreement Info" Value="AgreementInfo" />
                        <telerik:RadListBoxItem Text="Match Pair" Value="MatchPair" />
                        <telerik:RadListBoxItem Text="Sales Document" Value="SalesDocument" />
                        <telerik:RadListBoxItem Text="Signatures" Value="Signatures" />
                        <telerik:RadListBoxItem Text="Account Info" Value="AccountInfo" />
                        <telerik:RadListBoxItem Text="Funds Status" Value="FundsStatus" />
                        <telerik:RadListBoxItem Text="Comments" Value="Comments" />
                    </Items>
                </telerik:RadListBox>
                <hr />
                <h4>Office Filters</h4>
                <telerik:RadListBox runat="server" ID="rlbOfficeFilters" Width="100%" AutoPostBack="true" CheckBoxes="true" ShowCheckAll="true" OnCheckAllCheck="rlbOfficeFilters_CheckAllCheck" OnItemCheck="rlbOfficeFilters_ItemCheck" /><br /><br />
            </td>
            <td valign="top" align="left">
                <telerik:RadGrid runat="server" ID="rgCooperativeFunding" AutoGenerateColumns="false" OnNeedDataSource="rgCooperativeFunding_NeedDataSource">
                    <MasterTableView ShowFooter="true" CommandItemDisplay="Top" NoMasterRecordsText=" There are no funding records to display for this center." AllowSorting="true">
                        <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="ExportToExcelButton" Text="Download current view in Excel" Width="100%" OnClick="btnDownloadExcel_Click"  />
                        </CommandItemTemplate>
                        <ColumnGroups>
                            <telerik:GridColumnGroup Name="Account" HeaderText="Account" />
                            <telerik:GridColumnGroup Name="Customer" HeaderText="Customer" />
                            <telerik:GridColumnGroup Name="Signed" HeaderText="Signed" />
                            <telerik:GridColumnGroup Name="Funds" HeaderText="Funds" />
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridBoundColumn DataField="FiscalYear" HeaderText="FY" SortExpression="FiscalYear" UniqueName="FiscalYear" />
                            <telerik:GridBoundColumn DataField="OfficeCode" HeaderText="Office Code" SortExpression="OfficeCode" UniqueName="OfficeCode" />
                            <telerik:GridBoundColumn DataField="CustomerCode" HeaderText="Code" SortExpression="CustomerCode" UniqueName="CustomerCode" ColumnGroupName="Customer" />
                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName" UniqueName="CustomerName" ColumnGroupName="Customer" />
                            <telerik:GridBoundColumn DataField="CustomerAgreementType" HeaderText="Agreement Type" SortExpression="CustomerAgreementType" UniqueName="CustomerAgreementType" ColumnGroupName="Customer" />
                            <telerik:GridBoundColumn DataField="PurchaseOrderNumber" HeaderText="Purchase Order Number" SortExpression="PurchaseOrderNumber" UniqueName="PurchaseOrderNumber" />
                            <telerik:GridBoundColumn DataField="MatchPair" HeaderText="Match Pair" SortExpression="MatchPair" UniqueName="MatchPair" />
                            <telerik:GridBoundColumn DataField="SalesDocument" HeaderText="Sales Document" SortExpression="SalesDocument" UniqueName="SalesDocument" />
                            <telerik:GridBoundColumn DataField="ModNumber" HeaderText="Mod" SortExpression="ModNumber" UniqueName="ModNumber" />
                            <telerik:GridBoundColumn DataField="SignUSGSDate" HeaderText="USGS" SortExpression="SignUSGSDate" UniqueName="SignUSGSDate" DataFormatString="{0:d}" ColumnGroupName="Signed" />
                            <telerik:GridBoundColumn DataField="SignCustomerDate" HeaderText="Customer" SortExpression="SignCustomerDate" UniqueName="SignCustomerDate" DataFormatString="{0:d}" ColumnGroupName="Signed" />
                            <telerik:GridBoundColumn DataField="AccountNumber" HeaderText="Number" SortExpression="AccountNumber" UniqueName="AccountNumber" ColumnGroupName="Account" />
                            <telerik:GridBoundColumn DataField="AccountName" HeaderText="Name" SortExpression="AccountName" UniqueName="AccountName" ColumnGroupName="Account" />
                            <telerik:GridTemplateColumn DataField="Status" HeaderText="Status" SortExpression="AccountStatusID" UniqueName="Status">
                                <ItemTemplate>
                                    <%# ObjectToString(Container.DataItem, "Status") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <%# CoopFundsString %>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Remarks" HeaderText="Comment" SortExpression="Remarks" UniqueName="Remarks" />
                            <telerik:GridBoundColumn DataField="FundingUSGSCMF" HeaderText="USGS CMF" SortExpression="FundingUSGSCMF" UniqueName="FundingUSGSCMF"  Aggregate="Sum" ColumnGroupName="Funds" DataFormatString="{0:c2}" />
                            <telerik:GridBoundColumn DataField="FundingUSGSAllocation" UniqueName="FundingUSGSAllocation" HeaderText="USGS Allocation" ColumnGroupName="Funds" Aggregate="Sum" DataFormatString="{0:c}" />
                            <telerik:GridBoundColumn DataField="FundingCustomer" HeaderText="Customer" SortExpression="FundingCustomer" UniqueName="FundingCustomer" Aggregate="Sum" ColumnGroupName="Funds" DataFormatString="{0:c2}"  />
                            <telerik:GridBoundColumn DataField="FundingTotal" HeaderText="Total" SortExpression="FundingTotal" UniqueName="FundingTotal" Aggregate="Sum" ColumnGroupName="Funds" DataFormatString="{0:c0}"  />
                            <telerik:GridTemplateColumn HeaderText="Balance" UniqueName="Balance"  ColumnGroupName="Funds">
                                <ItemTemplate>
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    <%# String.Format("{0:c}", CoopAvailableFunds - USGSFundsTotal) %>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
