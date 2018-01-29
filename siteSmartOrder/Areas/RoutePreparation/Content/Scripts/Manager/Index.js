filterBranchToUserOnSession();
createIncidentSelectize();
clickToExport();
createTable();

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "Name=" + $("#Name").val() + "&" + "Company=" + $("#Company").val() + "&" + "IncidentId=" + $("#IncidentId").val() + "&" + "BranchId=" + $("#Branch").val();
        exportPerAjax(manager_Export + data);
    });
}

function createTable() {
    table.setUri(manager_Filter);
    table.setColumns(initColumns(), false, manager_Edit, manager_Delete);
    table.setFilters({ BranchId: "Branch", IncidentId: "IncidentId" });
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
            field: 'ImagePath',
            title: 'Avatar',
            align: 'center',
            sortable: false,
            valign: 'middle',
            width: '60',
            formatter: function (value) {
                return '<img class="AvatarToTable" src="' + value + '" alt="" style="width: 30px; height: 30px; border-radius: 10%; border:1px solid #FFFFFF">';
            }
        }, {
            field: 'Name',
            title: 'Nombre',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Company',
            title: 'Compa&ntilde;&iacute;a',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Address',
            title: 'Direcci&oacute;n',
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
            field: 'Incidents',
            title: 'Tipos de siniestros',
            width: "140",
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                var style = 'padding-left: 9px; padding-top:1px; padding-bottom:1px;font-size: 10px;';
                var incidents = "";
                $.each(value, function(index, incident) {
                    incidents += '<span class="badge badge-info " style="' + style + '">' + incident.Name + '</span> ';
                });
                return incidents;
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
        PageSize: 100,
        PageNumber: 1
    };
}
