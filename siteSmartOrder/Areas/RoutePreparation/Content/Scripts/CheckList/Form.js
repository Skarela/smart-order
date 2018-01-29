filterBranchToUserOnSession();
setBranchName();
submitButtons();
maxLength();
validate();

function changeBranchToUserOnSession() {
    $("#BranchId").change('input', function () {
        window.location.href = checkList_RedirectToView;
    });
}

function setBranchName() {
    $("#BranchName").val($("#Branch option[value='" + $("#BranchId").val() + "']").html());
}

function validate() {

    var actionUrl;
    if ($('#Id').val() > 0) {
        actionUrl = checkList_Update;
    } else {
        actionUrl = checkList_Create;
    }
    $('#Form').validateForm({
        Branch: true,
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: actionUrl,
        New: checkList_New,
        Index: checkList_Index
    });
}