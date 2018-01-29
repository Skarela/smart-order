<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Configuración
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function UploadFileSeleccionaSucursal() {
            var branchId = $('#ComboBranchId').val();
            $("#uploadBranch").val(branchId);
        }

    </script>
    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2" style="overflow-x: hidden;">
            <ul class="nav nav-tabs" id="tabconfig">
               <%-- <li <%:TempData["tabName"].ToString()=="Importar"?"class=active":"" %>><a href="#importar" id="importarTab">Importar</a></li>
                <li <%:TempData["tabName"].ToString()=="Descargar"?"class=active":"" %>><a href="#descargar" id="A1">Descargar</a></li>--%>
                <li <%:TempData["tabName"].ToString()=="Cuenta"?"class=active":"" %>><a href="#cuenta" id="cuentaTab">Cuenta</a></li>
                <li <%:TempData["tabName"].ToString()=="Correo"?"class=active":"" %>><a href="#correo" id="correoTab">Correo</a></li>
                <li <%:TempData["tabName"].ToString()=="Servicios"?"class=active":"" %>><a href="#servicios" id="serviciosTab">Servicios</a></li>
                <li <%:TempData["tabName"].ToString()=="Kilometraje"?"class=active":"" %>><a href="#Kilometraje" id="kilometrajeTab">Kilometraje</a></li>
                <li <%:TempData["tabName"].ToString()=="Cargar"?"class=active":"" %>><a href="#Cargar" id="cargarTab">Cargar</a></li>
                <li <%:TempData["tabName"].ToString()=="Cierre"?"class=active":"" %>><a href="#Cierre" id="cierreTab">Cierre</a></li>

            </ul>
            <div class="tab-content">
                <div id="<%:TempData["tabName"].ToString().ToLower()%>" class="tab-pane active">
                    <%: Html.Action(TempData["tabName"].ToString(), "Configuracion", new { branches = Model })%>
                </div>
               
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('#tabconfig a').click(function (e) {
            e.preventDefault(); $(this).tab('show');
            var tabName = $(this).html();
            window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>' + '?tab=' + tabName;
        }) 
        </script>
</asp:Content>
