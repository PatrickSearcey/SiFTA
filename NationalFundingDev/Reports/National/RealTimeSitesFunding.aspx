<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="RealTimeSitesFunding.aspx.cs" Inherits="NationalFundingDev.Reports.National.RealTimeSitesFunding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Real-Time Sites Funding
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <link rel="stylesheet" href="http://cdn.leafletjs.com/leaflet-0.7.3/leaflet.css" />
    <script src="http://cdn.leafletjs.com/leaflet-0.7.3/leaflet.js"></script>
    <style type="text/css">
        .info {
            padding: 6px 8px;
            font: 14px/16px Arial, Helvetica, sans-serif;
            background: white;
            background: rgba(255,255,255,0.8);
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            border-radius: 5px;
        }

        .title {
            z-index:15;
            position:absolute;
            top: 0px;
            left:100px !important;
            width:425px;
        }

        .info h4 {
            margin: 0 0 5px;
            color: #777;
        }

        .legend {
            line-height: 18px;
            color: #555;
        }

            .legend i {
                width: 18px;
                height: 18px;
                float: left;
                margin-right: 8px;
                opacity: 0.7;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div id="map" style="min-width: 800px; min-height: 700px;"></div>
    <script type="text/javascript">
        var map = L.map('map');
        var info = L.control();
        var title = L.control();

        function highlightFeature(e) {
            var layer = e.target;
            info.update(layer.feature.properties);
            layer.setStyle({
                weight: 2,
                color: 'black',
                dashArray: '',
                fillOpacity: 0.7
            });

            if (!L.Browser.ie && !L.Browser.opera) {
                layer.bringToFront();
            }
        }
        function resetHighlight(e) {
            var layer = e.target;
            layer.setStyle(
                {
                    weight: 2,
                    opacity: 1,
                    color: '#FFFFFF',
                    dashArray: '3',
                    fillOpacity: 0.7
                });
            info.update();
        }
        function zoomToFeature(e) {
            //map.fitBounds(e.target.getBounds());
        }
        function doubleClick(e) {
            var orgCode = e.target.feature.properties.OrgCode;
            window.open('http://sifta.water.usgs.gov/NationalFunding/Reports/Center/CenterReport.aspx?OrgCode=' + orgCode, 'SiteFunding', 'window settings');
        }
        function getColor(d) {
            return d == 100 ? '#1C9412' :
                    d > 96 ? '#35AE2B' :
                    d > 94 ? '#62DA2C' :
                    d > 92 ? '#86E337' :
                    d > 90 ? '#B5EF48' :
                    d > 88 ? '#E3FA5A' :
                    d > 86 ? '#F9F052' :
                    d > 84 ? '#F7CB49' :
                    d > 82 ? '#F6A240' :
                    d > 80 ? '#F47738' :
                    '#F2492F'
        };
        function style(feature) {
            return {
                fillColor: getColor(feature.properties.Percentage),
                weight: 2,
                opacity: 1,
                color: '#FFFFFF',
                dashArray: '3',
                fillOpacity: 0.7
            };
        }
        function onEachFeature(feature, layer) {
            layer.on({
                mouseover: highlightFeature,
                mouseout: resetHighlight,
                click: zoomToFeature,
                dblclick: doubleClick
            });
        }
        $(document).ready(function () {

            //Set its max zoom to be 14
            map.options.maxZoom = 14;
            map.setView([50.8282, -110.5795], 3);
            L.tileLayer('http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', { attribution: 'Map data &copy;' }).addTo(map);

            $.getJSON("http://sifta.water.usgs.gov/ServicesDev/Rest/site/CenterRealTimeSiteFunding.ashx", function (data) {
                L.geoJson(data, { style: style, onEachFeature: onEachFeature }).addTo(map);
            });



            info.onAdd = function (map) {
                this._div = L.DomUtil.create('div', 'info'); // create a div with a class "info"
                this.update();
                return this._div;
            };

            // method that we will use to update the control based on feature properties passed
            info.update = function (props) {
                this._div.innerHTML = (props ?
                    '<h4>' + props.Center + '</h4>' + props.TotalSites + ' Real-Time Sites <br/>'
                    + props.Percentage + '% (' + props.FundedSites + ' sites) Funded<br/>'
                    + (100 - props.Percentage).toFixed(2) + '% (' + (props.TotalSites - props.FundedSites) + ' sites) Unfunded'
                    : 'Hover over a Center for more information <br/> Double-click a Center to view a report');
            };

            info.addTo(map);
            var legend = L.control({ position: 'bottomright' });

            legend.onAdd = function (map) {

                var div = L.DomUtil.create('div', 'info legend'),
                    grades = [100, 98, 96, 94, 92, 90, 88, 86, 84, 82, 80, 0],
                    labels = [];
                div.innerHTML = "<b>(Funded %)</b><br/><br/>";
                div.innerHTML +=
                        '<i style="background:' + getColor(grades[0]) + '"></i> ' + grades[0] + "<br/>";// +
                //'<i style="background:' + getColor(95) + '"></i> ' + "90-99" + "<br/>"
                // loop through our density intervals and generate a label with a colored square for each interval
                for (var i = 1; i < grades.length - 1; i++) {
                    div.innerHTML +=
                        '<i style="background:' + getColor(grades[i]) + '"></i> ' +
                        grades[i + 1] + (grades[i] ? '&ndash;' + grades[i] + '<br>' : '&ndash;0');
                }

                return div;
            };
            legend.addTo(map);

            var title = L.control({ position: 'topleft' });

            title.onAdd = function (map) {

                var div = L.DomUtil.create('div', 'info title');
                div.innerHTML = "<h1>Real-time Sites Funding Status</h1>";

                return div;
            };
            title.addTo(map);
        });

    </script>
</asp:Content>
