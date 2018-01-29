filterBranchToUserOnSession();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "Name=" + $("#Name").val();
        exportPerAjax(contact_Export + data);
    });
}

function createTable() {
    table.setUri(contact_Filter);
    table.setColumns(initColumns(), false, contact_Edit, contact_Delete);
    table.setFilters({ BranchId: "Branch"});
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
            field: 'Email',
            title: 'Email',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Phone',
            title: 'Tel&eacute;fono',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Alerts',
            title: 'Alertas',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    var style = 'padding-left: 9px; padding-top:1px; padding-bottom:1px;font-size: 10px;';
                    var alerts = "";
                    $.each(response.Records, function (index, alert) {
                        alerts += '<span class="badge badge-info " style="' + style + '">' + alert.Name + '</span> ';
                    });
                    $("#alertsColumn" + row.Id).html(alerts);
                };
                filterPerAjax(alert_Filter, { ContactId: row.Id }, callback);
                return "<div id='alertsColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando alerta... </div>";
            }
        }, {
            field: 'BranchId',
            title: 'Nivel',
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var style = 'padding-top:1px; padding-bottom:1px;font-size: 11px;';
                return value  > 0 ? '<span class="badge badge-warning" style="' + style + '">Sucursal</span>' : '<span class="badge badge-success" style="' + style + '">Global</span>';
            }
        }
    ];
}

function getData() {
    return {
        Name: "",
        BranchId: $("#Branch").val(),  
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
