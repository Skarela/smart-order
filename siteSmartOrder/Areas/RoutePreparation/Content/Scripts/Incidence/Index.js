filterBranchToUserOnSession();
createStartDateRangePicker();
createEndDateRangePicker();
clickToExport();
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

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "TypeId=" + $("#TypeId").val() + "&ActionId=" + $("#ActionId").val() + "&UnitCode=" + $("#UnitCode").val() + "&StartDate=" + $("#StartDate").val() + "&" + "EndDate=" + $("#EndDate").val();
        exportPerAjax(incidence_Export + data);
    });
}

function createTable() {
    table.setUri(incidence_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({ BranchId: "Branch", TypeId: "TypeId", ActionId: "ActionId", StartDate: "StartDate", EndDate: "EndDate" });
    table.create(getData(), false);
}

function initColumns() {
    return [
        {
            field: 'User',
            title: 'Usuario',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Route',
            title: 'Ruta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#routeNameColumn" + row.Id).html(response.Record.Name);
                };
                if (row.RouteId >  0) {
                    getPerAjax(route_Get, { id: row.RouteId }, callback);
                    return "<div id='routeNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando ruta... </div>";
                }
                return "<div id='routeNameColumn" + row.Id + "'>-</div>";
            }
        }, {
            field: 'UnitCode',
            title: 'C&oacute;digo de unidad',
            align: 'left',
            sortable: false,
            valign: 'middle'
        },
        {
            field: 'Type',
            title: 'Tipo',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Action',
            title: 'Acci&oacute;n',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'CreatedOn',
            title: 'Fecha de captura',
            align: 'center',
            sortable: false,
            valign: 'middle'
        }
    ];
}

function getData() {
    return { 
        StartDate: $("#StartDate").val(),
        EndDate: $("#EndDate").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
