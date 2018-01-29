filterBranchToUserOnSession();
createUserSelectize();
eventToChangeBranch();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "Code=" + $("#Code").val() + "&" + "UserId=" + $("#UserId").val();
        exportPerAjax(customer_Export + data);
    });
}

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#UserId");
        createUserSelectize();
    });
}

function createTable() {
    table.setUri(customer_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({ BranchId: "Branch", UserId: "UserId" });
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
            field: 'Code',
            title: 'C&oacute;digo',
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
            field: 'Tools',
            title: 'Enfriadores',
            align: 'center',
            width: "20",
            sortable: false,
            valign: 'middle',
            formatter: function () {
                return " <a class='btnViewCoolers iconPointer' style='color:black'><i class='fa fa-barcode'></i></a>";
            },
            events: window.operateEvents = {
                'click .btnViewCoolers': function (e, value, row) {
                    window.location.href = cooler_Index + "?CustomerId=" + row.Id;
                }
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
