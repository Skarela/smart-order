filterBranchToUserOnSession();
disabledBranch();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" +"Name=" + $("#Name").val();
        exportPerAjax(alert_Export + data);
    });
}

function createTable() {
    table.setUri(alert_Filter);
    table.setColumns(initColumns(), false, alert_Edit, alert_Delete);
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
        }, {
            field: 'Description',
            title: 'Descripci&oacute;n',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'DisplayType',
            title: 'Tipo',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }
    ];
}

function getData() {
    return {
        Name: "",
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
