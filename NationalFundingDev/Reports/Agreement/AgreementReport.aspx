<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="AgreementReport.aspx.cs" Inherits="NationalFundingDev.Reports.Agreement.AgreementReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Agreement Overview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
    <style>
        h2 a {
            border: none;
            outline: none;
        }

        #ContentImage {
            height: 110px !important;
            width: 110px !important;
        }

            #ContentImage #divImage {
                height: 110px !important;
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

        .contactDetails {
            width: 100%;
        }

        .topBar {
            margin-top: 20px;
            height: 5px;
            background-color: #b5b9bd;
        }

        .skipLink {
            text-align: right;
            width: 1050px;
            padding: 0px;
            margin-top: -20px;
            margin-bottom: -10px;
            font-weight: bold;
        }

        .SectionContent h2 {
            background-color: #c2d1d3;
            -moz-border-radius: 20px 20px 0px 0px;
            -webkit-border-radius: 20px 20px 0px 0px;
            -khtml-border-radius: 20px 20px 0px 0px;
            border-radius: 20px 20px 0px 0px;
            margin: -10px -10px 5px -10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAJAXManager" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphImage" runat="server">
    <a name="Top"></a>
    <div id="divImage" style="padding-top: 10px; padding-right: 5px;">
        <asp:Image runat="server" ID="imgCustLogo" Height="100px" Width="100px" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphInformation" runat="server">
    <br />
    <b>Agreement: <%: agreement.PurchaseOrderNumber %></b>
    <br />
    <a href="<%: AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID)) %>">Customer: <%: agreement.Customer.Code %> - <%: agreement.Customer.Name %></a><br />
    FBMS Customer Number: <%: agreement.Customer.Number %><br />
    <a id="CustomerWebsite" href="<%= agreement.Customer.URL %>" target="_blank">Customer Website</a><br />
    <script type="text/javascript">
        if (!$("#CustomerWebsite").attr("href")) $("#CustomerWebsite").hide();
    </script>
    <a href='<%= AppendBaseURL(String.Format("Center.aspx?OrgCode={0}", agreement.Customer.OrgCode)) %>' style="color: orange;">Center Home</a> >> <a href='<%= AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID)) %>' style="color: orange;">Customer Portal</a> >> <a href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}", agreement.AgreementID)) %>' style="color: orange;">Agreement Portal</a> >> Agreement Overview<br />
    <br />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSidePanel" runat="server">
    <hr class="topBar" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cphBody" runat="server">
    <br />
    <div class="skipLink">
        <p>
            <a href="#SiteFunding">Skip to Details</a>
            <img src="../../Images/arrow.png" alt="Skip to details" style="float: right; padding: 3px 0px 0px 3px;" />
        </p>
    </div>
    <table style="margin: 0px; padding: 0px; border: 0px;">
        <tr style="margin: 0px; padding: 0px; border: 0px;">
            <td valign="top">
                <a name="AgreementInformation"></a>
                <div class="SectionContent">
                    <h2><a style="border: none; outline: none;" href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 0)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditAgreementInformation" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> Agreement Information</h2>
                    <table width="100%">
                        <tr>
                            <td valign="top" style="padding-right: 20px; border-right: solid; border-right-width: 1px;">
                                <table>
                                    <tr>
                                        <td align="right"><b title="Purchase Order Number">PON :</b></td>
                                        <td><%: agreement.PurchaseOrderNumber %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><b title="Match Pair Code">MPC :</b></td>
                                        <td><%: agreement.MatchPairCode %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><b title="Sales Order Number">SO :</b></td>
                                        <td><%: agreement.SalesDocument %></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td align="right"><b>Start Date :</b></td>
                                        <td><%: DateFormat(agreement.StartDate, "d") %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><b>End Date :</b></td>
                                        <td><%: DateFormat(agreement.EndDate, "d") %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><b>USGS Signed :</b></td>
                                        <td><%: DateFormat(agreement.AgreementMods.FirstOrDefault(p=>p.Number == 0).SignUSGSDate , "d") %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><b>Customer Signed :</b></td>
                                        <td><%: DateFormat(agreement.AgreementMods.FirstOrDefault(p=>p.Number == 0).SignCustomerDate , "d") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <a style="border: none; outline: none;" href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 6)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditAgreementLogs" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> <b>Last Log : </b>
                    <asp:Literal runat="server" ID="ltlAgreementLog" />
                    <br />
                    <br />
                    <asp:Panel runat="server" ID="pnlFundingSummary" />
                </div>
            </td>
            <td valign="top" style="width: 500px">
                <a name="FundedSiteMap"></a>
                <div class="SectionContent">
                    <h2>&nbsp;&nbsp;Funded Sites Map</h2>
                    <iframe runat="server" id="iMap" frameborder="0" style="border: 1px solid;" allowfullscreen="true" mozallowfullscreen="true" webkitallowfullscreen="true" height="250" width="512"></iframe>
                </div>
                <a name="Documents"></a>
                <div class="SectionContent">
                    <h2><a href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 2)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditDocuments" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> Documents</h2>
                    <telerik:RadGrid ID="rgAgreementDocuments" Width="100%" AutoGenerateColumns="false" runat="server" OnNeedDataSource="rgAgreementDocuments_NeedDataSource" Skin="Silk">
                        <MasterTableView DataKeyNames="Path">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Agreement Documents">
                                    <ItemTemplate>
                                        <a style="color: #4a95a1;" href='<%# Eval("URL") %>' target="_blank"><%# Eval("Name") %></a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
        <tr style="margin: 0px; padding: 0px; border: 0px;">
            <td colspan="2">
                <a name="Contacts"></a>
                <div class="SectionContent">
                    <h2><a href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 1)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditContacts" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> Contacts</h2>
                    <table style="padding: 0px; margin: 0px; border: 0px;">
                        <tr style="padding: 0px; margin: 0px; border: 0px;">
                            <!-- CUSTOMER Billing Contact-->
                            <td valign="top" style="width: 517px;">
                                <telerik:RadGrid runat="server" ID="rgcbContact" OnNeedDataSource="rgcbContact_NeedDataSource" Width="517" AutoGenerateColumns="false">
                                    <MasterTableView NoDetailRecordsText="There are no customer billing contacts assigned to this agreement." NoMasterRecordsText="There are no customer billing contacts assigned to this agreement.">

                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Customer Billing Contact">
                                                <ItemTemplate>
                                                    <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b> (<%# Eval("Title") %>)<br />
                                                    <table class="contactDetails">
                                                        <tr>
                                                            <td style="padding: 0px;">W: <%# PhoneFormat(Eval("PhoneWork")) %>
                                                            </td>
                                                            <td style="padding: 0px;">M: <%# PhoneFormat(Eval("PhoneMobile")) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 0px;">Fax: <%# PhoneFormat(Eval("PhoneFax")) %>
                                                            </td>
                                                            <td style="padding: 0px;">E: <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%# Eval("StreetOne")%> <%# Eval("StreetTwo") %>, <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <!-- Customer Technical Contact -->
                            <td valign="top" style="width: 517px;">
                                <telerik:RadGrid runat="server" ID="rgctContact" OnNeedDataSource="rgctContact_NeedDataSource" Width="517" AutoGenerateColumns="false">
                                    <MasterTableView NoDetailRecordsText="There are no customer technical contacts assigned to this agreement." NoMasterRecordsText="There are no customer technical contacts assigned to this agreement.">

                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Customer Technical Contact">
                                                <ItemTemplate>
                                                    <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b> (<%# Eval("Title") %>)<br />
                                                    <table class="contactDetails">
                                                        <tr>
                                                            <td style="padding: 0px;">W: <%# PhoneFormat(Eval("PhoneWork")) %>
                                                            </td>
                                                            <td style="padding: 0px;">M: <%# PhoneFormat(Eval("PhoneMobile")) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 0px;">Fax: <%# PhoneFormat(Eval("PhoneFax")) %>
                                                            </td>
                                                            <td style="padding: 0px;">E: <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%# Eval("StreetOne") %> <%# Eval("StreetTwo") %>, <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <!-- USGS Billing CONTACT-->
                            <td valign="top" style="width: 517px;">
                                <telerik:RadGrid runat="server" ID="rgubContact" OnNeedDataSource="rgubContact_NeedDataSource" Width="517" AutoGenerateColumns="false">
                                    <MasterTableView NoDetailRecordsText="There are no usgs billing contacts assigned to this agreement." NoMasterRecordsText="There are no USGS billing contacts assigned to this agreement.">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="USGS Billing Contact">
                                                <ItemTemplate>
                                                    <b><%# Eval("FullName") %></b> (<%# Eval("Title") %>)<br />
                                                    <table class="contactDetails">
                                                        <tr>
                                                            <td style="padding: 0px;">W: <%# PhoneFormat(Eval("PhoneWork")) %>
                                                            </td>
                                                            <td style="padding: 0px;">M: <%# PhoneFormat(Eval("PhoneMobile")) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 0px;">Fax: <%# PhoneFormat(Eval("PhoneFax")) %>
                                                            </td>
                                                            <td style="padding: 0px;">E: <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %>, <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <!-- USGS Technical Contact -->
                            <td valign="top" style="width: 517px;">
                                <telerik:RadGrid runat="server" ID="rgutContact" OnNeedDataSource="rgutContact_NeedDataSource" Width="517" AutoGenerateColumns="false">
                                    <MasterTableView NoDetailRecordsText="There are no usgs technical contacts assigned to this agreement." NoMasterRecordsText="There are no USGS technical contacts assigned to this agreement.">

                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="USGS Technical Contact">
                                                <ItemTemplate>
                                                    <b><%# Eval("FullName") %></b> (<%# Eval("Title") %>)<br />
                                                    <table class="contactDetails">
                                                        <tr>
                                                            <td style="padding: 0px;">W: <%# PhoneFormat(Eval("PhoneWork")) %>
                                                            </td>
                                                            <td style="padding: 0px;">M: <%# PhoneFormat(Eval("PhoneMobile")) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 0px;">Fax: <%# PhoneFormat(Eval("PhoneFax")) %>
                                                            </td>
                                                            <td style="padding: 0px;">E: <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %>, <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr style="margin: 0px; padding: 0px; border: 0px;">
            <td colspan="2">
                <div class="skipLink">
                    <p>
                        <img src="../../Images/backarrow.png" alt="Back to Top" style="padding: 3px 3px 0px 0px;" />
                        <a href="#Top" style="float: right;">Back to Top</a>
                    </p>
                </div>
                <a name="SiteFunding"></a>
                <div class="SectionContent">
                    <h2><a href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 4)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditSiteFunding" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> Site Funding</h2>
                    <telerik:RadGrid runat="server" ID="rgFundedSites" OnNeedDataSource="rgFundedSites_NeedDataSource" OnPreRender="rgFundedSites_PreRender" Width="1050px">
                        <MasterTableView AutoGenerateColumns="False" AllowSorting="true" ShowGroupFooter="true" ShowFooter="true" DataKeyNames="FundingSiteID" EditMode="EditForms">
                            <FooterStyle BackColor="Black" ForeColor="White" />
                            <CommandItemSettings ShowRefreshButton="false" />
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/FundingSitesControl.ascx" />
                            <Columns>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgFPS" Height="15px" Width="15px" ToolTip="FPS Eligible" ImageUrl='<%# AppendBaseURL(String.Format("Images/FPSScores/FPS{0}.gif", Eval("FPSScore"))) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Mod" DataField="ModNumber">
                                    <ItemTemplate>
                                        <%# Eval("ModNumber").ToString() == "0" ? "" : Eval("ModNumber") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowSorting="true" UniqueName="SiteNumber" HeaderText="Site" SortExpression="SiteNumber">
                                    <ItemTemplate>
                                        <a href='<%# AppendBaseURL(String.Format("Site.aspx?SiteNumber={0}", Eval("SiteNumber"))) %>' target="_blank" style="color: #4a95a1;"><%# Eval("SiteNumber") %> - <%# Eval("SiteName") %></a><br />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowSorting="true" HeaderText="Collection Code" DataField="Code" SortExpression="Code">
                                    <ItemTemplate>
                                        <p style="padding: 0px; margin: 0px;" title='<%# CollectionCodeString(Eval("CollectionCodeID")) %>'><%# Eval("Code") %></p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowSorting="true" HeaderText="Units" DataField="CollectionUnits" DataFormatString="{0:0.00}" />
                                <telerik:GridTemplateColumn AllowSorting="true" HeaderText="Diff. Factor" DataField="DifficultyFactor" SortExpression="DifficultyFactor">
                                    <ItemTemplate>
                                        <p style="padding: 0px; margin: 0px;" title='<%# Eval("DifficultyFactorReason", "Difficulty Factor Reason: {0}") %>'><%# Eval("DifficultyFactor","{0:0.0}") %></p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.Double" HeaderText="USGS CMF" DataField="FundingUSGSCMF" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.Double" HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                                <telerik:GridBoundColumn AllowSorting="true" HeaderText="Other" DataField="FundingOther" SortExpression="FundingOther" UniqueName="FundingOther" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                                <telerik:GridBoundColumn HeaderText="Total" DataField="FundingTotal" SortExpression="FundingTotal" UniqueName="FundingTotal" DataFormatString="{0:c0}" FooterAggregateFormatString="<b>{0:c0}</b>" Aggregate="Sum" />
                                <telerik:GridTemplateColumn AllowSorting="true" HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks">
                                    <ItemTemplate>
                                        <p style="padding: 0px; margin: 0px;" title='<%# Eval("Remarks") %>'><%# GetStationName(Eval("Remarks"), null) %></p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $(".rgGroupCol").hide();
                        });
                    </script>
                </div>
            </td>
        </tr>
        <tr style="margin: 0px; padding: 0px; border: 0px;">
            <td colspan="2">
                <a name="StudiesFunding"></a>
                <div class="SectionContent">
                    <h2><a href='<%= AppendBaseURL(String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreement.AgreementID, 5)) %>' target="_blank">
                        <asp:Image runat="server" ID="imgEditStudiesFunding" Visible="false" Style="padding-left: 10px;" title="Edit" Height="12" Width="12" src="../../Images/editPencil.png" ToolTip="Open Edit Interface" /></a> Studies / Support Funding</h2>
                    <telerik:RadGrid runat="server" AllowSorting="true" ID="rgStudiesSupport" OnNeedDataSource="rgStudiesSupport_NeedDataSource" AutoGenerateColumns="false" Width="1050px">
                        <MasterTableView DataKeyNames="FundingStudyID" NoDetailRecordsText="There is no studies funding recorded for this agreement" NoMasterRecordsText="There is no studies funding recorded for this agreement">
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/StudiesSupportFundingControl.ascx"></EditFormSettings>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Mod" DataField="Number" SortExpression="Number" AllowSorting="true">
                                    <ItemTemplate>
                                        <%# Eval("Number").ToString() == "0" ? "" : Eval("Number") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.String" HeaderText="BASIS Number" DataField="BasisProjectNumber" />
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.String" HeaderText="Code" DataField="Code" />
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.String" HeaderText="Description" DataField="Description" />
                                <telerik:GridBoundColumn DataType="System.String" HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.Double" HeaderText="USGS CMF" DataField="FundingUSGSCMF" DataFormatString="{0:c0}" />
                                <telerik:GridBoundColumn AllowSorting="true" DataType="System.Double" HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                                <telerik:GridBoundColumn AllowSorting="true" HeaderText="Other" DataField="FundingOther" SortExpression="FundingOther" UniqueName="FundingOther" DataFormatString="{0:c0}" />
                                <telerik:GridBoundColumn HeaderText="Total" DataField="FundingTotal" SortExpression="FundingTotal" UniqueName="FundingTotal" DataFormatString="{0:c0}" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="skipLink" style="margin-top: 0px;">
                    <p>
                        <img src="../../Images/backarrow.png" alt="Back to Top" style="padding: 3px 3px 0px 0px;" />
                        <a href="#Top" style="float: right;">Back to Top</a>
                    </p>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
