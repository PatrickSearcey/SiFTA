<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Center.aspx.cs" Inherits="NationalFundingDev.CenterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsCenterOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsCenterOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpCenterOptions" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCustomers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCustomers" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCoopFunding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbShowAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                    <telerik:AjaxUpdatedControl ControlID="rsbCoopFunding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rsbCoopFunding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <b><%: center.Name %> Home</b><br />
    <%: CenterAddress %><br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsCenterOptions" MultiPageID="rmpCenterOptions" Orientation="HorizontalTop" Skin="Silk" AutoPostBack="true" OnTabClick="rtsCenterOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Customers" Selected="true" TabIndex="0" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpCenterOptions" RenderSelectedPageOnly="true">
        <!-- Customers Section -->
        <telerik:RadPageView ID="rpvCustomers" runat="server" Selected="true" TabIndex="0">
            <telerik:RadComboBox runat="server"  ID="rcbCustomer" OnSelectedIndexChanged="rcbCustomer_SelectedIndexChanged" AutoPostBack="true" Skin="Silk">
                <Items>
                    <telerik:RadComboBoxItem Text="All Customers" Value="All" />
                    <telerik:RadComboBoxItem Text="Active Customers" Value="Active" />
                    <telerik:RadComboBoxItem Text="Customers (Recent)" Value="Recent" Selected="true" />
                </Items>
            </telerik:RadComboBox>
            <br />
            <br />
            <telerik:RadGrid runat="server" ID="rgCustomers" AllowSorting="True"
                OnNeedDataSource="rgCustomers_NeedDataSource" OnUpdateCommand="rgCustomers_UpdateCommand" OnInsertCommand="rgCustomers_InsertCommand"
                AutoGenerateColumns="False" Width="99%" Skin="Silk">
                <MasterTableView AutoGenerateColumns="False"
                    DataKeyNames="CustomerID" CommandItemDisplay="None" ShowFooter="true"
                    AlternatingItemStyle-BackColor="White" EditMode="EditForms">
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/CustomerControl.ascx" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="27px" DataType="System.SByte" DataField="Icon">
                            <ItemTemplate>
                                <a href='<%# String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID")) %>' style="color: #4a95a1;">
                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%# GetIconBytes(Eval("Icon")) %>'
                                AutoAdjustImageControlSize="false" Height="25px" Width="25px" ToolTip='<%#Eval("Name", "{0} Icon") %>'
                                AlternateText='<%#Eval("Name", "{0} Icon") %>'></telerik:RadBinaryImage>
                                    </a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Name" SortExpression="Name" DataField="Name" UniqueName="Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbAgreementId" runat="server" ForeColor="#4a95a1"
                                    PostBackUrl='<%# "Customer.aspx?CustomerID=" + Eval("CustomerID") %>'
                                    Text='<%# Eval("Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadTextBox runat="server" ID="rtbCName" Text='<%# Eval("Name") %>' />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Code" HeaderText="Cust. Code"
                            SortExpression="Code" UniqueName="Code" />
                        <telerik:GridBoundColumn DataField="Number" HeaderText="FBMS No."
                            SortExpression="Number" UniqueName="Number" DataType="System.Int64" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <!-- Cooperative Funding -->
        <telerik:RadPageView runat="server" ID="rpvCooperativeFunding" TabIndex="1">
            <table>
                <tr>
                    <td>
                        <telerik:RadSearchBox Width="250px" runat="server" ID="rsbCoopFunding" OnSearch="rsbCoopFunding_Search" Skin="Silk" DropDownSettings-Height="0px" />
                    </td>
                    <td>
                        <telerik:RadButton runat="server" ID="rbShowAll" Text="Show All"
                            Skin="Silk" OnClick="rbShowAll_Click" /> <telerik:RadButton runat="server" ID="rbViewReport" Text="View Report"
                            Skin="Silk" OnClick="rbViewReport_Click" /> 
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="rgCoopFunding" runat="server" Width="100%"
                AutoGenerateColumns="False" Skin="Silk" AllowPaging="true" 
                PageSize="10" AllowMultiRowSelection="False" OnNeedDataSource="rgCoopFunding_NeedDataSource" AllowSorting="true" OnDetailTableDataBind="rgCoopFunding_DetailTableDataBind" OnInsertCommand="rgCoopFunding_InsertCommand" OnUpdateCommand="rgCoopFunding_UpdateCommand">
                <PagerStyle PageSizes="10" Mode="NextPrevAndNumeric" />
                <ItemStyle />
                <MasterTableView Width="100%" DataKeyNames="AgreementID" AllowMultiColumnSorting="True">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <GroupHeaderItemStyle ForeColor="Black" />
                    <DetailTables>
                        <telerik:GridTableView AllowSorting="true" InsertItemDisplay="Bottom"  CommandItemDisplay="Top" ShowGroupFooter="true" ShowFooter="true" DataKeyNames="CooperativeFundingID"
                            Name="Accounts" Width="100%" EditMode="EditForms" AllowPaging="false">
                            <EditFormSettings UserControlName="Controls/RadGrid/CoopFundingControl.ascx" EditFormType="WebUserControl" />
                            <HeaderStyle BackColor="LightBlue" ForeColor="Black"  Font-Bold="true" />
                            <CommandItemSettings AddNewRecordText="Add New Account" ShowRefreshButton="false"  />
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" HeaderStyle-BackColor="#acdbe3" HeaderStyle-Font-Size="Small"
                                    CommandName="Edit" Text="Edit" UniqueName="Edit" />
                                <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FiscalYear" SortExpression="FiscalYear" AllowSorting="true" />
                                <telerik:GridTemplateColumn HeaderText="Mod" DataField="ModNumber" >
                                    <ItemTemplate>
                                        <%# (Eval("ModNumber").ToString() == "0") ? "" : String.Format("{0}", Eval("ModNumber")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Account" DataField="AccountNumber" FooterAggregateFormatString="Agreement Total" FooterStyle-HorizontalAlign="Right" Aggregate="Count" />
                                <telerik:GridBoundColumn HeaderText="USGS" DataField="FundingUSGSCMF" DataFormatString="{0:c2}" Aggregate="Sum" />
                                <telerik:GridBoundColumn HeaderText="Cooperator" DataField="FundingCustomer" DataFormatString="{0:c2}" Aggregate="Sum" />
                                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                                <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" />
                                <telerik:GridBoundColumn HeaderText="Last Edited" DataField="ModifiedDate" DataFormatString="{0:d}" />
                                <telerik:GridBoundColumn HeaderText="Edited By" DataField="ModifiedBy" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Code" HeaderText="Customer" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e5e4e4">
                            <ItemTemplate>
                                <a style="color: #2dabc1" title='<%# Eval("Name")  %>' href='<%# string.Format("Customer.aspx?CustomerID={0}",Eval("CustomerID")) %>'><%# Eval("Code")%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Number" HeaderText="Number" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e5e4e4" />
                        <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" HeaderText="Agreement">
                            <ItemTemplate>
                                <a style="color: #2dabc1" href='<%# string.Format("Agreement.aspx?AgreementID={0}",Eval("AgreementID")) %>'><%# Eval("PurchaseOrderNumber")%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="MatchPairCode" HeaderText="MP" ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="SalesDocument" HeaderText="Sales Order" ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:d}" HeaderText="Start"
                            ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:d}" HeaderText="End"  />
                        <telerik:GridBoundColumn DataField="SignUSGSDate" DataFormatString="{0:d}" HeaderText="USGS Sign"/>
                        <telerik:GridBoundColumn DataField="SignCustomerDate" DataFormatString="{0:d}" HeaderText="Cust. sign"  />
                        <telerik:GridBoundColumn DataField="FundsType" HeaderText="Fund Type"  />
                        <telerik:GridBoundColumn DataField="BillingCycleFrequency" HeaderText="Cycle"  />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Name" FormatString="{0}"  />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Name" FormatString="{0}"  />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
