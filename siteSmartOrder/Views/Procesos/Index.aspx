<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
﻿var tabsProcess = {
    Carga: {
        tags: ["ProcessId", "CreatedOn", "EndProcess", "Percent", "IsError"],
        details: [
            ["summary", '<i class="icon-list"></i>', 'ProcessId'],
            ["errorLog", '<i class="icon-exclamation-sign"></i>', "ProcessId"],
            ["viewProcess", '<i class="icon-eye-open"></i>', "ProcessId"]
        ],
        tableId: "#cargaGrid",
        type:2,
        name: "Carga"
    },
    Descarga: {
        tags: ["ProcessId", "CreatedOn", "EndProcess", "Percent", "IsError"],
        details: [
            ["summary", '<i class="icon-list"></i>', 'ProcessId'], 
            ["errorLog", '<i class="icon-exclamation-sign"></i>', "ProcessId"],
            ["viewProcess", '<i class="icon-eye-open"></i>', "ProcessId"]
        ],
        tableId: "#descargaGrid",
        type:3,
        name: "Descarga"
    },
    
    Generar: {
        tags: ["ProcessId", "CreatedOn", "EndProcess", "Percent", "IsError"],
        details: [
            ["summary", '<i class="icon-list"></i>', 'ProcessId'], 
            ["errorLog", '<i class="icon-exclamation-sign"></i>', "ProcessId"],
            ["viewProcess", '<i class="icon-eye-open"></i>', "ProcessId"]
        ],
        tableId: "#generaInfoGrid",
        type:1,
        name: "Generar"
    },

    ProcessFiles:{
        tags:["Name", "CreatedOn"],
        details: [["file", '<i class="icon-download-alt"></i>', "Name", "Extension"]],
        tableId: "#processFilesGrid",
        name: "ProcessFiles"
    },
    
    processPercent: {
        urlPercent: '<%:ResolveClientUrl("~/Procesos/GetProcessPercent?processId=")%>',
        divError: "#errorProgress",
        cancelButton: "#cancelButton",
        divProgress: "#progressPercent",
        divProgressMessage:"#progressMessage",
        modal: "#viewProcessModal",
        canClose: false,
        currentProcessId : 0
    }
};

﻿var logs = {
﻿    summary: {
﻿        tags:["TableName", "ToProcess", "Process", "IsWarning", "BranchCode"],
﻿        details:[],
﻿        tableId: "#summaryGrid",
﻿        modal: "#summaryModal",
﻿        url:'<%:ResolveClientUrl("~/Procesos/GetSummary?processId=")%>',
﻿    },
﻿    
    errorLog: {
        tags:["Description", "BranchCode"],
        details:[],
        tableId: "#logErrorGrid",
        modal: "#logErrorModal",
        url: '<%:ResolveClientUrl("~/Procesos/GetLogError?processId=")%>'
    }
﻿};
﻿
function OpenModals(e)
{  
    e.preventDefault();
    var link = $(this).attr('href');
    var split = link.split('/');

    SelectAction(split);
}

function SelectAction(parameters) {
    switch (parameters[0]) {
    case tabsProcess.Carga.details[0][0]:
    case tabsProcess.Carga.details[1][0]:
        GetLog(parameters[1], logs[parameters[0]]);
        break;
    case tabsProcess.Carga.details[2][0]:
        ViewProcess(parameters[1]);
        break;
    case tabsProcess.ProcessFiles.details[0][0]:
        DownloadFile(parameters[1], parameters[2]);
        break;
    }
}

function Initialize() { 
    GetProcesses(tabsProcess.Carga);
    $('#tabconfig a').click(ChangeTab);
    $('table').on('click', 'tr a.picLinks', OpenModals);
    $('#cancelButton').on('click', CancelProcess);    
    $('#viewProcessModal').on('hidden', CloseProcessPercentModal);
    $('#viewProcessModal').on('shown', function (){ $('body').unbind('ajaxStart');});
    $('#viewProcessModal').on('hidden', function() { $('body').bind('ajaxStart'); });
}

function GetProcesses(tab) {
    var branchId = $('#ComboBranchId').val();
            
    var url = '<%:ResolveClientUrl("~/Procesos/GetProcesses")%>' + "?branchId=" + branchId + "&type=" + tab.type;
    GetProcessesTable(url, tab);
}

function GetLog(processId, tab) {

    if (processId > 0) {
        GetLogTable(tab.url + processId, tab);
    }
}

function GetLogTable(url, tab) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);            
            if(tab.tableId == logs.summary.tableId) {
                $.each(response.Data, function(index, summary) {
                    summary.IsWarning = (summary.IsWarning) ? '<i class="icon-warning-sign"></i>' : '';
                });
            }            
            PrintTable(tab.tags, response.Data, tab.tableId, tab.details);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
    
    $(tab.modal).modal().show();
}

