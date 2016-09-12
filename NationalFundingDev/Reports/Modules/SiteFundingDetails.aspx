<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteFundingDetails.aspx.cs" Inherits="NationalFundingDev.Reports.Modules.SiteFundingDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Funding</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <table id="Main">
                <tr>
                    <td nowrap align="right" style="width: 200px;">
                        <b>Site Number:</b>
                    </td>
                    <td nowrap>
                        <%: siteFundingInformation.SiteNumber  %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Site Name:</b>
                    </td>
                    <td nowrap>
                        <%: siteFundingInformation.SiteName %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>USGS CWP:</b>
                    </td>
                    <td>
                        <%: String.Format("{0:C0}", siteFundingInformation.FundingUSGSCWP) %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Customer:</b>
                    </td>
                    <td>
                        <%: String.Format("{0:C0}", siteFundingInformation.FundingCustomer ) %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Other:</b>
                    </td>
                    <td>
                        <%: String.Format("{0:C0}", siteFundingInformation.FundingOther ) %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Collection Category:</b>
                    </td>
                    <td>
                        <%: siteFunding.lutCollectionCode.Category %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Collection Code:</b>
                    </td>
                    <td>
                        <%: siteFunding.lutCollectionCode.Code + " - " + siteFunding.lutCollectionCode.Description %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Units:</b>
                    </td>
                    <td>
                        <%: siteFunding.CollectionUnits %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Difficulty Factor:</b>
                    </td>
                    <td>
                        <%: siteFunding.DifficultyFactor %>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" nowrap>
                        <b>Difficulty Factor Reason:</b>
                    </td>
                    <td>
                        <%: siteFunding.DifficultyFactorReason %>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <b>Remarks:</b>
                    </td>
                    <td>
                        <%: siteFunding.Remarks %>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                var buffer = 80;
                var div = $("#Main");
                window.resizeTo(div.width() + buffer, div.height() + buffer);
            });
        </script>
    </form>
</body>
</html>
