 <%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-3.3.6/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-table/css/bootstrap-table.css")%>" />
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/CustomerData/Content/css/Custom.css")%>" />
    
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/js/jquery-1.11.3.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-3.3.6/js/bootstrap.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-3.3.6/js/dropdown.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-3.3.6/js/alert.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-table/js/bootstrap-table.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/CustomerData/Content/bootstrap-table/js/locale/bootstrap-table-es-MX.js") %>"></script>

    <style type="text/css">
    .pagination ul>li>a:hover, .pagination ul>.active>a, .pagination ul>.active>span
    {
    	background-color:#337ab7
    }
    .pagination ul>.active>a, .pagination ul>.active>span
    {
    	color:White
    }
    </style>
<div class="container-fluid">
    <div class="col-md-9 offset2">
        
        <div class="row">
            <div class="modal fade in" id="ModalDeleteUser" role="dialog" tabindex="-1" aria-hidden="true" style="left:50%; height:450px" >
                <div class="modal-body">
                    <table id="ModalTable" 
                    data-search="true"
                    data-row-style="rowStyle">
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="pagination-right">
                <p  style="text-align: right">
                    <h5>Seleccione una sucursal</h5>
                </p>
                <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px; margin-bottom:10px" })%>
            </div>
            <legend></legend>
        </div>

        <div class="row">
            <ul class="nav nav-tabs">
                <li role="presentation" class="active">
                    <a href="#">Clientes Facturables</a>
                </li>
            </ul><br />
        </div>
        <div class="row">
            <div id="message"></div>
        </div>

        <div class="row">

            <div id="toolbar">
                <%: Html.DropDownList("Route", new SelectList(new List<string>()), new { style = "height:25px; width: 250px; margin-right:20px" })%>
                <button id="btnSave" class="btn btn-success btn-xs">Guardar Cambios</button>
            </div>

            <table id="table" 
            data-toolbar="#toolbar"
            data-search="true"
            data-pagination="true"
            data-side-pagination="client"
            data-page-number=1
            data-page-size=10
            data-row-style="rowStyle">
            </table>
        </div>
        
    </div>
