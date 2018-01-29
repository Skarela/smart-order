submitButtons();
maxLength();
validate();

function validate() {

    var actionUrl;
    if ($('#Id').val() > 0) {
        actionUrl = coolerConfigTemplate_Update
    } else {
        actionUrl = coolerConfigTemplate_Create;
    }
    $('#Form').validateForm({
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: actionUrl,
        New: coolerConfigTemplate_New,
        Index: coolerConfigTemplate_Index
    });
}
