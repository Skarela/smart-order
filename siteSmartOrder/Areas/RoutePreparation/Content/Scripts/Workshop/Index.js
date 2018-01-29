filterBranchToUserOnSession();
filterUserPortal();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UserPortalId=" + $("#UserPortalId").val();
        exportPerAjax(workshop_Export + data);
    });
}

function createTable() {
    table.setUri(workshop_Filter);
    table.setColumns(initColumns(), false, workshop_Edit, workshop_Delete);
    table.setFilters({ BranchId: "Branch", UserPortalId: "UserPortalId" });
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
            field: 'SurveyId',
            title: 'Encuesta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#surveyNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(survey_Get, { id: row.SurveyId }, callback);
                return "<div id='surveyNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando encuesta... </div>";
            }
        }, {
            field: 'UserPortalId',
            title: 'Autor',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#userPortalNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(userPortal_Get, { id: row.UserPortalId }, callback);
                return "<div id='userPortalNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando usuario... </div>";
            }
        }
    ];
}

function getData() {
    return {
        BranchId: $("#Branch").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
