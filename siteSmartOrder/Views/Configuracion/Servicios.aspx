<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Confirmar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GuardarConfiguracion() {

            var urlSmartOrder = $("#SmartOrderServer").val();
            var urlCredit = $("#CreditServer").val();
            var url = { urlSmartOrder: urlSmartOrder, urlCredit: urlCredit };
            
           //  url: "/Configuracion/GuardaUrl?url="+ url,
            if (url!= "") {
                $.ajax({
                    url: '<%:ResolveClientUrl("~/Configuracion/GuardaUrl")%>' + '?url=' + JSON.stringify(url),
                    success: function (data) {
                        alert(data);
                    }
                });
            }
        }
    </script>
    <h3>
        Servidor web</h3>

     <form class="form-horizontal">
        <div class="control-group">
            <label class="control-label" for="SmartOrderServer">
               Workbycloud</label>
            <div class="controls">
                <input type="text" class="span8" id="SmartOrderServer" value="<%:ConfigurationManager.AppSettings["SmartOrderServer"] %>" />
            </div>
        </div>
        <div class="control-group" style="display: none">
            <label class="control-label" for="CreditServer">
                Credit</label>
            <div class="controls">
                <input type="text" class="span8" id="CreditServer" value="<%:ConfigurationManager.AppSettings["CreditServer"] %>" />
            </div>
        </div>
        
         <div class="control-group">
            <div class="controls">
                <button type="button" class="btn btn-success" onclick="GuardarConfiguracion();">
                    Guardar
                    </button>

            </div>
        </div>
        </form>
</asp:Content>