function GetProcessesTable(url, config) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);
            var errorRows = GetErrorRows(response.Data);
            PrintTable(config.tags, response.Data, config.tableId, config.details);
            ChangeRowBackground(config, errorRows);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
}

function GetErrorRows(data) {
    var rows = new Array();
    $.each(data, function(row) {
        if(data[row].Percent == 100 && data[row].IsError) {
            data[row].IsError = "Error";
            rows.push(row);
        }
    });

    return rows;
}

function ChangeRowBackground(tab, errorRows) {
    var gridRows = $(tab.tableId + " tr");
    $.each(errorRows, function(row, value) {
        var cells = gridRows[value + 1].getElementsByTagName('td');
        $(cells[$.inArray("IsError", tab.tags)]).css("color", "#BD362F");
    });
}

function SelectBranch() {
    var tabName = $("ul#tabconfig li.active a").attr('id');
    OnChangeTab(tabName);
}

function ChangeTab(e) {
    e.preventDefault();
    var tabName = $(this).attr('id');
    
    $(this).tab('show');
    
    OnChangeTab(tabName);
}

function OnChangeTab(tabName)
{  
    switch(tabName)
    {
        case tabsProcess.Carga.name:
        case tabsProcess.Descarga.name:
        case tabsProcess.Generar.name:
            CancelButtonBehavior(tabsProcess[tabName].type);
            GetProcesses(tabsProcess[tabName]);
            break;
        case tabsProcess.ProcessFiles.name:
            GetProcessesFiles(tabsProcess[tabName]);
            break;
    }
}

function ViewProcess(processId) {
    if(processId > 0) {
        ClearAllProgressBarComponent(tabsProcess.processPercent);
        tabsProcess.processPercent.currentProcessId = processId;

        tabsProcess.processPercent.canClose = false;
        
        ShowProcessPercent(tabsProcess.processPercent, 2000);
        $(tabsProcess.processPercent.modal).modal().show();
    }
}

function CancelProcess() {
    bootbox.confirm("¿Está seguro que desea cancelar este proceso?", "Cancelar", "Sí", function (answer) {    
        if(answer) {
            $.ajax({
                url: '<%:ResolveClientUrl("~/Procesos/CancelProcess")%>?processId=' + tabsProcess.processPercent.currentProcessId,
                success: function (data) {                    
                    var response = $.parseJSON(data);
                    if(response.IsSuccess) {
                        $(tabsProcess.processPercent.modal).modal('hide');
                        GetProcesses(tabsProcess.Carga);
                    } else {
                        $(tabsProcess.processPercent.divError).html("<div class='alert alert-info'>" + response.Message + "</div>");
                    }

                    bootbox.hideAll();
                }
            });
        }
    });
}

function CloseProcessPercentModal() {
    tabsProcess.processPercent.canClose = true;
}

function ShowProcessPercent(config, miliSeconds) {
        $(config.cancelButton).prop("disabled", false);
        $.ajax({
            url: config.urlPercent + config.currentProcessId,
            success: function (data) {                
                var responsePercent = $.parseJSON(data);
                if (responsePercent.Data == 100) {
                    $(config.divError).html("<div class='alert alert-info'>No. Proceso: " + config.currentProcessId + " finalizado...</div>");
                    $(config.cancelButton).prop("disabled", true);
                    config.canClose = true;
                    
                }
                $(config.divProgress).css({ 'width': responsePercent.Data + '%' });
                $(config.divProgressMessage).html("<p class='text-info'>" + responsePercent.Message + "</p>");
                
                if(!config.canClose) {
                    setTimeout(ShowProcessPercent, miliSeconds, config, miliSeconds);
                }
            }
        });
     }

function CancelButtonBehavior(processType) {
    $(tabsProcess.processPercent.cancelButton).on('click', CancelProcess);
    $(tabsProcess.processPercent.cancelButton).show();

    if(processType == tabsProcess.Generar.type) {
        $(tabsProcess.processPercent.cancelButton).unbind('click', CancelProcess);
        $(tabsProcess.processPercent.cancelButton).hide();
    }
}

