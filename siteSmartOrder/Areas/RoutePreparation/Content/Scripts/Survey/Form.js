loadSurvey();
function loadSurvey() {
    applyValidSurveyid();
    togglePoints();
    hidePoints();
    collapseAllPanels();
    createTouchSpin();
    maskSurveyForm();
    requiredMultipleOptions();
    eventToChangehSurveyName();
    eventToChangeHasPoint();
    refreshQuestionText();
    createTouchSpin();
    ShowModalGallery();
    showBtnAlert();
};


function applyValidSurveyid() {
    $("#SurveyId").val($("#ValidSurveyId").val());
}

function togglePoints() {
    if ($("#Weighted").is(":checked")) {
        if ($("#CampaignContent").length == 0)
            $(".points").show();
        $(".points input").attr("disabled", false);
        $("#ShowPoints").attr("disabled", false);
    } else {
        $(".points").hide();
        $(".points input").attr("disabled", true);
        $("#ShowPoints").attr("disabled", true);
    }
}

function createTouchSpin() {
    $("input.pointspin").TouchSpin({ max: 0 , min:1});
    $(".question").each(function () {
        var count = $(this).find(".answer").length;
        var max = count > 0 ? count :  1;
        $(this).find("input.touchspin").TouchSpin({ max: max, min: 1 });
    });
}

function maskSurveyForm() {
    if ($("#CampaignContent").length > 0) {
        $("#AditionalOptions").hide();
        $(".add-question[data-type='" + questionType.Numeric + "']").remove();
        $(".add-question[data-type='" + questionType.Photo + "']").remove();
        $(".add-question[data-type='" + questionType.Text + "']").remove();
        if (!$("#Weighted").is(":checked"))
            $("#Weighted").trigger("click");
        if (!$("#ShowPoints").is(":checked"))
            $("#ShowPoints").trigger("click");
    }
}

function hidePoints() {
    if ($("#CampaignContent").length > 0) {
        $(".points").hide();
        $(".btn-hasPoint").show();
    } 
}

function eventToChangehAnswerText() {
    $(".inputAnswerText").on('input', function () {
        $(this).closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
        $(this).parent().find(".inputAnswerTextMessage").hide();
        enabledButtons();
    });
}

function eventToChangehSurveyName() {
    $("#SurveyName").on('input', function () {
        enabledButtons();
    });
}

function eventToChangeHasPoint() {
    $(".btn-hasPoint.notEvent").on('click', function() {
        var total = 0;
        if ($(this).children().hasClass("fa-toggle-on")) {
            $(this).parent().find(".answerpoint").val(0);
            $(this).children().removeClass("fa-toggle-on").addClass("fa-toggle-off");
        } else {
            $(this).parent().find(".answerpoint").val(1);
            $(this).children().removeClass("fa-toggle-off").addClass("fa-toggle-on");
        }
        $.each($(this).closest(".question").find(".answerpoint"), function(index, answerPoint) {
            total = total + parseFloat($(answerPoint).val());
        });
        $(this).closest(".question").find(".questionpoint").val(total);
//        $(this).removeClass("notEvent");
    });
    $(".answer").find(".btn-hasPoint").removeClass("notEvent");
}

function eventToChangeAlert() {
    $("#AlertId").on('change', function () {
        var alertConfiguration = $(this).data("alertconfiguration");
        if ($(this).val() == "") {
            $("#" + alertConfiguration).val(0);
            $("#" + alertConfiguration).closest(".answer").find(".btn-alert").children().removeClass("fa-bell").addClass("fa-bell-o");
        } else {
            $("#" + alertConfiguration).val($(this).val());
            $("#" + alertConfiguration).closest(".answer").find(".btn-alert").children().removeClass("fa-bell-o").addClass("fa-bell");
        }
    });
}

function refreshQuestionText() {
    $(".inputName").on('input', function () {
        var text = $(this).val();
        $(this).closest(".question").find(".displayText").html(text);
        $(this).closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
        $(this).parent().find(".inputNameMessage").hide();
        enabledButtons();
    });

    $(".questionpoint").on('input', function () {
        $(this).closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
        $(this).parent().find(".inputNameMessage").hide();
        enabledButtons();
    });
}

function enabledButtons() {
    $("#Create, #CreateAndNew").css("cursor", "pointer");
    $("#Create,#CreateAndNew").attr("disabled", false);
}

function collapseAllPanels() {
    $(".panel-heading.collapsible").each(function () {
        var panel = $(this).closest('.panel');
        panel.find(".panel-body").hide();
        $('.collapse-button').addClass('panel-collapsed').find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    });
}

function requiredMultipleOptions() {
    var panel = $(".form-horizontal");
    $(panel).each(function () {
        if (!$(this).find(".option-required").is(":checked")) {
            $(this).find(".touchspin").val("0");
            $(this).find(".numberRequired").hide();
        }
    });
}

