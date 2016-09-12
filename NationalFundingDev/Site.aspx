<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Site.aspx.cs" Inherits="NationalFundingDev.SitePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    <%= site.SiteNumber %> - <%= site.Name %>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    <style type="text/css">
        #ContentInformation {
            height: 110px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsSiteOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsSiteOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpSiteOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbFiscalYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSiteFundingOverView" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <b><%= site.SiteNumber %> - <%= site.Name %></b> (<%= site.Latitude %>, <%= site.Longitude %>)<br />
    <%= site.OfficeCode %> - <%= site.OfficeName %><br />
    <a href='<%= String.Format("http://waterdata.usgs.gov/nwis/nwisman/?site_no={0}", site.SiteNumber) %>'>NWIS</a> | 
    <a href='<%= String.Format("http://sims.water.usgs.gov/SIMSClassic/StationInfo.asp?site_no={0}&agency_cd=USGS", site.SiteNumber) %>'>SIMS</a><br />
    <br />
    <a href='<%= String.Format("Center.aspx?OrgCode={0}", GetSiteCentersHomeURL()) %>' style="color: orange;">Center Home</a> >> Site Portal
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsSiteOptions" MultiPageID="rmpSiteOptions" Skin="Silk" Width="100%" Orientation="HorizontalTop" AutoPostBack="true" OnTabClick="rtsSiteOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Site Overview" Selected="true" TabIndex="0" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpSiteOptions" RenderSelectedPageOnly="true">
        <!-- Site Overview -->
        <telerik:RadPageView ID="rpvSiteOverView" runat="server" Selected="true">
            <div style="padding-left: 20px; padding-bottom: 20px; padding-right: 20px;">
                <telerik:RadComboBox runat="server" ID="rcbFiscalYear" Skin="Silk" OnSelectedIndexChanged="rcbFiscalYear_SelectedIndexChanged" AutoPostBack="true" />
                <br />
                <br />

                <telerik:RadGrid runat="server" ID="rgSiteFundingOverView" OnNeedDataSource="rgSiteFundingOverView_NeedDataSource" OnPreRender="rgSiteFundingOverView_PreRender" AutoGenerateColumns="false">
                    <MasterTableView DataKeyNames="AgreementID" AlternatingItemStyle-BackColor="LightBlue">
                        <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="AgreementID" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="border-color: white;" valign="top" colspan="2">
                                                <b><a href='<%# String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID")) %>' style="color: #4a95a1;"><%# Eval("Name") %></a></b><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: white; width: 30px;" valign="top">
                                                <a href='<%# String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID")) %>' style="color: #4a95a1;">
                                                    <img alt="" height="50px" width="50px" src='<%# String.Format("http://sifta.water.usgs.gov/Services/Rest/Customer/CustomerIcon.ashx?CustomerID={0}", Eval("CustomerID")) %>' /></a>
                                            </td>
                                            <td style="border-color: white;" align="left" valign="top">
                                                <b>
                                                    <asp:LinkButton ID="lbAgreementId" runat="server" ForeColor="#4a95a1"
                                                        PostBackUrl='<%# "./Reports/Agreement/AgreementReport.aspx?AgreementID=" + Eval("AgreementID") %>'
                                                        Text='<%# String.Format("Agreement: {0}", Eval("PurchaseOrderNumber")) %>' /></b><br />
                                                <%# String.Format("{0:MM/dd/yy} - {1:MM/dd/yy}", Eval("StartDate"), Eval("EndDate")) %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <div class="SiteFundingTable">
                                        <table>
                                            <tr>
                                                <td><b>Collection:</b> <%# Eval("CollectionCode") %> - <%# Eval("CollectionDescription") %></td>
                                            </tr>
                                            <tr>
                                                <td><b>Units:</b><%# Eval("CollectionUnits") %> | <b>Difficulty Factor:</b><%# Eval("DifficultyFactor") %> | <b>USGS CWP:</b><%# Eval("FundingUSGSCWP", "{0:c0}") %> | <b>Customer:</b><%# Eval("FundingCustomer", "{0:c0}") %></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><b>Remarks:</b><%# Eval("Remarks") %></td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Agreement" FieldName="PurchaseOrderNumber" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="PurchaseOrderNumber" />
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
