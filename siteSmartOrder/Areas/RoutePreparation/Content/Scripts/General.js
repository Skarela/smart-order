//VARIABLES ==================================================================
var markers = [];
var formatDate = 'DD/MM/YYYY';
var resultOk = "Success";
var resultFailure = "Failure";
var resultPrecondition = "Precondition";
var campaignMultimediaType = { Imagen: 1, Video: 2 };
var questionType = { None: 0, MultipleChoice: 1, Numeric: 2, Text: 3, Dichotomy: 4, Photo: 5 };
var categoryType = { None: 0, Campaign: 1, Checklist: 2, Workshop: 3, CoolerConfiguration: 4 };
var alertType = { None: 0, Configuration: 1, NotExist: 2, NewCooler: 3};
var SosAlertMultimediaType = { Image: 1, Audio: 2 };
var sosAlertStatus = { NotStarted: "NotStarted", InProgress: "InProgress", Finalized: "Finalized" };
var imageValidTypes = "image/jpg, image/jpeg, image/gif, image/bmp, image/png";
var videoValidTypes = "video/mp4";
var loading = "</div>" +
               "<div class='col-lg-12' style='text-align: center; font-size: 25px; padding: 50px; opacity: .5;'>" +
               "<i class='fa fa-cog fa-spin'></i><span> Cargando contenido... </span>" +
               "</div>";


var dateNow = function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10)
        dd = '0' + dd;

    if (mm < 10)
        mm = '0' + mm;

    today = dd + '/' + mm + '/' + yyyy;
    return today;
};

//GENERAL ==================================================================

function submitButtons() {
    $('#CreateAndNew').each(function () {
        $(this).click(function () {
            $("input[name=View]").val("New");
            $("#Create").click();
        });
    });
}

