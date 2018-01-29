(function($) {
    $.fn.validateForm = function(options, callback) {
        var self = this;
        var validateFields = {};

        if (options.Name) addName();
        if (options.Description) addDescription();
        if (options.Company) addCompany();
        if (options.DescriptionRequired) addDescriptionRequired();
        if (options.StartDate) addStartDate();
        if (options.EndDate) addEndDate();
        if (options.Branch) addBranch();
        if (options.Route) addRoute();
        if (options.Alert) addAlert();
        if (options.Email) addEmail();
        if (options.Customer) addCustomer();
        if (options.AssignedIncidents) addAssignedIncidents();
        if (options.AssignedBranches) addAssignedBranches();
        if (options.Type) addType();
        if (options.Survey) addSurvey();

        $(self).bootstrapValidator({
                fields: validateFields
            }).on('success.field.bv', function(e, data) {
                $("#CreateAndNew").css("cursor", "pointer");
                $("#CreateAndNew").attr("disabled", false);
                $("#" + $(data.element).attr("id") + "Message").css("display", "none");
                if (callback) callback(e);
            }).on('error.field.bv', function(e, data) {
                $("#CreateAndNew").css("cursor", "not-allowed");
                $("#CreateAndNew").attr("disabled", true);
                $("#" + $(data.element).attr("id") + "Message").css("display", "inline-block");
            })
            .on('success.form.bv', function(e) {
                e.preventDefault();
                var isValidSurvey = true;
                var isValidDates = true;

                if (options.ValidSurvey)
                    isValidSurvey = eventValidSurvey();

                if (options.ValidDates)
                    isValidDates = eventValidDate();

                if (isValidSurvey && isValidDates && options.ApplyDialog)
                    eventApplyDialog(e);
            });

        function addName() {
            validateFields["Name"] = {
                excluded: false,
                selector: "#Name",
                container: "#NameMessage",
                validators: {
                    notEmpty: {
                        message: 'El Nombre es requerido.'
                    }
                }
            };
        }

        function addDescription() {
            validateFields["Description"] = {
                excluded: false,
                selector: "#Description",
                container: "#DescriptionMessage",
                validators: {

                }
            };
        }

        function addCompany() {
            validateFields["Company"] = {
                excluded: false,
                selector: "#Company",
                container: "#CompanyMessage",
                validators: {
                    notEmpty: {
                        message: 'La Compa&ntilde;&iacute;a es requerida.'
                    }
                }
            };
        }

        function addDescriptionRequired() {
            validateFields["Description"] = {
                excluded: false,
                selector: "#Description",
                container: "#DescriptionMessage",
                validators: {
                    notEmpty: {
                        message: 'La descripci&oacute;n es requerida.'
                    }
                }
            };
        }

        function addStartDate() {
            validateFields["StartDate"] = {
                excluded: false,
                selector: "#StartDate",
                container: "#StartDateMessage",
                validators: {
                    notEmpty: {
                        message: 'La Fecha de inicio es requerida.'
                    },
                    callback: {
                        message: 'La Fecha de inicio debe ser menor a la Fecha fin.',
                        callback: function(value, validator, $field) {
                            return moment($("#StartDate").val(), formatDate) <= moment($("#EndDate").val(), formatDate);
                        }
                    }
                }
            };
        }

        function addEndDate() {
            validateFields["EndDate"] = {
                excluded: false,
                selector: "#EndDate",
                container: "#EndDateMessage",
                validators: {
                    notEmpty: {
                        message: 'La Fecha de finalizaci&oacute;n es requerida.'
                    },
                    callback: {
                        message: 'La Fecha de fin debe ser menor a la Fecha inicio.',
                        callback: function(value, validator, $field) {
                            return moment($("#StartDate").val(), formatDate) <= moment($("#EndDate").val(), formatDate);
                        }
                    }
                }
            };
        }

        function addBranch() {
            validateFields["BranchName"] = {
                excluded: false,
                selector: "#BranchName",
                container: "#BranchNameMessage",
                validators: {
                    notEmpty: {
                        message: 'La Sucursal es requerida.'
                    }
                }
            };
        }

        function addRoute() {
            validateFields["Route"] = {
                excluded: false,
                selector: "#Route",
                container: "#RouteMessage",
                validators: {
                    notEmpty: {
                        message: 'La Ruta es requerida.'
                    },
                }
            };
        }

        function addAlert() {
            validateFields["AlertIds"] = {
                excluded: false,
                selector: "#AlertIds",
                container: "#AlertMessage",
                validators: {
                    notEmpty: {
                        message: 'Agregar al menos una Alerta.'
                    },
                }
            };
        }

        function addEmail() {
            validateFields["Email"] = {
                excluded: false,
                selector: "#Email",
                container: "#EmailMessage",
                validators: {
                    notEmpty: {
                        message: 'El Correo es requerido.'
                    },
                    emailAddress: {
                        message: "Se requiere un Email v&aacute;lido."
                    },
                }
            };
        }

        function addSurvey() {
            validateFields["Survey"] = {
                excluded: false,
                selector: "#StartDate",
                container: "#StartDateMessage",
                validators: {
                    notEmpty: {
                        message: 'La Fecha de inicio es requerida.'
                    },
                    callback: {
                        message: 'La Fecha de inicio debe ser menor a la Fecha fin.',
                        callback: function(value, validator, $field) {
                            return moment($("#StartDate").val(), formatDate) <= moment($("#EndDate").val(), formatDate);
                        }
                    }
                }
            };
        }

        function addCustomer() {
            validateFields["CustomerId"] = {
                excluded: false,
                selector: "#Customer",
                container: "#CustomerMessage",
                validators: {
                    notEmpty: {
                        message: 'El Cliente es requerido.'
                    },
                }
            };
        }

        function addAssignedIncidents() {
            validateFields["AssignedIncidents"] = {
                excluded: false,
                selector: "#AssignedIncidents",
                container: "#AssignedIncidentsMessage",
                validators: {
                    notEmpty: {
                        message: 'Agregar al menos un Tipo de Incidencia.'
                    },
                }
            };
        }

        function addAssignedBranches() {
            validateFields["AssignedBranches"] = {
                excluded: false,
                selector: "#AssignedBranches",
                container: "#AssignedBranchesMessage",
                validators: {
                    notEmpty: {
                        message: 'Agregar al menos una Sucursal.'
                    },
                }
            };
        }

        function addType() {
            validateFields["Type"] = {
                excluded: false,
                selector: "#Type",
                container: "#TypeMessage",
                validators: {
                    notEmpty: {
                        message: 'El tipo es requerido.'
                    },
                }
            };
        }

        function eventValidSurvey() {
            var isValid = true;
            var inputName = $("#SurveyForm").find("#SurveyName");
            var inputDescription = $("#SurveyForm").find("#SurveyDescription");

            if (inputName.val() != "") {
                inputName.closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
                $("#SurveyNameMessage").hide();
            } else {
                inputName.closest(".control-group.form-group").removeClass("has-success").addClass("has-error");
                $("#SurveyNameMessage").show();
                isValid = false;
            }

            inputDescription.closest(".control-group.form-group").removeClass("has-error").addClass("has-success");

            if ($(".question").length > 0) {
                $("#QuestionMessage").closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
                $("#QuestionMessage").hide();
            } else {
                $("#QuestionMessage").closest(".control-group.form-group").removeClass("has-success").addClass("has-error");
                $("#QuestionMessage").show();
                isValid = false;
            }

            $.each($(".inputName"), function() {
                if ($(this).val() != "") {
                    $(this).closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
                    $(this).parent().find(".inputNameMessage").hide();
                } else {
                    $(this).closest(".control-group.form-group").removeClass("has-success").addClass("has-error");
                    $(this).parent().find(".inputNameMessage").show();
                    isValid = false;
                }
            });

            $.each($(".question"), function() {
                if ($(this).find(".options-container").length > 0) {
                    if ($(this).find(".answer").length > 0) {
                        $(this).find(".AnswerMessage").closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
                        $(this).find(".AnswerMessage").hide();
                    } else {
                        $(this).find(".AnswerMessage").closest(".control-group.form-group").removeClass("has-success").addClass("has-error");
                        $(this).find(".AnswerMessage").show();
                        isValid = false;
                    }
                }
            });

            $.each($(".inputAnswerText"), function() {
                if ($(this).val() != "") {
                    $(this).closest(".control-group.form-group").removeClass("has-error").addClass("has-success");
                    $(this).parent().find(".inputAnswerTextMessage").hide();
                } else {
                    $(this).closest(".control-group.form-group").removeClass("has-success").addClass("has-error");
                    $(this).parent().find(".inputAnswerTextMessage").show();
                    isValid = false;
                }
            });

            if (!isValid) {
                $("#Create,#CreateAndNew").css("cursor", "not-allowed");
                $("#Create,#CreateAndNew").attr("disabled", true);
            } else {
                $("#Create,#CreateAndNew").css("cursor", "pointer");
                $("#Create,#CreateAndNew").attr("disabled", false);
            }

            return isValid;
        }

        function eventValidDate() {
            var isValid = true;
            var count = 0;

            $.each($(".selectBranchRow"), function () {
                if (moment($("#StartDate" + count).val(), formatDate) > moment($("#EndDate" + count).val(), formatDate)) {
                    $("#StartDate" + count).css("border-color", "#a94442");
                    $("#EndDate" + count).css("border-color", "#a94442");
                    isValid = false;
                } else {
                    $("#StartDate" + count).css("border-color", "#3c763d");
                    $("#EndDate" + count).css("border-color", "#3c763d");
                }
                count ++;
            });

            if (isValid) {
                $(".error-dates").css("display", "none");
            } else {
                $(".error-dates").css("display", "inline-flex");
            }

            return isValid;
        }

        function eventApplyDialog(e) {
            $("#Create").removeAttr("disabled");
            $("#CreateAndNew").removeAttr("disabled");
            BootstrapDialog.show({
                title: "Confirmar",
                message: "&iquest;Desea guardar el registro?",
                type: BootstrapDialog.TYPE_SUCCESS,
                buttons: [
                    {
                        label: 'Guardar',
                        cssClass: 'btn-success',
                        action: function(dialog) {
                            dialog.enableButtons(false);
                            dialog.setClosable(false);
                            this.spin();
                            if(options.AjaxSubmit){
                                asyncSubmit(options.Action, options.New, options.Index);
                                dialog.close();
                            }
                            else{
                                $(e.target).data('bootstrapValidator').defaultSubmit();
                            }

                        }
                    }, {
                        label: 'Cancelar',
                        action: function(dialog) {
                            dialog.close();
                        }
                    }
                ]
            });
        }

    };
})(jQuery);