filterBranchToUserOnSession();
changeNameAndDescription();
submitButtons();
maxLength();


function changeBranchToUserOnSession() {
    $("#Branch").change('input', function () {
        $("#BranchId").val($("#Branch").val());
        $("#BranchName").val($("#Branch option[value='" + $("#Branch").val() + "']").html());
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