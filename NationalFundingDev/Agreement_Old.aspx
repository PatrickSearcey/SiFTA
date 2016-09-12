<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Agreement_Old.aspx.cs" Inherits="NationalFundingDev.AgreementOldPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Agreement
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    <style type="text/css">
        #ContentImage
        {
            height:115px !important;
            width: 110px !important;
        }
        #ContentImage #divImage
        {
            height:115px !important;
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
            <telerik:AjaxSetting AjaxControlID="rtsAgreementOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsAgreementOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpAgreementOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rgAgreementFundingOverView" />
                    <telerik:AjaxUpdatedControl ControlID="rgOverViewDocuments" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgOverViewDocuments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgOverViewDocuments" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFundedSites">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFundedSites" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbctContacts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgctContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbcbContacts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgcbContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbubContacts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbubContactRemove" />
                    <telerik:AjaxUpdatedControl ControlID="rcbubContacts" />
                    <telerik:AjaxUpdatedControl ControlID="rgubContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbutContacts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbutContactRemove" />
                    <telerik:AjaxUpdatedControl ControlID="rcbutContacts" />
                    <telerik:AjaxUpdatedControl ControlID="rgutContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbubContactRemove">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbubContactRemove" />
                    <telerik:AjaxUpdatedControl ControlID="rcbubContacts" />
                    <telerik:AjaxUpdatedControl ControlID="rgubContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbutContactRemove">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbutContactRemove" />
                    <telerik:AjaxUpdatedControl ControlID="rcbutContacts" />
                    <telerik:AjaxUpdatedControl ControlID="rgutContact" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgStudiesSupport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgStudiesSupport" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbUploadDocuments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreementDocuments" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAgreementDocuments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreementDocuments" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpAgreementOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbSubmitAgreementLog">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgreementLog" LoadingPanelID="ralp" />
                    <telerik:AjaxUpdatedControl ControlID="rdtpAgreementLogTime" />
                    <telerik:AjaxUpdatedControl ControlID="rcbModNumberAgreementLog" />
                    <telerik:AjaxUpdatedControl ControlID="rcbActionAgreementLog" />
                    <telerik:AjaxUpdatedControl ControlID="rtbRemarksAgreementLog" />
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
        <ClientEvents OnResponseEnd="CreateMap()" />
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="ralp" runat="server" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
    <div id="divImage" style="padding-top: 10px; padding-right: 5px;">
        <asp:Image runat="server" ID="imgCustLogo" Height="100px" Width="100px" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <b>Agreement: <%: agreement.PurchaseOrderNumber %></b>
    <br />
    <a href="<%: String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID) %>">Customer: <%: agreement.Customer.Code %> - <%: agreement.Customer.Name %></a><br />
    FBMS Customer Number: <%: agreement.Customer.Number %><br />
    <a href="<%# agreement.Customer.URL %>" target="_blank">Customer Website</a><br /><br />
    <%--//Removed 11/4/2014  to remove tags --%>
    <a href='<%= String.Format("Center.aspx?OrgCode={0}", agreement.Customer.OrgCode) %>' style="color: orange;" >Center Home</a> >> <a href='<%= String.Format("Customer.aspx?CustomerID={0}", agreement.Customer.CustomerID) %>' style="color: orange;" >Customer Portal</a> >> Agreement Portal
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsAgreementOptions" MultiPageID="rmpAgreementOptions" Skin="Silk" Width="100%" OnTabClick="rtsAgreementOptions_TabClick" Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab Text="Overview" Selected="true" TabIndex="0" />
            <telerik:RadTab Text="Contacts" TabIndex="1" />
            <telerik:RadTab Text="Mods" TabIndex="2" />
            <telerik:RadTab Text="Funded Sites" TabIndex="3" />
            <telerik:RadTab Text="Studies / Support" TabIndex="4" />
            <telerik:RadTab Text="Documents" TabIndex="5" />
            <telerik:RadTab Text="Agreement Log" TabIndex="8" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpAgreementOptions" RenderSelectedPageOnly="true">
        <!-- OverView -->
        <telerik:RadPageView ID="rpvOverView" runat="server" Selected="true">
            <table style="width: 100%;">
                <tr>
                    <td valign="top" style="width: 500px;">
                        <div id="divAgreementInfo" class="SectionContent" style="width: 500px;">
                            <h2>Agreement Information</h2>
                            <telerik:RadAjaxPanel runat="server" ID="rapAgreementInfo" OnLoad="rapAgreementInfo_Load">
                                <table style="width: 100%;">
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td align="right"><b>Start Date:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlAgreementStartDate" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right"><b>End Date:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlAgreementEndDate" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right"><b>Purchase Order #:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlPurchaseOrderNumber" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right"><b>MPC:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlMPC" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right"><b>FBMS:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlFBMS" /></td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td style="padding-left: 10px;" valign="top">
                                            <center><b style="font-size:medium;">Signed</b></center>
                                            <table>
                                                <tr>
                                                    <td align="right"><b>USGS:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlUSGSSigned" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right"><b>Customer:</b></td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlCustomerSigned" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                        <td align="center"><b>USGS CWP</b></td>
                                        <td align="center"><b>Customer</b></td>
                                        <td align="center"><b>Other</b></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="background-color: #fccc67; color: black;">Funded Sites<br />
                                            Studies/Support<br />
                                            <b>Total</b></td>
                                        <td align="center" style="background-color: #fccc67; color: black;">
                                            <asp:Literal runat="server" ID="ltlSiteFundingUSGSCWP" /><br />
                                            <asp:Literal runat="server" ID="ltlStudiesFundingUSGSCWP" /><br />
                                            <b>
                                                <asp:Literal runat="server" ID="ltlUSGSCWPFundingSum" /></b></td>
                                        <td align="center" style="background-color: #fccc67; color: black;">
                                            <asp:Literal runat="server" ID="ltlSiteFundingCustomer" /><br />
                                            <asp:Literal runat="server" ID="ltlStudiesFundingCustomer" /><br />
                                            <b>
                                                <asp:Literal runat="server" ID="ltlCustomerFundingSum" /></b></td>
                                        <td align="center" style="background-color: #fccc67; color: black;">0<br />
                                            0<br />
                                            <b>0</b></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="background-color: black; color: white;"><b>Funding Total</b></td>
                                        <td align="center" style="background-color: black; color: white;">
                                            <asp:Literal runat="server" ID="ltlFundingUSGSCWP" /></td>
                                        <td align="center" style="background-color: black; color: white;">
                                            <asp:Literal runat="server" ID="ltlFundingCustomer" /></td>
                                        <td align="center" style="background-color: black; color: white;">
                                            <asp:Literal runat="server" ID="ltlFundingOther" /></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="background-color: lightgray; color: black;"><b>Difference</b></td>
                                        <td align="center" style="background-color: lightgray; color: black;">
                                            <asp:Literal runat="server" ID="ltlDifferenceUSGS" /></td>
                                        <td align="center" style="background-color: lightgray; color: black;">
                                            <asp:Literal runat="server" ID="ltlDifferenceCustomer" /></td>
                                        <td align="center" style="background-color: lightgray; color: black;">
                                            <asp:Literal runat="server" ID="ltlDifferenceOther" /></td>
                                    </tr>
                                </table>
                            </telerik:RadAjaxPanel>
                        </div>
                        <br />
                        <div id="divDocuments" class="SectionContent">
                            <h2>Documents</h2>
                            <telerik:RadGrid ID="rgOverViewDocuments" Width="100%" AutoGenerateColumns="false" runat="server" OnNeedDataSource="rgAgreementDocuments_NeedDataSource" Skin="Silk" ShowHeader="false">
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
                            <br />
                        </div>
                    </td>
                    <td valign="top">
                        <div id="divFunding" class="SectionContent">
                            <h2>Funding</h2>
                            <telerik:RadGrid runat="server" ID="rgAgreementFundingOverView" AutoGenerateColumns="false" OnNeedDataSource="rgAgreementFundingOverView_NeedDataSource" OnDetailTableDataBind="rgAgreementFundingOverView_DetailTableDataBind" Width="100%">
                                <MasterTableView DataKeyNames="AgreementModID" HierarchyDefaultExpanded="true">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <DetailTables>
                                        <telerik:GridTableView runat="server" ShowHeader="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td></td>
                                                                <td align="center"><b>USGS CWP</b></td>
                                                                <td align="center"><b>Customer</b></td>
                                                                <td align="center"><b>Other</b></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="background-color: #fccc67;">Funded Sites<br />
                                                                    Studies/Support<br />
                                                                    <b>Total</b></td>
                                                                <td align="center" style="background-color: #fccc67;"><%# String.Format("{0:c0}", Eval("SiteFundingUSGSCWP")) %><br />
                                                                    <%# String.Format("{0:c0}", Eval("StudiesFundingUSGSCWP")) %><br />
                                                                    <b><%# String.Format("{0:c0}", Convert.ToDouble(Eval("SiteFundingUSGSCWP")) + Convert.ToDouble(Eval("StudiesFundingUSGSCWP"))) %></b></td>
                                                                <td align="center" style="background-color: #fccc67;"><%# String.Format("{0:c0}", Eval("SiteFundingCustomer")) %><br />
                                                                    <%# String.Format("{0:c0}", Eval("StudiesFundingCustomer")) %><br />
                                                                    <b><%# String.Format("{0:c0}",Convert.ToDouble(Eval("SiteFundingCustomer")) + Convert.ToDouble(Eval("StudiesFundingCustomer"))) %></b></td>
                                                                <td align="center" style="background-color: #fccc67;"><%# String.Format("{0:c0}", 0) %><br />
                                                                    <%# String.Format("{0:c0}", 0) %><br />
                                                                    <b><%# String.Format("{0:c0}", 0) %></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="background-color: black; color: white;"><b>Funding Total</b></td>
                                                                <td align="center" style="background-color: black; color: white;"><%# String.Format("{0:c0}",Eval("FundingUSGSCWP")) %></td>
                                                                <td align="center" style="background-color: black; color: white;"><%# String.Format("{0:c0}", Eval("FundingCustomer")) %></td>
                                                                <td align="center" style="background-color: black; color: white;"><%# String.Format("{0:c0}", Eval("FundingOther")) %></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="background-color: lightgray;"><b>Difference</b></td>
                                                                <td align="center" style="background-color: lightgray;"><%# String.Format("{0:c0}", Convert.ToDouble(Eval("FundingUSGSCWP")) - Convert.ToDouble(Eval("SiteFundingUSGSCWP")) - Convert.ToDouble(Eval("StudiesFundingUSGSCWP"))) %></td>
                                                                <td align="center" style="background-color: lightgray;"><%# String.Format("{0:c0}", Convert.ToDouble(Eval("FundingCustomer")) - Convert.ToDouble(Eval("SiteFundingCustomer")) - Convert.ToDouble(Eval("StudiesFundingCustomer"))) %></td>
                                                                <td align="center" style="background-color: lightgray;"><%# String.Format("{0:c0}", Eval("FundingOther")) %></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="" DataField="ModName" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                                        <telerik:GridBoundColumn HeaderText="USGS CWP" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Other" DataField="FundingOther" DataFormatString="{0:c0}" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="SectionContent">
                <h2>Contacts</h2>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;" valign="top">
                            <telerik:RadGrid runat="server" ID="rgctContactOverView" OnNeedDataSource="rgctContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no customer technical contacts assigned to this agreement." NoMasterRecordsText="There are no customer technical contacts assigned to this agreement.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Customer Technical Contact">
                                            <ItemTemplate>
                                                <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b><br />
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a style="color: #4a95a1;" href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne") %> <%# Eval("StreetTwo") %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <telerik:RadGrid runat="server" ID="rgcbContactOverView" OnNeedDataSource="rgcbContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no customer billing contacts assigned to this agreement." NoMasterRecordsText="There are no customer billing contacts assigned to this agreement.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Customer Billing Contact">
                                            <ItemTemplate>
                                                <b><%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) %></b><br />
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a style="color: #4a95a1;" href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne")%> <%# Eval("StreetTwo") %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;" valign="top">
                            <telerik:RadGrid runat="server" ID="rgutContactOverView" OnNeedDataSource="rgutContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no usgs technical contacts assigned to this agreement." NoMasterRecordsText="There are no USGS technical contacts assigned to this agreement.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="USGS Technical Contact">
                                            <ItemTemplate>
                                                <b><%# Eval("FullName") %></b>
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a style="color: #4a95a1;" href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <telerik:RadGrid runat="server" ID="rgubContactOverView" OnNeedDataSource="rgubContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                                <MasterTableView NoDetailRecordsText="There are no usgs billing contacts assigned to this agreement." NoMasterRecordsText="There are no USGS billing contacts assigned to this agreement.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="USGS Billing Contact">
                                            <ItemTemplate>
                                                <b><%# Eval("FullName") %></b>
                                                <%# Eval("Title") %><br />
                                                Work:<%# PhoneFormat(Eval("PhoneWork")) %><br />
                                                Mobile:<%# PhoneFormat(Eval("PhoneMobile")) %><br />
                                                Fax:<%# PhoneFormat(Eval("PhoneFax")) %><br />
                                                Email:<a style="color: #4a95a1;" href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a><br />
                                                <%# Eval("StreetOne").ToString() + " " + Eval("StreetTwo").ToString() %><br />
                                                <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <div class="SectionContent">
                            <h2>Studies Funding</h2>
                            <telerik:RadGrid runat="server" ID="rgStudiesFundingOverView" AutoGenerateColumns="false" OnNeedDataSource="rgStudiesFundingOverView_NeedDataSource">
                                <MasterTableView NoDetailRecordsText="This agreement does not have any Studies Funding tied to it." NoMasterRecordsText="This agreement does not have any Studies Funding tied to it.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Basis" DataField="BasisProjectNumber" />
                                        <telerik:GridBoundColumn HeaderText="Code" DataField="Code" />
                                        <telerik:GridBoundColumn HeaderText="Description" DataField="Description" />
                                        <telerik:GridBoundColumn HeaderText="USGS" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" HeaderStyle-Width="300px" FooterStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="Number" FieldAlias="Mod" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Number" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="SectionContent">
                            <h2>Site Funding</h2>
                            <telerik:RadGrid runat="server" ID="rgSiteFundingOverView" AutoGenerateColumns="false" OnNeedDataSource="rgSiteFundingOverView_NeedDataSource">
                                <MasterTableView NoDetailRecordsText="This agreement does not have any Site Funding tied to it." NoMasterRecordsText="This agreement does not have any Site Funding tied to it.">
                                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgNSIP" Height="20" Width="20" ImageUrl='<%# NSIPImageURL(Eval("NSIPScore")) %>' Visible='<%# NSIPImageVisible(Eval("NSIPScore")) %>' />
                                                <telerik:RadToolTip runat="server" ID="rttNSIPImage" TargetControlID="imgNSIP" ShowEvent="OnMouseOver"
                                                    OffsetX="0" OffsetY="0" HideEvent="LeaveToolTip" HideDelay="0"
                                                    Animation="None" Position="TopCenter" RelativeTo="Element" Skin="Silk">
                                                    NSIP-Eligible Site
                                                </telerik:RadToolTip>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Number" DataField="SiteNumber" />
                                        <telerik:GridTemplateColumn HeaderText="Name" DataField="SiteName">
                                            <ItemTemplate>
                                                <a style="color: #4a95a1;" href='<%#  String.Format("Site.aspx?SiteNumber={0}", Eval("SiteNumber")) %>'><%# Eval("SiteName") %></a>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Code" DataField="Code" />
                                        <telerik:GridBoundColumn HeaderText="Units" DataField="CollectionUnits" />
                                        <telerik:GridBoundColumn HeaderText="Difficulty Factor" DataField="DifficultyFactor" />
                                        <telerik:GridBoundColumn HeaderText="USGS" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                                        <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" FooterStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ModNumber" FieldAlias="Mod" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ModNumber" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="SectionContent">
                            <h2>Funded Sites Map</h2>
                            <link rel="stylesheet" href="CSS/Leaflet.css" />
                            <asp:HiddenField runat="server" ID="hfMap" ClientIDMode="Static" ValidateRequestMode="Disabled" />
                            <div id="map" style="height: 500px; width: 100%;"></div>
                            <script type="text/javascript" src="JavaScript/Map.js" ></script>
                        </div>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <!-- Contacts -->
        <telerik:RadPageView ID="Contacts" runat="server">
            <table>
                <tr>
                    <!-- Customer Technical Contact -->
                    <td valign="top" style="width: 50%; padding: 10px;">
                        <telerik:RadComboBox runat="server" ID="rcbctContacts" DataSourceID="ldsCustomerContacts" Width="100%" HighlightTemplatedItems="true" DataTextField="AddressTitle" DataValueField="CustomerContactAddressID" EnableVirtualScrolling="true" OnSelectedIndexChanged="rcbctContacts_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                            </Items>
                            <ItemTemplate>
                                <%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) + " - " + Eval("Type").ToString()  %>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <br />
                        <br />
                        <telerik:RadGrid runat="server" ID="rgctContact" OnNeedDataSource="rgctContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                            <MasterTableView NoDetailRecordsText="There are no customer technical contacts assigned to this agreement." NoMasterRecordsText="There are no customer technical contacts assigned to this agreement.">
                                <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
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
                    </td>
                    <!-- CUSTOMER Billing Contact-->
                    <td valign="top" style="width: 50%; padding: 10px;">
                        <telerik:RadComboBox runat="server" ID="rcbcbContacts" DataSourceID="ldsCustomerContacts" Width="100%" HighlightTemplatedItems="true" DataTextField="AddressTitle" DataValueField="CustomerContactAddressID" EnableVirtualScrolling="true" OnSelectedIndexChanged="rcbcbContacts_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                            </Items>
                            <ItemTemplate>
                                <%# ContactName(Eval("Salutation"), Eval("FirstName"), Eval("MiddleName"), Eval("LastName")) + " - " + Eval("Type").ToString()  %>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <br />
                        <br />
                        <telerik:RadGrid runat="server" ID="rgcbContact" OnNeedDataSource="rgcbContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                            <MasterTableView NoDetailRecordsText="There are no customer billing contacts assigned to this agreement." NoMasterRecordsText="There are no customer billing contacts assigned to this agreement.">
                                <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
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
                    </td>
                </tr>
                <tr>
                    <!-- USGS Technical Contact -->
                    <td valign="top" style="width: 50%; padding: 10px;">
                        <br />
                        <telerik:RadComboBox runat="server" ID="rcbutContacts" OnItemsRequested="rcbutContacts_ItemsRequested" OnSelectedIndexChanged="rcbutContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" AutoPostBack="true" Height="100px">
                        </telerik:RadComboBox>
                        <br />
                        <telerik:RadGrid runat="server" ID="rgutContact" OnNeedDataSource="rgutContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                            <MasterTableView NoDetailRecordsText="There are no usgs technical contacts assigned to this agreement." NoMasterRecordsText="There are no USGS technical contacts assigned to this agreement.">
                                <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
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
                    <!-- USGS Billing CONTACT-->
                    <td valign="top" style="width: 50%; padding: 10px;">
                        <br />
                        <telerik:RadComboBox runat="server" ID="rcbubContacts" OnItemsRequested="rcbubContacts_ItemsRequested" OnSelectedIndexChanged="rcbubContacts_SelectedIndexChanged" AppendDataBoundItems="true" Width="100%" HighlightTemplatedItems="true" AllowCustomText="true" EnableLoadOnDemand="true" ItemsPerRequest="5" Height="100px" AutoPostBack="true">
                        </telerik:RadComboBox>
                        <br />
                        <telerik:RadGrid runat="server" ID="rgubContact" OnNeedDataSource="rgubContact_NeedDataSource" Width="100%" AutoGenerateColumns="false">
                            <MasterTableView NoDetailRecordsText="There are no usgs billing contacts assigned to this agreement." NoMasterRecordsText="There are no USGS billing contacts assigned to this agreement.">
                                <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
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
                </tr>
            </table>
            <asp:LinqDataSource ID="ldsCustomerContacts" runat="server" OnSelecting="ldsCustomerContacts_Selecting"></asp:LinqDataSource>
        </telerik:RadPageView>
        <!-- Mods -->
        <telerik:RadPageView ID="rpvMods" runat="server">
            <telerik:RadGrid runat="server" ID="rgMods" OnNeedDataSource="rgMods_NeedDataSource" OnItemDataBound="rgMods_ItemDataBound" OnInsertCommand="rgMods_InsertCommand" OnUpdateCommand="rgMods_UpdateCommand" OnDeleteCommand="rgMods_DeleteCommand" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="AgreementModID">
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/AgreementModControl.ascx"></EditFormSettings>
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditMod" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="ModNumber">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <b><%# ModNameFormat(Eval("Number")) %></b>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataType="System.DateTime" HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn DataType="System.DateTime" HeaderText="End Date" DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn DataType="System.DateTime" HeaderText="USGS Sign" DataField="SignUSGSDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn DataType="System.DateTime" HeaderText="Cust Sign" DataField="SignCustomerDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <telerik:GridBoundColumn DataType="System.Double" HeaderText="USGS CWP" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn DataType="System.Double" HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn DataType="System.Double" HeaderText="Other" DataField="FundingOther" DataFormatString="{0:c0}" />
                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this mod?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="DeleteMod" Visible="false" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <!-- Funded Sites -->
        <telerik:RadPageView ID="rpvFundedSites" runat="server">
            <telerik:RadGrid runat="server" ID="rgFundedSites" OnNeedDataSource="rgFundedSites_NeedDataSource" OnInsertCommand="rgFundedSites_InsertCommand" OnUpdateCommand="rgFundedSites_UpdateCommand" OnDeleteCommand="rgFundedSites_DeleteCommand">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="FundingSiteID" EditMode="EditForms">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/FundingSitesControl.ascx" />
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridTemplateColumn  HeaderStyle-Width="60px" UniqueName="AgreementModID" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <b><%# ModName(Eval("ModNumber")) %></b>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn   UniqueName="SiteNumber">
                            <ItemTemplate>
                                <a href='<%# String.Format("Site.aspx?SiteNumber={0}", Eval("SiteNumber")) %>' target="_blank" style="color:#4a95a1;"><b><%# Eval("SiteNumber") %> - <%# Eval("SiteName") %></b></a><br />
                                <b>Collection Code:</b><%# Eval("Code") %> | <b>Units:</b><%# Eval("CollectionUnits") %> | <b>Difficulty Factor:</b><%# Eval("DifficultyFactor") %><br />
                                <b>USGS CWP:</b><%# String.Format("{0:c0}", Eval("FundingUSGSCWP")) %><br />
                                <b>Customer:</b><%# String.Format("{0:c0}", Eval("FundingCustomer")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Remarks">
                            <ItemTemplate>
                                <b>Remarks:</b><br />
                                <%# Eval("Remarks") %><br />
                                <b>Difficulty Factor Reason:</b><br />
                                <%# Eval("DifficultyFactorReason") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" ConfirmText="Are you sure you wish to remove this Site Funding?" UniqueName="DeleteSiteFunding" Visible="false" HeaderStyle-Width="20px" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <!-- Studies/Support -->
        <telerik:RadPageView ID="rpvStudiesSupport" runat="server">
            <telerik:RadGrid runat="server" ID="rgStudiesSupport" OnNeedDataSource="rgStudiesSupport_NeedDataSource" OnItemDataBound="rgStudiesSupport_ItemDataBound" OnInsertCommand="rgStudiesSupport_InsertCommand" OnUpdateCommand="rgStudiesSupport_UpdateCommand" OnDeleteCommand="rgStudiesSupport_DeleteCommand" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="FundingStudyID">
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/StudiesSupportFundingControl.ascx"></EditFormSettings>
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="ModNumber">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <b><%# ModNameFormat(Eval("Number")) %></b>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataType="System.String" HeaderText="BASIS Number" DataField="BasisProjectNumber" />
                        <telerik:GridBoundColumn DataType="System.String" HeaderText="Code" DataField="Code" />
                        <telerik:GridBoundColumn DataType="System.String" HeaderText="Description" DataField="Description" />
                        <telerik:GridBoundColumn DataType="System.Double" HeaderText="USGS CWP" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                        <telerik:GridBoundColumn DataType="System.Double" HeaderText="Customer" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this Studies/Support Funding?" ButtonType="ImageButton"
                            CommandName="Delete" Text="Remove" UniqueName="Delete" Visible="false" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <!-- Documents   -->
        <telerik:RadPageView ID="rpvDocuments" runat="server">
            <div class="SectionContent" style="width: 500px;">
                <h2>JFA Documents Generator</h2>
                <telerik:RadAjaxPanel runat="server" ID="rapBtn" LoadingPanelID="ralpDocument">
                    <table>
                        <tr>
                            <td align="right">Director:
                            </td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="rcbDirector" DataTextField="Name" DataValueField="CenterDirectorID" AppendDataBoundItems="true" Skin="Silk" Width="250px">
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
                                <telerik:RadComboBox runat="server" ID="rcbFinancialReviewer" DataTextField="Name" DataValueField="CenterFinancialReviewerID" AppendDataBoundItems="true" Skin="Silk" Width="250px">
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
                                <telerik:RadTextBox runat="server" ID="rtbDUNS" Skin="Silk" Width="250px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Project Number:
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="rtbProjectNumber" Skin="Silk" Width="250px" />
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
            <br />
            <br />
            <div class="SectionContent" style="width: 500px;">
                <h2>Upload Documents</h2>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td valign="bottom">
                                        <telerik:RadAsyncUpload MultipleFileSelection="Automatic" Visible="false" runat="server" ID="rauFile" Skin="Silk" Width="100%" />
                                    </td>
                                    <td valign="bottom" style="padding-bottom: 8px;">
                                        <telerik:RadButton Visible="false" runat="server" ID="rbUploadDocuments" Text="Upload" Skin="Silk" OnClick="rbUploadDocuments_Click" />
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

                <br />
                <br />
            </div>
        </telerik:RadPageView>
        <!-- Cooperative Funding -->
        <telerik:RadPageView runat="server" ID="rpvCooperativeFunding" TabIndex="6">
            <table>
                <tr>
                    <td>
                        <telerik:RadSearchBox Width="250px" runat="server" ID="rsbCoopFunding" OnSearch="rsbCoopFunding_Search" Skin="Silk" DropDownSettings-Height="0px" />
                    </td>
                    <td>
                        <telerik:RadButton runat="server" ID="rbShowAll" Text="Show All"
                            Skin="Silk" OnClick="rbShowAll_Click" />
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="rgCoopFunding" runat="server" Width="100%"
                AutoGenerateColumns="False" Skin="Silk" AllowPaging="true" 
                PageSize="10" AllowMultiRowSelection="False" OnNeedDataSource="rgCoopFunding_NeedDataSource" OnDetailTableDataBind="rgCoopFunding_DetailTableDataBind" OnInsertCommand="rgCoopFunding_InsertCommand" OnUpdateCommand="rgCoopFunding_UpdateCommand">
                <PagerStyle PageSizes="10" Mode="NextPrevAndNumeric" />
                <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                <ItemStyle />
                <MasterTableView Width="100%" DataKeyNames="AgreementID" AllowMultiColumnSorting="True">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <GroupHeaderItemStyle ForeColor="Black" />
                    <DetailTables>
                        <telerik:GridTableView InsertItemDisplay="Bottom"  CommandItemDisplay="Top" DataKeyNames="CooperativeFundingID"
                            Name="Accounts" Width="100%" EditMode="EditForms" AllowPaging="false">
                            <EditFormSettings UserControlName="Controls/RadGrid/CoopFundingControl.ascx" EditFormType="WebUserControl" />
                            <HeaderStyle BackColor="#2dabc1" ForeColor="White"  Font-Bold="true" Font-Size="Small" />
                            <CommandItemSettings AddNewRecordText="Add New Account" ShowRefreshButton="false"  />
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" HeaderStyle-BackColor="#fccc67" HeaderStyle-Font-Size="Small"
                                    CommandName="Edit" Text="Edit" UniqueName="Edit" />
                                <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FiscalYear" />
                                <telerik:GridBoundColumn HeaderText="Mod" DataField="ModNumber" />
                                <telerik:GridBoundColumn HeaderText="Account" DataField="AccountNumber" />
                                <telerik:GridBoundColumn HeaderText="USGS" DataField="FundingUSGSCWP" DataFormatString="{0:c0}" />
                                <telerik:GridBoundColumn HeaderText="Cooperator" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                                <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" />
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
        <telerik:RadPageView runat="server" TabIndex="7">
            <div class="SectionContent" style="width: 800px;">
                <h2>Edit Agreement</h2>
                <table>
                    <tr>
                        <td align="right">Agreement Number:
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbPurchaseOrderNumber" Skin="Silk" Text="<%# agreement.PurchaseOrderNumber %>" />
                        </td>
                        <td style="vertical-align: bottom;">
                            <a runat="server" id="AgreementToolTip">
                                <img src="http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                                    onmouseover="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                                    onmouseout="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
                            </a>
                            <telerik:RadToolTip runat="server" ID="rtt" TargetControlID="AgreementToolTip" ShowEvent="OnMouseOver"
                                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="Silk">
                                <div class="ToolTip" style="width: 200px;">
                                    Agreement number used in associated financial systems. If using the copy feature, please remove the words COPY_ from the Agreement entry
                                </div>
                            </telerik:RadToolTip>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Match Pair Code:
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbMPC" Skin="Silk" Text="<%# agreement.MatchPairCode %>" />
                        </td>
                        <td style="vertical-align: bottom;">
                            <a runat="server" id="MPCToolTip">
                                <img src="http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                                    onmouseover="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                                    onmouseout="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
                            </a>
                            <telerik:RadToolTip runat="server" ID="RadToolTip1" TargetControlID="MPCToolTip" ShowEvent="OnMouseOver"
                                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="Silk">
                                <div class="ToolTip" style="width: 200px;">
                                    Match Pair Code - Basis+ Specific code used to tie CWP funding to customer funding. Only relevant to JFA's
                                </div>
                            </telerik:RadToolTip>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Sales Order Number:
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbSalesDocument" Skin="Silk" Text="<%# agreement.SalesDocument %>" />
                        </td>
                        <td style="vertical-align: bottom;">
                            <a runat="server" id="SalesOrderNumberToolTip">
                                <img src="http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif" height="15" width="15"
                                    onmouseover="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTipHover.gif'"
                                    onmouseout="this.src='http://sifta.water.usgs.gov/NationalFunding/images/tooltip/ToolTip.gif'" />
                            </a>
                            <telerik:RadToolTip runat="server" ID="RadToolTip2" TargetControlID="SalesOrderNumberToolTip" ShowEvent="OnMouseOver"
                                OffsetX="10" OffsetY="10" HideEvent="LeaveToolTip" HideDelay="0"
                                Animation="None" Position="BottomRight" RelativeTo="Element" Skin="Silk">
                                <div class="ToolTip" style="width: 200px;">
                                    Auto-populated from FBMS
                                </div>
                            </telerik:RadToolTip>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Type:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbFundsType" runat="server" Skin="Silk" Width="160px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="F" Text="Fixed" />
                                    <telerik:RadComboBoxItem Value="A" Text="Actual" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Cycle:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbBillingCycle" runat="server" Skin="Silk" Width="160px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="Monthly" Text="Monthly" />
                                    <telerik:RadComboBoxItem Value="Quarterly" Text="Quarterly" />
                                    <telerik:RadComboBoxItem Value="Annually" Text="Annually" />
                                    <telerik:RadComboBoxItem Value="Semi-Annually" Text="Semi-Annual" />
                                    <telerik:RadComboBoxItem Value="Advance" Text="Advance" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <%--//Removed 11/4/2014 to remove tags--%> 
                    <%--<tr>
                        <td align="right">Tags:
                        </td>
                        <td>
                            <telerik:RadAutoCompleteBox runat="server" ID="racbTags" Skin="Silk" Delimiter=";" AllowCustomEntry="true" Width="450px" DataSourceID="ldsTags" DataTextField="Tag" />
                        </td>
                        <td>(Use ; to seperate tags)
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 136px;"></td>
                        <td>
                            <telerik:RadButton ID="btnUpdate" Text="Update" CommandName="Update" AutoPostBack="true" OnClick="btnUpdate_Click" CausesValidation="false" runat="server" Visible="true" Skin="Silk" />
                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" AutoPostBack="true" OnClick="btnCancel_Click" CausesValidation="False" CommandName="Cancel" Skin="Silk" />
                        </td>
                    </tr>
                </table>
                <asp:LinqDataSource ID="ldsTags" runat="server" ContextTypeName="NationalFundingDev.SiftaDBDataContext" EntityTypeName="" TableName="lutTags"></asp:LinqDataSource>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvAgreementLog" TabIndex="8">
            <div class="SectionContent">
                <h2>Add an Agreement Log</h2>
                <table>
                    <tr>
                        <td valign="top" align="right">Date and Time:
                        </td>
                        <td valign="top">
                            <telerik:RadDateTimePicker runat="server" ID="rdtpAgreementLogTime" Skin="Silk" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">Mod:
                        </td>
                        <td valign="top">
                            <telerik:RadComboBox runat="server" ID="rcbModNumberAgreementLog" Skin="Silk" Width="55px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">Action:
                        </td>
                        <td valign="top">
                            <telerik:RadComboBox runat="server" ID="rcbActionAgreementLog" Skin="Silk" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">Remarks:
                        </td>
                        <td valign="top">
                            <telerik:RadTextBox runat="server" ID="rtbRemarksAgreementLog" Skin="Silk" Width="500px" Height="200px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right"></td>
                        <td valign="top">
                            <telerik:RadButton runat="server" ID="rbSubmitAgreementLog" Text="Add Agreement Log" Skin="Silk" OnClick="rbSubmitAgreementLog_Click" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <div class="SectionContent">
                <h2>Agreement Logs</h2>
                <telerik:RadGrid runat="server" ID="rgAgreementLog" OnNeedDataSource="rgAgreementLog_NeedDataSource"
                    AutoGenerateColumns="False" AlternatingItemStyle-BackColor="LightGray" Width="100%"
                    Skin="Silk">
                    <HeaderStyle BackColor="#696f74" ForeColor="White" Font-Bold="true" Font-Size="Larger"
                        HorizontalAlign="Center" />
                    <MasterTableView AutoGenerateColumns="false" NoDetailRecordsText="No Logs have been entered."
                        NoMasterRecordsText="No Logs have been entered.">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Log" DataField="AgreementLog">
                                <ItemTemplate>
                                    <table cellpadding="0px" cellspacing="0px" width="100%">
                                        <tr>
                                            <td align="right">Update Type:
                                            </td>
                                            <td><%# AgreementLogType(Eval("AgreementModLogID").ToString()) %> made on <%# String.Format("{0:d}", Eval("LoggedDate")) %></td>
                                        </tr>
                                        <tr>
                                            <td align="right">Remarks:</td>
                                            <td><%# Eval("remarks") %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right"><b>Logged by <%# Eval("ModifiedBy") %> on <%# String.Format("{0:d} {0:t}", Eval("ModifiedDate")) %></b></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
