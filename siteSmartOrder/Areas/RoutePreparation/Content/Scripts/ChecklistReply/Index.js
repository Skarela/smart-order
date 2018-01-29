filterBranchToUserOnSession();
createListRouteSelectize();
createListUserSelectize();
FromCreationDateRangePicker();
ToCreationDateRangePicker();
eventToChangeBranch();
clickToExport();
clickToExportReport();
createTable();

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#RouteIds");
        createListRouteSelectize();
        destroySelectize("#UserIds");
        createListUserSelectize();
    });
}

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "RouteIds=" + $("#RouteIds").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(checkListReply_Export + data);
    });
}

function clickToExportReport() {
    $("#btnExportReport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "RouteIds=" + $("#RouteIds").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(checkListReply_ExportReport + data);
    });
}

function createTable() {
    table.setUri(checkListReply_Filter);
    table.setColumns(initColumns(), false, false, false, false);
    table.setFilters({ BranchId: "Branch", RouteIds: "RouteIds", UserIds: "UserIds" });
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
            field: 'UnitId',
            title: 'Unidad',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#unitCodeColumn" + row.Id).html(response.Record.Code);
                };
                getPerAjax(unit_Get, { id: row.UnitId }, callback);
                return "<div id='unitCodeColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando unidad... </div>";
            }
        }, {
            field: 'RouteId',
            title: 'Ruta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#RouteNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(route_Get, { id: row.RouteId }, callback);
                return "<div id='RouteNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando ruta... </div>";
            }
        }, {
            field: 'ApplyAssignedSurveyId',
            title: 'Encuesta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#surveyNameColumn" + row.Id).html(response.Record.AssignedSurvey.Survey.Name);
                };
                getPerAjax(applyAssignedSurvey_Get, { id: row.ApplyAssignedSurveyId }, callback);
                return "<div id='surveyNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando encuesta... </div>";
            }
        },  {
            field: 'CreationDate',
            title: 'Fecha',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'GoodCondition',
            title: 'Condici&oacute;n',
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var style = 'padding-left: 9px; padding-top:1px; padding-bottom:1px;font-size: 9px;';
                return value ? '<span class="badge badge-success" style="' + style + '"><i  class="fa fa-check" ></i></span>' : '<span class="badge badge-important" style="' + style + '"><i  class="fa fa-times"></i></span>';
            }
        }, {
            field: 'ViewDetail',
            title: '',
            align: 'left',
            width: "50",
            sortable: false,
            valign: 'middle',
            formatter: function () {
                return " <a class='btnViewDetail iconPointer' style='color:black;'><i class='fa fa-eye'></i></a>";
            },
            events: window.operateEvents = {
                'click .btnViewDetail': function (e, value, row) {
                    window.location.href = checkListReply_Detail + "?id=" + row.ApplyAssignedSurveyId;
                }
            }
        }
    ];
}

function getData() {
    return {
        BranchId: $("#Branch").val(),
        FromCreationDate: $("#FromCreationDate").val(),
        ToCreationDate: $("#ToCreationDate").val(),
        SortBy: "Id",
        Sort: "Asc",
        PageSize: 10,
        PageNumber: 1
    };
}
