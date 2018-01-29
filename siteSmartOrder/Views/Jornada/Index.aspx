<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script>
﻿var tabsJourneys = {
    Jornada: {
        tags: ["Index","UserCode", "DeviceId", "StartDate", "EndDate"],
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
        
        var url = '<%:ResolveClientUrl("~/Jornada/GetJorneys")%>' + '?branchId=' + branchId;
        GetJourneysTable(url, tab);
    }
}

function GetJourneysTable(url, config) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);
            //PrintTable(config.tags, response.Data, config.tableId, config.details);
            PrintJournalTable(config.tags, response.Data, config.tableId, config.details);

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
/*
function FinishCofirmJourney(workDayId) {
    bootbox.alert("para realizar el cierre de jornada, siga los siguientes pasos:" +
        "<br/>1.- visite a todos sus clientes en el SmartPhone."+
        "<br/>2.- Asegurate de tener conexión a internet en el SmartPhone" +
        "<br/>3.- Finalice su viaje en el SmartPhone." +
        "<br/>4.- Asegurate de no tener mas viajes por realizar puedes consultar en opecd o si la aplicación indica que faltan clientes por visitar" +
        "<br/>5.- Si nada de lo anterior funciona, contacta al service desk, indicando el mensaje de error que se te presenta para poder apoyarte" 
        );
}
*/
function FinishCofirmJourney(workDayId) {
        bootbox.confirm("¿Está seguro que desea finalizar la jornada?", "Cancelar", "Si",
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
        bootbox.confirm(message + " - ¿Desea forzar el cierre de jornada?", "Cancelar", "Si",
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
     
      var url = '<%:ResolveClientUrl("~/Jornada/finishJorneys")%>' + "?workDayId=" + workDayId;
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
    
      var url = '<%:ResolveClientUrl("~/Jornada/ForceFinishJorney")%>' + "?workDayId=" + workDayId;
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
</script>
    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        <%if(Model != null){ %>
          <div class="span12 pagination-right">
          <p>Seleccione una sucursal</p>
          <%: Html.DropDownList("ComboBranchId", new SelectList((IEnumerable)Model, "branchId", "name", Model.ToString()), new { onchange = "SelectBranch();"})%>
           <legend></legend>  
          </div>
          <%} %>

            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a href="#journey" id="cargaTab">Jornada</a></li>     
            </ul>
            <div class="tab-content">
                <div id="journey" class="tab-pane active">
                    <h3>Inicios de jornada</h3>
                      
                    <div id="cargaContainer">
                         <table class="table  table-hover table-striped" id="cargaGridJourney">
                            <thead>
                               <tr>
                               <th style="text-align:center"></th>
                                    <th style="text-align:center">UserCode</th>
                                    <th style="text-align:center">DeviceId</th>
                                    <th style="text-align:center"><i class="icon-calendar"></i>Inicio</th>
                                    <%--<th style="text-align:center"><i class="icon-calendar"></i>Fin</th>--%>
                                    <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                               </tr>
                            </thead>
                        </table>
                    </div>
                    <div class="pagination-centered" id="activeUsersPagination">
                    </div>
     
                </div>               
            </div>
                                                           
            <script type="text/javascript">                Initialize();</script>
        </div>
    </div>

</asp:Content>
