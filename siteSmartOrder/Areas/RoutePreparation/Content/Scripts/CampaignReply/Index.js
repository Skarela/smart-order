filterBranchToUserOnSession();
createListUserSelectize();
createListRouteSelectize();
FromCreationDateRangePicker();
ToCreationDateRangePicker();
eventToChangeBranch();
clickToExport();
clickToExportReport();
createTable();

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#UserIds");
        createListUserSelectize();
        destroySelectize("#RouteIds");
        createListRouteSelectize();
    });
}

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "RouteIds=" + $("#RouteIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(campaignReply_Export + data);
    });
}

function clickToExportReport() {
    $("#btnExportReport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "RouteIds=" + $("#RouteIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(campaignReply_ExportReport + data);
    });
}

function createTable() {
    table.setUri(campaignReply_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({ BranchId: "Branch", UserIds: "UserIds", RouteIds: "RouteIds" });
    table.create(getData(), false);
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
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#userNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(user_Get, { id: row.UserId }, callback);
                return "<div id='userNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando usuario... </div>";
            }
        }, {
            field: 'RouteId',
            title: 'Ruta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#routeNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(route_Get, { id: row.RouteId }, callback);
                return "<div id='routeNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando ruta... </div>";
            }
        }, {
            field: 'ApplyAssignedSurveyId',
            title: 'Encuesta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                var callback = function(response) {
                    $("#surveyNameColumn" + row.Id).html(response.Record.AssignedSurvey.Survey.Name);
                };
                getPerAjax(applyAssignedSurvey_Get, { id: row.ApplyAssignedSurveyId }, callback);
                return "<div id='surveyNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando encuesta... </div>";
            }
        }, {
            field: 'CreationDate',
            title: 'Fecha',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'ViewDetail',
            title: '',
            align: 'left',
            width: "50",
            sortable: false,
            valign: 'middle',
            formatter: function() {
                return " <a class='btnViewDetail iconPointer' style='color:black;'><i class='fa fa-eye'></i></a>";
            },
            events: window.operateEvents = {
                'click .btnViewDetail': function (e, value, row) {
                    window.location.href = campaignReply_Detail + "?id=" + row.ApplyAssignedSurveyId;
                }
            }
        }
    ];
}

function getData() {
    return {
        Name: "",
        BranchId: $("#Branch").val(),
        FromCreationDate: $("#FromCreationDate").val(),
        ToCreationDate: $("#ToCreationDate").val(),
        SortBy: "Id",
        Sort: "Asc",
        PageSize: 10,
        PageNumber: 1
    };
}
