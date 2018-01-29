filterUserPortal();
createStartDateRangePicker();
createEndDateRangePicker();
createTable();

function createStartDateRangePicker() {
    $("#StartDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": false,
        "autoUpdateInput": false,
        "linkedCalendars": false,
        "startDate": moment()
    }).on('apply.daterangepicker', function (ev, picker) {
        $('#StartDate').val(picker.startDate.format(formatDate)).trigger("change");
    });
}

function createEndDateRangePicker() {
    $("#EndDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": true,
        "autoUpdateInput": false,
        "linkedCalendars": false,
        "startDate": moment()
    }).on('apply.daterangepicker', function (ev, picker) {
        $('#EndDate').val(picker.startDate.format(formatDate)).trigger("change");
    });
}


function createTable() {
    table.setUri(campaign_Filter);
    table.setColumns(initColumns(), false, campaignTemplate_Edit, campaignTemplate_Delete);
    table.setFilters({ Name: "Campaign", UserPortalId: "UserPortalId", StartDate: "StartDate", EndDate: "EndDate" });
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
            field: 'Name',
            title: 'Encuesta',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Description',
            title: 'Descripci&oacute;n',
            align: 'left',
            sortable: false,
            valign: 'middle'
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
        Name: "",
        StartDate: $("#StartDate").val(),
        EndDate: $("#EndDate").val(),
        SortBy: "Id",
        Sort: "DESC",
        IsTemplate: true,
        PageSize: 10,
        PageNumber: 1
    };
}
