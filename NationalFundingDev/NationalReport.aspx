<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="NationalReport.aspx.cs" Inherits="NationalFundingDev.NationalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    National Report
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="ralp" runat="server" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <h2>National Report</h2>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsOptions" MultiPageID="rmpOptions" Skin="Silk" Orientation="HorizontalTop" Width="100%" AutoPostBack="true" OnTabClick="rtsOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Funded Sites Map" Selected="true" TabIndex="0" />
            <telerik:RadTab Text="NSIP" TabIndex="1"  />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpOptions">
        <!-- SITES MAP -->
        <telerik:RadPageView runat="server" TabIndex="0" Selected="true" ContentUrl="~/Reports/SiteFundingMap.aspx" Height="750px">
            
        </telerik:RadPageView>
        <!-- COLLECTION CODE -->
        <telerik:RadPageView runat="server" TabIndex="1" ContentUrl="~/Reports/NSIPOperationReport.aspx" Height="1600px">
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
