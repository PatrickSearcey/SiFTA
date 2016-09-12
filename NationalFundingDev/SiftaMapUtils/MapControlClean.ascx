<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapControlClean.ascx.cs" Inherits="NationalFundingDev.MapControlClean" ClassName="MapControlClean" %>
<script type="text/javascript" src="http://sifta.water.usgs.gov/NationalFundingDev/SiftaMapUtils/jquery/jquery-1.11.1.min.js"></script>
<script type="text/javascript" src="http://sifta.water.usgs.gov/NationalFundingDev/SiftaMapUtils/MapUtils/MapUtilsClean.js"></script>
<style type="text/css">
    .legend {
        position: absolute; 
        top: 5px; 
        right: 5px;
         z-index: 100;
        width: 130px;
        color: black;
        background-color: white;
        /* IE 8 */
        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=80)";
        /* IE 5-7 */
        filter: alpha(opacity=80);
        /* Netscape */
        -moz-opacity: 0.8;
        /* Safari 1.x */
        -khtml-opacity: 0.8;
        /* Good browsers */
        opacity: 0.8;
        -moz-border-radius: 2%;
        -webkit-border-radius: 2%;
        -khtml-border-radius: 2%;
        border-radius: 2%;
        padding: 10px;
        vertical-align: bottom;
    }
</style>
<script>
    var mapElemID = '<%= map.ClientID %>';
    var siteDataElemID = '<%= hfSites.ClientID %>';
    M.ready = function () {
        M.initMap({ "mapElemID": mapElemID, "mapDataElemID": siteDataElemID, "basemap": "topo", "refreshZoom": "points", "homeZoom": "points" });
    };
    $(document).ready(function () {
        var height = $('#<%= hfHeight.ClientID %>').val();
        var width = $('#<%= hfWidth.ClientID %>').val();
        var map = $('#<%= map.ClientID %>');
        map.css("height", height);
        map.css("width", width);
        $(".legendBody").hide();
    });
</script>

<asp:Panel runat="server" ID="map">
    <div class="legend">
    <center><b class="SiteType">Site Types</b></center>
    <div class="legendBody">
    <hr />
    <table>
        <tr>
            <td>
                    <img src="http://maps.waterdata.usgs.gov/mapper/images/act/sw_act_30.png" alt="Surface-Water" />
            </td>
            <td style="vertical-align: top;"><span style="vertical-align: top;">Surface-Water</span><div>
            </td>
        </tr>
        <tr>
            <td>
                    <img src="http://maps.waterdata.usgs.gov/mapper/images/act/gw_act_30.png" alt="Groundwater" />
            </td>
            <td style="vertical-align: top;"><span style="vertical-align: top;">Groundwater</span><div>
            </td>
        </tr>
        <tr>
            <td>
                    <img src="http://maps.waterdata.usgs.gov/mapper/images/act/sp_act_30.png" alt="Spring" />
            </td>
            <td style="vertical-align: top;"><span style="vertical-align: top;">Spring<br />
            </span>
            </td>
        </tr>
        <tr>
            <td>
                    <img src="http://maps.waterdata.usgs.gov/mapper/images/act/at_act_30.png" alt="Atmospheric" />
            </td>
            <td style="vertical-align: top;"><span style="vertical-align: top;">Atmospheric</span><div>
            </td>
        </tr>
        <tr>
            <td>
                    <img src="http://maps.waterdata.usgs.gov/mapper/images/act/ot_act_30.png" alt="Other" />
            </td>
            <td style="vertical-align: top;"><span style="vertical-align: top;">Other</span><div>
            </td>
        </tr>
    </table>
</div>
    <script>
        var legend = $(".legend");
        var legendBody = $(".legendBody");
        legend.hover(function () {
            legendBody.show();
        }, function () {
            legendBody.hide();
        });
    </script>
</div>
</asp:Panel>
<asp:HiddenField runat="server" ID="hfSites" Value="" />
<asp:HiddenField runat="server" ID="hfHeight" />
<asp:HiddenField runat="server" ID="hfWidth" />





