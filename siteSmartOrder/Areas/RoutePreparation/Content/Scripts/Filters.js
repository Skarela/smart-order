function filterUserPortal() {
    var data = {SortBy:"Name", Sort:"ASC"};

    var callback = function (response) {
        $('#UserPortalId').html(null);

        $('#UserPortalId').append($('<option>', {
            value: 0,
            text: "Todos"
        }));
        if (response.Records.length > 0) {
            $.each(response.Records, function (i, item) {
                $('#UserPortalId').append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
        }
    };

   getPerAjax(userPortal_Filter, data, callback);
}
function filterBranch() {
    var error = "Error al intentar recuperar las sucursales.";
    var data = {SortBy:"Name", Sort:"ASC"};
    var callback = function (response) {
        var branchId = 0;
        $('#BranchId').append($('<option>', {
            value: 0,
            text: "Todos"
        }));
        if (response.Record.Branches.length > 0) {
            $.each(response.Record.Branches, function (i, item) {
                $('#BranchId').append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
        }
    };

    otherActionPerAjax(branch_FilterByUserOnSession, data, error, callback, "GET", false);
}

function creationDateRangePicker() {
    $("#CreationDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": false,
        "autoUpdateInput": false,
        "linkedCalendars": false,
        "startDate": moment()
    }).on('apply.daterangepicker', function (ev, picker) {
        $('#CreationDate').val(picker.startDate.format(formatDate)).trigger("change");
    });
}

function FromCreationDateRangePicker() {
    $("#FromCreationDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": false,
        "autoUpdateInput": false,
        "linkedCalendars": false,
        "startDate": moment().startOf('month'),
    }).on('apply.daterangepicker', function (ev, picker) {
        $('#FromCreationDate').val(picker.startDate.format(formatDate)).trigger("change");
    });
    $('#FromCreationDate').val( moment().startOf('month').format(formatDate));
}

function ToCreationDateRangePicker() {
    $("#ToCreationDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": false,
        "autoUpdateInput": false,
        "linkedCalendars": false,
        "startDate": moment().endOf('month'),
    }).on('apply.daterangepicker', function (ev, picker) {
        $('#ToCreationDate').val(picker.startDate.format(formatDate)).trigger("change");
    });
     $('#ToCreationDate').val( moment().endOf('month').format(formatDate));
}