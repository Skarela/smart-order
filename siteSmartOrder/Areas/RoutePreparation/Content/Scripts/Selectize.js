function filterBranchToUserOnSession(remoteCallback) {
    var data = { };
    var error = "Error al intentar recuperar las sucursales.";

    var callback = function (response) {
        var branchId = 0;
        if (response.Record.Branches.length > 0) {
            $.each(response.Record.Branches, function (i, item) {
                $('#Branch').append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
            branchId = response.Record.SelectedBranch == 0 ? $("#Branch option:first").val() : response.Record.SelectedBranch;
            $("#Branch").val(branchId).trigger("change");
        }

        //if (remoteCallback) remoteCallback(branchId);
    };

    actionToChangeBranch(remoteCallback);
    otherActionPerAjax(branch_FilterByUserOnSession, data, error, callback, "GET", false);
}

function actionToChangeBranch(remoteCallback) {
    var callback = function (response) {
        if (remoteCallback) remoteCallback();
    };

    $("#Branch").change("input", function () {
        var data = {id:$(this).val()};
        var error = "Error al intentar seleccionar una sucursal.";
       
        otherActionPerAjax(branch_SelectedBranchSession, data, error, callback, "GET", false);
    });
}

function createListUserSelectize() {
    
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        $('#UserIds').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de usuarios",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#UserIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(user_Filter, callbackLoadFilter);
            }
        });
        clearOpacity();
    };

    filterPerAjax(user_Filter, data, callback);
}

function createListCustomerSelectize() {
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        $('#CustomerIds').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de clientes",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#CustomerIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(customer_Filter, data, callbackLoadFilter);
            }
        });
        clearOpacity();
    };

    filterPerAjax(customer_Filter, data, callback);
}

function createListRouteSelectize() {
   
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        $('#RouteIds').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de rutas",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#RouteIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(route_Filter, data, callbackLoadFilter);
            }
        });
        clearOpacity();
    };

    filterPerAjax(route_Filter, data, callback);
}

function createListMechanicSelectize() {
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        $('#MechanicIds').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de mecanicos",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#MechanicIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(mechanic_Filter, data, callbackLoadFilter);
            }
        });
        clearOpacity();
    };

    filterPerAjax(mechanic_Filter, data, callback);
}

function createListUnitSelectize() {
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        $('#UnitIds').selectize({
            valueField: 'Id',
            labelField: 'Code',
            searchField: 'Code',
            sortField: 'Code',
            sortDirection: 'asc',
            placeholder: "Lista de unidades",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#UnitIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Code"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(unit_Filter, data, callbackLoadFilter);
            }
        });
        clearOpacity();
    };

    filterPerAjax(unit_Filter, data, callback);
}

function createListAlertSelectize() {
    var data = {
        Name: "",
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function(response) {
        var select = $('#AlertIds').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de alertas",
            options: response.Records,
            create: false,
            onItemAdd: function() {
                $("#AlertIdsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function(keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function(responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(alert_Filter, data, callbackLoadFilter);
            }
        });
        appendAlertsToSelectize(select);
        clearOpacity();
    };

    filterPerAjax(alert_Filter, data, callback);
}

function appendAlertsToSelectize(select) {
    var selectize = select[0].selectize;
    var callback = function(response) {
        $.each(response.Records, function(index, alert) {
            var sltOption = { id: parseInt(alert.Id), title: alert.Name };
            selectize.addOption(sltOption);
            selectize.addItem(parseInt(sltOption.id));
        });
    };
    filterPerAjax(alert_Filter, { ContactId: $("#Id").val() }, callback);
}

function createUserSelectize() {
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#UserId').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de usuarios",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#UserContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(user_Filter, data, callbackLoadFilter);
            }
        });
        addOptionSelectedToSelectize("User", select);
        clearOpacity();
    };

    filterPerAjax(user_Filter, data, callback);
}

function createRouteSelectize() {
    var data = {
        Name: "",
        BranchId: $("#Branch").val(),
        StartPage: 1,
        EndPage: 100,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#RouteId').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de rutas",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#RouteContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(route_Filter, data, callbackLoadFilter);
            }
        });
        addOptionSelectedToSelectize("Route", select);
        clearOpacity();
    };

    filterPerAjax(route_Filter, data, callback);
}

function createAlertSelectize(type) {
    var data = {
        Name: "",
        Type: !type ? "0" : type,
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#AlertId').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de alertas",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#AlertContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(alert_Filter, data, callbackLoadFilter);
            }
        });
        appendValueToSelectize(select);
        clearOpacity();
    };

    filterPerAjax(alert_Filter, data, callback);
}

