//===========================================================
// MapUtils.js - javascript module for adding one or more independant leaflet maps with site points to a webpage
//
//-----------------------------------------------------------
// REFERENCING THIS MODULE:
//
// <script type="text/javascript" src="[PATH_TO_THIS_FILE]/MapUtils.js"></script>
// <script>
//     // create map(s) when SiftaMapUtils is ready
//     // initMap takes the following input:
//     //   "mapElemID"     - [REQUIRED] html map element id
//     //   "mapDataElemID" - [REQUIRED] html hidden element id containing map point geoJSON data
//     //   "basemap"       - [OPTIONAL] startup basemap to use when map is created, one of:
//     //                        "topo" [DEFAULT]
//     //                        "streets"
//     //                        "imagery"
//     //                        "relief"
//     //   "refreshZoom"   - [OPTIONAL] zoom mode when map is refreshed with new data, one of
//     //                        "points" : zoom to all site points on map
//     //                        "extend" : extend the current map extent to include any points outside the current extent
//     //                        "lower48": zoom to lower 48 US States
//     //                        "noZoom" : do nothing [DEFAULT]
//     //   opts.homeZoom    - [OPTIONAL] zoom mode when home button in zoom control is clicked, one of
//     //                        "points" : zoom to all site points on map
//     //                        "lower48": zoom to lower 48 US States [DEFAULT]
//     M.ready = function() {
//         M.initMap({ "mapElemID":"map01", "mapDataElemID":"mapData01", "basemap":"topo",    "refreshZoom":"points",  "homeZoom":"points"  });
//         M.initMap({ "mapElemID":"map02", "mapDataElemID":"mapData02", "basemap":"streets", "refreshZoom":"extend",  "homeZoom":"lower48" });
//         //...more maps...
//     };
//
//     // when sites data for one or more maps changes:
//     // (1) update the geoJSON string stored in the "mapDataElemID"
//     // (2) call M.refreshMaps();
// </script>
//
// NOTES:
//
// The page this is used in must:
// * have element(s) for 1 or more maps with an id set
// * have a HIDDEN element for each of the map(s) storing a geoJSON string of the current site points to show on the map
//   geoJSON must be like:
//     {
//        "type":"FeatureCollection",
//        "features":[
//            {
//                "type":"Feature",
//                "properties":{
//                    "SiteNumber"  : "08157560",
//                    "SiteName"    : "Waller Ck at E 1st St, Austin, TX",
//                    "iconURL"     : "sw_act_30.png",
//                    "SiteType"    : "Surface Water",
//                    "RealTimeSite": "Real-Time Site"
//               },
//               "geometry":{
//                    "type":"Point",
//                    "coordinates":[
//                        -97.739722,
//                        30.261944
//                    ]
//                }
//            },
//            ...MORE POINTS...
//        ]
//     }
// * reference this MapUtils module and init the map(s) as shown above
// * update the data in the hidden element(s) when the sites to show on the map(s) or their attributes change
// * call the M.refreshMaps(); function to refresh all maps with the new data
//
//-----------------------------------------------------------
// DEPENDANCIES:
//
// All required dependencies are dynamically loaded by the module.
//
//-----------------------------------------------------------
// TODO:
// ...add todo items here...
//
// BUGS:
// ...add bugs here...
//
//===========================================================
// "use strict"; // !!! DO NOT USE FOR PROD

// create module object
window.M = new Object();

// handle unavailable console logging - some browsers error if console not supported / unavailable
if (typeof window.console === "undefined") {
    window.console = {};
    window.console.log   = function (s) { return null; };
    window.console.warn  = function (s) { return null; };
    window.console.error = function (s) { return null; };
    window.console.clear = function (s) { return null; };
}

// get the path to the module directory (needed for css, js, and image files)
M.getScriptPath = function () {
    var scripts = document.getElementsByTagName("script");     // all scripts
    var path    = scripts[scripts.length-1].src.split("?")[0]; // get last one (this one) and remove any ?query
    var mydir   = path.split("/").slice(0, -1).join("/")+"/";  // remove last filename part of path
    return mydir;
}
M.rootPath = M.getScriptPath() + "../";