function reindexQuestions() {
    $.each($(".question"), function(questionIndex, content) {
        var fieldName = "Questions[" + questionIndex + "].";
        var fieldId = "Questions_" + questionIndex + "__";
        var questionNumber = questionIndex + 1;

        $(content).attr("id", "question_" + questionIndex);
        $(content).attr("data-index", questionIndex);

        $(content).find("[id*='Id']").attr("id", fieldId + "Id");
        $(content).find("[id*='Id']").attr("name", fieldName + "Id");

        $(content).find("[id*='QuestionType']").attr("id", fieldId + "QuestionType");
        $(content).find("[id*='QuestionType']").attr("name", fieldName + "QuestionType");

        $(content).find("[id*='QuestionNumber']").attr("id", fieldId + "QuestionNumber");
        $(content).find("[id*='QuestionNumber']").attr("name", fieldName + "QuestionNumber");
        $(content).find("[id*='QuestionNumber']").val(questionNumber);

        $(content).find(".collapse-button").attr("data-id", questionIndex);
        $(content).find(".question-number").html(questionNumber);

        $(content).find("[id*='Text']").attr("id", fieldId + "Text");
        $(content).find("[id*='Text']").attr("name", fieldName + "Text");

        $(content).find("[id*='QuestionValue']").attr("id", fieldId + "QuestionValue");
        $(content).find("[id*='QuestionValue']").attr("name", fieldName + "QuestionValue");

        $(content).find("[id*='Required']").attr("id", fieldId + "Required");
        $(content).find("[id*='Required']").attr("name", fieldName + "Required");
        $(content).find("[name*='Required']").attr("name", fieldName + "Required");

        $(content).find("[id*='FileImages']").attr("name", fieldName + "FileImages");
        $(content).find("[id*='FileImages']").attr("id", fieldName + "FileImages");

        $(content).find("[id*='QuestionImages']").attr("id", fieldId + "QuestionImages");
        $(content).find("[id*='QuestionImages']").attr("name", fieldName + "QuestionImages");

        $(content).find("[id*='AnswerRequiredNumber']").attr("id", fieldId + "AnswerRequiredNumber");
        $(content).find("[id*='AnswerRequiredNumber']").attr("name", fieldName + "AnswerRequiredNumber");

        reindexOptions(questionIndex);
    });
}

function reindexOptions(questionIndex) {
    $.each($("#question_" + questionIndex).find(".answer"), function (answerIndex, content) {
        var fieldName = "Questions[" + questionIndex + "].Answers[" + answerIndex + "].";
        var fieldId = "Questions_" + questionIndex + "__Answers_" + answerIndex + "__";
        var optionNumber = answerIndex + 1;

        $(content).find("[id*='_Id']").attr("id", fieldId + "Id");
        $(content).find("[id*='_Id']").attr("name", fieldName + "Id");

        $(content).find("label").html("Opci&oacute;n " + optionNumber);

        $(content).find("[id*='Text']").attr("id", fieldId + "Text");
        $(content).find("[id*='Text']").attr("name", fieldName + "Text");

        $(content).find("[id*='AnswerPoint']").attr("id", fieldId + "AnswerPoint");
        $(content).find("[id*='AnswerPoint']").attr("name", fieldName + "AnswerPoint");

        $(content).find("[id*='AlertId']").attr("id", fieldId + "AlertId");
        $(content).find("[id*='AlertId']").attr("name", fieldName + "AlertId");
    });
}

function questionIsValid() {
    $("#QuestionMessage").closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
    $("#QuestionMessage").hide();
    enabledButtons();
}

function showBtnAlert() {
    if ($("#CategoryType").val() == categoryType.CoolerConfiguration) {
        $(".btn-alert").show();
    }
}

$(window).on('shown.bs.modal', function () {
    if ($("#AlertId").length > 0) {
        createAlertSelectize(alertType.Configuration);
        eventToChangeAlert();
    }
});

$(document).on('click', '.btn-alert', function () {
    var alertConfiguration = $(this).closest(".answer").find('.alertConfiguration').attr("id");
    var alertId = $(this).closest(".answer").find('.alertConfiguration').val();
    var message = '<div id="AlertContainer" class="control-group form-group">' +
        '<div class="controls">' +
        '<select id="AlertId" placeholder="Alerta" data-alertid="' + alertId + '" data-alertconfiguration="' + alertConfiguration + '" style="width: 100%;"></select>' +
        '</div>' +
        '</div>';

    BootstrapDialog.show({
        title: "Asignar alerta",
        message: message,
        onshown: function () {
        },
        type: BootstrapDialog.TYPE_PRIMARY,
        buttons: [{
            label: 'Cerrar',
            action: function (dialog) {
                dialog.close();
            }
        }
        ]
    });
});

$('#Weighted').click(function () {
    togglePoints();
});

