/*******************************************************
    Funciones del componente ProgressBar

********************************************************/

var ProgressBarComponent = function(config) {
    $.ajax({
        url: config.urlProcess + config.aditional,
        success: function(data) {
            var response = $.parseJSON(data);
            var processId = response.Data;
            if (response.IsSuccess) {
                $(config.divError).html("<div class='alert alert-success'>No. Proceso: " + processId + " Iniciado</div>");
                $('body').unbind('ajaxStart');
                GetProcessPercent(config, processId, 2000);
            } else if (response.Data > 0)
                SeeOpenProcess(config, processId, 2000);
            else
                $(config.divError).html("<div class='alert alert-error'>" + response.Message + "</div>");
        }
    });
};

function SeeOpenProcess(config, processId, miliSeconds) {
    bootbox.confirm("El proceso: " + processId + " esta abierto, no de puede continuar, desea visualizarlo?", "Cancelar", "Si",
         function (result) {
             if (result) {
                 GetProcessPercent(config, processId, miliSeconds);
             }
             else {
                 $(config.divError).html("<div class='alert alert-error'>No. Proceso: " + processId + " " + config.errorMessage + "</div>");
                 bootbox.hideAll();
             }
         });
     }

function GetProcessPercent(config, processId, miliSeconds) {
    var timer = setInterval(function () {
        $.ajax({
            url: config.urlPercent + processId,
            success: function (data) {
                var responsePercent = $.parseJSON(data);
                if (responsePercent.Data == 100) {
                    $(config.divError).html("<div class='alert alert-info'>No. Proceso: " + processId + " finalizado...</div>");
                    $(config.divButtonStart).removeAttr("disabled");
                    timer = clearInterval(timer);
                }
                $(config.divProgress).css({ 'width': responsePercent.Data + '%' });
                $(config.divProgressMessage).html("<p class='text-info'>" + responsePercent.Message + "</p>");
            }
        });
    }, miliSeconds);
     }

function ClearAllProgressBarComponent(config) {

         $(config.divProgress).css({ 'width': '0%' });
         $(config.divProgressMessage).html("");
         $(config.divError).html("");
     }

/*********************************************************

     Funciones para la creacion de la tabla dinamica

***************************************************************/

 function PrintTableWithFilter(tags, json, grid, details,filter,alert) {
     if (json.length > 0 ) {
         $(filter).show();
         $(alert).hide();
         PrintTable(tags,json,grid,details);
     }
     else {
         $(grid + " > tbody:last").children().remove();
         //$(filter).hide();
         $(alert).show();
     }
 }

 function PrintReferenceTable(tags, json, grid, details) {
     var tabla = $(grid);
     $(grid + " > tbody:last").children().remove();
     $.each(json, function () {
         var $linea2 = $('<tr onclick="trClick(this);" id="Reference' + this["Id"] + '"></tr>');
         for (var ii = 0; ii < tags.length; ii++) {
             $linea2.append($('<td style="text-align:center"></td>').html(this[tags[ii]]));
         }
         if (details.length > 0) {
             var atributoa = "";
             for (var k = 0; k < details.length; k++) {
                 var detalle = details[k];
                 var complemento = "/" + this[detalle[2]];
                 if (detalle.length > 3) {
                     for (var j = 3; j < detalle.length; j++) {
                         complemento += "/" + this[detalle[j]];
                     }

                 }
                 atributoa += '<a class="picLinks" href="' + detalle[0] + complemento + '"> ' + detalle[1] + '</a>';
             }
             $linea2.append($('<td style="text-align:center"></td>').html(atributoa));
         }

         tabla.append($linea2);
     });
 }

 function PrintTable(tags, json, grid, details) {
     var tabla = $(grid);
         $(grid + " > tbody:last").children().remove();
         $.each(json, function () {
             var $linea2 = $('<tr></tr>');
             for (var ii = 0; ii < tags.length; ii++) {
                     $linea2.append($('<td style="text-align:center"></td>').html(this[tags[ii]]));               
             }
             if (details.length > 0) {
                 var atributoa = "";
                 for (var k = 0; k < details.length; k++) {
                     var detalle = details[k];
                     var complemento = "/" + this[detalle[2]];
                     if (detalle.length > 3) {
                         for (var j = 3; j < detalle.length; j++) {
                             complemento += "/" + this[detalle[j]];
                         }

                     }
                     atributoa += '<a class="picLinks" href="' + detalle[0] + complemento + '"> ' + detalle[1] + '</a>';
                 }
                 $linea2.append($('<td style="text-align:center"></td>').html(atributoa));
             }

             tabla.append($linea2);
         });
     }

/************************************************************************************
     
   Funcion para pintar el paginador de las tablas  
     
*************************************************************************************/
 function PrintPagination(page, count, config) {
         var options = {
             numberOfPages: 10,
             currentPage: page,
             totalPages: count,
             size: 'normal',
             alignment: 'center',
             tooltipTitles: function (type, pagex, current) {
                 switch (type) {
                     case "first":
                         return "Primera pagina";
                     case "prev":
                         return "Pagina anterior";
                     case "next":
                         return "Pagina siguiente";
                     case "last":
                         return "Ultima pagina";
                     case "page":
                         return "Mover a la pagina: " + pagex;
                 }
             },
             onPageClicked: function (e, originalEvent, type, pagex) {
                 config.funcion(pagex);
             }
         };
         $(config.idPaginador).bootstrapPaginator(options);
     }
     


 function evaluateKeyPress(e, funcion) {
     if (e.keyCode == 13) {
         return eval(funcion);
     }
 }

 /************************************************************************************
     
   Funcion para pintar tabla de jornadas con index 
     
*************************************************************************************/
 function PrintJournalTable(tags, json, grid, details) {
     var tabla = $(grid);
//     var arrayIndex = [];
//     var currentPage = page;
//     alert(page);
//     var pagin = 15;
//     var firstNumber = 15 * currentPage - 15;
//     for (var j = 0; j < pagin; j++) {
//         firstNumber++;      
//         arrayIndex.push(firstNumber);
//     }
     var cont = 0;
     
         $(grid + " > tbody:last").children().remove();
         $.each(json, function () {
             var $linea2 = $('<tr></tr>');
             for (var ii = 0; ii < tags.length; ii++) {
                 if (ii == 0) {
                     
                     $linea2.append($('<td style="text-align:center"></td>').html(cont + 1));
                     cont++;
                 }
                 else {
                     $linea2.append($('<td style="text-align:center"></td>').html(this[tags[ii]]));
                 }
                 
             }
             if (details.length > 0) {
                 var atributoa = "";
                 for (var k = 0; k < details.length; k++) {
                     var detalle = details[k];
                     var complemento = "/" + this[detalle[2]];
                     if (detalle.length > 3) {
                         for (var j = 3; j < detalle.length; j++) {
                             complemento += "/" + this[detalle[j]];
                         }

                     }
                     atributoa += '<a class="picLinks" href="' + detalle[0] + complemento + '"> ' + detalle[1] + '</a>';
                 }
                 $linea2.append($('<td style="text-align:center"></td>').html(atributoa));
             }

             tabla.append($linea2);
         });
     }

/************************************************************************************/