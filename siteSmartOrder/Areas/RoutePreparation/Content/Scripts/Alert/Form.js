filterBranchToUserOnSession();
createAlertTypeSelectize();
disabledBranch();
submitButtons();
maxLength();
validate();

function validate() {
    $('#Form').validateForm({
        Name: true,
        DescriptionRequired: true,
        Type: true,
        ApplyDialog: true
    });
}