// required css & js for this module
// will be loaded in the orders below
// need full paths
M.dendancyUrls = [];
if (!window.jQuery) { M.dendancyUrls = [ M.rootPath + "jquery/jquery-1.11.1.min.js"]; } // need load jQuery
M.dendancyUrls = M.dendancyUrls.concat([ // add others
    // ...leaflet...
    M.rootPath + "leaflet/leaflet-0.7.3/leaflet.css",
    M.rootPath + "leaflet/leaflet-0.7.3/leaflet.js",
    // ...esri utils leaflet plugin...
    M.rootPath + "leaflet/esri-leaflet-0.0.1-beta.5/esri-leaflet.js",
    // ...zoom with home leaflet plugin...
    M.rootPath + "leaflet/leaflet-zoom-home/L.Control.ZoomHome.css",
    M.rootPath + "leaflet/leaflet-zoom-home/L.Control.ZoomHome.js",
    // ...fullscreen map leaflet plugin...
    M.rootPath + "leaflet/Leaflet.fullscreen-gh-pages/leaflet.fullscreen.css",
    M.rootPath + "leaflet/Leaflet.fullscreen-gh-pages/Leaflet.fullscreen.min.js",
    // ...lat-lng mouse position leaflet plugin...
    M.rootPath + "leaflet/Leaflet.Coordinates/Leaflet.Coordinates-0.1.4.css",
    M.rootPath + "leaflet/Leaflet.Coordinates/Leaflet.Coordinates-0.1.4.min.js",
    // ...jQuery UI...
    M.rootPath + "jquery/jquery-ui-1.11.2.custom/jquery-ui.min.css",
    M.rootPath + "jquery/jquery-ui-1.11.2.custom/jquery-ui.min.js",
    // ...this module...
    M.rootPath + "MapUtils/MapUtils.css"
]);


//===========================================================
// MODULE PROPERTIES

// all props are initialized with init() after dependencies loaded


//===========================================================
// FUNCTIONS

//-----------------------------------------------------------
// init
//   initialize module when all dependencies loaded
M.init = function () {
    var funcName = "MapUtils [init]: ";
    console.log(funcName + "");
    
    //-----------------------------------
    // startup options:
    M.startup_basemapOpacity = 0.7;
    
    //-----------------------------------
    // module configuration
    
    // object of maps keyed on map element id
    M.maps = {};
    
    // site icons
    var siteIcon = L.Icon.extend({
        "options" : {
            "iconSize"     : [22, 30],
            "iconAnchor"   : [10, 27],
            "popupAnchor"  : [ 0,-25]
        }
    });
    M.iconSW = new siteIcon({ "iconUrl" : M.rootPath+"MapUtils/images/iconSW.png" });
    M.iconGW = new siteIcon({ "iconUrl" : M.rootPath+"MapUtils/images/iconGW.png" });
    M.iconSP = new siteIcon({ "iconUrl" : M.rootPath+"MapUtils/images/iconSP.png" });
    M.iconAT = new siteIcon({ "iconUrl" : M.rootPath+"MapUtils/images/iconAT.png" });
    M.iconOT = new siteIcon({ "iconUrl" : M.rootPath+"MapUtils/images/iconOT.png" });
    
    // site markers to use if invalid icon specified in geoJson
    M.siteInvalidIcon = {
        "radius"      : 4,
        "fillColor"   : "#0ff",
        "color"       : "#000",
        "weight"      : 1,
        "opacity"     : 1,
        "fillOpacity" : 1
    };
    
    // bounds of lower 48 US States
    M.boundsUsa = L.latLngBounds( L.latLng(24,-125), L.latLng(50,-66) );
    
    // options for fitting map to bounds
    M.fitBoundsOpts = {
        "padding" : [0,0], // topleft & bottom right padding [px]
        "animate" : true
    };
    
    // zoom level to map scale lookup table
    // SOURCE: http://gis.stackexchange.com/questions/7430/what-ratio-scales-do-google-maps-zoom-levels-correspond-to
    M.scaleZoomLookup = {
        19 :       "1,128",
        18 :       "2,257",
        17 :       "4,514",
        16 :       "9,028",
        15 :      "18,056",
        14 :      "36,112",
        13 :      "72,224",
        12 :     "144,448",
        11 :     "288,895",
        10 :     "577,791",
         9 :   "1,155,581",
         8 :   "2,311,162",
         7 :   "4,622,325",
         6 :   "9,244,649",
         5 :  "18,489,298",
         4 :  "36,978,597",
         3 :  "73,957,194",
         2 : "147,914,388",
         1 : "295,828,775",
         0 : "591,657,551"
    };
    
    //-----------------------------------
    // execute ready function
    if (typeof M.ready === "function") {
        console.log(funcName + "executing 'ready' function");
        M.ready();
    } else {
        console.warn(funcName + "no 'ready' function defined");
    }
    
}; // init


//-----------------------------------------------------------
// refreshMaps
//   refresh all existing maps created by initMap() with any new data
M.refreshMaps = function () {
    var funcName = "MapUtils [refreshMaps]: ";
    console.log(funcName + "");
    
    // call initMap for all maps to update
    $.each( M.maps, function( mapElemID ) {
        M.initMap({"mapElemID":mapElemID});
    });
    
}; // refreshMaps


