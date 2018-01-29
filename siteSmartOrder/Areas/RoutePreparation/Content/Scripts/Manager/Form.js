setAttributesToFile();
filterBranchToUserOnSession();
createAssignedIncidentsSelectize();
createAssignedBranchesSelectize();
eventToClickToFile();
eventToChangeFiles();
submitButtons();
disabledBranch();
maxLength();
validate();

function setAttributesToFile() {
    $("#File").attr("accept", imageValidTypes);
}

function eventToClickToFile() {
    $('#btn_uploadMultiemedia,#previewMultimedia').on('click', function () {
        $("#Files").trigger("click");
    });
}

function eventToChangeFiles() {
    $("#File").change(function() {
        var files = $(this)[0].files;
        if (files && files.length > 0) {
            var item = files[0];
            var reader = new FileReader();
            reader.onload = function(e) {
                $("#AvatarPreview").attr('src', e.target.result);
            };
            reader.readAsDataURL(item);

        } else {
            $("#AvatarPreview").attr('src', $("#OriginalImagePath").val());
            $("#FileAvatar").val("");
        }
    });
}

function validate() {
    $('#Form').validateForm({
        Name: true,
        Company: true,
        Email: true,
        Phone: true,
        AssignedIncidents: true,
        AssignedBranches: true,
        ApplyDialog: true
    });
}