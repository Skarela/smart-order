filterUserPortal();
filterBranch();
createStartDateRangePicker();
createEndDateRangePicker();
clickToExport();
createTable();
$(document).ready(function () {
    $("#selectAll").click(switchBulkEdit);
    switchBulkEdit()
});



function switchBulkEdit() {
    if ($("input.rowSelector:checked").is(':empty')) {
        $("#editAll-btn").prop('disabled', false)
        $("#deleteAll-btn").prop('disabled', false)
    } else {
        $("#editAll-btn").prop('disabled', true)
        $("#deleteAll-btn").prop('disabled', true)
    }
}
$(document).ready(
function () {
    $("#NewStartDate").daterangepicker({
        singleDatePicker: true,
        showDropdowns: false,
        locale: {
            autoUpdateInput: false,
            format: "DD/MM/YYYY"
        },
        minDate: moment()
    }).change('input', function () {
        validDates();
    });

    $("#NewEndDate").daterangepicker({
        singleDatePicker: true,
        showDropdowns: false,
        locale: {
            autoUpdateInput: false,
            format: "DD/MM/YYYY"
        },
        minDate: moment()
    }).change('input', function () {
        validDates();
    });
}
)
function validDates() {
    var content = $('#myModal');
    var startDate = $('#NewStartDate').val();
    var endDate = $('#NewEndDate').val();
    if (moment(startDate, formatDate) > moment(endDate, formatDate)) {
        $("#NewStartDate").css("border-color", "#a94442");
        $("#NewEndDate").css("border-color", "#a94442");
        $(".error-dates").css("display", "inline-flex");
        $("#AssingNewDates-btn").prop("disabled", true);
    } else {
        $("#NewStartDate").css("border-color", "#3c763d");
        $("#NewEndDate").css("border-color", "#3c763d");
        $(".error-dates").css("display", "none");
        $("#AssingNewDates-btn").prop("disabled", false);
    }
}


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
        var data = "?" + "BranchId=" + $("#BranchId").val() + "&" + "Name=" + $("#Campaign").val() + "&" + "UserPortalId=" + $("#UserPortalId").val() + "&" + "StartDate=" + $("#StartDate").val() + "&" + "EndDate=" + $("#EndDate").val();
        exportPerAjax(campaign_Export + data);
    });
}

function createTable() {
    table.setUri(campaign_Filter);
    table.setColumns(initColumns(), false, campaign_Edit, campaign_Delete);
    table.setFilters({ BranchId: "BranchId", Name: "Campaign", UserPortalId: "UserPortalId", StartDate: "StartDate", EndDate: "EndDate" });
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
            field: 'StartDate',
            title: 'Fecha Inicio',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'EndDate',
            title: 'Fecha Fin',
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
                return "<div id='branchNameColumn" + row.Id + "' class='branchNameColumn'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando sucursal... </div>";
            }
        },
        {
            field: 'selector',
            title: "<input type='checkbox' id='selectAll'>",
            align: 'center',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var checkbox = "<input type='checkbox' id='rowSelector" + row.Id + "' class='rowSelector' value='" + row.Id + "' onclick='switchBulkEdit()'>";
                return checkbox;

            }
        }

    ];
}

function getData() {
    return {
        Name: "",
        BranchId: $("#BranchId").val(),
        StartDate: $("#StartDate").val(),
        EndDate: $("#EndDate").val(),
        SortBy: "Id",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}




function getCheckedCampaigns() {
    var checkedCampaigns = $("input.rowSelector:checked");
    var campaignIdList = [];
    for (var i = 0; i < checkedCampaigns.length; i++) {
        var campaign =
        {
            id: parseInt(checkedCampaigns[i].value),
            branchName: $(checkedCampaigns[i]).closest('tr').find('.branchNameColumn').text(),
            surveyName: $(checkedCampaigns[i]).closest('tr').children().first().text()
        };
        campaignIdList.push(campaign);
    }
    return campaignIdList;
}


function isChecked(checkbox) {
    var checked = $(checkbox).prop('checked');
    if (checked == true) {
        return true;
    }
    return false;
}

$("#selectAll").click(function () {

    if (isChecked(this)) {
        $('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('input[type=checkbox]').prop('checked', false);
    }
});

$("#deleteAll-btn").click(function () {
    var campaignsToDelete = getCheckedCampaigns();
    var campaignsIdsToDelete = campaignsToDelete.map(function (elem) { return elem.id })
    if (campaignsIdsToDelete != null && campaignsIdsToDelete != "undefined" && campaignsIdsToDelete.length > 0) {
        var message = "&iquest;Est&aacute; de eliminar todos los registros seleccionados?";
        confirmDeleteDialog(message, campaignsIdsToDelete);
    }
});

function confirmDeleteDialog(message, campaignsToDelete) {
    bootbox.confirm(message, "Cancelar", "Si",
                function (result) {
                    if (result) {
                        deleteAllCampaigns(campaignsToDelete);
                    }

                    else {
                        bootbox.hideAll();
                    }
                });
}

function deleteAllCampaigns(campaignsToDelete) {
    var campaignsTotal = campaignsToDelete.length;
    var campaignsDeleted = 0;
    function tryReload() {
        campaignsDeleted++;
        if (campaignsDeleted = campaignsTotal) {
            location.reload()
        }
    }
    for (var i = 0; i < campaignsTotal; i++) {
        var campaignId = campaignsToDelete[i];
        $.ajax({
            url: campaign_Delete,
            type: 'DELETE',
            async: true,
            data: { id: campaignId },
            success: tryReload,
            error: function () {
                alertError("No se ha podido establecer conexi&oacute;n con el servicio.");
            }
        });
    }
}



$("#AssingNewDates-btn").click(function () {

    var campaignsToEdit = getCheckedCampaigns();
    if (campaignsToEdit != null && campaignsToEdit != "undefined" && campaignsToEdit.length > 0) {
        var message = "&iquest;Est&aacute; seguro de editar todos los registros seleccionados?";
        confirmEditDialog(message, campaignsToEdit);
    }
});

function confirmEditDialog(message, campaignsToEdit, callback) {
    bootbox.confirm(message, "Cancelar", "Si",
                function (result) {
                    if (result) {
                        datesBulkEdit(campaignsToEdit);
                    }

                    else {
                        bootbox.hideAll();
                    }
                });
}

function datesBulkEdit(campaignsToEdit) {

    function reloadView() {
        location.reload()

    }
    var newStartDate = $("#NewStartDate").val();
    var newEndDate = $("#NewEndDate").val();

    var dataObj = JSON.stringify({ "CampaignList": campaignsToEdit,
        "StartDate": newStartDate,
        "EndDate": newEndDate
    })

    $.ajax({
        url: campaign_DatesBulkUpdate,
        type: 'PUT',
        async: true,
        traditional: true,
        data: dataObj,
        contentType: 'application/json; charset=utf-8',
        success: reloadView,
        error: function () {
            alertError("No se ha podido establecer conexi&oacute;n con el servicio.");
        }
    });

}