function GetProcessesFiles(config) {
    $.ajax({
        url: '<%:ResolveClientUrl("~/Procesos/GetFiles")%>',
        success: function (data) {
            var response = JSON.parse(data.Data);
            PrintTable(config.tags, response.Data, config.tableId, config.details);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
}

function DownloadFile(name, extension)
{
    window.location = '<%:ResolveClientUrl("~/Procesos/Download?fileName=")%>' + name + '&fileExtension=' + extension;    
}

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
                <li class="active"><a href="#carga" id="Carga">Carga</a></li>
                <li><a href="#descarga" id="Descarga">Descarga</a></li>
                <li><a href="#generaInfo" id="Generar">Generar</a></li>
                <li><a href="#processFiles" id="ProcessFiles">Logs</a></li>    
            </ul>
            <div class="tab-content">
                <div id="carga" class="tab-pane active">
                    <h3>Procesos de carga</h3>
                      
                    <div id="cargaContainer">
                         <table class="table  table-hover table-striped" id="cargaGrid">
                            <thead>
                               <tr>
                                    <th style="text-align:center">Proceso</th>
                                    <th style="text-align:center"><i class="icon-calendar"></i>Inicio</th>
                                    <th style="text-align:center"><i class="icon-calendar"></i>Fin</th>
                                    <th style="text-align:center"><i class="icon-signal"></i>%</th>
                                    <th style="text-align:center"><i class="icon-exclamation-sign"></i>Error</th>
                                    <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                               </tr>
                            </thead>
                        </table>
                    </div>
                    <div class="pagination-centered" id="activeUsersPagination">
                    </div>
     
                </div>

                <div id="descarga" class="tab-pane">
                   <h3>Procesos de descarga</h3>
                 
                  <div id="descargaContainer">
                      <table class="table  table-hover table-striped" id="descargaGrid">
                        <thead>
                           <tr>
                                <th style="text-align:center">Proceso</th>
                                <th style="text-align:center"><i class="icon-calendar"></i>Inicio</th>
                                <th style="text-align:center"><i class="icon-calendar"></i>Fin</th>
                                <th style="text-align:center"><i class="icon-signal"></i>%</th>
                                <th style="text-align:center"><i class="icon-exclamation-sign"></i>Error</th>
                                <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                           </tr>
                        </thead>
                    </table>
                  </div>
                  <div class="pagination-centered" id="availableUsersPagination">
                  </div>
                </div> 
                
                <div id="generaInfo" class="tab-pane">
                   <h3>Generar Informaci&oacute;n</h3>
                 
                  <div id="generaInfoContainer">
                    <table class="table  table-hover table-striped" id="generaInfoGrid">
                        <thead>
                           <tr>
                                <th style="text-align:center">Proceso</th>
                                <th style="text-align:center"><i class="icon-calendar"></i>Inicio</th>
                                <th style="text-align:center"><i class="icon-calendar"></i>Fin</th>
                                <th style="text-align:center"><i class="icon-signal"></i>%</th>
                                <th style="text-align:center"><i class="icon-exclamation-sign"></i>Error</th>
                                <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                           </tr>
                        </thead>
                    </table>
                  </div>
                  <div class="pagination-centered" id="Div3">
                  </div>
                </div>
                
                <div id="processFiles" class="tab-pane">
                   <h3>Logs</h3>
                 
                   <div id="processFilesContainer">
                   <table class="table  table-hover table-striped" id="processFilesGrid">
                        <thead>
                           <tr>
                                <th style="text-align:center">Nombre de Archivo</th>
                                <th style="text-align:center"><i class="icon-calendar"></i>Fecha de Creaci&oacute;n</th>
                                <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                           </tr>
                        </thead>
                    </table>
                  </div>
                </div>                 
            </div>
                                               
            <div id="summaryModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3>Resumen</h3>
                </div>
                <div class="modal-body">
                    <table class="table  table-hover table-striped" id="summaryGrid">
                        <thead>
                            <tr>
                                <th style="text-align:center">Nombre de Tabla</th>
                                <th style="text-align:center"><i class="icon-resize-small"></i>A Proceso</th>
                                <th style="text-align:center"><i class="icon-retweet"></i>Proceso</th>
                                <th style="text-align:center"><i class="icon-warning-sign"></i>Advertencia</th>
                                <th style="text-align:center"><i class="icon-map-marker"></i>Sucursal</th>
                            </tr>
                        </thead>
                    </table>
                </div>                
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                </div>
            </div>
            
            <div id="logErrorModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3>Log de Errores</h3>
                </div>
                <div class="modal-body">
                    <table class="table  table-hover table-striped" id="logErrorGrid">
                        <thead>
                            <tr>
                                <th style="text-align:center"><i class="icon-pencil"></i>Descripci&oacute;n</th>
                                <th style="text-align:center"><i class="icon-map-marker"></i>Sucursal</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                </div>
            </div>
            
            <div id="viewProcessModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3>Progreso del Proceso</h3>
                </div>
                <div class="modal-body">
                    <div id="errorProgress" style="font-weight: bold;">
                  
                    </div>

                     <div class="progress progress-striped active">
                        <div id="progressPercent" class="bar" style="width: 0%;">
                        </div>
                     </div>
                     <div id="progressMessage">
                    
                     </div>
                     <div id="cancelProcess">
                         <button type="button" class="btn btn-danger" id="cancelButton">Cancelar</button>                         
                     </div>                                      
                </div>                
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                </div>
            </div>

            <script type="text/javascript">Initialize();</script>
        </div>
    </div>
</asp:Content>
