<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Cargar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

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

                 <h3>
                        Cargar informaci&oacuten de pedidos y ventas</h3>

                  
                    <p> Seleccione el archivo de texto <b>(*.txt) o (*.js)</b> generado en el dispositivo movil </p>
                    <% using (Html.BeginForm("CargarJson", "Configuracion", FormMethod.Post, new { enctype = "multipart/form-data", id = "FormUpload" }))
                       {%>
                    <div id="formulario" class="form-inline">
                       <p></p>
                        <label for="file">
                            Archivo:</label>
                        <input type="file" id="file" class="btn" name="file" style="background: #FDFCFC;
                            border: thin solid black; border-radius: 5px; margin-left: 3%;" placeholder="Seleccione el archivo xml con plantilla" />
                        
                        <button type="submit" class="btn btn-primary" style="margin-left: 3%;" onclick="ActiveLoading()">
                            <i class="icon-arrow-up icon-white"></i>Cargar</button>
                    </div>
                    <%} %>
                   <%  
                            var mensaje = "";
                            if (TempData["result"] != null)
                            {
                                mensaje = "<script>UploadFileResult('"+TempData["tipo"]+"',"+'"'+TempData["result"]+'"'+");</script>";
                               
                            }

                            Response.Write(mensaje);
                        %>
                     


</asp:Content>