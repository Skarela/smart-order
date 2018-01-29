changeBranchToUserOnSession();
filterBranchToUserOnSession();
submitButtons();
maxLength();
validate();

function changeBranchToUserOnSession() {
    $("#Branch").change('input', function () {
        $("#BranchId").val($("#Branch").val());
        $("#BranchName").val($("#Branch option[value='" + $("#Branch").val() + "']").html());
    });
}

function validate() {
    $('#Form').validateForm({
        Branch: true,
        ValidSurvey: true,
        ApplyDialog:true
    });
}
