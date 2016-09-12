<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudiesFundingDetails.aspx.cs" Inherits="NationalFundingDev.Reports.Modules.StudiesFundingDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Studies Funding</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <table id="Main">
        <tr>
            <td align="right" valign="top">
                <b>Type:</b>
            </td>
            <td>
                <%: studyFunding.lutResearchCode.Description %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <b>Basis Project Number:</b>
            </td>
            <td>
                <%: studyFunding.BasisProjectNumber %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <b>USGS CWP:</b>
            </td>
            <td>
                <%: String.Format("{0:C0}", studyFunding.FundingUSGSCWP) %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <b>Customer:</b>
            </td>
            <td>
                <%: String.Format("{0:C0}", studyFunding.FundingCustomer) %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <b>Other:</b>
            </td>
            <td>
                <%: String.Format("{0:C0}", studyFunding.FundingOther) %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <b>Remarks:</b>
            </td>
            <td>
                <%: studyFunding.Remarks %>
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
