<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Referencias de entrega   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formExcel" runat="server">
        <input type="file" id="excel" />
    
    </form>
    <button id="enviar">Importar archivo</button>
    <div>
        <table id="Resultados">
            <thead></thead>
            <tbody></tbody>
        </table>
    </div>
<script type="text/javascript">

    $("#enviar").click(function (e) {
        e.preventDefault();
        var input = $("#excel");
        var archivo = input.files[0];
        var formData = new FormData();
        data.append('excel', archivo);
        $.ajax({
            url: '<%:ResolveClientUrl("~/Kilometraje/ImportarExcel")%>',
            type: 'POST',
            data: formData,
            success: function (data) {
                if ($.fn.DataTable.isDataTable('#Resultados')) {
                    $('#Resultados').DataTable().destroy();
                }
                cargarTabla(data);
            }
        });

    });

    function cargarTabla(jsonObject) {
        table = $('#Resultados').DataTable({
            "aaData": jsonObject,
            "columns": [
                { title: "BRANCH", "data": "BranchId" },
                { title: "ROUTE", "data": "RouteId" },
                { title: "TRAVELS", "data": "Travels" },
                { title: "MONDAY", "data": "Monday" },
                { title: "TUESDAY", "data": "Tuesday" },
                { title: "WENDESDAY", "data": "Wednesday" },
                { title: "THURSDAY", "data": "Thursday" },
                { title: "FRIDAY", "data": "Friday" },
                { title: "SATURDAY", "data": "Saturday" },
                { title: "SUNDAY", "data": "Sunday" },
            ],
            "initComplete": function (settings, json) {
                //IntranetUtils.ocultarCargador();
                //loading.hideloading();
            }
        });
    }

</script>
</asp:Content>
