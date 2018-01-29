<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    function Initialize() {
        GetReport();
    }

    function GetReport() {
        var url = '<%:ResolveClientUrl("~/Reporte/GetReport")%>';
        $.ajax({
            url: url,
            success: function (data) {
                setDataReport(data);
            }, error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
            }
        });
    }

    function setDataReport(data) {
        var entrada = $("#entrada ul");
        var salida = $("#salida ul");
        for (var i = 0; i < data.length; i++) {
            var reportId = data[i].reportId;
            var type = data[i].type
            if (type == 1)
                entrada.append('<li><a href="<%:ResolveClientUrl("~/Reports/Report.aspx")%>?reportId=' + reportId + '" target="_blank">' + data[i].name + '</a></li>');
            else if (type == 2)
                salida.append('<li><a href="<%:ResolveClientUrl("~/Reports/Report.aspx")%>?reportId=' + reportId + '" target="_blank">' + data[i].name + '</a></li>');
            //lista.append("<li>" + data[i].reportId + "</li>");
        }

    }
</script>

   <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a href="#entrada" id="entradaTab">Informaci&oacute;n de Entrada</a></li>
                <li><a href="#salida" id="salidaTab">Informaci&oacute;n de Salida</a></li>
            </ul>
            <div class="tab-content">
                <div id="entrada" class="tab-pane active">
                 <!--%: Html.Action("Pedidos", "Reporte")%-->
                     <ul>
                     <!--<li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateDeliveryReport.aspx")%>" target="_blank">Concentrado de Ventas basadas en Entregas</a></li>                 -->
                       <!-- <li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateOrdersReport.aspx")%>" target="_blank">Concentrado de Pedidos</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateSalesReport.aspx")%>" target="_blank">Concentrado de Ventas</a></li>
                    
                        <li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateProductivity.aspx")%>" target="_blank">Concentrado de Productividad</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/CustomerScannedReport.aspx")%>" target="_blank">Concentrado de Clientes Visitados</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/CustomerBinnacleFailed.aspx")%>" target="_blank">Concentrado de Clientes no Visitados</a></li>-->

                        <!--Recien comentado<li><a href="<%:ResolveClientUrl("~/Reports/StockProduct.aspx")%>" target="_blank">Inventario</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/PriceList.aspx")%>" target="_blank">Lista de Precios</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/StockBasedDelivery.aspx")%>" target="_blank">Productos a Entregar</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/WorkLoadUser.aspx")%>" target="_blank">Carga de Trabajo por Usuario</a></li>-->

                    </ul>
                </div>

                 <div id="salida" class="tab-pane">
                     <ul>
                        <!--Recien comentado<li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateSalesReport.aspx")%>" target="_blank">Concentrado de Ventas</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateOrdersReport.aspx")%>" target="_blank">Concentrado de Pedidos</a></li>-->
                        <!--<li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateDeliveryReport.aspx")%>" target="_blank">Concentrado de Ventas basadas en Entregas</a></li>                 -->
                        <!--Recien comentado<li><a href="<%:ResolveClientUrl("~/Reports/ConcentrateProductivity.aspx")%>" target="_blank">Concentrado de Productividad</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/CustomerScannedReport.aspx")%>" target="_blank">Concentrado de Clientes Visitados</a></li>
                        <li><a href="<%:ResolveClientUrl("~/Reports/CustomerBinnacleFailed.aspx")%>" target="_blank">Concentrado de Clientes no Visitados</a></li>-->

                    </ul>
                </div>
               
            </div>
        </div>
    </div>
      
    <script type="text/javascript">
        $('#tabconfig a').click(function (e) {
            e.preventDefault(); $(this).tab('show');
        });
        Initialize();</script>

</asp:Content>
