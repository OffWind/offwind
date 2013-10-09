var MapManager = {};
var TableManager = {};
var UrlSetPoint = '';
var UrlPointPage = '';
var UrlGetMapData = '';
var UrlDatabaseSwitch = '';

(function () {
    var map;
    var clusters = null;
    var dbMarker;
    var dbHoverMarker;
    var centerPoint = new google.maps.LatLng(11, 50);

    function createMarker(point, icon) {
        return new google.maps.Marker({
            position: point,
            draggable: true,
            raiseOnDrag: false,
            icon: icon,
            title: 'Sample',
            map: map
        });
    }

    function createClusterItem(dbItem, place) {
        var marker = new google.maps.Marker({
            position: place,
            draggable: false,
            clickable: true,
            title: dbItem[3] + ' [' + dbItem[1] + '; ' + dbItem[2] + ']'
        });
        google.maps.event.addListener(marker, 'click', function (e) {
            $.ajax({
                url: UrlSetPoint,
                dataType: "json",
                data: { coord: e.latLng.toString() }
            }).done(function () {
                window.open(UrlPointPage);
            });
        });
        marker.id = dbItem[0];
        marker.setIcon("https://maps.gstatic.com/mapfiles/ms2/micons/yellow-dot.png");
        return marker;
    }

    MapManager.clearMap = function () {
        if (clusters) {
            clusters.clearMarkers();
            clusters = null;
        }
    };

    MapManager.refreshMap = function () {
        var data = null;
        $.ajax({
            url: UrlGetMapData,
            dataType: "json",
            data: data
        }).done(function (data) {
            var markers = [];
            MapManager.clearMap();
            for (var i = 0; i < data.length; i++) {
                var coord = new google.maps.LatLng(data[i][1], data[i][2]);
                var m = createClusterItem(data[i], coord);
                markers.push(m);
            }
            clusters = new MarkerClusterer(map, markers, {
                maxZoom: 20,
                gridSize: null,
                styles: null
            });
            setTimeout(function() {
                $('#wait_dlg').modal('hide');
            }, 1000);
        });
    };


    MapManager.initialize = function () {
        var mapOptions = {
            zoom: 1,
            minZoom: 1,
            center: centerPoint,
            mapTypeId: google.maps.MapTypeId.SATELLITE
        };
        map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

        dbMarker = createMarker(new google.maps.LatLng(0, 0));
        dbMarker.setIcon("https://maps.gstatic.com/mapfiles/ms2/micons/yellow-dot.png");
        dbMarker.visible = false;
        dbMarker.zIndex = 1;

        dbHoverMarker = createMarker(new google.maps.LatLng(0, 0));
        dbHoverMarker.setIcon("https://maps.gstatic.com/mapfiles/ms2/micons/ltblue-dot.png");
        dbHoverMarker.visible = false;
        dbHoverMarker.zIndex = 2;

        MapManager.refreshMap();
    };

    MapManager.clearClusters = function (e) {
        e.preventDefault();
        e.stopPropagation();
        clusters.clearMarkers();
    };

    MapManager.setHover = function (point) {
        dbHoverMarker.setPosition(point);
        dbHoverMarker.title = "";
        dbHoverMarker.visible = true;
        map.setCenter(point);
    };
    MapManager.hideHover = function () {
        dbHoverMarker.visible = false;
    };
})();

$(document).ready(function () {
    $('#wait_dlg').modal('show');
    MapManager.initialize();
    $("input:radio[name=DbType]").change(function () {
        var value = $("input[name=DbType]:checked").val();
        $('#wait_dlg').modal('show');
        setTimeout(function() {
            $.ajax({
                url: UrlDatabaseSwitch,
                data: { id: value }
            }).done(function() {
                MapManager.refreshMap();
            });
        }, 1000);
    });
});
