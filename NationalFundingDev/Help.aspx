<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="NationalFundingDev.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Help Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div style="padding:25px;">
        <h2>Documents</h2>
         <ul>
            <li><a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/SiFTAUserManual.pdf">SiFTA User Manual</a></li>
            <li><a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/NSIP_Agreement.pdf">NSIP Agreement Guidance</a></li>
            	<li><a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/NGWMN_Agreement.pdf">National Groundwater Monitoring Network (NGWMN) Agreement Guidance</a> &middot; <a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/NGWMN_Agreement_1pager.pdf">NGWMN Agreement 1-pager</a> </li>
            <li><a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/USACE_Guidance.pdf">USACE Guidance</a> &middot <a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/USACE_1pager.pdf">USACE 1-pager</a></li>
            <li><a style="color:#2dabc1;" href="http://sifta.water.usgs.gov/Documents/AddingCustomerLogos.pdf">Customer Logo Guidelines</a></li>
        </ul>
        <h2>Contact Support</h2>
        <a href="mailto:siftahelp@usgs.gov" target="_blank">siftahelp@usgs.gov</a>
    </div>
    <script type="text/javascript" src="https://my.usgs.gov/jira/s/en_US3i0l72-418945332/845/6/1.2.9/_/download/batch/com.atlassian.jira.collector.plugin.jira-issue-collector-plugin:issuecollector/com.atlassian.jira.collector.plugin.jira-issue-collector-plugin:issuecollector.js?collectorId=6796af2a"></script>
</asp:Content>
