clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "CustomerId=" + $("#CustomerId").val() + "&" + "Serie=" + $("#Serie").val();
        exportPerAjax(cooler_Export + data);
    });
}

function createTable() {
    table.setUri(cooler_Filter);
    table.setColumns(initColumns(), false, false, false);
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
            field: 'Serie',
            title: 'Serie',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Name',
            title: 'Nombre',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'DoorsNumber',
            title: 'Puertas',
            align: 'center',
            sortable: false,
            valign: 'middle'
        }
    ];
}

function getData() {
    return {
        CustomerId: $("#CustomerId").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
