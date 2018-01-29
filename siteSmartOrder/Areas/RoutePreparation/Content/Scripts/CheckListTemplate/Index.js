filterUserPortal();
filterBranch();
createTable();

function createTable() {
    table.setUri(checkList_Filter);
    table.setColumns(initColumns(), false, checkListTemplate_Edit, checkListTemplate_Delete);
    table.setFilters({ UserPortalId: "UserPortalId" });
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
        }
    ];
}

function getData() {
    return {
        Name: "",
        IsTemplate: true,
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
