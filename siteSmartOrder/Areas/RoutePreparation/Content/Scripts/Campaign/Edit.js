setBranchName();
createStartDateRangePicker();
createEndDateRangePicker();
toggleDates();
validate();

function setBranchName() {
    $("#BranchName").val($("#Branch option[value='" + $("#BranchId").val() + "']").html());
}

function createStartDateRangePicker() {
    if ($("#StartDate").val() == "") {
        $("#StartDate").attr("disabled", "disabled");
    }

    $("#StartDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": true,
        "linkedCalendars": false,
        "minDate": $("#StartDate").val() == "" ? moment() : $("#StartDate").val(),
        "startDate": $("#StartDate").val() == "" ? moment() : $("#StartDate").val()
    }).change('input', function () {
        $('#Form').bootstrapValidator('revalidateField', 'StartDate');
        $('#Form').bootstrapValidator('revalidateField', 'EndDate');
    });
}

function createEndDateRangePicker() {

    if ($("#EndDate").val() == "") {
        $("#EndDate").attr("disabled", "disabled");
    }

    $("#EndDate").daterangepicker({
        "singleDatePicker": true,
        "locale": {
            format: formatDate
        },
        "autoApply": true,
        "linkedCalendars": false,
        "minDate": $("#StartDate").val() == "" ? moment() : $("#StartDate").val(),
        "startDate": $("#EndDate").val() == "" ? moment() : $("#EndDate").val()
    }).change('input', function () {
        $('#Form').bootstrapValidator('revalidateField', 'StartDate');
        $('#Form').bootstrapValidator('revalidateField', 'EndDate');
    });
}

function toggleDates() {
    $(".btn-hasDates").on('click', function () {
        if ($(this).children().hasClass("fa-toggle-on")) {
            $(this).children().removeClass("fa-toggle-on").addClass("fa-toggle-off");
            $("#StartDate").attr("disabled", true);
            $("#EndDate").attr("disabled", true);
            $("#StartDate").val(dateNow());
            $("#EndDate").val(dateNow());
            $('#Form').bootstrapValidator('revalidateField', 'StartDate');
            $('#Form').bootstrapValidator('revalidateField', 'EndDate');
        } else {
            $(this).children().removeClass("fa-toggle-off").addClass("fa-toggle-on");
            $("#StartDate").attr("disabled", false);
            $("#EndDate").attr("disabled", false);
        }

    });

}

function validate() {
    $('#Form').validateForm({
        Name: true,
        Description: true,
        StartDate: true,
        EndDate: true,
        Branch: true,
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: campaign_Update,
        New: campaign_New,
        Index: campaign_Index
    });
}

$('#AsyncUpdate').click(function () {
    asyncSubmit(campaign_Update,campaign_New,campaign_Index,false);
});

$('#AsyncUpdateAndNew').click(function () {
    asyncSubmit(campaign_Update,campaign_New,campaign_Index,true);
});