<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Licencia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function RegistrarLicencia() {
        var key = $("#key").val();
        $.ajax({
            url: '<%:ResolveClientUrl("~/Configuracion/RegistrarLicencia")%>' + '?key=' + key,
            success: function (data) {
                var response = $.parseJSON(data);
                if (response.IsSuccess) {
                    $("#divMessageLicense").html("<div class='alert alert-success'>Registro exitoso</div>");
                    MostrarRegistro(response.Data);
                    $("#key").val("");
                } else {
                    $("#divMessageLicense").html("<div class='alert alert-error'>" + response.Message + "</div>");
                }
            }
        });
    }

    function MostrarRegistro(descripcion) {
   
        var parts = descripcion.split("-");
        var html = "<p>Aplicacion:" + parts[0] + "</p><p>Version:" + parts[1] + "</p><p>Total:" + parts[3] +
        "</p><p>Fecha de compra:" + parts[4] + "</p><p>Dispositivos:" + parts[6] + "</p>";
        $("#descripcion").html(html);
    }

    function MostrarError(Mensaje) {
        $("#divMessageLicense").html("<div class='alert alert-error'>" + Mensaje + "</div>");
    }

    
</script>
   <form>
        <legend>Licencia</legend>    
        <!--label class="control-label">Key </label-->  
        <textarea rows="2" class="field span6" id="key"></textarea>        
        <button type="button" class="btn btn-success" onclick="RegistrarLicencia();">Activar</button>  
   </form>
   <div id="divMessageLicense" style="font-weight: bold;">
                        
   </div>
   <div class="hero-unit" id="descripcion">
   </div>
    <%if (Model != null)
      {
          var mensaje = "";
          if (Model.IsSuccess)
          {
              mensaje = "<script>MostrarRegistro('" + Model.Data + "')</script>";
          }
          else {
              mensaje = "<script>MostrarError('" + Model.Message + "')</script>";
          }
          Response.Write(mensaje);
      }
      else { %>
        <div class='alert alert-error'><p>Ocurrio un error desconocido</p></div>
     <% } %>
      
</asp:Content>
