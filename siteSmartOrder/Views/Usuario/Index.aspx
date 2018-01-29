<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var currentUserActivePage = 1;
    $(function () {
        $('table').on('click', 'tr a.picLinks', function (e) {
            e.preventDefault(); 
            var link = $(this).attr('href');
            var split = link.split('/');
            var branchName = $('#ComboBranchId').find(":selected").text();
            if (split[0] == "OpenBarCodePopUp") {
                var url = '<%:ResolveClientUrl("~/Content/QRCobratario.ashx")%>';
                $('#imagen').html("<img src='" + url + "?userCode=" + split[1] + "&userName=" + split[2] + "&branchCode=" + split[3] +"&branchName="+ branchName + "'/>");
                $('#myModalLabel').html(split[1] + " " + split[2]);
                $('#myModal').modal('show');
            } else if (split[0] == "ConfirmaInactivarUsuario") {
                ConfirmaInactivarUsuario(split[1], split[2]);
            }
        });
    });
    

    var TabsUsuarios = {
        Activos: {
            tags: ["code", "name", "routeName", "deviceId", "TypeName"],
            details: [["OpenBarCodePopUp", '<i class="icon-qrcode"></i>','code',"name","branchCode"], ["ConfirmaInactivarUsuario", '<i class="icon-remove"></i>', "userId", "deviceId"]],
            idContenedor: "#activeUserGrid",
            idPaginador: '#activeUsersPagination',
            funcion: PaginaActivos, tipo: 'Active', 
            abierto: false, iniciaLista: function () { AplicarFiltroActivos(); },
            idFilter: '#filterActiveDiv',
            idNotification:'#infoBoxActive'
        },
        Disponibles: {
            tags: ["code", "name", "routeName", "TypeName"],
            details: [["OpenBarCodePopUp", '<i class="icon-qrcode"></i>', 'code',"name","branchCode"]],
            idContenedor: "#availableUserGrid",
            idPaginador: '#availableUsersPagination',
            funcion: PaginaDisponibles,
            tipo: 'Available', 
            abierto: false, iniciaLista: function () { AplicarFiltroDisponibles(); },
            idFilter: '#filterAvailableDiv',
            idNotification: '#infoBoxAvailable'
        }
    };
    
    function UsuarioSeleccionaSucursal() {
       
        var tabName = $("ul#tabconfig li.active a").html();
        TabsUsuarios.Activos.abierto = false;
        TabsUsuarios.Disponibles.abierto = false;
        $(TabsUsuarios[tabName].idPaginador + " ul").remove();
        TabsUsuarios[tabName].iniciaLista();

    }

    function PaginaActivos(page) {
        currentUserActivePage = page;
        var branchId = $('#ComboBranchId').val();
        var filter = $('#filterActivos').val();
        if (branchId > 0) {
            var URL = '<%:ResolveClientUrl("~/Usuario/Paginacion")%>' + '?page=' + page + '&branchId=' + branchId + '&type=Active&filter=' + filter;
            ExecutePaging(URL,page, TabsUsuarios.Activos);
        }
    }

    function AplicarFiltroActivos() {
        PaginaActivos(1);
    }

    function AplicarFiltroDisponibles() {
        PaginaDisponibles(1);
    }

    function PaginaDisponibles(page) {
        var branchId = $('#ComboBranchId').val();
        var filter = $('#filterDisponibles').val();
        if (branchId > 0) {
            var URL = '<%:ResolveClientUrl("~/Usuario/Paginacion")%>' + '?page=' + page + '&branchId=' + branchId + '&type=Available&filter='+filter;
            ExecutePaging(URL,page, TabsUsuarios.Disponibles);
        }
    }


    function ConfirmaInactivarUsuario(id,deviceId) {
        bootbox.confirm("¿Está seguro que desea inactivar?", "Cancelar", "Si",
         function (result) {
            if (result) {
                InactivarUsuarioDevice(id, deviceId);
            }
            else
                bootbox.hideAll();
        });      
    }

    function InactivarUsuarioDevice(id, deviceId) {
        var URL = '<%:ResolveClientUrl("~/Usuario/Inactivar")%>' + '?userId=' + id + '&deviceId=' + deviceId;

        $.ajax({
            url: URL,
            success: function (jsonData) {
                var response = $.parseJSON(jsonData);
                if (response.IsSuccess) {
                    AplicarFiltroActivos(currentUserActivePage);
                    //                     $("#tr_" + deviceId).remove();
                    //                     if ($('#activeUserGrid tr').length == 1)
                    //                         $("#infoBox").show();
                } else {
                    alert(response.Message);
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
            }
        });
     }

     var configs = {
         aditional : "", 
         urlProcess: '<%:ResolveClientUrl("~/Usuario/GeneraInformacion")%>'+'?branchId=',
         urlPercent: '<%:ResolveClientUrl("~/Usuario/ProcesoPorcent")%>' + '?processId=',
         divProgress: "#barraProgreso",
         divProgressMessage: "#processMessage",
         divError: "#divAlerta",
         errorMessage: "se encuentra iniciado, no se puede continuar"
     };

     function GenerarInformacion() {
         var branchId = $('#ComboBranchId').val();
         if (branchId > 0) {
             
             configs.aditional = branchId;
             $('body').unbind('ajaxStart');
             ClearAllProgressBarComponent(configs);
             ProgressBarComponent(configs);
         } else {
            alert("No se ha seleccionado una sucursal");
         }
     }

     function ExecutePaging(url, page, config) {
         $.ajax({
             url: url,
             success: function (data) {

                 var response = JSON.parse(data);
                 if (response.IsSuccess) {
                     var datos = response.Data.Data;
                     var count = response.Data.DataCount;
                     if (count == 0)
                         count = 1;
                     PrintTableWithFilter(config.tags, datos, config.idContenedor, config.details, config.idFilter, config.idNotification);
                     PrintPagination(page, count, config);
                 } else {
                     alert(response.Message);
                 }

             }, error: function (xhr, ajaxOptions, thrownError) {
                 if (xhr.status == 404)
                     window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
             }
         });
     }
</script>
  <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        <%if(Model != null){ %>
          <div class="span12 pagination-right">
          <p>Seleccione una sucursal</p>
          <%: Html.DropDownList("ComboBranchId", new SelectList((IEnumerable)Model,"branchId", "name", Model.ToString()), new { onchange = "UsuarioSeleccionaSucursal();"})%>
           <legend></legend>  
          </div>
          <%} %>

            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a href="#activos" id="activosTab">Activos</a></li>
                <li><a href="#disponibles" id="disponiblesTab">Disponibles</a></li>
                <li><a href="#generar" id="generarInfoTab">Generar</a></li>            
            </ul>
            <div class="tab-content">
                <div id="activos" class="tab-pane active">
                    <h3>Lista Activos</h3>
                    <div class="alert alert-info" id="infoBoxActive" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>              
                     <div class="input-append" id="filterActiveDiv" style="display: none">
                        <input class="span3 offset8"id="filterActivos" onkeypress="evaluateKeyPress(event,'AplicarFiltroActivos()')"type="text"/>
                      <button class="btn" type="button" onclick="AplicarFiltroActivos();"><i class="icon-search"></i></button>
                    </div> 

                    <div id="activeUsersContainer">
                         <table class="table  table-hover table-striped" id="activeUserGrid">
                            <thead>
                               <tr>
                                  <%--  <th style="text-align:center">Index</th>--%>
                                    <th style="text-align:center">C&oacute;digo</th>
                                    <th style="text-align:center"><i class="icon-user"></i>Nombre</th>
                                    <th style="text-align:center"><i class="icon-user"></i>Ruta</th>
                                    <th style="text-align:center"><i class="icon-signal"></i>DeviceID</th>
                                    <th style="text-align:center"><i class="icon-tag"></i>Tipo</th>
                                    <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                               </tr>
                            </thead>
                        </table>
                    </div>
                    <div class="pagination-centered" id="activeUsersPagination">
                    </div>
                    <script type="text/javascript">
                        var branchId = $('#ComboBranchId').val();
                        if (branchId > 0) {
                            TabsUsuarios.Activos.iniciaLista();
                        }
                    </script>
                </div>

                <div id="disponibles" class="tab-pane">
                 <!--%: Html.Action("Disponibles", "Usuario")%-->
                   <h3>Lista Disponibles</h3>
                  <div class="alert alert-info" id="infoBoxAvailable" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                  </div>      
                  <div class="input-append" id="filterAvailableDiv" style="display: none">
                     <input class="span3 offset8"id="filterDisponibles" onkeypress="evaluateKeyPress(event,'AplicarFiltroDisponibles()')" type="text"/>
                     <button class="btn" type="button" onclick="AplicarFiltroDisponibles();"><i class="icon-search"></i></button>
                  </div>
                  <div id="availableUsersContainer">
                      <table class="table  table-hover table-striped" id="availableUserGrid">
                        <thead>
                           <tr>
                                <th style="text-align:center">C&oacute;digo</th>
                                <th style="text-align:center"><i class="icon-user"></i>Nombre</th>
                                <th style="text-align:center"><i class="icon-home"></i>Ruta</th>
                                <th style="text-align:center"><i class="icon-tag"></i>Tipo</th>
                                <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                           </tr>
                        </thead>
                    </table>
                  </div>
                  <div class="pagination-centered" id="availableUsersPagination">
                  </div>
                </div>

                <div id="generar" class="tab-pane">
                    <div id="divAlerta" style="font-weight: bold;">
                  
                    </div>
                    <legend>Generar informacion</legend>

                     <div class="progress progress-striped active">
                        <div id="barraProgreso" class="bar" style="width: 0%;">
                        </div>
                     </div>
                     <div id="processMessage">
                    
                     </div>
                    <button type="button" class="btn btn-success" onclick="GenerarInformacion();"><i class="icon-play icon-white"></i> Iniciar</button>
                </div>
               
            </div>
            
             <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h3 id="myModalLabel">Modal header</h3>
                      </div>
                      <div class="modal-body">
                          <div id="imagen" style="vertical-align: central"></div>
                          
                      </div>
                      <div class="modal-footer">
                        <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
                      </div>
                    </div>

        </div>
    </div>
      
    <script type="text/javascript">
        $('#tabconfig a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
            var tabName = $(this).html();
            var branchId = $('#ComboBranchId').val();
            if (branchId > 0) {
                if (!TabsUsuarios[tabName].abierto) {
                    $(TabsUsuarios[tabName].idPaginador + " ul").remove();
                    TabsUsuarios[tabName].iniciaLista();
                }
            }
        });
        </script>
</asp:Content>