//-----------------------------------------------------------
// initMap
//   create / refresh map using current site data
//
// input opts: (required)
//   opts.mapElemID     - [REQUIRED] html map element id
//   opts.mapDataElemID - [OPTIONAL] html hidden element id containing map point data
//                        if not input the last value specified for this map is used
//                        if no value has been previously specified, an error occurs
//   opts.basemap       - [optional] startup basemap to use when map is created, one of:
//                          "topo" [DEFAULT]
//                          "streets"
//                          "imagery"
//                          "relief"
//   opts.refreshZoom   - [OPTIONAL] zoom mode when map is refreshed with new data, one of
//                          "points" : zoom to all site points on map
//                          "extend" : extend the current map extent to include any points outside the current extent
//                          "lower48": zoom to lower 48 US States
//                          "noZoom" : do nothing
//                        if not input the last mode specified for this map is used
//                        if no mode has been previously specified, "noZoom" used
//   opts.homeZoom      - [OPTIONAL] zoom mode when home button in zoom control is clicked, one of
//                          "points" : zoom to all site points on map
//                          "lower48": zoom to lower 48 US States
//                        if not input the last mode specified for this map is used
//                        if no mode has been previously specified, "lower48" used
M.initMap = function ( opts ) {
    var funcName = "MapUtils [initMap]: ";
    
    // do nothing if map element not input or does not exist
    if (opts.mapElemID === undefined) {
        console.warn(funcName + "required input 'mapElemID' not specified - stopping");
        return 1;
    }
    if ( $( "#"+opts.mapElemID ).length < 1 ) {
        console.warn(funcName + "no element with id '" + opts.mapElemID + "' in document to put the map in - stopping");
        return 1;
    }
    
    // see if map exists and create if needed
    if ( M.maps[opts.mapElemID] === undefined ) {
        console.log(funcName + "map does not exist: " + opts.mapElemID + " - creating");
        M.makeMap(opts);
    } else {
        console.log(funcName + "map exists: " + opts.mapElemID);
    }
    var map = M.maps[opts.mapElemID];
    
    // get data element for map and make sure exists
    var mapDataElemID = undefined; // no default
    if (map.M_mapDataElemID !== undefined) { mapDataElemID = map.M_mapDataElemID; } // set to last specified if exists
    if (opts.mapDataElemID  !== undefined) { mapDataElemID = opts.mapDataElemID;  } // set to input if specified
    if (mapDataElemID === undefined) {
        console.warn(funcName + "'mapDataElemID' not specified - stopping");
        return 1;
    }
    if ( $( "#"+mapDataElemID ).length < 1 ) {
        console.warn(funcName + "no element with id '" + opts.mapDataElemID + "' in document to get site data from - stopping");
        return 1;
    }
    map.M_mapDataElemID = mapDataElemID; // update
    
    // get point data from hidden field
    var currSiteObjStr = $.trim( $( "#"+mapDataElemID ).val() );
    
    // do nothing if the current sites have not changed (map does not need refreshing)
    if ( currSiteObjStr === map.prevSiteObjStr ) {
        console.log(funcName + "current site list has not changed - doing nothing");
        return 0;
    }
    
    //--------------------------------
    // need to update map
    console.log(funcName + "refreshing map");
    
    // delete any old point layer
    if (map.siteLayer !== undefined) { map.removeLayer(map.siteLayer); }
    
    // update prevSiteObj for future comparison to detect change in site list
    map.prevSiteObjStr = currSiteObjStr;
    
    // zoom to default view & do nothing if string empty (no points) - shouldn't happen
    if ( currSiteObjStr === "" ) {
        console.log(funcName + "no points to add");
        map.fitBounds( M.boundsUsa, M.fitBoundsOpts );  // zoom to USA
        map.zoomControl.options.homeGeom = M.boundsUsa; // update home button bounds
        return 0;
    }
    
    // create geoJson layer
    //     for geoJson specs see: http://geojson.org/geojson-spec.html
    //   validate and see on map: http://geojsonlint.com/
    //  good leaflet walkthrough: http://leafletjs.com/examples/geojson.html
    //    {
    //        "type":"FeatureCollection",
    //        "features":[
    //            {
    //                "type":"Feature",
    //                "properties":{
    //                    "SiteNumber"  : "08157560",
    //                    "SiteName"    : "Waller Ck at E 1st St, Austin, TX",
    //                    "iconURL"     : "sw_act_30.png",
    //                    "SiteType"    : "Surface Water",
    //                    "RealTimeSite": "Real-Time Site"
    //               },
    //               "geometry":{
    //                    "type":"Point",
    //                    "coordinates":[
    //                        -97.739722,
    //                        30.261944
    //                    ]
    //                }
    //            },
    //            ...MORE POINTS...
    //        ]
    //    }
    map.siteLayer = L.geoJson( $.parseJSON( currSiteObjStr ), {
        
        "pointToLayer": function (feature, latlng) {
            // set marker
            if (typeof feature.properties.iconURL !== "string") { feature.properties.iconURL = "NA"; }
            switch( feature.properties.iconURL.substring(0,2).toLowerCase() ) {
                case "sw":
                    return L.marker(latlng,{"icon":M.iconSW});
                    break;
                case "gw":
                    return L.marker(latlng,{"icon":M.iconGW});
                    break;
                case "sp":
                    return L.marker(latlng,{"icon":M.iconSP});
                    break;
                case "at":
                    return L.marker(latlng,{"icon":M.iconAT});
                    break;
                case "ot":
                    return L.marker(latlng,{"icon":M.iconOT});
                    break;
                default: // problem
                    console.warn(funcName + "point property 'iconURL' does not start with 'sw','gw','sp','at' or 'ot' (" + feature.properties.iconURL + ") - cyan circle used");
                    return L.circleMarker( latlng, M.siteInvalidIcon );
                    break;
            }
        },
        
        "onEachFeature": function (feature, layer) {
            // set popup
            layer.bindPopup(
                L.popup({
                    "keepInView"     : true,
                    "autoPanPadding" : [10,10]
                }).setContent(
                    "<b>" + $.trim(feature.properties.SiteNumber) + " " + $.trim(feature.properties.SiteName) + "</b>" + "<br/>" +
                    "<br/>" +
                    "<a href='http://sifta.water.usgs.gov/NationalFunding/Site.aspx?SiteNumber="+ $.trim(feature.properties.SiteNumber) +"'        target='_blank'>Funding Details Page              </a>" + "<br/>" +
                    "<a href='http://sims.water.usgs.gov/SIMSClassic/StationInfo.asp?agency_cd=USGS&site_no="+ $.trim(feature.properties.SiteNumber) +"' target='_blank'>SIMS Station Information Page Page</a>" + "<br/>" +
                    "<a href='http://waterdata.usgs.gov/nwis/inventory?site_no="+ $.trim(feature.properties.SiteNumber) +"'                              target='_blank'>NWIS Web                          </a>" + "<br/>" +
                    "<br/>" +
                    "<a href='javascript: M.maps[\""+opts.mapElemID+"\"].setView(["+feature.geometry.coordinates[1]+","+feature.geometry.coordinates[0]+"], M.maps[\""+opts.mapElemID+"\"].getMaxZoom()); void(0);'>Zoom To</a>"
                )
            );
        }
    });
    
    // zoom map based on opts.refreshZoom [optional]
    //   "points" : zoom to all site points on map
    //   "extend" : extend the current map extent to include any points outside the current extent
    //   "lower48": zoom to lower 48 US States
    //   "noZoom" : do nothing
    // if not input the last mode specified is used
    // if no mode has been previously specified, "noZoom" used
    var refreshZoom = "noZoom"; // default if no last specified or not input
    if (map.M_refreshZoom !== undefined) { refreshZoom = map.M_refreshZoom; } // set to last specified if exists
    if (opts.refreshZoom  !== undefined) { refreshZoom = opts.refreshZoom;  } // set to input if specified
    map.M_refreshZoom = refreshZoom; // update
    switch(refreshZoom) {
        case "points":
            // zoom to all site points on map
            map.fitBounds( map.siteLayer.getBounds(), M.fitBoundsOpts );
            break;
        case "extend":
            // extend the current map extent to include any points outside the current extent
            map.fitBounds( map.getBounds().extend( map.siteLayer.getBounds() ), M.fitBoundsOpts );
            break;
        case "lower48":
            // zoom to lower 48 US States
            map.fitBounds( M.boundsUsa, M.fitBoundsOpts );
            break;
        case "noZoom":
        default:
            // do nothing
    }
    
    // update home button bounds based on opts.homeZoom [optional]
    //   "points" : zoom to all site points on map
    //   "lower48": zoom to lower 48 US States
    // if not input the last mode specified for this map is used
    // if no mode has been previously specified, "lower48" used
    var homeZoom = "lower48"; // default if no last specified or not input
    if (map.M_homeZoom !== undefined) { homeZoom = map.M_homeZoom; } // set to last specified if exists
    if (opts.homeZoom  !== undefined) { homeZoom = opts.homeZoom;  } // set to input if specified
    map.M_homeZoom = homeZoom; // update
    switch(homeZoom) {
        case "points":
            // zoom to all site points on map
            map.zoomControl.options.homeGeom = map.siteLayer.getBounds();
            break;
        case "lower48":
        default:
            // zoom to lower 48 US States
            map.zoomControl.options.homeGeom = M.boundsUsa;
            break;
    }
    
    // add points to map
    map.siteLayer.addTo(map);
    
}; // initMap