function appendValueToSelectize(select) {
    var alertId = $("#AlertId").data("alertid");
    var selectize = select[0].selectize;

    if (alertId != "" && alertId > 0) {
        var callback = function(response) {
            var sltOption = { id: parseInt(response.Record.Id), title: response.Record.Name };
            selectize.addOption(sltOption);
            selectize.addItem(parseInt(sltOption.id));
        };

        getPerAjax(alert_Get, { id: alertId }, callback);
    }
}

function createSosAlertStautsSelectize() {
    var status = [{ Id: sosAlertStatus.NotStarted, Name: "NO iniciado" }, { Id: sosAlertStatus.InProgress, Name: "En progreso" }, { Id: sosAlertStatus.Finalized, Name: "Finalizado"}];

    $('#AlertsStatus').selectize({
        valueField: 'Id',
        labelField: 'Name',
        searchField: 'Name',
        sortField: 'Name',
        sortDirection: 'asc',
        placeholder: "Lista de estados",
        options: status,
        create: false,
        onItemAdd: function() {
            $("#AlertsStatusContainer").find("input[tabindex]").css("width", "1px");
        }
    });
    clearOpacity();
}

function createIncidentSelectize() {
    var data = {
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#IncidentId').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de incidencias",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#IncidentContainer").find("input[tabindex]").css("width", "1px");
            },
            
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(incident_Filter, data, callbackLoadFilter);
            }
        });
        addOptionSelectedToSelectize("Incidents", select);
        clearOpacity();
    };

    filterPerAjax(incident_Filter, data, callback);
}

function createAssignedIncidentsSelectize() {
    var data = {
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#AssignedIncidents').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de tipos de inicidencias",
            options: response.Records,
            create: false,
            onItemAdd: function () {
                $("#AssignedIncidentsContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                otherActionPerAjax(incident_Filter, data, false, callbackLoadFilter, "GET", true);
            }
        });

        var jsonIncidents = $("#AssignedIncidentsContainer").attr("data-incidents");
        var incidents = jQuery.parseJSON(jsonIncidents);
         var selectize = select[0].selectize;
         $.each(incidents, function (index, incident) {
             var sltOption = { id: parseInt(incident.Id), title: incident.Name };
                selectize.addOption(sltOption);
                selectize.addItem(parseInt(sltOption.id));
        });

        clearOpacity();
    };

    otherActionPerAjax(incident_Filter, data, false, callback, "GET", true);
}

function createAssignedBranchesSelectize() {
    var data = {
        StartPage: 1,
        EndPage: 10,
        Sort: "ASC",
        SortBy: "Name"
    };

    var callback = function (response) {
        var select = $('#AssignedBranches').selectize({
            valueField: 'Id',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Id',
            sortDirection: 'asc',
            placeholder: "Lista de sucursales",
            options: response.Record.Branches,
            create: false,
            onItemAdd: function () {
                $("#AssignedBranchesContainer").find("input[tabindex]").css("width", "1px");
            },
            load: function (keyWord, callbackLoad) {
                if (!keyWord.length)
                    return callbackLoad();

                data["Name"] = keyWord;
                var callbackLoadFilter = function (responseLoadFilter) {
                    callbackLoad(responseLoadFilter.Records);
                };

                filterPerAjax(branch_FilterByUserOnSession, data, callbackLoadFilter);
            }
        });
        
        var jsonBranches = $("#AssignedBranchesContainer").attr("data-branches");
        var branches = jQuery.parseJSON(jsonBranches);
        var selectize = select[0].selectize;
        $.each(branches, function (index, branch) {
            var sltOption = { id: parseInt(branch.Id), title: branch.Name };
            selectize.addOption(sltOption);
            selectize.addItem(parseInt(sltOption.id));
        });

        clearOpacity();
    };

    filterPerAjax(branch_FilterByUserOnSession, data, callback);
}

function createAlertTypeSelectize() {
    var data = {};

    var callback = function(response) {
        var select = $('#Type').selectize({
            valueField: 'Value',
            labelField: 'Name',
            searchField: 'Name',
            sortField: 'Name',
            sortDirection: 'asc',
            placeholder: "Lista de tipos",
            options: response.Records,
            create: false,
            onItemAdd: function() {
                $("#TypeContainer").find("input[tabindex]").css("width", "1px");
            },
        });

        var selectize = select[0].selectize;
        $.each($("#Type").children(), function(index, option) {
            selectize.setValue(parseInt(option.value));
        });

        clearOpacity();
    };

    otherActionPerAjax(alert_GetTypes, data, false, callback, "GET", true);
}
