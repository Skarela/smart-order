<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Descargar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $('body').bind('ajaxStart');
        var configs = {
            aditional: "",
            urlProcess: '<%:ResolveClientUrl("~/Configuracion/StartEtlProcess")%>' + '?etlType=1&',
            urlPercent: '<%:ResolveClientUrl("~/Configuracion/ProcesoPorcent")%>' + '?processId=',
            divProgress: "#EtlBarraProgreso",
            divProgressMessage: "#EtlProcessMessage",
            divError: "#alerta",
            errorMessage: "se encuentra iniciado, no se puede continuar",
            divButtonStart: '#btnDownload'
        };
        
        function StartDownloadEtl() {
            $("#alerta").html("");
            var downloadType = $("#downlodType input[type='radio']:checked").val();
            configs.aditional = "complements=" + downloadType;
            ClearAllProgressBarComponent(configs);
            $(configs.divButtonStart).attr("disabled", "disabled");
            ProgressBarComponent(configs);
        }

    </script>
    <div id="alerta" style="font-weight: bold;">
                  
                  </div>

                 <h3>
                        Descargar informaci&oacuten del sistema</h3>
                  <form id="downlodType">
                      <label class="radio">
                          <input type="radio" name="optionsRadios" id="optionComplete" value="1"  checked/>
                         Descarga completa
                      </label>
                      <label class="radio">
                          <input type="radio" name="optionsRadios" id="optionPartial" value="0" />
                        Descarga actualizaciones
                      </label>
                  </form>
                    <div id="divProcess" >
                     
                        <legend>Iniciar</legend>
                        <input type ="hidden" id="fileName"/>
                        <div class="progress progress-striped active">
                            <div id="EtlBarraProgreso" class="bar" style="width: 0%;">
                            </div>
                        </div>
                     <div id="EtlProcessMessage">
                    
                     </div>
                    <a class="btn btn-primary" id="btnDownload" href="#" onclick="StartDownloadEtl();"><i class="icon-download-alt icon-white"></i>Iniciar Descarga</a>
                    <!--a class="btn btn-primary" href="#" onclick="CancelUpload();"><i class="icon-download-alt icon-white"></i>Cancelar</a-->
                    </div>

                     

</asp:Content>

