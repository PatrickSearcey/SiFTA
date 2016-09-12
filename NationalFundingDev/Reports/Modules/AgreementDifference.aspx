<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgreementDifference.aspx.cs" Inherits="NationalFundingDev.AgreementDifference" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agreement Funding Overview</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager runat="server" ID="rsm">
            </telerik:RadScriptManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />
            <telerik:RadAjaxPanel runat="server" ID="rap" LoadingPanelID="ralp">
                <table id="Main" style="min-width: 320px;">
                    <tr>
                        <td>
                            <telerik:RadTabStrip runat="server" ID="rtsMod" MultiPageID="rmpMod" Width="100%" AutoPostBack="true" OnTabClick="rtsMod_TabClick">
                            </telerik:RadTabStrip>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadMultiPage runat="server" ID="rmpMod" RenderSelectedPageOnly="false">
                                        </telerik:RadMultiPage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <telerik:RadButton ID="rbRefresh" runat="server" Text="Refresh">
                                            <Icon PrimaryIconCssClass="rbRefresh" />
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
            </telerik:RadAjaxPanel>
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
