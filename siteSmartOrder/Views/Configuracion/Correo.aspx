<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Exportar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function ValidaAutentificacion() {
            if ($("#autentificacion").is(":checked")) {
                $("#divUsuario").show();
                $("#divPassword").show();
            } else {
                $("#divUsuario").hide();
                $("#divPassword").hide();
            }
        }

        function GuardarConfiguracionCorreo() {
            var Id = $("#Id").val();
            var smtp = $("#Smtp").val();
            var sender = $("#Sender").val();
            var port = $("#Port").val();
            var security = $("#Security").val() == "ssl" ? true : false;
            var autentication = $("#autentificacion").is(":checked");
            var user = $("#User").val();
            var password = $("#Password").val();
            var setting = { SmtpSettingId: Id, Smtp: smtp, Sender: sender, Port: port, Ssl: security, Autentication: autentication, UserCredential: user, Password: password };
            $.ajax({
                url: '<%:ResolveClientUrl("~/Configuracion/ChangeServerSmtpSettings")%>'+'?setting=' + JSON.stringify(setting),
                success: function (data) {
                    var response = $.parseJSON(data.Data);
                    if (response.IsSuccess) {
                        $("#divMessageCorreo").html("<div class='alert alert-success'>" + response.Message + "</div>");
                    } else {
                        $("#divMessageCorreo").html("<div class='alert alert-error'>" + response.Message + "</div>");
                    }
                    
                }
            }); 

        }
    </script>

    <div class="row" style="padding-left: 30px">
        <h4>
            Datos de Configuraci&oacute;n</h4>
        <p>Configura la informaci&oacute;n del servidor de correo.</p>
        <br />
        <div id="divMessageCorreo" style="font-weight: bold;">
                        
        </div>
        <form class="form-horizontal">
        <input type="hidden" id="Id" value="<%:Model==null?"0":Model.SmtpSettingId %>"/>
        <div class="control-group">
            <label class="control-label" for="Smtp">
                Servidor SMTP</label>
            <div class="controls">
                <input type="text" id="Smtp" value="<%:Model==null?"":Model.Smtp %>"/>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="Sender">
                Remitente</label>
            <div class="controls">
                <input type="text" id="Sender" value="<%:Model==null?"":Model.Sender %>"/>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="Port">
                Puerto</label>
            <div class="controls">
                <input type="text" id="Port" value="<%:Model==null?"":Model.Port %>"/>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="Security">
                Seguridad</label>
            <div class="controls">
                <select id="Security">
                    <option value="ninguna">Ninguna</option>
                    <option value="ssl"  <%:Model!=null?Model.Ssl==true?"Selected":"":"" %>>SSL</option>
                </select>
            </div>
        </div>
         <div class="control-group">
            <label class="control-label" for="autentificacion">
                Autentificacion</label>
            <div class="controls">
                <input type="checkbox" id="autentificacion" <%:Model!=null?Model.Autentication==true?"Checked":"":"" %> onclick="ValidaAutentificacion();"/>
            </div>
        </div>
        <div class="control-group" <%:Model!=null?Model.Autentication==false?"style=display:none":"":"style=display:none" %> id="divUsuario">
            <label class="control-label" for="User">
                Usuario</label>
            <div class="controls">
                <input type="text" id="User" value="<%:Model!=null?Model.Autentication==false?"":Model.UserCredential:"" %>" />
            </div>
        </div>
        <div class="control-group" <%:Model!=null?Model.Autentication==false?"style=display:none":"":"style=display:none" %> id="divPassword">
            <label class="control-label" for="Password">
                Password</label>
            <div class="controls">
                <input type="password" id="Password" value="<%:Model!=null?Model.Autentication==false?"":Model.Password:"" %>" />
            </div>
        </div>        
        
         <div class="control-group">
            <div class="controls">
                <button type="button" class="btn btn-success" onclick="GuardarConfiguracionCorreo()">
                    Guardar</button>

            </div>
        </div>
        </form>
    </div>
</asp:Content>