$(document).on('change', 'input[type="file"]', function () {
    var questionIndex = $(this).closest(".question").attr("data-index");
    var content = "#question_" + questionIndex;
    var files = $(this)[0].files;
    $(content).find(".btn_uploadFiles").hide();
    $(content).find(".showFiles").html("");
    $(content).find(".showFiles").show();
    if (files && files.length > 0) {
        $.each(files, function (i, item) {
            var reader = new FileReader();
            reader.onload = function () {
                var name = item.name.length > 35 ? item.name.substring(0, 35) + "... " + item.type.split('/')[1] : item.name;
                var label = "<span class='label label-default' style='margin:5px;'>" + name + "</span>";
                $(content).find(".showFiles").append(label);
            };
            reader.readAsDataURL(item);
        });
    } else {
        $(content).find(".btn_uploadFiles").show();
        $(content).find(".showFiles").hide();
    }
});

$(document).on('click', '.btn_uploadFiles,.showFiles', function () {
    var questionIndex = $(this).closest(".question").attr("data-index");
    var content = "#question_" + questionIndex;
    $(content).find("input[type='file']").trigger("click");
});

$(document).on('click', '.option-required', function () {
    var panel = $(this).closest(".form-horizontal");
    if (panel.length > 0) {
        if (!$(this).is(":checked")) {
            $(panel).find(".touchspin").val("0");
            $(panel).find(".numberRequired").hide();
        } else {
            $(panel).find(".numberRequired").show();
        }
    }
});

$(document).on('click', '.collapsible .panel-title', function () {
    var $this = $(this).closest('.panel').find('.collapse-button');
    if (!$this.hasClass('panel-collapsed')) {
        $this.closest('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    } else {
        $this.closest('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    }
});

$(document).on('click', '.add-question', function () {
    var questionIndex = $(".question").length;
    var typeToAdd = $(this).data("type");
    var data = {
        QuestionType: typeToAdd,
        index: questionIndex
    };
    var callback = function () {
        createTouchSpin();
        refreshQuestionText();
        togglePoints();
        requiredMultipleOptions();
        questionIsValid();
        eventToChangehAnswerText();
        eventToChangeHasPoint();
        hidePoints();
        showBtnAlert();
    };
    appendPartialView("questions-container", survey_Question, data, callback);
});


$(document).on('click', '.add-option', function () {
    var questionIndex = $(this).data("question");
    var answerIndex = $("#question_" + questionIndex).find(".answer").length;
    var container = "question_" + questionIndex + " .options-container";
    var typeToAdd = $(this).data("type");
    var data = {
        questionType: typeToAdd,
        questionIndex: questionIndex,
        answerIndex: answerIndex
    };
    var callback = function () {
        togglePoints();
        eventToChangehAnswerText();
        eventToChangeHasPoint();
        hidePoints();
        enabledButtons();
        showBtnAlert();
        $("#question_" + questionIndex).find(".AnswerMessage").hide();
        $("#question_" + questionIndex).find("input.touchspin").trigger("touchspin.updatesettings", { max: answerIndex + 1 });
    };
    appendPartialView(container, survey_answer, data, callback);
});


$(document).on('click', '.remove-option', function () {
    var closestQuestion = $(this).closest('.question');
    $(this).parent().find(".answerpoint").val(0);

    var questionIndex = $(this).closest('.question').attr("data-index");
    var container = $(this).closest('.question');
    var element = $(this).closest('.answer');
    $(element).slideUp(function () {
        $(this).remove();
        reindexOptions(questionIndex);
        $("#Create,#CreateAndNew").css("cursor", "pointer");
        $("#Create,#CreateAndNew").attr("disabled", false);
        var count = $(container).find(".answer").length;
        $(container).find("input.touchspin").trigger("touchspin.updatesettings", { max: count });
    });
    var total = 0;
    $.each(closestQuestion.find(".answerpoint"), function (index, answerPoint) {
        total = total + parseFloat($(answerPoint).val());
    });
    closestQuestion.find(".questionpoint").val(total);
});

$(document).on('click', '.remove-question', function () {
    var container = $(this).closest('.question');
    var message = '&iquest;Estas seguro de remover la pregunta?';
    BootstrapDialog.confirm(message, function (result) {
        if (result) {
            $(container).slideUp(function () {
                $(this).remove();
                reindexQuestions();
                $("#Create,#CreateAndNew").css("cursor", "pointer");
                $("#Create,#CreateAndNew").attr("disabled", false);
            });
        }
    });
});

$(document).on('keyup', '.questionpoint', function () {
    var value = $(this).val();
    $(this).parents(".Dichotomy").find(".dicho-value").val(value);
});

$(document).on('click', '.remove-file', function () {
    var element = $(this).closest('li');
    $(element).slideUp(function () {
        $(this).remove();
    });
});