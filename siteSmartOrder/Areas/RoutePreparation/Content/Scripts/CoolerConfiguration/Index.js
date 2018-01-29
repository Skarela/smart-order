filterUserPortal();
filterBranch();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "CustomerId=" + $("#CustomerId").val() + "&" + "SurveyId=" + $("#SurveyId").val() + "&" + "UserPortalId=" + $("#UserPortalId").val() + "&" + "BranchId=" + $("#BranchId").val();
        exportPerAjax(coolerConfig_Export + data);
    });
}

function createTable() {
    table.setUri(coolerConfig_Filter);
    table.setColumns(initColumns(), false, coolerConfig_Edit, coolerConfig_Delete);
    table.setFilters({ BranchId: "BranchId",  UserPortalId: "UserPortalId" });
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
                return "<div id='surveyNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando Encuesta... </div>";
            }
        }, {
            field: 'UserPortalId',
            title: 'Usuario',
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#userPortalNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(userPortal_Get, { id: row.UserPortalId }, callback);
                return "<div id='userPortalNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando Usuario... </div>";
            }
        }, {
            field: 'BranchId',
            title: 'Sucursal',
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#branchNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(branch_Get, { id: row.BranchId }, callback);
                return "<div id='branchNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando Sucursal... </div>";
            }
        }
    ];
}

function getData() {
    return {
        CustomerId: $("#CustomerId").val(),
        BranchId: $("#BranchId").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
