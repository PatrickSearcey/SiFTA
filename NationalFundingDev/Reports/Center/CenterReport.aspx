<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="CenterReport.aspx.cs" Inherits="NationalFundingDev.CenterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    <asp:Literal runat="server" ID="ltlTitle" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsCenterReportOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsCenterReportOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpCenterReportOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpSilk" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphInformation" runat="server">
    <h2>Center Report</h2>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsCenterReportOptions" MultiPageID="rmpCenterReportOptions" Orientation="HorizontalTop" Skin="Silk" AutoPostBack="true" OnTabClick="rtsCenterReportOptions_TabClick">
        <Tabs>
            <telerik:RadTab TabIndex="0" Text="Unfunded Real - Time Sites" Selected="true" />
            <telerik:RadTab TabIndex="1" Text="Funding Overview" />
            <telerik:RadTab TabIndex="2" Text="Contacts" />
            <telerik:RadTab TabIndex="3" Text="Collection Codes" />
            <telerik:RadTab Text="Exceptions">
                <Tabs>
                    <telerik:RadTab TabIndex="4" Text="Customers Missing Icons" />
                    <telerik:RadTab TabIndex="5" Text="Agreements Missing Documents" />
                    <telerik:RadTab TabIndex="6" Text="Agreement Status" />
                    <telerik:RadTab TabIndex="7" Text="Agreement Difference" />
                </Tabs>
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpCenterReportOptions" RenderSelectedPageOnly="true">
        <telerik:RadPageView TabIndex="0" runat="server" ID="rpvUnfundedRTSites" Selected="true">
            <telerik:RadAjaxPanel runat="server" ID="rapUnfundedRTSites" LoadingPanelID="ralpSilk">
                <center>
                    <asp:Literal runat="server" ID="ltlRTSiteUnfundedNumber" /> unfunded real-time sites after 
                    <telerik:RadDatePicker runat="server" ID="rdpUnfundedRTSitesDate" OnSelectedDateChanged="rdpUnfundedRTSitesDate_SelectedDateChanged" AutoPostBack="true" /> (Default: Todays Date)
                    <telerik:RadButton ID="btnViewMap" runat="server" OnClick="btnViewMap_Click" autopostback="true" Text="View National Map" />
                    <a href="https://my.usgs.gov/confluence/display/SIFTA/Get+a+station+off+the+Unfunded+Real-time+Sites+report">How to Remove Sites</a>
                </center>
                <br />
                <telerik:RadGrid ID="rgUnfundedSites" runat="server" AllowSorting="True" OnNeedDataSource="rgUnfundedSites_NeedDataSource"
                    AutoGenerateColumns="False"
                    Width="100%" Skin="Silk">
                    <MasterTableView CommandItemDisplay="None" ShowFooter="false" ShowGroupFooter="true">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Site Number" HeaderStyle-HorizontalAlign="Center" Visible="true" SortExpression="SiteNumber">
                                <ItemTemplate>
                                    <a style="color: #4a95a1;" href='<%# AppendBaseURL(string.Format("Site.aspx?SiteNumber={0}", Eval("SiteNumber"))) %>' target="_blank"><%# Eval("SiteNumber") %></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Name" HeaderText="Station Name" SortExpression="Name" />
                            <telerik:GridBoundColumn DataField="OfficeName" HeaderText="Office" SortExpression="OfficeName" />
                            <telerik:GridBoundColumn DataField="TypeCode" HeaderText="Type" SortExpression="TypeCode" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView TabIndex="1" runat="server" ID="rpvFundingOverview">
            <center><asp:Literal runat="server" ID="ltlAgreementsEnding" /> active agreements between <telerik:RadDatePicker runat="server" ID="rdpFOStart" OnSelectedDateChanged="agreementEndingChanged" AutoPostBack="true" /> and <telerik:RadDatePicker runat="server" ID="rdpFOEnd" OnSelectedDateChanged="agreementEndingChanged" AutoPostBack="true" /></center>
                <br />
            <telerik:RadButton runat="server" ID="rbFundingOverview" Skin="MetroTouch" Text="Download Excel File (.xlsx)[All Data]" OnClick="rbFundingOverviewDownload_Click" Width="100%" AutoPostBack="true" />
            <telerik:RadGrid GroupingEnabled="true" runat="server" ID="rgFundingOverview" AutoGenerateColumns="false" OnNeedDataSource="rgFundingOverview_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="FBMS Number" SortExpression="FBMSNumber" DataField="FBMSNumber">
                            <ItemTemplate>
                                <a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID"))) %>' target="_blank"><%# Eval("FBMSNumber") %></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Customer Code" SortExpression="CustomerCode" DataField="CustomerCode" />
                        <telerik:GridTemplateColumn HeaderText="Purchase Order Number" SortExpression="PurchaseOrderNumber" DataField="PurchaseOrderNumber">
                            <ItemTemplate>
                                <a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}", Eval("AgreementID"))) %>' target="_blank"><%# Eval("PurchaseOrderNumber") %></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Sales Document" SortExpression="SalesDocument" DataField="SalesDocument" />
                        <telerik:GridBoundColumn HeaderText="Start Date" SortExpression="StartDate" DataField="StartDate" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn HeaderText="End Date" SortExpression="EndDate" DataField="EndDate" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn HeaderText="USGS Sign Date" SortExpression="SignUSGSDate" DataField="SignUSGSDate" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn HeaderText="Customer Sign Date" SortExpression="SignCustomerDate" DataField="SignCustomerDate" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn HeaderText="Funding Type" SortExpression="FundsType" DataField="FundsType" />
                        <telerik:GridBoundColumn HeaderText="Billing Cycle" SortExpression="BillingCycleFrequency" DataField="BillingCycleFrequency" />
                        <telerik:GridBoundColumn HeaderText="USGS Funding" SortExpression="FundingUSGSCMFSum" DataField="FundingUSGSCMFSum" DataFormatString="{0:C0}" />
                        <telerik:GridBoundColumn HeaderText="Customer Funding" SortExpression="FundingCustomerSum" DataField="FundingCustomerSum" DataFormatString="{0:C0}" />
                        <telerik:GridBoundColumn HeaderText="Other Funding" SortExpression="FundingOtherSum" DataField="FundingOtherSum" DataFormatString="{0:C0}" />
                        <telerik:GridBoundColumn HeaderText="Total Funding" SortExpression="TotalFundingSum" DataField="TotalFundingSum" DataFormatString="{0:C0}" />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="CustomerName" FieldName="CustomerName" HeaderText="Customer" FormatString="<b>{0}</b>" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="CustomerName" FieldAlias="CustomerName" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView TabIndex="2" runat="server" ID="rpvContacts">
            <telerik:RadGrid runat="server" ID="rgContacts" AllowPaging="true" PageSize="20" OnNeedDataSource="rgContacts_NeedDataSource" GroupingEnabled="true" AutoGenerateColumns="false">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Name">
                            <ItemStyle VerticalAlign="Top" />
                            <ItemTemplate>
                                <b><%# Eval("Salutation") %> <%# Eval("FirstName") %> <%# Eval("MiddleName") %> <%# Eval("LastName") %></b><br />
                                <%# Eval("Title") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Address">
                            <ItemStyle VerticalAlign="Top" />
                            <ItemTemplate>
                                <b><%# Eval("Type") %> Address</b>
                                <%# Eval("StreetOne") %> <%# Eval("StreetTwo") %>, <%# Eval("City") %> <%# Eval("State") %> <%# Eval("ZipCode") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Contact Information">
                            <ItemStyle VerticalAlign="Top" />
                            <ItemTemplate>
                                <b>Work:</b><%# FormatPhone(Eval("PhoneWork")) %><br />
                                <b>Mobile:</b><%# FormatPhone(Eval("PhoneMobile")) %><br />
                                <b>Fax:</b><%# FormatPhone(Eval("PhoneFax")) %><br />
                                <b>Email:</b><a style="color: #4a95a1;" href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="CustomerName" FieldName="CustomerName" HeaderText="Customer" FormatString="<b>{0}</b>" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="CustomerName" FieldAlias="CustomerName" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView TabIndex="3" runat="server" ID="rpvCollectionCodes">
            <telerik:RadGrid runat="server" ID="rgCollectionCodes" AllowSorting="true" OnNeedDataSource="rgCollectionCodes_NeedDataSource" AutoGenerateColumns="false" GroupingEnabled="true">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Collection Code" DataField="ProperName" SortExpression="ProperName" AllowSorting="true" />
                        <telerik:GridBoundColumn HeaderText="Times Used" DataField="TimesUsed" SortExpression="TimesUsed" AllowSorting="true" />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="ProperCategory" FieldName="ProperCategory" HeaderText="Category" FormatString="<b>{0}</b>" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ProperCategory" FieldAlias="ProperCategory" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView TabIndex="4" runat="server" ID="rpvCustomerMissingIcons">
            <telerik:RadGrid runat="server" MasterTableView-CommandItemSettings-ShowAddNewRecordButton="false" ID="rgCustomersMissingIcons" MasterTableView-CommandItemSettings-ShowRefreshButton="true" MasterTableView-CommandItemDisplay="Top" AutoGenerateColumns="false" OnNeedDataSource="rgCustomersMissingIcons_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Customer">
                            <ItemTemplate>
                                <a href='<%# AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}&selected=2", Eval("CustomerID"))) %>' style="color: #4a95a1;" target="_blank"><%# Eval("Name") %></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView TabIndex="5" runat="server" ID="rpvAgreementsMissingDocuments">
            <telerik:RadAjaxPanel runat="server" ID="rapMissingDocuments">
                <center>Show agreements ending after
                <telerik:RadDatePicker runat="server" ID="rdpAgreementsMissingDocuments" AutoPostBack="true" OnSelectedDateChanged="rdpAgreementsMissingDocuments_SelectedDateChanged" /></center>
                <telerik:RadGrid runat="server" ID="rgAgreementsMissingDocuments" MasterTableView-CommandItemSettings-ShowAddNewRecordButton="false" MasterTableView-CommandItemDisplay="Top" MasterTableView-CommandItemSettings-ShowRefreshButton="true" AutoGenerateColumns="false" OnNeedDataSource="rgAgreementsMissingDocuments_NeedDataSource">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Agreements">
                                <ItemTemplate>
                                    <a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&selected=2", Eval("Key"))) %>' target="_blank"><%# Eval("Value") %></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvAgreementStatus" TabIndex="6">
            <telerik:RadAjaxPanel runat="server" ID="rapAgreementStatus">
                <center>Showing Agreements and Mods with end dates after <telerik:RadDatePicker runat="server" ID="rdpAgreementStatusEndDate" OnSelectedDateChanged="rdpAgreementStatusEndDate_SelectedDateChanged" AutoPostBack="true" /></center>
                <br />
                <br />
                <telerik:RadGrid runat="server" ID="rgAgreementStatus" MasterTableView-CommandItemDisplay="Top" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="rgAgreementStatus_NeedDataSource" AllowFilteringByColumn="true">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView>
                        <CommandItemTemplate>
                            <telerik:RadButton runat="server" ID="btnClearFilters" Text="Clear Filters" Skin="Silk" OnClick="btnClearFilters_Click" AutoPostBack="true" Width="100%" />
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Customer" DataField="CustomerName" SortExpression="CustomerName" AllowSorting="true" AllowFiltering="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <%# Eval("CustomerName") %>
                                    <%--<a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID"))) %>' target="_blank"><%# Eval("CustomerName") %></a>--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Agreement" DataField="AgreementName" SortExpression="AgreementName" AllowSorting="true" AllowFiltering="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <%# Eval("AgreementName") %>
                                    <%--<a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}", Eval("AgreementID"))) %>' target="_blank"><%# Eval("AgreementName") %></a>--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Request" DataField="Request" SortExpression="Request" AllowSorting="true" AllowFiltering="true" DataType="System.DateTime" FilterListOptions="VaryByDataType">
                                <ItemTemplate>
                                    <%# DateLink(Eval("AgreementID"),Eval("Request"), 6) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Review Sent" DataField="ReviewSent" SortExpression="ReviewSent" AllowSorting="true" AllowFiltering="true" DataType="System.DateTime" FilterListOptions="VaryByDataType">
                                <ItemTemplate>
                                    <%# DateLink(Eval("AgreementID"),Eval("ReviewSent"), 6) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Review Returned" DataField="ReviewReturn" SortExpression="ReviewReturn" AllowSorting="true" AllowFiltering="true" DataType="System.DateTime" FilterListOptions="VaryByDataType">
                                <ItemTemplate>
                                    <%# DateLink(Eval("AgreementID"),Eval("ReviewReturn"), 6) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Sign USGS" DataField="SignUSGSDate" SortExpression="SignUSGSDate" AllowSorting="true" AllowFiltering="true" DataType="System.DateTime" FilterListOptions="VaryByDataType">
                                <ItemTemplate>
                                    <%# DateLink(Eval("AgreementID"),Eval("SignUSGSDate"), 0) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Sign Customer" DataField="SignCustomerDate" SortExpression="SignCustomerDate" AllowSorting="true" AllowFiltering="true" DataType="System.DateTime" FilterListOptions="VaryByDataType">
                                <ItemTemplate>
                                    <%# DateLink(Eval("AgreementID"),Eval("SignCustomerDate"), 0) %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvAgreementDifference" TabIndex="7">
            <telerik:RadAjaxPanel runat="server" ID="rapAgreementDifference">
                <center>Showing Agreements and Mods with end dates after <telerik:RadDatePicker runat="server" ID="rdpAgreementDifference" OnSelectedDateChanged="rdpAgreementDifference_SelectedDateChanged" AutoPostBack="true" /></center>
                <br />
                <br />
                <telerik:RadGrid runat="server" ID="rgAgreementDifference" MasterTableView-CommandItemDisplay="Top" AllowPaging="true" PageSize="50" Width="100%" AllowSorting="true" AutoGenerateColumns="false" OnNeedDataSource="rgAgreementDifference_NeedDataSource" OnPreRender="rgAgreementDifference_PreRender">
                    <MasterTableView CommandItemSettings-ShowRefreshButton="true">
                        <CommandItemStyle />
                        <CommandItemTemplate>
                            <table style="margin: 10px; width: 100%;">
                                <tr>

                                    <td style="background-color: #FFCEC2; width: 150px;" title="The amount of money assigned to the agreement or agreement mod is higher than the money spent on sites and studies combined.">
                                        <center>Recorded > Allotted</center>
                                    </td>
                                    <td align="right" style="padding-right: 10px;">
                                        <telerik:RadButton runat="server" ID="rbRefreshAgreementDifference" Text="Refresh" AutoPostBack="true" OnClick="rbRefreshAgreementDifference_Click" />
                                    </td>
                                </tr>
                                <tr>

                                    <td style="background-color: #FFFFAD;" title="The amount of money assigned to the agreement or agreement mod is less than the money spent on sites and studies combined.">
                                        <center>Allotted > Recorded</center>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Agreement" DataField="Name" SortExpression="Name" AllowSorting="true">
                                <ItemTemplate>
                                    <a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}", Eval("AgreementID"))) %>' target="_blank"><%# Eval("Name") %></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataFormatString="{0:c0}" HeaderText="USGS" DataField="USGSCMFDifference" SortExpression="USGSCMFDifference" UniqueName="USGS" AllowSorting="true" />
                            <telerik:GridBoundColumn DataFormatString="{0:c0}" HeaderText="Customer" DataField="CustomerDifference" SortExpression="CustomerDifference" UniqueName="Customer" AllowSorting="true" />
                            <telerik:GridBoundColumn DataFormatString="{0:c0}" HeaderText="Other" DataField="OtherDifference" SortExpression="OtherDifference" UniqueName="Other" AllowSorting="true" />
                            <telerik:GridBoundColumn DataFormatString="{0:c0}" HeaderText="Total" DataField="TotalDifference" SortExpression="TotalDifference" UniqueName="Total" AllowSorting="true" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