</div>
    <script type="text/javascript">
        $(document).ready(function () {
            var _url = '<%:ResolveClientUrl("~/CustomerData/Branch/Get")%>';
            $.ajax({
                url: _url,
                success: function (data) {
                    if (data != null && data.length) {
                        branchSelected = data[0].BranchId;
                        SetBranches(data, branchSelected);
                        GetRoutes(branchSelected);
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
                }

            });

            $('#btnSave').click(function () {
                var data = $('#table').bootstrapTable('getData');
                var diferent = jQuery.grep(data, function (n, i) {
                    return n.Status != n.OldStatus;
                });

                if (diferent != null && diferent.length > 0) {
                    var json = JSON.stringify(diferent);
                    var _url = '<%:ResolveClientUrl("~/CustomerData/Customer/Set")%>';

                    $.ajax({
                        type: "POST",
                        url: _url,
                        data: json,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            RefreshTable($("#Route").val());
                            ShowMessage(data);
                        }
                    });
                }
            });

            $('#Branch').on('change', function (e) {
                var valueSelected = this.value;
                RefreshRoutes(valueSelected);
            });

            $('#Route').on('change', function (e) {
                var valueSelected = this.value;
                RefreshTable(valueSelected);
            });
        });

        function ShowMessage(data) {
            var message = "";
            if (data != null && data.length > 0) {
                message += '<div class="alert alert-danger alert-dismissible" role="alert">';
                message += '<button type="button" class="close" data-dismiss="alert" aria-label="Close">';
                message += '<span aria-hidden="true">&times;</span>';
                message += '</button>';
                message += 'Algunos clientes no se actualizaron!, pulsa ';
                message += '<button id="btnDetails" class="btn btn-danger btn-xs">aqui</button>';
                message += ' para ver los detalles.';
                message += '</div>';
                $('#message').html(message);
                $('#btnDetails').click(function () {
                    $('#ModalDeleteUser').on('show.bs.modal', function (e) {
                        BuildModalTable(data);
                    });
                    $('#ModalDeleteUser').modal('show');

                });
            }
            else {
                message += '<div class="alert alert-success alert-dismissible" role="alert">';
                message += '<button type="button" class="close" data-dismiss="alert" aria-label="Close">';
                message += '<span aria-hidden="true">&times;</span>';
                message += '</button>';
                message += 'Los cambios se realizaron con exito!.';
                message += '</div>';
                $('#message').html(message);
            }
        }

        function BuildModalTable(data) {
            $('#ModalTable').bootstrapTable({
                height: 300,
                data: data,
                columns: [{
                    field: 'Id',
                    title: 'Id',
                    visible: false
                }, {
                    field: 'Code',
                    title: 'CUC'
                }, {
                    field: 'Message',
                    title: 'Mensage'
                }]
            });

        }

        function GetRoutes(branchSelected) {
            var _url = '<%:ResolveClientUrl("~/CustomerData/Route/Get")%>' + '?branchId='+branchSelected;
            $.ajax({
                url: _url,
                success: function (data) {
                    if (data != null && data.length > 0) {
                        routeSelected = data[0].Id;
                        SetRoutes(data, routeSelected);
                        SetTable(routeSelected);
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
                }
            });
        }

        function SetRoutes(data, routeSelected) {
            $.each(data, function (key, value) {
                $('#Route').append($("<option></option>")
                    .attr("value", value.Id)
                    .text(value.Name));
            });
            $('#Route').val(routeSelected);
        }

        function SetBranches(data, branchSelected) {
            $.each(data, function (key, value) {
                $('#Branch').append($("<option></option>")
                    .attr("value", value.BranchId)
                    .text(value.Name));
            });
            $('#Branch').val(branchSelected);
        }

        function RefreshRoutes(branchSelected) {
            $('#Route').find('option').remove().end();
            GetRoutes(branchSelected);
        }

        function RefreshTable(routeSelected) {
            var _urlCustomers = '<%:ResolveClientUrl("~/CustomerData/Customer/Get")%>' + '?routeId=' + routeSelected; ;
            $('#table').bootstrapTable('refresh', { url: _urlCustomers });
        }

        function SetTable(routeSelected) {
            var urlService = '<%:ResolveClientUrl("~/CustomerData/Customer/Get")%>' + '?routeId=' + routeSelected;
            $('#table').bootstrapTable({
                height: getHeight(),
                url: urlService,
                columns: [{
                    field: 'Id',
                    title: 'Id',
                    visible:false
                }, {
                    field: 'Code',
                    title: 'CUC'
                }, {
                    field: 'Name',
                    title: 'Nombre'
                }, {
                    field: 'Ftr',
                    title: 'RFC'
                }, {
                    field: 'BusinessName',
                    title: 'Razon Social'
                }, {
                    field: 'FiscalAddress',
                    title: 'Direccion Fiscal'
                }, {
                    field: 'HelpStatus',
                    title: '',
                    checkbox:true,
                    formatter:stateFormatter
                    
                }, {
                    field: 'Status',
                    title: '',
                    visible:false
                }, {
                    field: 'OldStatus',
                    title: '',
                    visible: false
                }]
            });
        }

        $('#table').on('check.bs.table', function (e, row, fn) {
            row.Status = true;
        });

        $('#table').on('uncheck.bs.table', function (e, row, fn) {
            row.Status = false;
        });

        $('#table').on('check-all.bs.table', function (e, rows) {
            $.each(rows, function (index, element) {
                element.Status = true;
            });
        });

        $('#table').on('uncheck-all.bs.table', function (e, rows) {
            $.each(rows, function (index, element) {
                element.Status = false;
            });
        });

        function getHeight() {
            return 640;
        }

        function rowStyle(row, index) {
            return {
                css: { "font-size": "11px" }
            };
        }

        function stateFormatter(value, row, index) {
            return row.Status;
        }
    </script>

</asp:Content>