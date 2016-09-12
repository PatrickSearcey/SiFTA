<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="NationalFundingDev.Map.Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- jQuery -->
    <!-- NOTE: omit if main app already uses jQuery -->
    <script src="./jquery/jquery-1.11.1.min.js"></script>

    <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
    <!-- SiftaMapUtils -->
    <script type="text/javascript" src="./MapUtils/MapUtils.js"></script>
    <script>
        // configure: 

        // REQUIRED: element id to create the map in
        M.mapElemID = "map";

        // REQUIRED:
        // hidden element id that contains site geojson data to put on map
        // the main app must populate the data and call M.refreshMap(); after each update to update the map
        M.siteDataElemID = "mapPointData_hidden_element_id";

        // OPTIONAL:
        // zoom mode when map is refreshed, one of:
        //    "zoomToPoints": zoom to all site points on map
        //          "extend": extend the current map extent to include any points outside the current extent
        //         "zoomUsa": zoom to lower 48 US States
        //          "noZoom": do nothing (DEFAULT if not specified here)
        M.refreshZoomMode = "zoomToPoints";
    </script>
    <script>
        $(document).ready(function () {
            var map = $("#map");
            var height = $(window).height();
            var width = $(window).width();
            map.css("height", height);
            map.css("width", width);
        });
    </script>
    <style type="text/css">
        html, body, #map {
            margin: 0px;
            padding: 0px;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <!-- map -->
        <div id="map"></div>
        
        <!-- hidden data element that the real app updates -->
        <asp:HiddenField runat="server" ID="mapPointData_hidden_element_id" Value="" ClientIDMode="Static" />
        <script>
            // set static hidden element data to use: 5 sites, each a different site type
            // IMPORTANT: real app needs to set this and then call "M.refreshMap();" when data is updated to refresh map
            // IMPORTANT: below is the structure expected by the module - see jvrabel if adjustments need to be made
            var geoJson = {
                "type": "FeatureCollection",
                "features": [
                    {
                        "type": "Feature",
                        "properties": {
                            "SiteNumber": "08157560",
                            "SiteName": "Waller Ck at E 1st St, Austin, TX",
                            "iconURL": "sw_act_30.png",
                            "SiteType": "Surface Water",
                            "RealTimeSite": "Real-Time Site"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [
                                -97.739722,
                                30.261944
                            ]
                        }
                    },
                    {
                        "type": "Feature",
                        "properties": {
                            "SiteNumber": "301423097495901",
                            "SiteName": " YD-58-50-211",
                            "iconURL": "gw_act_30.png",
                            "SiteType": "Ground Water",
                            "RealTimeSite": "Real-Time Site"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [
                                -97.8275,
                                30.245
                            ]
                        }
                    },
                    {
                        "type": "Feature",
                        "properties": {
                            "SiteNumber": "08155500",
                            "SiteName": "Barton Spgs at Austin, TX",
                            "iconURL": "sp_act_30.png",
                            "SiteType": "Spring",
                            "RealTimeSite": "Real-Time Site"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [
                                -97.771111,
                                30.263333
                            ]
                        }
                    },
                    {
                        "type": "Feature",
                        "properties": {
                            "SiteNumber": "99999999",
                            "SiteName": "BOGUS NON-REALTIME ATMOSPHERIC SITE",
                            "iconURL": "at_act_30.png",
                            "SiteType": "Atmospheric",
                            "RealTimeSite": ""
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [
                                -97.5,
                                30.2
                            ]
                        }
                    },
                    {
                        "type": "Feature",
                        "properties": {
                            "SiteNumber": "88480206",
                            "SiteName": "USGS Central TX PO at Ferguson Ln, Austin, TX",
                            "iconURL": "ot_act_30.png",
                            "SiteType": "Other",
                            "RealTimeSite": "Real-Time Site"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [
                                -97.675833,
                                30.344722
                            ]
                        }
                    }
                ]
            };
            $("#mapPointData_hidden_element_id").val(JSON.stringify(geoJson));
        </script>
        </div>
    </form>
</body>
</html>