function asyncSubmit(uriToAction, uriToNew, uriToIndex) {
    var createNew = $('#View').val() == "New";
    $('form').ajaxSubmit({
        success: function (response, textStatus, jqXHR) {
            if (response.Record.Success == true) {
                if (createNew == true) {
                    return window.location.href = uriToNew;
                }
                else {

                    return window.location.href = uriToIndex;
                }
            }
            else {
                alertError(response.Record.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });   
}


function clearOpacity() {
    $('body').removeClass('loading');
}

function addOpacity() {
    $('body').addClass('loading');
}

function isNumberKeyUp(evt) {
    var charCode = (evt.keyCode.which) ? evt.keyCode.which : event.keyCode;
    if (charCode == 45 || (charCode != 46 && (charCode > 31))
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function isNumberKeyDow(evt) {
    if (evt.keyCode != 86 || !evt.ctrlKey)
        return false;

    return true;
}

function elementExistInArray(element, array) {
    return $.inArray(element, array) > -1;
}

function formatNumber() {
    $('input.number').number(true, 0);
}

function disabledBranch() {
    $("#Branch").attr("disabled", "disabled");
}

//GODAL GALLERY ==================================================================
function ShowModalGallery() {
    $(".seeAllImages").click(function () {
        var jsonFiles = this.getAttribute("data-allimages");
        var files = jQuery.parseJSON(jsonFiles);
        setImagesToModal(files);

    });
}

function setImagesToModal(files) {
    var images = [];
    var videos = [];
    var audios = [];
    var carouselIndicatorsTemplate = '';
    var carouselImagesTemplate = '';
    var carouselVideosTemplate = '';
    var carouselAudiosTemplate = '';
    $.each(files, function (index, file) {
        if (file.IsImage)
            images.push(file);
        else if (file.IsVideo)
            videos.push(file);
        else if(file.IsAudio)
            audios.push(file);
    });

    $.each(images, function (index, image) {
        var classToFirst = (index == 0 ? "active" : "");
        carouselImagesTemplate += '<div class="' + classToFirst + ' item" style="text-align: center;"> <img src="' + image.Source + '" style="max-height: 400px; margin: 0 auto;" /> </div>';
    });

    $.each(videos, function (index, video) {
        var classToFirst = (index == 0 && images.length == 0 ? "active" : "");
        carouselVideosTemplate += '<div class="' + classToFirst + ' item" style="text-align: center;"><video width="520" height="340"  src="' + video.Source + '" controls>Your browser does not support the <code>video</code> element.</video></div>';
                 
    });
    if (audios.length > 0) {
        var classToFirst = (videos.length == 0 && images.length == 0 ? "active" : "");
        carouselAudiosTemplate += '<div class="' + classToFirst + ' item form-horizontal"   >';

        $.each(audios, function (index, audio) {
            carouselAudiosTemplate += '<div class="form-group col-xs-12 col-sm-6 col-lg-3"><label class="control-label text-right col-md-4" style = "font-weight: bold">Audio ' + (index + 1) + ': </label> <div class="input-group col-md-8"><audio  src="' + audio.Source + '" class="form-control" controls>Your browser does not support the <code>audio</code> element.</audio></div></div>';
             
        });
        carouselAudiosTemplate += '</div>';

    }
     
    var modalTemplate = '' +
            '<div id="ModalCarousel" class="carousel slide">' +
            '<ol class="carousel-indicators">' +
            carouselIndicatorsTemplate +
            '</ol>' +
            '<div class="carousel-inner">' +
            carouselImagesTemplate +
            carouselVideosTemplate +
            carouselAudiosTemplate +
            ' </div>' +
            '<a class="carousel-control left" href="#ModalCarousel" data-slide="prev">&lsaquo;</a>' +
            '<a class="carousel-control right" href="#ModalCarousel" data-slide="next">&rsaquo;</a>' +
            ' </div>';


    //                        {{each(index, pdf) pdfs}}
    //                        {{if (index == 0 && images.length == 0)}}
    //                        <div class="active item" style="text-align: center;">
    //                            <iframe src="${pdf}" style="width:90%; height:500px" ></iframe>
    //                        </div>
    //                        {{else}}
    //                        <div class="item" style="text-align: center;">
    //                            <iframe src="${pdf}" style="width:90%; height: 500px" ></iframe>
    //                        </div>
    //                        {{/if}}
    //                        {{/each}}



    clearOpacity();
    BootstrapDialog.show({
        title: "Multimedia",
        message: modalTemplate,
        type: BootstrapDialog.TYPE_PRIMARY
    });

    $("#ModalCarousel").carousel({
        interval: false,
        wrap: false
    });

}

//DATE & TIME ==================================================================
function clock() {
    // Create two variable with the names of the months and days in an array
    var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    var dayNames = ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado"];

    // Create a newDate() object
    var newDate = new Date();
    // Extract the current date from Date object
    newDate.setDate(newDate.getDate());
    // Output the day, date, month and year   
    $('#Date').html(dayNames[newDate.getDay()] + ", " + newDate.getDate() + ' ' + monthNames[newDate.getMonth()] + ' ' + newDate.getFullYear());

    $("#sec").html((new Date().getSeconds() < 10 ? "0" : "") + new Date().getSeconds());
    $("#min").html((new Date().getMinutes() < 10 ? "0" : "") + new Date().getMinutes());
    $("#hours").html((new Date().getHours() < 10 ? "0" : "") + new Date().getHours());

    setInterval(function() {
        // Create a newDate() object and extract the seconds of the current time on the visitor's
        var seconds = new Date().getSeconds();
        // Add a leading zero to seconds value
        $("#sec").html((seconds < 10 ? "0" : "") + seconds);
    }, 1000);

    setInterval(function() {
        // Create a newDate() object and extract the minutes of the current time on the visitor's
        var minutes = new Date().getMinutes();
        // Add a leading zero to the minutes value
        $("#min").html((minutes < 10 ? "0" : "") + minutes);
    }, 1000);

    setInterval(function() {
        // Create a newDate() object and extract the hours of the current time on the visitor's
        var hours = new Date().getHours();
        // Add a leading zero to the hours value
        $("#hours").html((hours < 10 ? "0" : "") + hours);
    }, 1000);
}

function clickToIconCalendar() {
    $('.dateRangePicker i').click(function () {
        $(this).parent().find('input').click();
    });
}

//FORMS ==================================================================
function maxLength() {
    $('textarea[maxlength]').maxlength();
    $('input[maxlength]').maxlength();
    $("input[tabindex]").attr("maxlength", "20").maxlength();
}

//SELECTIZE ==================================================================
function destroySelectize(input) {
    $(input)[0].selectize.clear();
    $(input)[0].selectize.destroy();
}

function addOptionSelectedToSelectize(controller, select) {
    var selectize = select[0].selectize;
    $.each($("#" + controller + "Id").children(), function(index, option) {
        var callback = function(response) {
            var sltOption = { id: parseInt(response.Record.Id), title: response.Record.Name };
            selectize.addOption(sltOption);
            selectize.addItem(parseInt(sltOption.id));
        };
        if ($(option).val() != "")
            getPerAjax(controller, { id: $(option).val() }, callback);
    });
}


//TOOLTIP ==================================================================
function refreshTooltip(item, title) {
    $(item).tooltip('hide').attr('data-original-title', title).tooltip('fixTitle').tooltip('show');
}

//TABLE ==================================================================
function truncateString(value, length, useWordBoundary) {
    length = (length == undefined || length == null) ? 16 : length;
    useWordBoundary = (useWordBoundary == undefined || useWordBoundary == null) ? false : useWordBoundary;

    if (value != undefined && value != null && (typeof value == "string" || value instanceof string !== false)) {
        var toLong = value.length > length;
        var s = toLong ? value.substr(0, length) : value;

        if (useWordBoundary) {
            s = useWordBoundary && toLong ? s.substr(0, s.lastIndexOf(' ')) : s;
        }

        value = toLong ? s + '&hellip;' : s;
    }

    return value;
}

//AJAX ==================================================================
function getPartialView(controller, action, data, rollBack) {
    $.ajax({
        url: controller + '/' + action,
        type: "GET",
        async: true,
        data: data,
        dataType: 'html',
        success: function(responseView) {
            if (rollBack) rollBack(responseView);
        },
        error: function() {
            alertError("Configuraci&oacute;n de Ajax inv&aacute;lido");
        }
    });
}
function appendPartialView(idToAppendd, uri, data, rollBack) {
    $.ajax({
        url: uri,
        type: "GET",
        async: true,
        data: data,
        dataType: 'html',
        success: function(responseView) {
            $("#" + idToAppendd).append(responseView);
            if (rollBack) rollBack();
        },
        error: function() {
            alertError("Configuraci&oacute;n de Ajax inv&aacute;lido");
        }
    });
}

function filterPerAjax(uri, data, callback) {
    $.ajax({
        url: uri,
        type: 'GET',
        dataType: 'json',
        async: true,
        data: data,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function () {
            alertError("Error al intentar recuperar los registros.");
        }
    });
}

function getPerAjax(uri, data, callback) {
    $.ajax({
        url: uri,
        type: 'GET',
        dataType: 'json',
        async: true,
        data: data,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function () {
            alertError("Error al intentar recuperar el registro.");
        }
    });
}

function createPerAjax(controller, data, callback) {
    $.ajax({
        url: controller + '/Create',
        type: 'POST',
        dataType: 'json',
        async: true,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function () {
            alertError("Error al intentar crear el registro.");
        }
    });
}

function updatePerAjax(controller, data, callback) {
    $.ajax({
        url: controller + '/Update',
        type: 'POST',
        dataType: 'json',
        async: true,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function () {
            alertError("Error al intentar editar el registro.");
        }
    });
}

function deletePerAjax(controller, id, callback) {
    $.ajax({
        url: controller + '/Delete',
        type: 'DELETE',
        async: true,
        data: { id: id },
        success: function(response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function() {
            alertError("No se ha podido establecer conexi&oacute;n con el servicio.");
        }
    });
}

function otherActionPerAjax(uri, data, error, callback, method, async) {
    $.ajax({
        url: uri,
        type: method,
        dataType: 'json',
        async: async,
        data: data,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            checkResponseToAjax(response);
            if (callback) callback(response);
        },
        error: function () {
            var message = error ? error : "Error al intentar recuperar los registros.";
            alertError(message);
        }
    });
}

function exportPerAjax(uri) {
    alertInfo("Obteniendo informaci&oacute;n, su descarga estar&aacute; disponible en un momento.");
    window.location = uri;
}

function checkResponseToAjax(response) {
    if (response.Result == resultFailure && response.Message)
        alertError(response.Message);
}

//NOTIFICATIONS ==================================================================
var options = { extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right', theme: 'flat' };

function alertInfo(message) {
    Messenger.options = options;

    Messenger().post({
        message: message,
        type: 'info',
        showCloseButton: true
    });
}

function alertSuccess(message) {
    Messenger.options = options;

    Messenger().post({
        message: message,
        type: 'success',
        showCloseButton: true
    });
}

function alertError(message) {
    Messenger.options = options;

    Messenger().post({
        message: message,
        type: 'error',
        hideAfter: 20,
        showCloseButton: true
    });
}
function getBranchesFromHtml(selector) {

    branchesJson = [];

    $(selector + ' option').each(function () {
        branch = {};
        branch['option'] = $(this).text();
        branch['value'] = $(this).val();
        branchesJson.push(branch);
    });

    return branchesJson;
}




$(function () {


    $('#addBranch-btn').click(function () {
        var counter = $(".selectBranchRow").length;
        var template = "<tr class='selectBranchRow'>" +
        "<td>" +
            "<div style='display:inline-block;'>" +
                 "<select id='selectBranch" + counter + "' type='text' name= 'BranchesList[" + counter + "].Id' placeholder='Seleccione un sucursal' style='width:210px !important; margin-right:5px;'></select>" +
            "</div>" +
         "</td>" +
        "<td>" +
            "<div style='display:inline-block'><div class='btn btn-danger remove-a-branch'>Eliminar</div></div>" +
       "</td>" +
       "</tr>";

        $('#BranchesContainerTable').append(template);

        var branches = getBranchesFromHtml("#Branch");
        addOptionsToSelect("#selectBranch" + counter, branches);

        return false;
    });


    $('.remove-a-branch').live('click', function () {
        var rowCount = $(".selectBranchRow").length;
        if (rowCount > 1) {
            $(this).closest(".selectBranchRow").remove();
            reindexBranches();
        }
    });

});
function reindexBranches() {
    $.each($(".selectBranchRow"), function (branchIndex, content) {
        var fieldName = "BranchesList[" + branchIndex + "].";

        $(content).find("[id*='selectBranch']").attr("id", "selectBranch" + branchIndex);
        $(content).find("[id*='selectBranch']").attr("name", fieldName + "Id");

    });
}
function getBranchesFromHtml(selector) {

    branchesJson = [];

    $(selector + ' option').each(function () {
        branch = {};
        branch['option'] = $(this).text();
        branch['value'] = $(this).val();
        branchesJson.push(branch);
    });

    return branchesJson;
}



function addOptionsToSelect(selectId, options) {

    for (i = 0; i < options.length; i++) {

        var opt = "<option value='" + options[i].value + "'>" + options[i].option + "</option>";
        $(selectId).append(opt);
    }
}