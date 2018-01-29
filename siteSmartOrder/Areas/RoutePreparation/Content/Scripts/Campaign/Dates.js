firstDateRanges();

function firstDateRanges() {
    $("#StartDate0").daterangepicker({
        singleDatePicker: true,
        showDropdowns: false,
        locale: {
            autoUpdateInput: false,
            format: "DD/MM/YYYY"
        },
        minDate:  moment()
    }).change('input', function () {
        validDates();
    });

    $("#EndDate0").daterangepicker({
        singleDatePicker: true,
        showDropdowns: false,
        locale: {
            autoUpdateInput: false,
            format: "DD/MM/YYYY"
        },
        minDate:  moment() 
    }).change('input', function () {
        validDates();
    });
}
function validDates() {
    var content = $('form');
    var startDate =$('#StartDate0').val();
    var endDate =  $('#EndDate0').val();
        if (moment(startDate, formatDate) > moment(endDate, formatDate)) {
            $(content.find(".dateSelector")[0]).css("border-color", "#a94442");
            $(content.find(".dateSelector")[1]).css("border-color", "#a94442");
            $(".error-dates").css("display", "inline-flex");
        } else {
            $(content.find(".dateSelector")[0]).css("border-color", "#3c763d");
            $(content.find(".dateSelector")[1]).css("border-color", "#3c763d");
            $(".error-dates").css("display", "none");
        }
}