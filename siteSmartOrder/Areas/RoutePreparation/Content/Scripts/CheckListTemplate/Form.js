submitButtons();
maxLength();
validate();

function validate() {

    var actionUrl;
    if ($('#Id').val() > 0) {
        actionUrl = checkListTemplate_Update
    } else {
        actionUrl = checkListTemplate_Create;
    }
    $('#Form').validateForm({
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: actionUrl,
        New: checkListTemplate_New,
        Index: checkListTemplate_Index
    });
}