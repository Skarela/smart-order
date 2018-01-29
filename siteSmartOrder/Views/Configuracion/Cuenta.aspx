<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Importar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function CreaTabla() {
        var tags = ["User_Name", "score"];
//        var titulos = ['<i class="icon-user"></i>Nombre', '<i class="icon-signal"></i>Titulo2'];
        var valores = [
            { "User_Name": "John Doe", "score": "10", "team": "1" },
            { "User_Name": "Jane Smith", "score": "15", "team": "2" },
            { "User_Name": "Chuck Berry", "score": "12", "team": "2" }
        ];

        var tabla = $('#tabla');
//        var $thead = $('<thead></thead>');
//        var $tr = $('<tr></tr>');
//        
//        $thead.append($tr);
//        for (var i = 0; i < titulos.length; i++) {
//            var $linea = $('<th  style="text-align:center"></th>').html(titulos[i]);
//            $tr.append($linea);
//        }
//        tabla.append($thead);
        $.each(valores, function () {
                var $linea2 = $('<tr></tr>');
                for (var ii = 0; ii < tags.length; ii++) {
                    $linea2.append($('<td style="text-align:center"></td>').html(this[tags[ii]]));
                }
                tabla.append($linea2);
        });
       
    }
    function AbrePopUp() {
        $('#imagen').html("<img src='/Content/QRCobratario.ashx?userCode=2'/>");
        $('#myModal').modal('show');
    }

    function CambiarPassword() {
     
        var valor = $("#contrasena").val();
        if (valor.length == 0) {
            alert("Inserte un valor");
            return false;
        }

        var nuevoValor = hex_md5(valor).toUpperCase();
        $.ajax({
            url: '<%:ResolveClientUrl("~/Configuracion/ChangePassword")%>'+'?password=' + nuevoValor,
            success: function (data) {
                var response = $.parseJSON(data.Data);
                $("#contrasena").val("");
                if (response.IsSuccess) {
                    $("#divMessage").html("<div class='alert alert-success'>" + response.Message + "</div>");                  
                } else {
                    $("#divMessage").html("<div class='alert alert-error'>" + response.Message + "</div>"); 
                }
            }
        });
        
    }
</script>
     <h3>Usuario administrador del portal </h3>
                    
                    <div id="divMessage" style="font-weight: bold;">
                        
                    </div>
                    <form class="form-horizontal">
                        <div class="control-group">
                             <label class="control-label"  for="administrador">Usuario:</label>
                             <div class="controls">
                                <input type="text" value="<%:TempData["NickName"]%>" disabled/>
                             </div>
                        </div>
                        <div class="control-group">
                             <label class="control-label" for="contrasena">Contraseña:</label>
                             <div class="controls">
                                <input type="password" id="contrasena"/>
                               
                             </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <button type="button" class="btn btn-success" onclick="CambiarPassword();">Guardar</button>  
                              
                            </div>
                        </div>                                              
                               
                    </form>
                    
                  
</asp:Content>
