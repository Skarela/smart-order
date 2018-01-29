createListAlertSelectize();
showBranchContainer();
changeBranchToUserOnSession();
filterBranchToUserOnSession();
eventToChangeHasBranch();
submitButtons();
maxLength();
validate();

function changeBranchToUserOnSession() {
    $("#Branch").change('input', function () {
        $("#BranchId").val($("#Branch").val());
        $("#BranchName").val($("#Branch option[value='" + $("#Branch").val() + "']").html());
    });
}

function showBranchContainer() {
    if ($("#BranchId").val() > 0) {
        $("#BranchContainer").show();
    } else {
        $("#BranchContainer").hide();
        disabledBranch();
        $("#BranchId").attr("disabled", "disabled");
    }
}

function eventToChangeHasBranch() {
    $(".btn-hasBranch").on('click', function () {
        if ($(this).children().hasClass("fa-toggle-on")) {
            $("#BranchId").attr("disabled", "disabled");
            $("#BranchContainer").hide();
            $("#Branch").attr("disabled", "disabled");
            $(this).children().removeClass("fa-toggle-on").addClass("fa-toggle-off");
        } else {
            $("#BranchId").removeAttr("disabled");
            $("#BranchContainer").show();
            $("#Branch").removeAttr("disabled");
            $(this).children().removeClass("fa-toggle-off").addClass("fa-toggle-on");
        }
    });
}

function validate() {
    $('#Form').validateForm({
        Name: true,
        Email: true,
        Phone: true,
        Alert: true,
        Branch: true,
        ApplyDialog: true
    });
}