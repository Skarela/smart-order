filterBranchToUserOnSession();
setBranchName();
submitButtons();
maxLength();
validate();

function changeBranchToUserOnSession() {
    $("#BranchId").change('input', function () {
        window.location.href = coolerConfiguratio_RedirectToView;
    });
}

function setBranchName() {
    $("#BranchName").val($("#Branch option[value='" + $("#BranchId").val() + "']").html());
}

function validate() {

    var actionUrl;
    if ($('#Id').val() > 0) {
        actionUrl =  coolerConfig_Update
    } else {
        actionUrl =  coolerConfig_Create;
    }
    $('#Form').validateForm({
        Branch: true,
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: actionUrl,
        New:  coolerConfig_New,
        Index:  coolerConfig_Index
    });
}
