<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Agreement.aspx.cs" Inherits="NationalFundingDev.AgreementPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Agreement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
    <style type="text/css">
        #ContentImage {
            height: 115px !important;
            width: 110px !important;
        }

            #ContentImage #divImage {
                height: 115px !important;
                width: 110px !important;
                position: absolute;
                left: 5px;
                top: 161px;
            }

        #ContentInformation {
            position: absolute;
            left: 125px;
            top: 175px;
        }

        .templateButton {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsAgreementOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsAgreementOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpAgreementOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAgreements">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreements" LoadingPanelID="ralpSilk" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbUploadDocuments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreementDocuments" LoadingPanelID="ralpSilk" />
                    <telerik:AjaxUpdatedControl ControlID="cbJFADocument" />
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
        <ClientEvents />
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpSilk" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphImage" runat="server">
    <div id="divImage" style="padding-top: 10px; padding-right: 5px;">
        <asp:Image runat="server" ID="imgCustLogo" Height="100px" Width="100px" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphInformation" runat="server">
    <b>Agreement: <%: agreement.PurchaseOrderNumber %></b>
    <br />
    <a href="<%: String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID) %>">Customer: <%: agreement.Customer.Code %> - <%: agreement.Customer.Name %></a><br />
    FBMS Customer Number: <%: agreement.Customer.Number %><br />
    Sales Order Number: <%: agreement.SalesDocument %><br />
    <a id="CustomerWebsite" href="<%= agreement.Customer.URL %>" target="_blank">Customer Website</a><br />
    <script type="text/javascript">
        if (!$("#CustomerWebsite").attr("href")) $("#CustomerWebsite").hide();
    </script>
    <%--//Removed 11/4/2014  to remove tags --%>
    <a href='<%= String.Format("Center.aspx?OrgCode={0}", agreement.Customer.OrgCode) %>' style="color: orange;">Center Home</a> >> <a href='<%= String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID) %>' style="color: orange;">Customer Portal</a> >> Agreement Portal
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwFundingSummary" Skin="MetroTouch" runat="server" ShowContentDuringLoad="false"
                Title="Funding Summary" Behaviors="Default" Height="500" Width="500" RestrictionZoneID="ContentTable">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadTabStrip runat="server" ID="rtsAgreementOptions" MultiPageID="rmpAgreementOptions" Skin="Silk" Width="100%" Orientation="HorizontalTop" OnTabClick="rtsAgreementOptions_TabClick">
        <Tabs>
            <telerik:RadTab TabIndex="0" Text="Agreement" Selected="true" />
            <telerik:RadTab TabIndex="1" Text="Contacts" />
            <telerik:RadTab Text="Documents">
                <Tabs>
                    <telerik:RadTab TabIndex="5" Text="Upload Documents" />
                    <telerik:RadTab TabIndex="4" Text="JFA Document Generator" />
                    <telerik:RadTab TabIndex="20" Text="Summary Attachment" Target="_blank" />
                    <telerik:RadTab TabIndex="21" Text="Detailed Attachment" Target="_blank" />
                </Tabs>
            </telerik:RadTab>
            <telerik:RadTab TabIndex="2" Text="Site Funding" />
            <telerik:RadTab TabIndex="3" Text="Studies / Support" />
            <telerik:RadTab TabIndex="6" Text="Agreement Log" />
            <telerik:RadTab Text="Account Funding" Value="Coop" >
                <Tabs>
                    <telerik:RadTab TabIndex="7" Text="Account Summary" />
                    <telerik:RadTab TabIndex="8" Text="Fund Sources" />
                </Tabs>
            </telerik:RadTab>
            <telerik:RadTab TabIndex="9" Text="Agreement Overview" Target="_blank" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpAgreementOptions" Width="100%">
        <telerik:RadPageView runat="server" ID="rpvModifyAgreement" TabIndex="0" Selected="true">
            <div runat="server" id="agLock" style="float: right; margin-bottom: 15px;">
                <span runat="server" id="agLockText"></span>
                <asp:Button runat="server" ID="agLockButton" OnClick="agLockButtonClick" visible="false"></asp:Button>
            </div>
            <telerik:RadGrid ID="rgAgreements" runat="server" AutoGenerateColumns="False"
                OnNeedDataSource="rgAgreements_NeedDataSource" OnUpdateCommand="rgAgreements_UpdateCommand" OnInsertCommand="rgAgreements_InsertCommand" OnDeleteCommand="rgAgreements_DeleteCommand" OnPreRender="rgAgreements_PreRender"
                AllowSorting="True" AllowMultiRowSelection="false" Skin="Silk" Width="100%">
                <MasterTableView DataKeyNames="AgreementModID" CommandItemDisplay="None" CommandItemSettings-AddNewRecordText="Add New Agreement Mod" Name="tvAgreement"
                    AlternatingItemStyle-BackColor="White" EditMode="EditForms">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <EditFormSettings UserControlName="Controls/RadGrid/AgreementModControl.ascx" EditFormType="WebUserControl" />

                    <Columns>
                        <telerik:GridButtonColumn UniqueName="copyAgreement" Visible="false" CommandName="Copy" ButtonType="ImageButton" ImageUrl="Images/copyIcon.gif" ConfirmText="Are you sure you want to copy this agreement?" />
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridBoundColumn HeaderText="Mod" SortExpression="Number" DataField="ModName" UniqueName="ModName" />
                        <telerik:GridBoundColumn HeaderText="Purchase Order Number" SortExpression="PurchaseOrderNumber" DataField="PurchaseOrderNumber" UniqueName="PurchaseOrderNumber" />
                        <telerik:GridBoundColumn HeaderText="MPC" DataField="MatchPair" SortExpression="MatchPair" UniqueName="MatchPair" Visible="false" />
                        <telerik:GridBoundColumn HeaderText="Start" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="End" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="USGS Sign" DataField="SignUSGSDate" SortExpression="SignUSGSDate" UniqueName="SignUSGSDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="Cust Sign" DataField="SignCustomerDate" SortExpression="SignCustomerDate" UniqueName="SignCustomerDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn HeaderText="Billing Type" DataField="FundsType" SortExpression="FundsType" UniqueName="FundsType" />
                        <telerik:GridBoundColumn HeaderText="Billing Cycle" DataField="BillingCycleFrequency" SortExpression="BillingCycleFrequency" UniqueName="BillingCycleFrequency" />
                        <telerik:GridBoundColumn HeaderText="USGS CMF" DataField="FundingUSGSCMF" SortExpression="FundingUSGSCMF" UniqueName="FundingUSGSCMF" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FundingCustomer" SortExpression="FundingCustomer" UniqueName="FundingCustomer" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn HeaderText="Other" DataField="FundingOther" SortExpression="FundingOther" UniqueName="FundingOther" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn HeaderText="Total" DataField="FundingTotal" SortExpression="FundingTotal" UniqueName="FundingTotal" DataFormatString="{0:c0}" />

                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this mod?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
                    </Columns>

                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvContacts" TabIndex="1">
            <telerik:RadAjaxPanel runat="server" ID="rapContacts">
                <table>
                    <tr>
                        <!-- CUSTOMER Billing Contact-->
                        <td valign="top" style="width: 50%; padding: 10px;">
                            <telerik:RadComboBox runat="server" ID="rcbcbContacts" OnItemsRequested="rcbcbContacts_ItemsRequested" OnSelectedIndexChanged="rcbcbContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" AutoPostBack="true" Height="100px">
                            </telerik:RadComboBox>
                            <br />
                            <telerik:RadGrid runat="server" ID="rgcbContact" OnNeedDataSource="rgcbContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no customer billing contacts assigned to this agreement." NoMasterRecordsText="There are no customer billing contacts assigned to this agreement.">

                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Customer Billing Contact">
                                            <ItemTemplate>
                                                <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b><br />
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne")%> <%# Eval("StreetTwo") %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadButton runat="server" ID="rbcbContactRemove" Text="Remove Contact" Visible="false" OnClick="rbcbContact_Click" AutoPostBack="true" Skin="Silk" />
                        </td>
                        <!-- Customer Technical Contact -->
                        <td valign="top" style="width: 50%; padding: 10px;">
                            <telerik:RadComboBox runat="server" ID="rcbctContacts" OnItemsRequested="rcbctContacts_ItemsRequested" OnSelectedIndexChanged="rcbctContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" AutoPostBack="true" Height="100px">
                            </telerik:RadComboBox>
                            <br />
                            <telerik:RadGrid runat="server" ID="rgctContact" OnNeedDataSource="rgctContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no customer technical contacts assigned to this agreement." NoMasterRecordsText="There are no customer technical contacts assigned to this agreement.">

                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Customer Technical Contact">
                                            <ItemTemplate>
                                                <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b><br />
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne") %> <%# Eval("StreetTwo") %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadButton runat="server" ID="rbctContactRemove" Text="Remove Contact" Visible="false" OnClick="rbctContact_Click" AutoPostBack="true" Skin="Silk" />
                        </td>
                    </tr>
                    <tr>
                        <!-- USGS Billing CONTACT-->
                        <td valign="top" style="width: 50%; padding: 10px;">
                            <br />
                            <a onclick="window.open('TrackEmployee.aspx', 'TrackEmployee', 'width=450, height=150');" style="color:#2dabc1;">Can't Find Employee.</a>
                            <telerik:RadComboBox runat="server" ID="rcbubContacts" OnItemsRequested="rcbubContacts_ItemsRequested" OnSelectedIndexChanged="rcbubContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" Height="100px" AutoPostBack="true">
                            </telerik:RadComboBox>
                            <br />
                            <telerik:RadGrid runat="server" ID="rgubContact" OnNeedDataSource="rgubContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no usgs billing contacts assigned to this agreement." NoMasterRecordsText="There are no USGS billing contacts assigned to this agreement.">

                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="USGS Billing Contact">
                                            <ItemTemplate>
                                                <b><%# Eval("FullName") %></b>
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadButton runat="server" ID="rbubContactRemove" Text="Remove Contact" Visible="false" OnClick="rbubContactRemove_Click" AutoPostBack="true" Skin="Silk" />
                        </td>
                        <!-- USGS Technical Contact -->
                        <td valign="top" style="width: 50%; padding: 10px;">
                            <br />
                            <a onclick="window.open('TrackEmployee.aspx', 'TrackEmployee', 'width=450, height=150').focus();" style="color:#2dabc1;">Can't Find Employee.</a>
                            <telerik:RadComboBox runat="server" ID="rcbutContacts" OnItemsRequested="rcbutContacts_ItemsRequested" OnSelectedIndexChanged="rcbutContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" AutoPostBack="true" Height="100px">
                            </telerik:RadComboBox>
                            <br />
                            <telerik:RadGrid runat="server" ID="rgutContact" OnNeedDataSource="rgutContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no usgs technical contacts assigned to this agreement." NoMasterRecordsText="There are no USGS technical contacts assigned to this agreement.">

                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="USGS Technical Contact">
                                            <ItemTemplate>
                                                <b><%# Eval("FullName") %></b>
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadButton runat="server" ID="rbutContactRemove" Text="Remove Contact" Visible="false" OnClick="rbutContactRemove_Click" AutoPostBack="true" Skin="Silk" />
                        </td>
                    </tr>
                </table>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvSiteFunding" TabIndex="2">
            <script>
                function getUrlVars()
                {
                    var vars = [], hash;
                    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for(var i = 0; i < hashes.length; i++)
                    {
                        hash = hashes[i].split('=');
                        vars.push(hash[0]);
                        vars[hash[0]] = hash[1];
                    }
                    return vars;
                }
                var bulkDownloadClicked = function (sender, args) {
                    var parameters = getUrlVars();
                    var agreementID = parameters["AgreementID"];
                    console.log(agreementID);
                    window.open("Documents/AgreementSiteBulkEdit.ashx?AgreementID=" + agreementID);
                }
                function rbUploadedEvent() {
                    document.getElementById("uploadButtonSpan").style.visibility = "";
                }

                window.onerror = function (err) {
                    if (err == "Uncaught Sys.WebForms.PageRequestManagerServerErrorException: Sys.WebForms.PageRequestManagerServerErrorException: An unknown error occurred while processing the request on the server. The status code returned from the server was: 500") {
                        alert("Problem with special characters entered in field 'Remarks'");
                    }
                }
            </script>
            
            <telerik:RadAjaxPanel runat="server" ID="rapFundedSites" LoadingPanelID="ralpSilk">
                <div runat="server" id="bulkDlUl" style="border-radius:4px; border: lightgray 1px solid; padding: 10px;" visible="false">
                    <span style="font-weight:bold;">Bulk Upload Help available <a href="https://my.usgs.gov/confluence/display/SIFTA/Bulk+Upload+for+Site+Funding">here</a>.</span>
                    <br /><br />
                    <hr />
                    <span style="font-weight:bold;">Download</span>
                    <br /><br />
                    <span style="margin-left:40px;"><telerik:RadButton Text="Download" runat="server" AutoPostBack="true" OnClientClicked="bulkDownloadClicked"/></span>
                    <hr />

                    <span style="font-weight:bold;">Upload</span>
                    <br /><br />
                    <div>
                        <span style="float:left;margin-left:40px;">
                            <telerik:RadAsyncUpload runat="server" ID="rauBulkSiteUpload" MaxFileInputsCount="1" AllowedFileExtensions=".xlsx" OnClientFileUploaded="rbUploadedEvent" />
                        </span>
                        <span style="float:left; visibility:hidden;" id="uploadButtonSpan">
                            <telerik:RadButton runat="server" ID="rbUploadBulkSiteTemplate" AutoPostBack="true" Text="Upload Site Funding Spreadsheet" OnClick="rbUploadBulkSiteTemplate_Click"  />
                        </span></div>
                    <br /><br /><br />
                    <span style="margin-left:40px;"">
                        <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
                    </span>
                </div><br />

                <telerik:RadGrid runat="server" ID="rgFundedSites" AllowSorting="true" OnNeedDataSource="rgFundedSites_NeedDataSource" OnInsertCommand="rgFundedSites_InsertCommand" OnUpdateCommand="rgFundedSites_UpdateCommand" OnDeleteCommand="rgFundedSites_DeleteCommand" OnPreRender="rgFundedSites_PreRender">
                    <MasterTableView AutoGenerateColumns="False" ShowGroupFooter="true" ShowFooter="true" DataKeyNames="FundingSiteID" EditMode="EditForms">
                        <FooterStyle BackColor="Black" ForeColor="White" />
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add Site-Specific Funding Details" />
                        <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/FundingSitesControl.ascx" />
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" HeaderStyle-Width="10px" />
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgFPS" ToolTip="FPS Eligible" Height="15px" Width="15px" ImageUrl='<%# String.Format("~/Images/FPSScores/FPS{0}.gif", Eval("FPSScore")) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Mod" DataField="ModNumber">
                                <ItemTemplate>
                                    <%# Eval("ModNumber").ToString() == "0" ? "" : Eval("ModNumber") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="SiteNumber" HeaderText="Site" SortExpression="SiteNumber">
                                <ItemTemplate>
                                    <a href='<%# String.Format("Site.aspx?SiteNumber={0}", Eval("SiteNumber")) %>' target="_blank" style="color: #4a95a1;" title='<%# Eval("SiteNumber")%> - <%# Eval("SiteName") %>'><%# Eval("SiteNumber") %> - <%# Eval("SiteName") %></a><br />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Collection Code" DataField="Code" SortExpression="Code">
                                <ItemTemplate>
                                    <p style="padding: 0px; margin: 0px;" title='<%# CollectionCodeString(Eval("CollectionCodeID")) %>'><%# Eval("Code") %></p>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Units" DataField="CollectionUnits" DataFormatString="{0:0.00}" SortExpression="CollectionUnits" />
                            <telerik:GridTemplateColumn HeaderText="Diff. Factor" DataField="DifficultyFactor" SortExpression="DifficultyFactor">
                                <ItemTemplate>
                                    <p style="padding: 0px; margin: 0px;" title='<%# Eval("DifficultyFactorReason", "Difficulty Factor Reason: {0}") %>'><%# Eval("DifficultyFactor","{0:0.0}") %></p>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataType="System.Double" HeaderText="USGS CMF" DataField="FundingUSGSCMF" SortExpression="FundingUSGSCMF" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                            <telerik:GridBoundColumn DataType="System.Double" HeaderText="Customer" DataField="FundingCustomer" SortExpression="FundingCustomer" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                            <telerik:GridBoundColumn HeaderText="Other" DataField="FundingOther" SortExpression="FundingOther" UniqueName="FundingOther" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                            <telerik:GridBoundColumn HeaderText="Total" DataField="FundingTotal" SortExpression="FundingTotal" UniqueName="FundingOther" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                            <telerik:GridBoundColumn HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:d}" />
                            <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" DataFormatString="{0:d}" />
                            <telerik:GridTemplateColumn HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks">
                                <ItemTemplate>
                                    <p style="padding: 0px; margin: 0px;" title='<%# Eval("Remarks") %>'><%# GetStationName(Eval("Remarks"), null) %></p>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton"
                                CommandName="Delete" Text="Remove" ConfirmText="Are you sure you wish to remove this Site Funding?" UniqueName="DeleteSiteFunding" Visible="false" HeaderStyle-Width="20px" />
                        </Columns>
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="Task" FormatString="{0} Data Collection" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Task" />
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                    </MasterTableView>
                </telerik:RadGrid>
                
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvStudiesFunding" TabIndex="3">
            <telerik:RadAjaxPanel runat="server" ID="rapStudiesSupport" LoadingPanelID="ralpSilk">
                <telerik:RadGrid runat="server" ID="rgStudiesSupport" AllowSorting="true" OnNeedDataSource="rgStudiesSupport_NeedDataSource" OnItemDataBound="rgStudiesSupport_ItemDataBound" OnInsertCommand="rgStudiesSupport_InsertCommand" OnUpdateCommand="rgStudiesSupport_UpdateCommand" OnDeleteCommand="rgStudiesSupport_DeleteCommand" AutoGenerateColumns="false">
                    <MasterTableView DataKeyNames="FundingStudyID" CommandItemSettings-AddNewRecordText="Add New Studies/Support Funding">
                        <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/StudiesSupportFundingControl.ascx"></EditFormSettings>

                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                            <telerik:GridTemplateColumn HeaderText="Mod" DataField="Number" SortExpression="Number" AllowSorting="true">
                                <ItemTemplate>
                                    <%# Eval("Number").ToString() == "0" ? "" : Eval("Number") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="BASIS Number" DataField="BasisProjectNumber" SortExpression="BasisProjectNumber" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Code" DataField="Code" SortExpression="Code" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Description" DataField="Description" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />
                            <telerik:GridBoundColumn DataType="System.Double" HeaderText="USGS CMF" SortExpression="FundingUSGSCMF" DataField="FundingUSGSCMF" DataFormatString="{0:c0}" />
                            <telerik:GridBoundColumn DataType="System.Double" HeaderText="Customer" SortExpression="FundingCustomer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                            <telerik:GridBoundColumn HeaderText="Other" DataField="FundingOther" SortExpression="FundingOther" UniqueName="FundingOther" DataFormatString="{0:c0}" />
                            <telerik:GridBoundColumn HeaderText="Total" DataField="FundingTotal" SortExpression="FundingTotal" UniqueName="FundingOther" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                            <telerik:GridBoundColumn HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:d}" />
                            <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" DataFormatString="{0:d}" />
                            <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this Studies/Support Funding?" ButtonType="ImageButton"
                                CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvDocuments" TabIndex="4">
            <telerik:RadAjaxPanel runat="server" ID="rapJFADocuments" LoadingPanelID="ralpSilk">
                <div class="SectionContent" style="width: 500px;">
                    <telerik:RadAjaxPanel runat="server" ID="rapBtn" LoadingPanelID="ralpDocument">
                        <table>
                            <tr>
                                <td align="right">Director:
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="rcbDirector" DataTextField="Name" DataValueField="CenterDirectorID" AppendDataBoundItems="false" Skin="Silk" Width="250px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Financial Reviewer:
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="rcbFinancialReviewer" DataTextField="Name" DataValueField="CenterFinancialReviewerID" AppendDataBoundItems="false" Skin="Silk" Width="250px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">DUNS:
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="rtbDUNS" Skin="Silk" Width="250px" Text="<%# agreement.Customer.Center.DUNS %>" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Project Number:
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="rtbProjectNumber" Skin="Silk" Text="<%# agreement.Customer.Center.ProjectNumber %>" Width="250px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right"></td>
                                <td>
                                    <img id="imgLoad" src="Images/loadingBar.gif" width="100%" hidden="hidden" /><br />
                                    <telerik:RadButton runat="server" ID="rbDownload" Text="Download JFA Documents" AutoPostBack="true" OnClick="rbDownloadJFADocuments_Click" Skin="Silk" Width="250px" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadAjaxPanel>

                    <br />
                    <br />
                </div>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvDocumentUpload" TabIndex="5">
            <div class="SectionContent" style="width: 500px;">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td valign="bottom">
                                        <telerik:RadAsyncUpload MultipleFileSelection="Disabled" Visible="false" runat="server" ID="rauFile" Skin="Silk" Width="100%" />
                                    </td>
                                    <td valign="bottom" style="padding-bottom: 8px;">
                                        <telerik:RadButton Visible="false" runat="server" ID="rbUploadDocuments" Text="Upload" Skin="Silk" OnClick="rbUploadDocuments_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbJFADocument" Text="Rename with Agreement ID" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <telerik:RadGrid ID="rgAgreementDocuments" Width="100%" AutoGenerateColumns="false" runat="server" OnNeedDataSource="rgAgreementDocuments_NeedDataSource" OnDeleteCommand="rgAgreementDocuments_DeleteCommand" Skin="Silk">
                                <MasterTableView DataKeyNames="Path">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Agreement Documents">
                                            <ItemTemplate>
                                                <a style="color: #4a95a1;" href='<%# Eval("URL") %>' target="_blank"><%# Eval("Name") %></a>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn ConfirmText="Are you sure you would like to delete this document?" ButtonType="ImageButton"
                                            CommandName="Delete" Text="Remove" UniqueName="DeleteDocument" Visible="false" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvAgreementLog" runat="server" TabIndex="6">
            <asp:PlaceHolder runat="server" ID="phAgreementLog" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvCoopFunding" runat="server" TabIndex="7">
            <telerik:RadGrid ID="rgCoopFunding" runat="server" Width="100%" 
                AutoGenerateColumns="False" Skin="Silk" AllowPaging="true"
                PageSize="50" AllowMultiRowSelection="False" OnNeedDataSource="rgCoopFunding_NeedDataSource" OnItemDataBound="rgCoopFunding_ItemDataBound" >
                <ItemStyle />
                <MasterTableView Width="100%" DataKeyNames="AgreementID" AllowMultiColumnSorting="True" GroupsDefaultExpanded="true" HierarchyDefaultExpanded="true">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <GroupHeaderItemStyle ForeColor="Black" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="FundSourceFY" HeaderText="Fiscal Year" />
                        <telerik:GridBoundColumn DataField="AccountNumber" HeaderText="Account Number" />
                        <telerik:GridBoundColumn DataField="AccountName" HeaderText="Account Name" />
                        <telerik:GridBoundColumn DataField="FundingUSGSCMF" HeaderText="USGS" DataFormatString="{0:c0}"  />
                        <telerik:GridBoundColumn DataField="FundingUSGSAllocation" HeaderText="USGS Appropriated" DataFormatString="{0:c0}"  />
                        <telerik:GridBoundColumn DataField="FundingReimbCustomer" HeaderText="Cooperator" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn DataField="Total" HeaderText="Total" DataFormatString="{0:c0}" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div runat="server" id="totalsDiv" style="border-radius:4px; border: lightgray 1px solid; padding: 10px;">
                <table style="width:100%;table-layout:auto;empty-cells:show;border-collapse:collapse;">
                    <tr style="background-color: #bd8f04; color: white; font-weight: bold">
                        <td align="right" style="padding:3px;"></td>
                        <td style="padding:3px;">Planned Total (From Fund Sources)</td>
                        <td align="right" style="padding:3px;">USGS Total:</td>
                        <td align="right" style="padding:3px;" runat="server" id="dirTd"></td>
                        <td align="right" style="padding:3px;">Cooperator Total:</td>
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
        </telerik:RadPageView>

        <telerik:RadPageView ID="rpvReciever" runat="server" TabIndex="8">
            <asp:PlaceHolder runat="server" ID="phReceiver" />
        </telerik:RadPageView>

    </telerik:RadMultiPage>
</asp:Content>
