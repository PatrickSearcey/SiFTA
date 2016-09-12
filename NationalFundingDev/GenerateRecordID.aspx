<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateRecordID.aspx.cs" Inherits="NationalFundingDev.GenerateRecordID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Assigned Record ID's</h2>
        <table>
            <tr>
                <td align="right"><b>Agreements :</b></td>
                <td><asp:Literal runat="server" ID="ltlAgreement" /></td>
            </tr>
            <tr>
                <td align="right"><b>Mods :</b></td>
                <td><asp:Literal runat="server" ID="ltlMods" /></td>
            </tr>
            <tr>
                <td align="right"><b>Site Funding :</b></td>
                <td><asp:Literal runat="server" ID="ltlSiteFunding" /></td>
            </tr>
            <tr>
                <td align="right"><b>Studies Funding :</b></td>
                <td><asp:Literal runat="server" ID="ltlStudiesFunding" /></td>
            </tr>
        </table>
        <h2 style="color:red;">Record ID's Updated For These Records!</h2>
    </div>
    </form>
</body>
</html>
