<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="National.aspx.cs" Inherits="NationalFundingDev.Reports.National.National" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    National Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
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
            <telerik:AjaxSetting AjaxControlID="rtsNational">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpNational" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCollectionCodes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCollectionCodes" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />

    <telerik:RadTabStrip runat="server" ID="rtsNational" MultiPageID="rmpNational">
        <Tabs>
            <telerik:RadTab Text="One Sided Agreements" TabIndex="1" />
            <telerik:RadTab Text="Collection Codes" TabIndex="2" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="rmpNational">
        <telerik:RadPageView runat="server" TabIndex="1">
            <telerik:RadGrid runat="server" ID="rgOneSidedAgreements" AutoGenerateColumns="false" OnNeedDataSource="rgOneSidedAgreements_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Agreement" DataField="PurchaseOrderNumber">
                            <ItemTemplate>
                                <a style="color: #2DABC1;" target="_blank" href='<%# AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}", Eval("AgreementID"))) %>'><%# String.Format("{0}", Eval("PurchaseOrderNumber")) %></a> - <%# Eval("CustomerName") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="MPC" DataField="MatchPairCode" />
                        <telerik:GridBoundColumn HeaderText="Start" DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="End" DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="USGS Sign" DataField="USGSSignDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="Cust Sign" DataField="CustomerSignDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridNumericColumn DataField="FundingUSGSCMF" SortExpression="FundingUSGSCMF" AllowSorting="true"
                            UniqueName="FundingUSGSCMF" HeaderText="USGS CMF" DataFormatString="{0:c0}" DataType="System.Decimal"
                            Aggregate="Sum" FooterAggregateFormatString="{0:c0}"
                            ColumnEditorID="ceFunds" />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="CenterName" FieldAlias="Center" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="CenterName" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" TabIndex="2">
            <telerik:RadGrid runat="server" ID="rgCollectionCodes" AutoGenerateColumns="false" MasterTableView-AllowSorting="true" OnNeedDataSource="rgCollectionCodes_NeedDataSource">
                <MasterTableView CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <telerik:RadButton runat="server" ID="ExportToExcelButton" Text="Download Excel" Width="100%" OnClick="rbDownloadCollectionCodes_Click" />
                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Code" DataField="Code" AllowSorting="true" SortExpression="Code" />
                        <telerik:GridBoundColumn HeaderText="Description" DataField="Description" AllowSorting="true" SortExpression="Description" />
                        <telerik:GridBoundColumn HeaderText="Occurences" DataField="Occurences" AllowSorting="true" SortExpression="Occurences" Aggregate="Sum" />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Name" HeaderValueSeparator="" HeaderText="- " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Name" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Category" HeaderValueSeparator="" HeaderText="- " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Category" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
