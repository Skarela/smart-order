var infobubbles = [];

function createMap() {
    var mapOptions = {
        center: convertToPosition(20.967136, -89.6249467),
        zoom: 12,
        panControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var newMap = new google.maps.Map(document.getElementById("Map_canvas"), mapOptions);
    google.maps.visualRefresh = true;
    map = newMap;
}

function addListenerToMapByClick(icon) {
    google.maps.event.addListener(map, 'click', function (event) {
        clearMarkers();
        changePositionToMarker(event.latLng, icon, false);
        revalidateLatitudeAndLongitude();
    });
}

function addListenerToMapByClickDiferentMarker(callback) {
    google.maps.event.addListener(map, 'click', function (event) {
        clearMarkers();
        callback(event);
        revalidateLatitudeAndLongitude();
    });
}

function geocodeLatLng(latlng) {
    var geocoder = new google.maps.Geocoder;
    geocoder.geocode({ 'location': latlng }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                console.log(results[1]);
            } else {
                window.alert('No results found');
            }
        } else {
            window.alert('Geocoder failed due to: ' + status);
        }
    });
}

function createMarker(position, icon) {
    var marker = new google.maps.Marker({
        position: position,
        map: map,
        icon: icon
    });

    markers.push(marker);
    return marker;
}

function CreateInfobubble(marker, content, position, numberChars, rows) {
    var maxWidth = 350;
    var minWidth = 350;

    if (numberChars <= 5) {
        minWidth = 75;
        maxWidth = 75;
    } else if (numberChars > 5 && numberChars <= 10) {
        minWidth = 125;
        maxWidth = 125;
    } else if (numberChars > 10 && numberChars <= 15) {
        minWidth = 155;
        maxWidth = 155;
    } else if (numberChars > 15 && numberChars <= 20) {
        minWidth = 175;
        maxWidth = 175;
    } else if (numberChars > 20 && numberChars <= 25) {
        minWidth = 195;
        maxWidth = 195;
    } else if (numberChars > 25 && numberChars <= 30) {
        minWidth = 235;
        maxWidth = 235;
    } else if (numberChars > 30 && numberChars <= 35) {
        minWidth = 280;
        maxWidth = 280;
    } else if (numberChars > 35 && numberChars <= 40) {
        minWidth = 300;
        maxWidth = 300;
    } else if (numberChars > 40 && numberChars <= 45) {
        minWidth = 330;
        maxWidth = 330;
    }

    var minHeight = 45;
    var maxHeight = 45;

    if (rows == 2) {
        minHeight += 15;
        maxHeight += 15;
    }
    else if (rows == 3) {
        minHeight += 30;
        maxHeight += 30;
    }
    else if (rows == 3) {
        minHeight += 45;
        maxHeight += 45;
    }
    else if (rows == 4) {
        minHeight += 60;
        maxHeight += 60;
    }

    if (numberChars > 45) {
        minHeight += 25;
        maxHeight += 25;
    }

    var infoBubble = new InfoBubble({
        content: content,
        firstContent: content,
        position: position,
        minWidth: minWidth,
        maxWidth: maxWidth,
        firstMinHeight: minHeight,
        firstMaxHeight: maxHeight,
        minHeight: minHeight,
        maxHeight: maxHeight,
        disableAutoPan: false,
        hideCloseButton: false,
        borderRadius: 0,
        backgroundClassName: 'phoney',
        overflow: 'none'
    });

    google.maps.event.addListener(marker, 'click', function () {
        closeAllInfoBubbles();
        infoBubble.open(map, marker);
    });

    infobubbles.push(infoBubble);
    return infoBubble;
}

function clearMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers.length = 0;
}

function clearInfoBubbles() {
    for (var i = 0; i < infobubbles.length; i++) {
        infobubbles[i].close();
    }
    infobubbles.length = 0;
}

function closeAllInfoBubbles() {
    for (var i = 0; i < infobubbles.length; i++)
        infobubbles[i].close();
}

function convertToPosition(latitude, longitude) {
    return new google.maps.LatLng(latitude, longitude);
}

function resizeMap() {
    google.maps.event.trigger(map, "resize");
}

function changePositionToMarker(position, icon, isManual) {
    clearMarkers();
    createMarker(position, icon);

    if (isManual) {
        map.setCenter(position);
    } else {
        $("#Latitude").val(position.lat().toPrecision(16));
        $("#Longitude").val(position.lng().toPrecision(16));
    }
    enabledActionMapButtons();
}

function revalidateMap() {
    if ($("[name='Map'")[0] != undefined) {
        $('#Form').data('bootstrapValidator').revalidateField("Map");
    }
}

function revalidateLatitudeAndLongitude() {
    $('#Form').data('bootstrapValidator').revalidateField("Latitude");
    $('#Form').data('bootstrapValidator').revalidateField("Longitude");
}

function enabledActionMapButtons() {
    $("#ActionMapButtons").children().removeAttr("disabled");
}

function disabledActionMapButtons() {
    $("#ActionMapButtons").children().attr("disabled", "disabled");
}

function clickToMapButtons() {

    $(".ChangeIconMap").click(function () {
        if (markers.length > 0) {
            var icon = $($($(".ChangeIconMap").not(".active")[0]).find("img")[0]).attr("src");
            markers[0].setIcon(icon);
        }
    });

    $("#CenterMarkerMap").click(function () {
        var position = convertToPosition($("#Latitude").val(), $("#Longitude").val());
        map.setCenter(position);
    });

    $("#RemoveMarkerMap").click(function () {
        $("#Latitude").val(0);
        $("#Longitude").val(0);

        clearMarkers();
        disabledActionMapButtons();
        revalidateMap();
        revalidateLatitudeAndLongitude();
    });
}

/*==================================================================*/
/*                             A L E R T S                          */
/*==================================================================*/

function addAlertsToMap(array) {
    $.each(array, function (index, alert) {
        addAlertToMap(alert);
    });
}

function addAlertToMap(alert) {
    var pointer = "Blue.png";
    if (alert.Status == sosAlertStatus.InProgress) 
        pointer = 'Orange.png';
    else  if (alert.Status == sosAlertStatus.Finalized)
        pointer = 'Green.png';

    var icon = markerFolder + pointer;
        var position = convertToPosition(alert.Latitude, alert.Longitude);

        var marker = createMarker(position, icon);
        createInfoBubbleToAlert(marker, alert);
    }

    function createInfoBubbleToAlert(marker, alert) {
    var data = { IncidentId: alert.IncidentId, };

    var callback = function(response) {
        var managers = "";
        $.each(response.Records, function(index, manager) {
            managers += '<div style="font-size: 11px; text-align: center; color:gray;">' + manager.Name + " - "+ manager.Phone +  " - " + manager.Email+ " - "+ manager.Address +'</div><legend style="margin-bottom: 2px;"></legend>';
        });
        var content = '<strong><div style="font-size: 14px; text-align: center;">Responsables </div></strong>' + managers;

        var position = convertToPosition(alert.Latitude, alert.Longitude);
        var numberChars = alert.NumberChars;
        var rows = response.Count;

        var infoBubble = CreateInfobubble(marker, content, position, numberChars, rows);

        $("#alert_" + alert.Id).click(function() {
            google.maps.event.trigger(marker, 'click');
        });
        return infoBubble;
    };
    filterPerAjax(manager_Filter, data, callback);


}
