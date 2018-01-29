<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Importar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

    $('body').bind('ajaxStart');
    
    function UploadFileSuccess() {
        $("#divUpload").hide();
        $("#divProcess").show();
        $('#uploadType').hide();
    }

    function CancelUpload() {
        $("#divUpload").show();
        $("#divProcess").hide();
    }

    var configs = { 
        aditional:"",
        urlProcess: '<%:ResolveClientUrl("~/Configuracion/StartEtlProcess")%>' + '?etlType=0&',
        urlPercent: '<%:ResolveClientUrl("~/Configuracion/ProcesoPorcent")%>'+'?processId=',
        divProgress: "#EtlBarraProgreso",
        divProgressMessage: "#EtlProcessMessage",
        divError:"#alerta",
        errorMessage: "se encuentra iniciado, no se puede continuar",
        divButtonStart: '#btnUpload'
    };


    /*function OpenProcess(processId) {
        bootbox.confirm("El proceso: " + processId + " esta abierto, desea visualizarlo?", "Cancelar", "Si",
         function (result) {
             if (result) {
                 GetProcessPercent(configs, processId, 2000);
                 UploadFileSuccess();
             }
             else
                 bootbox.hideAll();
         });  
        
     }*/

    function StartUploadEtl() {
        $("#alerta").html("");
        var uploadType = $("#uploadType input[type='radio']:checked").val();

        configs.aditional = "complements=" + uploadType;

        ClearAllProgressBarComponent(configs);
        $(configs.divButtonStart).attr("disabled", "disabled");
        ProgressBarComponent(configs);
    }

    function UploadFileResult(tipo,mensaje,fileName) {

        if (tipo == 'success') {
            $("#fileName").val(fileName);
            UploadFileSuccess();
        }
        $("#alerta").html("<div class='alert alert-"+tipo+"'>" + mensaje + "</div>");
    }
    
    function radiobuttonFile() {
        $('#divUpload').show();
        $('#divProcess').hide();
    }

    function radiobuttonDb() {
        $('#divUpload').hide();
    }

</script>                     
                  <div id="alerta" style="font-weight: bold;">
                  
                  </div>

                 <h3>
                        Importar informaci&oacuten al sistema</h3>
                   <form id="uploadType">
<%--                      <label class="radio">
                          <input type="radio" name="optionsRadios" id="optionBd" value="baseDatos" onclick="radiobuttonDb();" />
                         Base de datos
                      </label>--%>
                      <label class="radio">
                          <input type="radio" name="optionsRadios" id="optionFile" value="file"  checked/>
                        Archivo
                      </label>
                  </form>
                   <div id="divUpload" >
                    <p>
                        Seleccione El archivo <b>EXCEL(*.xls)</b> generado a partir de la plantilla, para cargar
                        los datos en el sistema.</p>
                    <% using (Html.BeginForm("AltaInfo", "Configuracion", FormMethod.Post, new { enctype = "multipart/form-data", id="FormUpload" }))
                       {%>
                    <div id="formulario" class="form-inline">
                  <%--   <%if(Model != null){ %>
                                <!--div class="span12 pagination-right"-->
                                <label for="ComboBranchId">Sucursal: </label>
                                <%: Html.DropDownList("ComboBranchId", new SelectList((IEnumerable)Model, "branchId", "name", Model.ToString()), new { onchange = "UploadFileSeleccionaSucursal();"})%>
                                <!--legend></legend>  
                                </div-->
                             <%} %>--%>
                       <p></p>
                        <label for="file">
                            Archivo Excel:</label>
                        <input type="file" id="file" class="btn" name="file" style="background: #FDFCFC;
                            border: thin solid black; border-radius: 5px; margin-left: 3%;" placeholder="Seleccione el archivo xml con plantilla" />
                        
                        <button type="submit" class="btn btn-primary" style="margin-left: 3%;" onclick="ActiveLoading()">
                            <i class="icon-arrow-up icon-white"></i>Cargar</button>
                    </div>
                    <%} %>
                    </div>
                     
                    <div id="divProcess" style="display:none">
                     
                        <legend>Iniciar</legend>
                        <input type ="hidden" id="fileName"/>
                        <div class="progress progress-striped active">
                            <div id="EtlBarraProgreso" class="bar" style="width: 0%;">
                            </div>
                        </div>
                     <div id="EtlProcessMessage">
                    
                     </div>
                    <a class="btn btn-primary" id="btnUpload" href="#" onclick="StartUploadEtl();"><i class="icon-upload icon-white"></i>Iniciar Carga</a>
                    <!--a class="btn btn-primary" href="#" onclick="CancelUpload();"><i class="icon-download-alt icon-white"></i>Cancelar</a-->
                    </div>

                      <!--div id="alerta" style="font-weight: bold;">
                        
                      </div-->
                        <%  
                            var mensaje = "";
                            if (TempData["result"] != null)
                            {
                                mensaje = "<script>UploadFileResult('"+TempData["tipo"]+"','"+TempData["result"]+"','"+TempData["fileName"]+"');</script>";
                               
                            }
                            if (TempData["process"] != null) {
                                mensaje += "<script>OpenProcess('"+TempData["process"]+"')</script>";
                            }
                            Response.Write(mensaje);
                        %>
</asp:Content>