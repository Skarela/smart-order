changeNameAndDescription();
createStartDateRangePicker();
createEndDateRangePicker();
toggleDates();
submitButtons();
maxLength();
validate();



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

function changeNameAndDescription() {
    $("#SurveyName").attr("disabled", "disabled");
    $("#SurveyDescription").attr("disabled", "disabled");

    $("#Name").on('input', function () {
        $("#SurveyName").val($(this).val());
    });

    $("#Description").on('input', function () {
        $("#SurveyDescription").val($(this).val());
    });
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

function validate() {
    var actionUrl;
    if ($('#Id').val() > 0) {
        actionUrl = campaignTemplate_Update
    } else {
        actionUrl = campaignTemplate_Create;
    }

    $('#Form').validateForm({
        Name: true,
        Description: true,
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: actionUrl,
        New: campaignTemplate_New,
        Index: campaignTemplate_Index
    });
}