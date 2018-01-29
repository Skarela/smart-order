createMap();
filterBranchToUserOnSession();
createRouteSelectize();
createUserSelectize();
createIncidentSelectize();
createSosAlertStautsSelectize();
eventToChangeBranch();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "RouteId=" + $("#RouteId").val() + "&" + "UserId=" + $("#UserId").val() + "&" + "Status=" + $("#AlertsStatus").val()+ "&"+ "BranchId=" + $("#Branch").val();
        exportPerAjax(sosAlert_Export + data);
    });
}

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#RouteId");
        createRouteSelectize();
        destroySelectize("#UserId");
        createUserSelectize();
    });
}

function createTable() {
    table.setUri(sosAlert_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({BranchId: "Branch", RouteId: "RouteId", IncidentId: "IncidentId",UserId: "UserId", Status: "AlertsStatus"});
    table.create(getData() );
}

function initColumns() {
    return [
        {
            field: 'Id',
            align: 'center',
            visible: false,
            valign: 'middle'
        }, {
            field: 'UserId',
            title: 'Usuario',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                var callback = function(response) {
                    $("#userNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(user_Get, { id: row.UserId }, callback);
                return "<div id='userNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando usuario... </div>";
            }
        }, {
            field: 'IncidentId',
            title: 'Incidencia',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#incidentsNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(incident_Get, { id: row.IncidentId }, callback);
                return "<div id='incidentsNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando incidencia... </div>";
            }
        }, {
            field: 'RouteId',
            title: 'Ruta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                var callback = function(response) {
                    $("#routeNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(route_Get, { id: row.RouteId }, callback);
                return "<div id='routeNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando ruta... </div>";
            }
        }, {
            field: 'PhoneNumber',
            title: 'Tel&eacute;fono',
            align: 'left',
            sortable: false,
            valign: 'left',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#routePhoneNumberColumn" + row.Id).html(response.Record.PhoneNumber);
                };
                getPerAjax(route_Get, { id: row.RouteId }, callback);
                return "<div id='routePhoneNumberColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando tel&eacute;fono... </div>";
            }
        },

        {
            field: 'Comment',
            title: 'Comentario',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'UpdatedAt',
            title: 'Actualizado',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Status',
            title: 'Estado',
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                var style = 'padding-left: 9px; padding-top:1px; padding-bottom:1px;font-size: 10px;';
                switch (value) {
                case sosAlertStatus.InProgress:
                    return'<span class="badge badge-warning" style="' + style + '">En progreso</span>';
                case sosAlertStatus.Finalized:
                    return'<span class="badge badge-success" style="' + style + '">Finalizado</span>';
                default:
                    return'<span class="badge badge-info" style="' + style + '">NO iniciado</span>';
                }
            }
        }, {
            field: 'OtherButtons',
            title: '',
            align: 'left',
            width: "100",
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                addAlertToMap(row);
                var btnMultimediaImage = " <a id='multimediaImage_" + row.Id + "' class='btnMultimediaImage iconPointer' style='color:black; margin-right:10px'><i class='fa fa-picture-o'></i></a>";
                var btnMultimediaAudio = " <a id='multimediaAudio_" + row.Id + "' class='btnMultimediaAudio iconPointer' style='color:black; margin-right:10px'><i class='fa fa-file-audio-o'></i></a>";
                var btnAlert = "<a id='alert_" + row.Id + "' class='btnLocation iconPointer' style='color:black; margin-right:10px'><i class='fa fa-location-arrow'></i></a>";
                var btnComment = row.Status == sosAlertStatus.Finalized ? " <a style='color:grey;'><i class='fa fa-bell'></i></a>" : " <a class='btnComment iconPointer' style='color:black;'><i class='fa fa-bell'></i></a>";
                return btnMultimediaImage + btnMultimediaAudio + btnAlert + btnComment;
            },
            events: window.operateEvents = {
                'click .btnMultimediaImage': function(e, value, row) {
                    var callback = function(response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin contenido multimedia.");
                            $("#multimediaImage_" + row.Id).removeClass("btnMultimediaImage").removeClass("iconPointer").css("color", "grey").unbind();
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    otherActionPerAjax(sosAlert_GetMultimedias, { alertId: row.Id, multimediaType: SosAlertMultimediaType.Image }, false, callback, "GET", true);
                },
                'click .btnMultimediaAudio': function (e, value, row) {
                    var callback = function (response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin contenido de audio.");
                            $("#multimediaAudio_" + row.Id).removeClass("btnMultimediaAudio").removeClass("iconPointer").css("color", "grey").unbind();
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    otherActionPerAjax(sosAlert_GetMultimedias, { alertId: row.Id, multimediaType: SosAlertMultimediaType.Audio }, false, callback, "GET", true);
                },
                'click .btnLocation': function(e, value, row) {
                    var position = convertToPosition(row.Latitude, row.Longitude);
                    map.setCenter(position);
                },
                'click .btnComment': function(e, value, row) {
                    var title = "";
                    var action = "";
                    if (row.Status == sosAlertStatus.NotStarted) {
                        title = "Procesar alerta";
                        action = "Starting";
                    } else if (row.Status == sosAlertStatus.InProgress) {
                        title = "Finalizar alerta";
                        action = "Finalizing";
                    }
                    createdBootstrapDialogToComment(title, action,row);
                }
            }
        }
    ];
}

function createdBootstrapDialogToComment(title, action, alert) {
    var data = {userId: alert.UserId, incidentId: alert.IncidentId, routeId: alert.RouteId};
    var callback = function (responseView) {

        BootstrapDialog.show({
            title: title,
            message: responseView.trim(),
            type: BootstrapDialog.TYPE_PRIMARY,
            nl2br:false,
            buttons: [
                {
                    label: 'Guardar',
                    cssClass: 'btn-success',
                    action: function(dialog) {
                        dialog.enableButtons(false);
                        dialog.setClosable(false);
                        this.spin();
                        
                        $.ajax({
                            url: 'SosAlert/' + action,
                            type: "POST",
                            async: true,
                            data: { Id: alert.Id, Comment: $("#Comment").val() },
                            success: function(response) {
                                checkResponseToAjax(response);
                                $("#Table").bootstrapTable('refresh');

                                if (response.Result == resultOk)
                                    alertSuccess(response.Message);
                                dialog.close();
                            },
                            error: function() {
                                alertError("No se ha podido establecer conexión con el servicio.");
                                dialog.close();
                            }
                        });
                    }
                }, {
                    label: 'Cancelar',
                    action: function(dialog) {
                        dialog.close();
                    }
                }
            ]
        });
        clearOpacity();
    };
    addOpacity();
    getPartialView("SosAlert", "_NewComment", data, callback);
}

function getData() {
    return {
        Name: "",
        BranchId: $("#Branch").val(),   
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 100,
        PageNumber: 1
    };
}