//-----------------------------------------------------------
// makeMap
//   create map with controls
//   assumes map element exists
//   adds map to M.maps obj keyed on map element id
//
// opts: (required)
//   opts.mapElemID - [required] html map element id
//   opts.basemap   - [optional] startup basemap to use, one of:
//                      "topo" [DEFAULT]
//                      "streets"
//                      "imagery"
//                      "relief"
M.makeMap = function ( opts ) {
    var funcName = "MapUtils [makeMap]: ";
    console.log(funcName + "making map in id = " + opts.mapElemID);
    
    // add map overlay to hide while being created
    $("<div id='" + opts.mapElemID + "_overlay'></div>")
        .css({
            "position"         : "relative",
            "width"            : "100%",
            "height"           : "100%",
            "left"             : "0px",
            "top"              : "0px",
            "z-index"          : "999",
            "overflow"         : "hidden",
            "background-color" : "white"
        })
        .appendTo( $("#"+opts.mapElemID) ) // to map only
        .fadeIn(0);
    
    // basemaps (uses esri-leaflet.js) - need new layers for each map
    var basemap_topo = L.layerGroup([
        L.esri.tiledMapLayer("http://basemap.nationalmap.gov/ArcGIS/rest/services/USGSTopo/MapServer", {"opacity":M.startup_basemapOpacity} ) // base with labels
    ]);
    var basemap_streets = L.layerGroup([
        L.esri.tiledMapLayer("http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer", {"opacity":M.startup_basemapOpacity} ) // base with labels
    ]);
    var basemap_imagery = L.layerGroup([
        L.esri.tiledMapLayer("http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer",                           {"opacity":M.startup_basemapOpacity} ), // base
        L.esri.tiledMapLayer("http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer", {"opacity":M.startup_basemapOpacity} )  // labels
    ]);
    var basemap_relief = L.layerGroup([
        L.esri.tiledMapLayer("http://server.arcgisonline.com/ArcGIS/rest/services/World_Shaded_Relief/MapServer",                     {"opacity":M.startup_basemapOpacity} ), // base
        L.esri.tiledMapLayer("http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer", {"opacity":M.startup_basemapOpacity} )  // labels
    ]);
    
    // create map
    var map = new L.Map( opts.mapElemID, {
        // ...map state options...
        "layers"    : basemap_topo,     // startup basemap
        "center"    : L.latLng(39,-97), // startup center point
        "zoom"      : 4,  // startup zoom level
        "minZoom"   : 3,  // farthest out allowed
        "maxZoom"   : 13, // farthest in  allowed - NOTE: basemap_relief max available zoom is 13
        // ...interaction options...
        "dragging"           : true,
        "touchZoom"          : true,
        "scrollWheelZoom"    : true,
        "doubleClickZoom"    : true,
        "boxZoom"            : true,
        "tap"                : true,
        "tapTolerance"       : 15,
        "trackResize"        : true, // whether the map automatically handles browser window resize to update itself
        "worldCopyJump"      : true, // whether to jump to original map when pan around the world so all map objects are still visible
        "closePopupOnClick"  : false,
        "bounceAtZoomLimits" : true,
        // ...keyboard navigation options...
        "keyboard"           : true,
        "keyboardPanOffset"  : 80,
        "keyboardZoomOffset" : 1,
        // ...panning inertia options...
        "inertia"             : true,
        "inertiaDeceleration" : 3000,
        "inertiaMaxSpeed"     : 1500,
        //"inertiaThreshold"    : 14, // depends on mobile or not - use default
        // ...control options...
        "zoomControl"        : false, // using plugin with home button
        "attributionControl" : false,
        // ...animation options...
        //"fadeAnimation"        : true, // depends on css3 support - default true if supported
        //"zoomAnimation"        : true, // depends on css3 support - default true if supported
        "zoomAnimationThreshold" : 4
        //"markerZoomAnimation"  : true, // depends on css3 support - default true if supported
    }).whenReady( function() {
        // refresh
        L.Util.requestAnimFrame( this.invalidateSize, this, false, this.getContainer() );
        //L.Util.requestAnimFrame( map.invalidateSize, map, false, map.getContainer() ); // in general use this
        
        // fade out overlay then delete
        $("#"+opts.mapElemID+"_overlay").fadeOut(3000, function() {
            $(this).remove();
        });
    });
    
    //-----------------------------------
    // basemap menu
    
    //// get the startup basemap
    //if (typeof opts.basemap !== "string") { opts.basemap = "topo"; } // default
    //opts.basemap = opts.basemap.toLowerCase();
    //if ( $.inArray(opts.basemap,["topo","streets","imagery","relief"]) < 0 ) { opts.basemap = "topo"; } // invalid
    
    //// set control
    //var basemapLayerControl = L.control.layers(
    //    { // basemaps (radio buttons - only 1 shown at a time)
    //        "<span class='basemap-span'><div class='M-basemap-thumb topo'     ></div>Topographic</span>": basemap_topo,
    //        "<span class='basemap-span'><div class='M-basemap-thumb streets'  ></div>Streets    </span>": basemap_streets,
    //        "<span class='basemap-span'><div class='M-basemap-thumb imagery'  ></div>Imagery    </span>": basemap_imagery,
    //        "<span class='basemap-span'><div class='M-basemap-thumb relief'   ></div>Relief     </span>": basemap_relief
    //    },
    //    { // overlays (checkboxes - can show independently)
    //        // none
    //    },
    //    { // options
    //        "position"   : "topright", // "topleft", "topright", "bottomleft", "bottomright"
    //        "collapsed"  : true,       // true = use icon that opens to menu, false = no icon and menu always open
    //        "autoZIndex" : true        // true = automatically assign z-indexes to preserve order
    //    }
    //);
    //map.addControl( basemapLayerControl );
    
    //// set the startup basemap selected
    //$("#"+opts.mapElemID).find(".M-basemap-thumb."+opts.basemap).closest(".basemap-span").addClass("selected");
    
    //// customize button
    //$( basemapLayerControl.getContainer() ).find(".leaflet-control-layers-toggle").prop("id",opts.mapElemID+"_button_basemap").addClass("M-button");
    //$("#"+opts.mapElemID+"_button_basemap").append("<span>Change Basemap</span>");
    
    //// setup styling and behavior for basemap menu items
    //$("#"+opts.mapElemID+" .basemap-span").closest("label").addClass("basemap-menu-item"); // item wrapper including radio button, thumb, & label
    //$("#"+opts.mapElemID+" .basemap-span.selected").closest("label").addClass("selected"); // set the 1st selected for startup
    //$("#"+opts.mapElemID+" .basemap-menu-item")
    //    .hover( // fade thumb in-out & set border when hover over any item element that is not selected
    //        function() { $(this).not(".selected").find(".M-basemap-thumb").css({"border":"2px solid Gold"}).stop().animate({"opacity":"1.0"}, 300); },
    //        function() { $(this).not(".selected").find(".M-basemap-thumb").css({"border":"2px solid Gray"}).stop().animate({"opacity":"0.4"}, 300); }
    //    )
    //    .click( function() {
    //        // do nothing if selected
    //        if ( $(this).hasClass("selected") ) { return 0; }
            
    //        // de-select all items
    //        $("#"+opts.mapElemID+" .basemap-menu-item")
    //            .removeClass("selected")
    //            .find(".M-basemap-thumb").stop().css({"border":"2px solid Gray", "opacity":"0.4"});
    //        // select the clicked item
    //        $(this)
    //            .addClass("selected")
    //            .find(".M-basemap-thumb").stop().css({"border":"2px solid LimeGreen", "opacity":"1.0"});
    //        // update cursor
    //        $("#"+opts.mapElemID+" .basemap-menu-item.selected"        ).find("*").css({"cursor":"default"}); // startup: selected item cursor
    //        $("#"+opts.mapElemID+" .basemap-menu-item").not(".selected").find("*").css({"cursor":"pointer"}); // startup: non-selected item cursors
    //    });
    //$("#"+opts.mapElemID+" .basemap-menu-item.selected"        ).find(".M-basemap-thumb").css({"border":"2px solid LimeGreen", "opacity":"1.0"}); // startup: selected item image
    //$("#"+opts.mapElemID+" .basemap-menu-item").not(".selected").find(".M-basemap-thumb").css({"border":"2px solid Gray",      "opacity":"0.4"}); // startup: non-selected item images
    //$("#"+opts.mapElemID+" .basemap-menu-item.selected"        ).find("*").css({"cursor":"default"}); // startup: selected item cursor
    //$("#"+opts.mapElemID+" .basemap-menu-item").not(".selected").find("*").css({"cursor":"pointer"}); // startup: non-selected item cursors
    
    //// click startup basemap to show
    //$("#"+opts.mapElemID).find(".M-basemap-thumb."+opts.basemap).closest(".basemap-menu-item").click();
    
    //// add transparency slider
    //$( basemapLayerControl.getContainer() ).find(".leaflet-control-layers-list").append("<div id='"+opts.mapElemID+"_slider_basemap' style='margin:10px 10px 0px 10px;'></div>");
    //$("#"+opts.mapElemID+"_slider_basemap")
    //    .slider({
    //        "disabled"    : false,
    //        "orientation" : "horizontal",
    //        "range"       : false,
    //        "animate"     : "fast",
    //        "min"         : 0,
    //        "max"         : 1,
    //        "step"        : 0.05,
    //        "value"       : M.startup_basemapOpacity,
    //        "change" : function (evt,ui) {
    //            // set opacity of all layers in all basemap layer groups
    //            $.each([basemap_topo, basemap_streets, basemap_imagery, basemap_relief], function(idx,layerGroup) {
    //                layerGroup.eachLayer(function(layer) { layer.setOpacity(ui.value); });
    //            });
    //        },
    //        "slide" : function (evt,ui) {
    //            // set opacity of all layers in all basemap layer groups
    //            $.each([basemap_topo, basemap_streets, basemap_imagery, basemap_relief], function(idx,layerGroup) {
    //                layerGroup.eachLayer(function(layer) { layer.setOpacity(ui.value); });
    //            });
    //        }
    //    })
    //    .prop("title","Click or drag slider to set the basemap transparency");
    //$("#"+opts.mapElemID+"_slider_basemap").add( $("#"+opts.mapElemID+"_slider_basemap").find("*") ).css({"cursor":"pointer"});
    
    //-----------------------------------
    // fullscreen button (leaflet.fullscreen plugin)
    //   map.isFullscreen()     // is the map fullscreen?
    //   map.toggleFullscreen() // either go fullscreen, or cancel the existing fullscreen.
    var mapFullscreenControl = new L.Control.Fullscreen({
        "position" : "topleft",
        "title"    : {
            "false" : "View map in fullscreen",
            "true"  : "Exit fullscreen mode"
        }
    });
    map.addControl( mapFullscreenControl );
    map.on("fullscreenchange", function () {
        // do  nothing
    });
    
    //-----------------------------------
    // zoom widget with home button (L.Control.ZoomHome.js plugin)
    var zoomControl = new L.Control.ZoomHome({
        "position"      : "topleft",   // position on map, "topleft", "topright", "bottomleft", "bottomright"
        "zoomInText"    : "+",         // zoom in   button label
        "zoomInTitle"   : "Zoom in",   // zoom in   tooltip
        "zoomOutText"   : "-",         // zoom out  button label
        "zoomOutTitle"  : "Zoom out",  // zoom out  tooltip
        "zoomHomeText"  : "H",         // zoom home button label
        "zoomHomeTitle" : "Zoom home", // zoom home tooltip
        // ...added options (jvrabel@usgs.gov)...
        "buttonOrder"  : ["in","home","out"], // button order from top to bottom. 3-element array containing strings "in", "home", and "out". order in array sets order in widget.
        "homeGeom"     : M.boundsUsa          // bound to set when home button clicked
    });
    map.addControl( zoomControl ); // add control to map
    map.zoomControl = zoomControl; // add obj to map obj so can update bounds
    
    //-----------------------------------
    // lat-lng of mouse position
    
    // add control
    //var mouseCoordsControl = L.control.coordinates({
    //    "position"              : "bottomleft",  // "topleft", "topright", "bottomleft", "bottomright"
    //    "decimals"              : 3,             // # decimals used if not using DMS or labelFormatter functions
    //    "decimalSeperator"      : ".",           // decimal character (eg: "." US or "," EUR)
    //    "labelTemplateLat"      : "Lat: {y}&nbsp;", // latitude  label template
    //    "labelTemplateLng"      : "Lon: {x}",       // longitude label template
    //    "useLatLngOrder"        : true,          // if true lat-lng instead of lng-lat label ordering is used
    //    "useDMS"                : false,         // use Degree-Minute-Second?
    //    "enableUserInput"       : true,          // switch on/off input fields on click - when user enters coord marker put on map
    //    "centerUserCoordinates" : true           // if true user given coordinates are centered
    //});
    //map.addControl( mouseCoordsControl );
    
    //// show/hide when mouse enters/exits map
    //map.on("mouseover", function () { $( mouseCoordsControl.getContainer() ).stop().animate({"opacity":"0.6"}, 300) });
    //map.on("mouseout",  function () { $( mouseCoordsControl.getContainer() ).stop().animate({"opacity":"0.0"}, 300) });
    
    //// highlight/un-highlight & increase/decrease opacity when mouse enters/exits control
    //$( mouseCoordsControl.getContainer() ).mouseenter( function() {
    //    // highlight & full opacity
    //    $( mouseCoordsControl.getContainer() ).stop().addClass("highlight").animate({"opacity":"1.0"}, 200);
    //});
    //$( mouseCoordsControl.getContainer() ).mouseleave( function() {
    //    // un-highlight & reduce opacity
    //    $( mouseCoordsControl.getContainer() ).stop().removeClass("highlight").animate({"opacity":"0.6"}, 200);
    //    // collapse if editable when mouse leaves
    //    mouseCoordsControl.collapse();
    //});
    
    //// add map scale to reported lat-lng
    //$( mouseCoordsControl.getContainer() ).find(".uiElement.label").prepend("<span id='"+opts.mapElemID+"_mouseCoordsScale'></span>"); // add span for inserting map scale
    //map.on("zoomend", function () {
    //    // update scale when map zooms using fixed values based on zoom level
    //    var scale = M.scaleZoomLookup[ this.getZoom() ];
    //    if (scale !== undefined) { $("#"+opts.mapElemID+"_mouseCoordsScale").html("Scale: 1:" + scale + "&nbsp;&nbsp;"); }
    //});
    
    //// init on startup
    //$( mouseCoordsControl.getContainer() ).find(".labelFirst").html("Lat: Lon:"); // no lat-lng vals in case mouse not in map
    //$("#mouseCoordsScale").html("Scale: 1:" + M.scaleZoomLookup[ map.getZoom() ] + "&nbsp;&nbsp;"); // init scale
    //$( mouseCoordsControl.getContainer() ).css({"opacity":"0.6"}); // init opacity
    //$( mouseCoordsControl.getContainer() ).prop("title","Click to enter a coordinate and a place marker on the map"); // tooltip
    //$( mouseCoordsControl.getContainer() ).find(".inputX").prop("title","Enter a longitude (decimal degrees) to place marker on the map"); // tooltip
    //$( mouseCoordsControl.getContainer() ).find(".inputY").prop("title","Enter a latitude (decimal degrees) to place marker on the map" ); // tooltip
    
    //-----------------------------------
    // scale bar
    //L.control.scale(
    //    {
    //        "position"       : "bottomleft", // "topleft", "topright", "bottomleft", "bottomright"
    //        "maxWidth"       : 200,          // max width [px]
    //        "metric"         : true,         // show metric? (km/m)
    //        "imperial"       : true,         // show imperial? (mi/ft)
    //        "updateWhenIdle" : false         // true = wait for map to stop moving before updating, false = update as map is moved
    //    }
    //).addTo(map);
    
    //-----------------------------------
    // add map to map array
    M.maps[opts.mapElemID] = map;
    
}; // makeMap


