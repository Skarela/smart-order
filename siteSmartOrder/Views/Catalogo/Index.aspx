<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Catalogos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="http://jqwidgets.com/jquery-widgets-demo/jqwidgets/jqx-all.js"></script>
<link rel="stylesheet" type="text/css" href="http://jqwidgets.com/jquery-widgets-demo/jqwidgets/styles/jqx.base.css" />
 <% Html.RenderPartial("~/Areas/RoutePreparation/Views/Shared/_UrlActions.ascx"); %>
     
<script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>"></script>
<script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js")%>" defer></script>
    
<script type="text/javascript">
    
    $(function () {
        $('table').on('click', 'tr a.picLinks', function (e) {
//            e.preventDefault();
//            var url = '<%:ResolveUrl("~/Precio/Detalles?id=") %>';
//            var link = $(this).attr('href');
//            var split = link.split('/');
//            window.location = url + split[1];
        });
        var seleccioneSucursal = function () {
            var tabName = $("ul#tabconfig li.active a").attr( "rel" );
            TabsCatalogos.Clientes.abierto = false;
            TabsCatalogos.Productos.abierto = false;
            TabsCatalogos.Precios.abierto = false;
            TabsCatalogos.RoutePhones.abierto = false;
            $(TabsCatalogos[tabName].idPaginador + " ul").remove();
            TabsCatalogos[tabName].iniciaLista();
            TabsCatalogos[tabName].abierto = true;
        }

        filterBranchToUserOnSession(seleccioneSucursal);
    });
    
    var TabsCatalogos = {
        Clientes: {
            tags: ["code", "name", "contact", "routeName"],
            details:[],
            idContenedor: '#customerGrid',
            idPaginador: '#customerPagination',
            funcion: PaginaCliente, tipo: '<%:ResolveClientUrl("~/Cliente")%>', abierto: false, iniciaLista: function () { AplicarFiltroClientes(); }, idFilter: '#filterClientesDiv',
            idNotification: '#infoBoxClientes',
            /*dataFields: [
                            { name: "code", type: "string" },
                            { name: "name", type: "string" },
                            { name: "contact", type: "string" },
                            { name: "routeName", type: "string" }
                        ], idContent:"#gridCustomers",
            columns: [
                    { text: "Codigo", datafield: "code" },
                    { text: "Nombre", datafield: "name" },
                    { text: "Contacto", datafield: "contact" },
                    { text: "Ruta", datafield: "routeName" }
                ]*/
        },
        Productos: {
            tags: ["code", "name", "brand"],
            details: [],
            idContenedor: '#productsGrid',
            idPaginador: '#productsPagination',
            funcion: PaginaProducto, tipo: '<%:ResolveClientUrl("~/Producto")%>', abierto: false, iniciaLista: function () { AplicarFiltroProductos(); }, idFilter: '#filterProductosDiv',
            idNotification: '#infoBoxProductos',
            /*dataFields: [
                            { name: "code", type: "string" },
                            { name: "name", type: "string" },
                            { name: "brand", type: "string" }
                        ], idContent: "#gridProducts",
            columns: [
                    { text: "Codigo", datafield: "code" },
                    { text: "Nombre", datafield: "name" },
                    { text: "Marca", datafield: "brand" }
                ]*/
        },
        Precios: {
            tags: ["code", "name", "master"],
            details: [['<%:ResolveUrl("~/Precio/Detalles") %>', '<i class="icon-eye-open"></i>', "priceListId"]],
            idContenedor: '#priceGrid',
            idPaginador: '#pricePagination',
            funcion: PaginaPrecio, tipo: '<%:ResolveClientUrl("~/Precio")%>', abierto: false, iniciaLista: function () { AplicarFiltroPrecios(); }, idFilter: '#filterPreciosDiv',
            idNotification: '#infoBoxPrecios'
        },
        RoutePhones: {
            tags: ["Code", "Name", "UserName","PhoneNumber"],
            details: [],
            idContenedor: '#routePhonesGrid',
            idPaginador: '#routePhonesPagination',
            funcion: PageRoutePhones, abierto: false, iniciaLista: function () { filterRoutePhones(); }, idFilter: '#filterRoutePhonesDiv',
            idNotification: '#infoBoxRoutePhones',
        }
    };

    function AplicarFiltroClientes() {
        var page = 1;
        PaginaCliente(page);
    }

    function AplicarFiltroProductos() {
        var page = 1;
        PaginaProducto(page);
    }

    function AplicarFiltroPrecios() {
        var page = 1;
        PaginaPrecio(page);
    }

    function filterRoutePhones() {
        var page = 1;
        PageRoutePhones(page);
    }

    function iniciar() {
        var branchId = $('#Branch').val();
        if (branchId > 0) {
            TabsCatalogos.Clientes.iniciaLista();
        }
    }

    function PaginaProducto(page) {
        var branchId = $('#Branch').val();
        var filter = $('#filtroProductos').val();
        if (branchId > 0) {
            var URL = '<%:ResolveClientUrl("~/Producto/Paginacion")%>'+ '?page=' + page + '&branchId=' + branchId+"&filter="+filter;
            ExecutePaging(URL, page, TabsCatalogos.Productos);
            //var URL = '<%:ResolveClientUrl("~/Producto/Paginacion_")%>' + '?page=' + page + '&branchId=' + branchId + "&filter=" + filter;
            //ExecutePaging_(URL, page, TabsCatalogos.Productos);
        }
    }

    function PageRoutePhones(page) {
        var itemsPerPage = 10;
        var startIndex = (page - 1) * itemsPerPage;
        var endIndex = startIndex + itemsPerPage;

        if (startIndex == 0)
        {
            startIndex = 1; //El Web Services "SurveyEngine" pide iniciar de 1
        }

        var data = {
            UserName: $("#filterRoutePhones_UserName").val(),
            Code: $("#filterRoutePhones_RouteCode").val(),
            BranchId: $("#Branch").val(),
            StartPage: startIndex,
            EndPage: endIndex,
            Sort: "ASC",
            SortBy: "Name"
        };

  
        var callback = function (response) {
            PrintTableWithFilter(TabsCatalogos.RoutePhones.tags, response.Records, TabsCatalogos.RoutePhones.idContenedor, TabsCatalogos.RoutePhones.details, TabsCatalogos.RoutePhones.idFilter, TabsCatalogos.RoutePhones.idNotification);
            var count = response.Count;
            if (count == 0)
                count = 1;
            var totalPages = Math.ceil(count / itemsPerPage)
            PrintPagination(page, totalPages, TabsCatalogos.RoutePhones);
        };
        getPerAjax(route_FilterByUser, data, callback);
    
    }
 

    function evaluateKeyPress(e, funcion) {
        if (e.keyCode == 13) {
            return eval(funcion);
        }
    }

 
    function PaginaPrecio(page) {
        var branchId = $('#Branch').val();
        var filter = $('#filtroPrecios').val();
        if (branchId > 0) {
            var URL = '<%:ResolveClientUrl("~/Precio/Paginacion")%>' + '?page=' + page + '&branchId=' + branchId + "&filter=" + filter;
            ExecutePaging(URL,page, TabsCatalogos.Precios);
        }
    }

    function PaginaCliente(page) {
        var branchId = $('#Branch').val();
        var filter = $('#filtroClientes').val();
        if (branchId > 0) {
            var URL = '<%:ResolveClientUrl("~/Cliente/Paginacion")%>' + '?page=' + page + "&branchId=" + branchId + "&filter=" + filter;
            ExecutePaging(URL, page, TabsCatalogos.Clientes);
            //var URL = '<%:ResolveClientUrl("~/Cliente/Paginacion_")%>' + '?page=' + page + "&branchId=" + branchId + "&filter=" + filter;
            //ExecutePaging_(URL, page, TabsCatalogos.Clientes);

        }
    }

    

    function ExecutePaging(url, page, config) {
        $.ajax({
            url: url,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSuccess) {
                    var datos = response.Data.Data;
                    var count = response.Data.DataCount;
                    if (count == 0)
                        count = 1;
                    PrintTableWithFilter(config.tags, datos, config.idContenedor, config.details, config.idFilter, config.idNotification);
                    PrintPagination(page, count, config);

                } else {
                    alert(response.Message);
                }

            }, error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
            }
        });
    }

    function ExecutePaging_(urlReq, page, config) {
        var source =
                    {
                        datatype: "json",
                        datafields: config.dataFields,
                        url: urlReq,
                        type: "GET"

                    };
        var dataAdapter = new $.jqx.dataAdapter(source);
        // initialize jqxGrid
        $(config.idContent).jqxGrid(
            {
                width: 850,
                source: dataAdapter,
                columns: config.columns,
                sortable: true,
                filterable: true,
                showfilterrow: true
            });
    }
