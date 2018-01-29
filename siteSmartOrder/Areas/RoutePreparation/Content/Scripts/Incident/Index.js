filterBranchToUserOnSession();
clickToExport();
disabledBranch();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        exportPerAjax(incident_Export);
    });
}

function createTable() {
    table.setUri(incident_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({});
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
            title: 'Nombre',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }
    ];
}

function getData() {
    return {
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
