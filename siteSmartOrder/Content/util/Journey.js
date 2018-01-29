﻿var tabsJourneys = {
    Jornada: {
        tags: ["UserId", "DeviceId", "StartDate"],
        details: [["FinishCofirmJourney", '<i class="icon-ok-sign"></i>', 'WorkDayId']],
        tableId: "#cargaGridJourney"
    }
};

function Initialize() {
    GetJourneys(tabsJourneys.Jornada);
}

function GetJourneys(tab) {
    var branchId = $('#ComboBranchId').val();
    
    if (branchId > 0) {
        var url = "/Jornada/GetJorneys" + "?branchId=" + branchId;
        GetJourneysTable(url, tab);
    }
}

function GetJourneysTable(url, config) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);
            PrintTable(config.tags, response.Data, config.tableId, config.details);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
}

function SelectBranch() {
    var tabName = $("ul#tabconfig li.active a").html();
    GetJourneys(tabsJourneys[tabName]);
}


///Ended Journey

function FinishCofirmJourney(workDayId) {
        bootbox.confirm("¿Está seguro que desea finalizar jornada?", "Cancelar", "Si",
         function (result) {
            if (result) {
                InactivarUsuarioDevice(workDayId);
                Initialize();
            }
            else
                bootbox.hideAll();
        });      
    }

function ForceFinishJourney(workDayId, message) {
        bootbox.confirm(message + " - Desea forzar finalizar jornada?", "Cancelar", "Si",
         function (result) {
            if (result) {
                ForceFinishJorneyPortal(workDayId);
                Initialize();
            }
            else
                bootbox.hideAll();
        });      
    }
    
function InactivarUsuarioDevice(workDayId) {
      var url = "/Jornada/finishJorneys" + "?workDayId=" + workDayId;
      $.ajax({
             url: url,
             success: function (jsonData) {
                 var response = $.parseJSON(jsonData);
                 if (response.IsSuccess) {
                     alert(response.Message);
                 } else {
                     ForceFinishJourney(workDayId, response.Message);
                     //alert(response.Message);
                 }
             }, error: function (xhr, ajaxOptions, thrownError) {
                 if (xhr.status == 404)
                     window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
             }
         });
     }

function ForceFinishJorneyPortal(workDayId) {
      var url = "/Jornada/ForceFinishJorney" + "?workDayId=" + workDayId;
      $.ajax({
             url: url,
             success: function (jsonData) {
                 var response = $.parseJSON(jsonData);
                 if (response.IsSuccess) {
                     alert(response.Message);
                 } else {
                     alert(response.Message);
                 }
             }, error: function (xhr, ajaxOptions, thrownError) {
                 if (xhr.status == 404)
                     window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
             }
         });
     }

$(function () {
        $('table').on('click', 'tr a.picLinks', function (e) {
            e.preventDefault();
            var link = $(this).attr('href');
            var split = link.split('/');
            if (split[0] == "FinishCofirmJourney") {
                FinishCofirmJourney(split[1]);
            }
        });
    });