//-----------------------------------------------------------
// ...more functions...


//===========================================================
// STARTUP

//-----------------------------------------------------------
// load css & js dependancies then init()

// function to load external script(s) and/or style sheet(s) into the document
// input "urls" is string array of urls: [ 'url1.css', 'url2.js', ... ]
// .js extension will load as script, .css extension will load as css
// loads in order given
// does not load next until 1st is loaded so scripts/css that depend on other scripts/css being loaded 1st (eg: jQuery & jQuery UI) can be handled
// optional 2nd arg "doneFunc" is function to execute when all are loaded (does nothing if not input)
M.loadJsCss = function (urls,doneFunc) {
    var funcName = "MapUtils [loadJsCss]: ";
    if (urls.length <= 0) {
        // done with all - execute function if given
        if (typeof doneFunc === "function") { doneFunc(); }
        return 0;
    };
    var url = urls.shift(); // get the 1st and remove from list
    
    // load current js or css
    if ( url.toLowerCase().indexOf(".css", url.length - ".css".length) !== -1 ) {
        // ends with .css
        console.log(funcName + "loading css: " + url);
        var ref = document.createElement("link");
        ref.setAttribute("rel","stylesheet");
        ref.setAttribute("type","text/css");
        ref.setAttribute("href",url);
        if (typeof ref !== "undefined" ) {document.getElementsByTagName("head")[0].appendChild(ref); }
        M.loadJsCss(urls,doneFunc); // call again with remaining
        
    } else if ( url.toLowerCase().indexOf(".js", url.length - ".js".length) !== -1 ) {
        // ends with .js
        console.log(funcName + "loading js: " + url);
        var script  = document.createElement("script");
        script.type = "text/javascript";
        script.src  = url;
        var head = document.getElementsByTagName("head")[0];
        var completed = false;
        script.onload = script.onreadystatechange = function () {
            if (!completed && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) {
                completed = true;
                M.loadJsCss(urls,doneFunc); // call again with remaining
                script.onload = script.onreadystatechange = null;
                head.removeChild(script);
            }
        };
        head.appendChild(script);
    } else {
        // neither
        console.warn(funcName + "url is not .css or .js (" + url + ") - doing nothing");
        M.loadJsCss(urls,doneFunc); // call again with remaining
    }
}; // loadJsCss

// load dependancies in order given then execute init when document ready
M.loadJsCss(M.dendancyUrls, function() {
    $( document ).ready(function() {
        M.init();
    });
});


//===========================================================
// END