</script>
         
        <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
          <%if(Model != null){ %>
          <div class="span12 pagination-right">
          <p>Seleccione una sucursal</p>
               <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px;" })%>
          <%--<%: Html.DropDownList("Branch", new SelectList((IEnumerable)Model, "branchId", "name", Model.ToString()), new { onchange = "seleccionaSucursal();" })%>--%>
          <legend></legend>  
          </div>
          <%} %>

           
            <ul class="nav nav-tabs" id="tabconfig" >
                <li class="active"><a href="#clientes" id="clientesTab" rel="Clientes" >Clientes</a></li>
                <li><a href="#productos" id="productosTab" rel="Productos">Productos</a></li>
                <li><a href="#precios" id="preciostab" rel="Precios">Precios</a></li>
                <li><a href="#telefonos" id="telefonostab" rel="RoutePhones">Tel&eacute;fonos</a></li>
            </ul>
            <div class="tab-content">
                <div id="clientes" class="tab-pane active">
                    <div class="alert alert-info" id="infoBoxClientes" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>  
                    <div id="filterClientesDiv">
                        <div class="form-inline" style="text-align:right">
                            <input class="input-large" id="filtroClientes" placeholder="C&oacute;digo o nombre del cliente" onkeypress="evaluateKeyPress(event,'AplicarFiltroClientes()')" type="text"/>
                            <button class="btn" type="button" onclick="AplicarFiltroClientes();"><i class="icon-search"></i></button>
                        </div>
                    </div>                 
                    <div id="customerGridContainer">
                          <table class="table  table-hover table-striped" id="customerGrid">
                            <thead>
                               <tr>
                                    <th style="text-align:center">C&oacute;digo</th>
                                    <th style="text-align:center"><i class="icon-user"></i>Nombre</th>
                                    <th style="text-align:center"><i class="icon-home"></i>Contacto</th>
                                    <th style="text-align:center"><i class="icon-wrench"></i>Ruta</th>
                               </tr>
                            </thead>
                          </table>
                    </div>
                    <div class="pagination-centered" id="customerPagination">
                    </div>
                    <div id="gridCustomers"></div>
                </div>
                
                <div id="productos" class="tab-pane">
                    <div class="alert alert-info" id="infoBoxProductos" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>   
                    <div id="filterProductosDiv">
                        <div class="form-inline" style="text-align:right">
                            <input class="input-large" id="filtroProductos" placeholder="C&oacute;digo, nombre o marca del producto" onkeypress="evaluateKeyPress(event,'AplicarFiltroProductos()')" type="text"/>
                            <button class="btn" type="button" onclick="AplicarFiltroProductos();"><i class="icon-search"></i></button>    
                        </div>

                    </div>            
                    <div id="productsGridContainer">
                         <table class="table  table-hover table-striped" id="productsGrid">
                            <thead>
                             <tr>
                                 <th style="text-align:center">C&oacute;digo</th>
                                 <th style="text-align:center"><i class="icon-folder-open"></i> Descripci&oacute;n</th>
                                 <th style="text-align:center"><i class="icon-tags"></i> Marca</th>                            
                             </tr>  
                            </thead>
                     </table>
                    </div>
                    <div class="pagination-centered" id="productsPagination">
                    </div>  
                    <div id="gridProducts"></div>                        
                </div>


                <div id="precios" class="tab-pane">
                     <div class="alert alert-info" id="infoBoxPrecios" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>   
                     <div id="filterPreciosDiv">
                        <div class="form-inline" style="text-align:right">
                            <input class="input-large"id="filtroPrecios" placeholder="C&oacute;digo o nombre de la lista" onkeypress="evaluateKeyPress(event,'AplicarFiltroPrecios()')" type="text"/>
                            <button class="btn" type="button" onclick="AplicarFiltroPrecios();"><i class="icon-search"></i></button>  
                        </div>
                     </div>
                     <div id="priceGridContainer">
                          <table class="table  table-hover table-striped" id="priceGrid">
                             <thead>
                               <tr>
                                    <th style="text-align:center">C&oacute;digo</th>
                                    <th style="text-align:center"><i class="icon-th-large"></i> Nombre</th>
                                    <th style="text-align:center"><i class=" icon-list-alt"></i>Tipo</th>
                                    <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                               </tr>
                            </thead>
                        </table>
                     </div>  
                     <div class="pagination-centered" id="pricePagination">
                     </div>
                </div>

                <div id="telefonos" class="tab-pane">
                    <div class="alert alert-info" id="infoBoxRoutePhones" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>   
                    <div id="filterRoutePhonesDiv">
                        <div class="form-inline" style="text-align:right">
                            <input type="text" class="input-large" id="filterRoutePhones_RouteCode" placeholder="C&oacute;digo de la ruta" onkeypress="evaluateKeyPress(event,'filterRoutePhones()')"/>
                            <input type="text" class="input-large" id="filterRoutePhones_UserName" placeholder="Nombre colaborador" onkeypress="evaluateKeyPress(event,'filterRoutePhones()')"/>
                            <button type="button" onclick="filterRoutePhones();" class="btn"><i class="icon-search"></i></button>
                        </div>
                        </div>            
                    <div id="routePhonesGridContainer">
                         <table class="table  table-hover " id="routePhonesGrid">
                            <thead>
                             <tr>
                                 <th style="text-align:center">C&oacute;digo de ruta</th>
                                 <th style="text-align:center"><i class="icon-wrench"></i>Nombre de la ruta</th> 
                                 <th style="text-align:center"><i class="icon-user"></i>Nombre del colaborador</th>
                                 <th style="text-align:center"><i class="icon-home"></i>Telefono</th>
                             </tr>  
                            </thead>
                     </table>
                    </div>
                    <div class="pagination-centered" id="routePhonesPagination">
                    </div>                           
                </div>
            </div>
             <script type="text/javascript">iniciar();</script>
        </div>
    </div>
      
            <script type="text/javascript">

                $('#tabconfig a').click(function (e) {
                    e.preventDefault();
                    $(this).tab('show');
                    var tabName = $(this).attr( "rel" );
                    var branchId = $('#Branch').val();
                    if (branchId > 0) {
                        if (!TabsCatalogos[tabName].abierto) {
                            $(TabsCatalogos[tabName].idPaginador + " ul").remove();
                            TabsCatalogos[tabName].iniciaLista();
                            TabsCatalogos[tabName].abierto = true;
                        } 
                    }
                });
            </script>

</asp:Content>
