<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="NationalFundingDev.Reports.Maps.SiteMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Map
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
    <style>
        b{
            font-size:medium;
            font-weight:bold;
            color:#555555;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div style="margin-top:20px; margin-left:10px;">
        <b>Enter agreement start and end dates to view sites.</b>
    </div>
    <iframe runat="server" id="mapFrame" height="800" width="1050" style="border:none;" allowfullscreen="true" mozallowfullscreen="true" webkitallowfullscreen="true"></iframe>
</asp:Content>
