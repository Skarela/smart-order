filterUserPortal();
filterBranch();
createTable();

function createTable() {
    table.setUri(checkList_Filter);
    table.setColumns(initColumns(), false, checkList_Edit, checkList_Delete);
    table.setFilters({ BranchId: "BranchId", UserPortalId: "UserPortalId"});
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
        },  {
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
        }, {
            field: 'BranchId',
            title: 'Sucursal',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#branchNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(branch_Get, { id: row.BranchId }, callback);
                return "<div id='branchNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando sucursal... </div>";
            }
        }
    ];
}

function getData() {
    return {
        Name: "",
        BranchId: $("#BranchId").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
