<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="NationalFundingDev.CustomerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    <style type="text/css">
       .ruBrowse
       {
           background-position: 0 -46px !important;
           width: 150px !important;
       }
        #ContentImage
        {
            height:110px !important;
            width: 110px !important;
        }
        #ContentImage #divImage
        {
            height:110px !important;
            width: 110px !important;
            position: absolute;
            left: 5px;
            top: 161px;
        }
        #ContentInformation
        {
            position: absolute;
            left: 125px;
            top: 175px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsCustomerOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsCustomerOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpCustomerOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAgreements">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreements" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbAgreements">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreements" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgContacts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgContacts" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Content4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsCustomerOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpCustomerOptions" />
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
    <telerik:RadAjaxLoadingPanel ID="ralp" runat="server" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
    <div id="divImage" style="padding-top: 10px; padding-right: 5px;">
        <asp:Image runat="server" ID="imgCustLogo" Height="100px" Width="100px" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <b><%: customer.Code %> - <%: customer.Name %></b><br />
    FBMS Customer Number: <%: customer.Number %><br />
    <a id="CustomerWebsite" href="<%= customer.URL %>" target="_blank">Customer Website</a><br /> 
    <script type="text/javascript">
        if (!$("#CustomerWebsite").attr("href")) $("#CustomerWebsite").hide();
    </script>
    <b style="color:mediumpurple"><i>Remarks: <%= String.IsNullOrEmpty(customer.Remarks) == true ? "N/A" : customer.Remarks %></i></b><br />
    <%--//Removed 11/4/2014 for to remove tags--%> 
    <a href='<%= String.Format("Center.aspx?OrgCode={0}", customer.OrgCode) %>' style="color: orange;" >Center Home</a> >> Customer Portal
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsCustomerOptions" MultiPageID="rmpCustomerOptions" Skin="Silk" Width="100%" AutoPostBack="true" Orientation="HorizontalTop" OnTabClick="rtsCustomerOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Agreements"  TabIndex="0" />
            <telerik:RadTab Text="Contacts" TabIndex="1" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpCustomerOptions" RenderSelectedPageOnly="true">
        <!-- AGREEMENTS -->
        <telerik:RadPageView ID="rpvAgreements" runat="server"  TabIndex="0">
            <div style="width:100%;">
                <div style="float:left;">
                    <telerik:RadComboBox ID="rcbAgreements" runat="server" Skin="Silk" OnSelectedIndexChanged="rcbAgreements_SelectedIndexChanged"
                        AutoPostBack="true" Width="200px">
                        <Items>
                            <telerik:RadComboBoxItem Value="All" Text="All Agreements" />
                            <telerik:RadComboBoxItem Value="Recent" Text="Agreements (recent)" Selected="true" />
                            <telerik:RadComboBoxItem Value="Active" Text="Active Agreements" />
                            <telerik:RadComboBoxItem Value="Sites" Text="Agreements With Sites" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div style="float:right;">
                    <asp:Panel runat="server" ID="pnlCopyAgreeement" Visible="false" HorizontalAlign="Right">
                        <asp:CheckBox runat="server" ID="cbCopyAgreement" AutoPostBack="true" OnCheckedChanged="cbCopyAgreement_CheckedChanged" />
                        Allow Copy Agreement Functionality
                    </asp:Panel>
                </div>
            </div>
            <br /><br /><br />
            <telerik:RadGrid ID="rgAgreements" runat="server" AutoGenerateColumns="False"
                OnNeedDataSource="rgAgreements_NeedDataSource" OnUpdateCommand="rgAgreements_UpdateCommand" OnInsertCommand="rgAgreements_InsertCommand" OnItemCommand="rgAgreements_ItemCommand"
                AllowSorting="True" AllowMultiRowSelection="false" Skin="Silk" Width="100%">
                <MasterTableView DataKeyNames="AgreementID" CommandItemDisplay="None" Name="tvAgreement"
                    AlternatingItemStyle-BackColor="White" EditMode="EditForms">
                    <CommandItemSettings ShowRefreshButton="false"
                        AddNewRecordText="Add New Agreement" />
                    <EditFormSettings UserControlName="Controls/RadGrid/AgreementControl.ascx" EditFormType="WebUserControl" />
                    <Columns>
                        <telerik:GridButtonColumn UniqueName="copyAgreement" Visible="false" CommandName="Copy" ButtonType="ImageButton" ImageUrl="Images/copyIcon.gif" ConfirmText="Are you sure you want to copy this agreement to the current fiscal year?" />
                        <telerik:GridTemplateColumn UniqueName="Edit" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbAgreementId" runat="server" ForeColor="#4a95a1" ToolTip="Open Edit Interface"
                                    PostBackUrl='<%# "Agreement.aspx?AgreementID=" + Eval("AgreementID") %>'
                                     >
                                    <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/editPencil.png" ToolTip="Open Edit Interface" Height="15" Width="15" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridTemplateColumn HeaderText="Agreement" SortExpression="PurchaseOrderNumber" DataField="PurchaseOrderNumber" UniqueName="PurchaseOrderNumber">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbAgreementOverview" runat="server" ForeColor="#4a95a1"
                                    PostBackUrl='<%# "Reports/Agreement/AgreementReport.aspx?AgreementID=" + Eval("AgreementID") %>'
                                    Text='<%# Eval("PurchaseOrderNumber") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Sales Document" DataField="SalesDocument" SortExpression="SalesDocument" UniqueName="SalesDocument" />
                        <telerik:GridBoundColumn HeaderText="Start" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="End" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="USGS Sign" DataField="SignUSGSDate" SortExpression="SignUSGSDate" UniqueName="SignUSGSDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="Cust Sign" DataField="SignCustomerDate" SortExpression="SignCustomerDate" UniqueName="SignCustomerDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="Type" DataField="FundsType" SortExpression="FundsType" UniqueName="FundsType" />
                        <telerik:GridBoundColumn HeaderText="Cycle" DataField="BillingCycleFrequency" SortExpression="BillingCycleFrequency" UniqueName="BillingCycleFrequency" />
                        <telerik:GridBoundColumn HeaderText="USGS CMF" DataField="FundingUSGSCMFSum" SortExpression="FundingUSGSCMFSum" UniqueName="FundingUSGSCMFSum" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FundingCustomerSum" SortExpression="FundingCustomerSum" UniqueName="FundingCustomerSum" DataFormatString="{0:c0}" />
                        <telerik:GridTemplateColumn HeaderText="Total">
                            <ItemTemplate>
                                <%# String.Format("{0:c0}", Convert.ToDouble(Eval("FundingUSGSCMFSum")) + Convert.ToDouble(Eval("FundingCustomerSum"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <!-- CONTACTS -->
        <telerik:RadPageView ID="rpvContacts" runat="server" TabIndex="1">
            <telerik:RadGrid runat="server" ID="rgContacts" Skin="Silk" Width="100%" AutoGenerateColumns="false" OnNeedDataSource="rgContacts_NeedDataSource" OnDetailTableDataBind="rgContacts_DetailTableDataBind" OnPreRender="rgContacts_PreRender" OnUpdateCommand="rgContacts_UpdateCommand" OnInsertCommand="rgContacts_InsertCommand" OnDeleteCommand="rgContacts_DeleteCommand">
                <MasterTableView DataKeyNames="CustomerContactID" Name="Contact" >
                    <CommandItemSettings AddNewRecordText="Add New Contact" ShowRefreshButton="false" />
                    <EditFormSettings UserControlName="Controls/RadGrid/ContactEditForm.ascx"
                        EditFormType="WebUserControl" />
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="CustomerContactAddressID" Name="Address">
                            <EditFormSettings UserControlName="Controls/RadGrid/AddressEditForm.ascx"
                                EditFormType="WebUserControl" />
                            <CommandItemSettings AddNewRecordText="Add New Address" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit"
                                    Text="Edit" Visible="true" UniqueName="EditAddress" />
                                <telerik:GridBoundColumn HeaderText="Address Type" DataField="Type" />
                                <telerik:GridBoundColumn HeaderText="Address" DataField="StreetOne" />
                                <telerik:GridBoundColumn HeaderText=" " DataField="StreetTwo" />
                                <telerik:GridBoundColumn HeaderText="City" DataField="City" />
                                <telerik:GridBoundColumn HeaderText="State" DataField="State" />
                                <telerik:GridBoundColumn HeaderText="Zip Code" DataField="ZipCode" />
                                <telerik:GridButtonColumn ConfirmText="Removing this address will remove it from all agreements it was tied to. Do you wish to continue?" ButtonType="ImageButton"
                                    CommandName="Delete" Text="Remove" UniqueName="DeleteAddress" Visible="false" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit"
                            Text="Edit" Visible="true" UniqueName="Edit" />
                        <telerik:GridTemplateColumn HeaderText="Name">
                            <ItemTemplate>
                                <telerik:RadButton ForeColor="#4a95a1" Font-Underline="true" CommandName="Edit" runat="server" ID="rbName" ButtonType="ToggleButton" CommandArgument='<%# Eval("CustomerContactID") %>' ReadOnly="<%# !user.CanUpdate %>" Text='<%# String.Format("{0} {1} {2} {3}", Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Title" DataField="Title" />
                        <telerik:GridTemplateColumn HeaderText="Work" DataField="PhoneWork">
                            <ItemTemplate>
                                <%# FormatPhone(Eval("PhoneWork")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Mobile" DataField="PhoneMobile">
                            <ItemTemplate>
                                <%# FormatPhone(Eval("PhoneMobile")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Fax" DataField="PhoneFax">
                            <ItemTemplate>
                                <%# FormatPhone(Eval("PhoneFax")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" DataFormatString='<a style="color:#4a95a1;" href="mailto:{0}" target="_blank">{0}</a>' />
                        <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" />
                        <telerik:GridButtonColumn ConfirmText="Removing this contact will remove all addresses and agreements this contact was assigned to. Do you wish to continue?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="DeleteContact" Visible="false" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvEditCustomer" TabIndex="2">
            <div class="SectionContent" style="width: 800px;">
                <h2>Edit Customer</h2>
                <table cellpadding="3px">
                    <tr>
                        <td align="right">
                            <telerik:RadBinaryImage runat="server" ID="imgCustomer" 
                                Width="100px" Height="100px" ResizeMode="Fit"></telerik:RadBinaryImage>
                        </td>
                        <td valign="bottom">
                            <telerik:RadAsyncUpload runat="server" ID="rauImage" MultipleFileSelection="Disabled" TargetFolder="~/icons/" Localization-Select="Upload Customer Icon"   AllowedFileExtensions="jpg,jpeg,png,gif"
                                DisableChunkUpload="true" Skin="Silk" MaxFileInputsCount="1" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Customer Code:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbCustomerCd" runat="server" EmptyMessage="Example: TX014"  EmptyMessageStyle-Font-Italic="true" Skin="Silk" Width="450px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Agreement Type:
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="rcbAgreementType"
                                Skin="Silk" AppendDataBoundItems="true" DataSourceID="ldsAgreementType"
                                DataTextField="Type" DataValueField="CustomerAgreementTypeID">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="rfvAgreementType" runat="server" ErrorMessage="* Required"
                                ControlToValidate="rcbAgreementType" ValidationGroup="vgCustomer" Font-Size="X-Small" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">FBMS Customer Number:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="rntbCustomerNo"  Type="Number" EmptyMessage="Example: 6000000481" Skin="Silk">
                                <NumberFormat AllowRounding="false" DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Customer Name: 
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbCustomerName" runat="server" Skin="Silk" EmptyMessage="Example: City of Austin" EmptyMessageStyle-Font-Italic="true"
                                Text='<%# customer.Name %>' Width="450px" />
                            <asp:RequiredFieldValidator ID="rfvCustomerNm" runat="server" ErrorMessage="* Required"
                                ControlToValidate="rtbCustomerName" ValidationGroup="vgCustomer" Font-Size="X-Small" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Customer Abbreviation:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbCustomerAbbrev" runat="server" Skin="Silk" EmptyMessage="Example: Austin" EmptyMessageStyle-Font-Italic="true"
                                Text='<%# customer.Abbreviation %>' Width="450px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Customer Url: 
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbCustomerUrl" runat="server" Skin="Silk" EmptyMessage="Example: http://www.austintexas.gov/" EmptyMessageStyle-Font-Italic="true"
                                Text='<%# customer.URL %>' Width="450px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">Remarks: 
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbRemarks" runat="server" Height="50px" TextMode="MultiLine" Skin="Silk"
                                Text='<%# customer.Remarks %>' Width="450px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="ltCustomerTin" runat="server" Text="Tax Identification Number (TIN):" Visible="true" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbCustomerTin" runat="server" Skin="Silk" EmptyMessage="Example: 74-6000085" EmptyMessageStyle-Font-Italic="true"
                                Text='<%# customer.TaxIdentificationNumber %>' Visible="true" Width="450px" />
                        </td>
                    </tr>
                    <%--//Removed 11/4/2014 to remove tags --%>
                    <%--<tr>
                        <td align="right" valign="top">Tags:
                        </td>
                        <td>
                            <telerik:RadAutoCompleteBox runat="server" ID="racbTags" Skin="Black" Delimiter=";" AllowCustomEntry="true" Width="450px" DataTextField="Tag" DataSourceID="ldsTags" />
                            (Use ; to seperate tags)
                        </td>
                    </tr>--%>
                    <tr>
                        <td></td>
                        <td>
                            <telerik:RadButton ID="btnUpdate" Text="Update" CommandName="Update" CausesValidation="True" runat="server" Visible="true" Skin="Silk" OnClick="btnUpdate_Click" AutoPostBack="true" ValidationGroup="vgCustomer" />
                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" Skin="Silk" OnClick="btnCancel_Click" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
                <asp:LinqDataSource ID="ldsAgreementType" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutCustomerAgreementTypes"></asp:LinqDataSource>
                <asp:LinqDataSource ID="ldsTags" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutTags"></asp:LinqDataSource>
            </div>
        </telerik:RadPageView>
        <!-- Cooperative Funding -->
        <telerik:RadPageView runat="server" ID="rpvCooperativeFunding" TabIndex="3">
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
                PageSize="10" AllowMultiRowSelection="False" OnNeedDataSource="rgCoopFunding_NeedDataSource" OnDetailTableDataBind="rgCoopFunding_DetailTableDataBind" OnInsertCommand="rgCoopFunding_InsertCommand" OnUpdateCommand="rgCoopFunding_UpdateCommand">
                <PagerStyle PageSizes="10" Mode="NextPrevAndNumeric" />
                <ItemStyle />
                <MasterTableView Width="100%" DataKeyNames="AgreementID" AllowMultiColumnSorting="True">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <GroupHeaderItemStyle ForeColor="Black" />
                    <DetailTables>
                        <telerik:GridTableView AllowSorting="true" InsertItemDisplay="Bottom"  CommandItemDisplay="Top" DataKeyNames="CooperativeFundingID"
                            Name="Accounts" Width="100%" EditMode="EditForms" AllowPaging="false">
                            <EditFormSettings UserControlName="Controls/RadGrid/CoopFundingControl.ascx" EditFormType="WebUserControl" />
                            <HeaderStyle BackColor="#2dabc1" ForeColor="White"  Font-Bold="true" Font-Size="Small" />
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
