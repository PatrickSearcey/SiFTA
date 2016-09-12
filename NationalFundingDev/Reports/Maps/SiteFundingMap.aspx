<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="SiteFundingMap.aspx.cs" Inherits="NationalFundingDev.Reports.SiteFundingMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="http://sifta.water.usgs.gov/NationalFundingDev2/Themes/Base/StyleSheets/Main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadAjaxManager runat="server" ID="ram">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="rbSelect">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="mapPanel" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
        <telerik:RadScriptManager runat="server" ID="rsm1">
        </telerik:RadScriptManager>
        <div class="SectionContent">
            <table>
                <tr>
                    <td>Organization</td>
                    <td>Start Date</td>
                    <td>End Date</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox runat="server" ID="rcbOrganization" Skin="Silk" Width="350px">
                            <Items>
                                <telerik:RadComboBoxItem Text="National Streamflow Information Program" Value="NSIP" />
                                <telerik:RadComboBoxItem Text="National Water-Quality Assessment" Value="NAWQA" />
                                <telerik:RadComboBoxItem Text="United States Army Corps of Engineers" Value="USACE" />
                                <telerik:RadComboBoxItem Text="International Joint Commission" Value="IJC" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="rdpSiteMapStartDate" Skin="Silk" />
                    </td>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="rdpSiteMapEndDate" Skin="Silk" />
                    </td>
                    <td>
                        <telerik:RadButton runat="server" ID="rbSelect" Text="Select" Skin="Silk" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder runat="server" ID="mapPanel"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
