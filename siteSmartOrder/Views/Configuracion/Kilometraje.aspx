<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Kilometraje
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<script type="text/javascript">
    
    function ObtenerArchivo(e) {
        
    }

    function SubirArchivo(e) {
        //e.preventDefault();
        //var archivo = ObtenerArchivo();
            <%--$.ajax({
            url: '<%:ResolveClientUrl("~/Configuracion/CargarKilometraje")%>',
            data: archivo,
            type: 'POST',           
            success: function (data) {
                alert(data);
                //var response = $.parseJSON(data.Data);
                /* if ($.fn.DataTable.isDataTable('#tablaResultados')) {
                    $('#tablaResultados').DataTable().destroy();
                }*/
                //if (response != null && response.IsSuccess) {
                //    $("#successMsg").html("<p>" + response.Message + "</p>");
                //}
                //else {
                //    $("#successMsg").html("<p>" + response.Message + "</p>");
                //}
                //$('#tablaResultados tbody').empty();
                //cargarTabla(data);
            }
        });  --%>

    }

    
    

    function UploadFileSuccess() {
        $("#divUpload").hide();
        $("#divProcess").show();
    }
    
    function UploadFileResult(tipo, mensaje) {

        /*if (tipo == 'success') {
            $("#fileName").val(fileName);
            UploadFileSuccess();
        }*/
        alert(mensaje);
        $("#alerta").html("<div class='alert alert-" + tipo + "'>" + mensaje + "</div>");
    }

</script>            
       
                  <div id="alerta" style="font-weight: bold;">
                  
                  </div>
                 <h3>Cargar Kilometraje Teórico</h3>              
                    <p>Seleccione el archivo <b>EXCEL(*.xls)</b> generado a partir de la plantilla, para cargar
                        los datos en el sistema.</p>
                   <%-- 
                    <% using (Html.BeginForm("CargarKilometraje", "Configuracion", FormMethod.Post, new { enctype = "multipart/form-data", id = "FormUpload" }))
                       {%>--%>
                    <form id="formulario" class="form-inline">
                       <p></p>
                        <label for="file">
                            Archivo:</label>
                        <input type="file" id="file" class="btn" name="file" style="background: #FDFCFC;
                            border: thin solid black; border-radius: 5px; margin-left: 3%;" placeholder="Seleccione el archivo xls o xlsx con plantilla"/>
                        
                        <button type="button" id="subirArchivo" class="btn btn-primary" style="margin-left: 3%;" data-request-url="<%:ResolveClientUrl("~/Configuracion/CargarKilometraje")%>">
                            <i class="icon-arrow-up icon-white"></i>Cargar</button>
                    </form>
                   <%-- <%} %>--%>
                   <%  
                            var mensaje = "";
                            if (TempData["result"] != null)
                            {
                                mensaje = "<script>UploadFileResult('"+TempData["tipo"]+"',"+'"'+TempData["result"]+'"'+");</script>";
                               
                            }

                            Response.Write(mensaje);
                        %>
                    
                    <div id="resultados">
                        
                        <div id="alertMsg"></div>
                        <table id="tablaResultados" class="table table-hover table-striped" width="100%">
                            <thead></thead>
                            <tbody></tbody>
                        </table>
                    </div>
    
    <script type="text/javascript" src="<%:ResolveClientUrl("~/Scripts/DataTables/dataTables.bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Scripts/DataTables/dataTables.buttons.min.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Scripts/DataTables/buttons.bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Scripts/jszip.min.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Scripts/DataTables/buttons.html5.min.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Scripts/DataTables/dataTable.language.spanish.js")%>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/TheoreticalMileage/FileManager.js")%>" defer="defer"></script>
</asp:Content>

