<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-table/css/bootstrap-table.css")%>" />
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/css/Custom.css")%>" />
    
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/js/jquery-1.11.3.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/bootstrap.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/dropdown.js") %>"></script>
    <!--<script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/modal.js") %>"></script>-->
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-table/js/bootstrap-table.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-table/js/locale/bootstrap-table-es-MX.js") %>"></script>

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
    <input id="BranchId" name="BranchId" type="hidden" value="<%:ViewBag.BranchId %>" />
    <div class="col-md-9 offset2">
        
        <div class="row">
            <div class="modal fade in" id="ModalDeleteUser" role="dialog" tabindex="-1" aria-hidden="true" style="left:50%; height:140px" >
                <div class="modal-body">
                    <h4>¿Está seguro que desea eliminar este usuario?</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button id="bntConfirmDelete" type="button" class="btn btn-danger">Eliminar</button>
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
                    <a href="#">Usuarios</a>
                </li>
                <!--<li>
                    <a id="tab_new" href="#">Nuevo Usuario</a>                    
                </li>-->
            </ul><br />
        </div>

        <div class="row">
            <h3><b>Usuarios</b></h3>
        </div>

        <div class="row">

            <div id="toolbar">
                <button id="add" class="btn btn-success btn-xs">
                    <i class="glyphicon glyphicon-plus"></i> Nuevo Usuario
                </button>
            </div>

            <table id="table" 
            data-toolbar="#toolbar"
            data-search="true"
            data-pagination="true"
            data-side-pagination="client"
            data-page-number=1
            data-page-size=6
            data-detail-view="true"
            data-detail-formatter="detailFormatter">
            </table>
        </div>
    </div>



</div>
    <script type="text/javascript">
        $(document).ready(function () {
            var _url = '<%:ResolveClientUrl("~/NoticeRecharge/Branch/Get")%>';
            $.ajax({
                url: _url,
                success: function (data) {
                    var branchSelected = $('#BranchId').val();
                    if (branchSelected == 0 && data != null)
                        branchSelected = data[0].BranchId;
                    SetBranches(data, branchSelected);
                    SetTabUser(branchSelected);
                    SetTable(branchSelected);
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
                }
            });
            
            $('#Branch').on('change', function (e) {
                var valueSelected = this.value;
                RefreshTable(valueSelected)
                SetTabUser(valueSelected);
            });
        });

        function SetTabUser(branchSelected) {
            $('#add').click(function () {
                window.location = '<%:ResolveClientUrl("~/NoticeRecharge/User/New")%>' + '?branchId=' + branchSelected;
            });

            //var _tab_url = '<%:ResolveClientUrl("~/NoticeRecharge/User/New")%>' + '?branchId=' + branchSelected;
            //$('#tab_new').attr('href', _tab_url);
        }

        function SetBranches(data, branchSelected) {
            $.each(data, function (key, value) {
                $('#Branch').append($("<option></option>")
                    .attr("value", value.BranchId)
                    .text(value.Name));
            });
            $('#Branch').val(branchSelected);
        }

        function RefreshTable(branchSelected) {
            var _urlUsers = '<%:ResolveClientUrl("~/NoticeRecharge/User/GetUsers")%>' + '?branchId=' + branchSelected;
            $('#table').bootstrapTable('refresh', { url: _urlUsers });
        }

        function SetTable(branchSelected) {
            var urlService = '<%:ResolveClientUrl("~/NoticeRecharge/User/GetUsers")%>' + '?branchId=' + branchSelected;
            $('#table').bootstrapTable({
                height: getHeight(),
                url: urlService,
                columns: [{
                    field: 'Id',
                    title: 'Id'
                }, {
                    field: 'Name',
                    title: 'Nombre'
                }, {
                    field: 'Mail',
                    title: 'Correo',
                    formatter: mailFormatter
                }, {
                    field: 'PhoneNumber',
                    title: 'Telefono',
                    formatter: phoneNumberFormatter
                }, {
                    field: 'operate',
                    title: '',
                    align: 'center',
                    events: operateEvents,
                    formatter: operateFormatter
                }]
            });
        }

        function mailFormatter(value, row, index) {
            var iconName="";
            if (row.MailEnabled)
                iconName = "<i class='glyphicon glyphicon-ok pull-right' style='font-size: 9px' title='Activo'></i>";
            else
                iconName = "<i class='glyphicon glyphicon-ban-circle pull-right' style='font-size: 9px' title='Inactivo'></i>";

            return [row.Mail, iconName].join('');
        }
        
        function phoneNumberFormatter(value, row, index) {
            var iconName = "";
            if (row.PhoneNumberEnabled)
                iconName = "<i class='glyphicon glyphicon-ok pull-right iconsize' style='font-size: 9px' title='Activo'></i>";
            else
                iconName = "<i class='glyphicon glyphicon-ban-circle pull-right iconsize' style='font-size: 9px' title='Inactivo'></i>";

            return [row.PhoneNumber, iconName].join('');
        }

        function operateFormatter(value, row, index) {
            return [
            '<a class="edit" href="javascript:void(0)" title="Editar">',
            '<i class="glyphicon glyphicon-edit" style="font-size: 13px; margin-right: 5px"></i>',
            '</a>  ',
            '<a class="remove" href="javascript:void(0)" title="Eliminar">',
            '<i class="glyphicon glyphicon-remove" style="font-size: 13px; margin-left: 5px"></i>',
            '</a>'
        ].join('');
        }

        function detailFormatter(index, row) 
        {           
            var routesCodeName = [];
            $.each(row.Routes, function (index, route) {
                routesCodeName.push('<button type="button" class="btn btn-info btn-xs" style="margin-bottom: 5px">' + route.Name + '</button>');
            });
            return '<p><h5>Total de rutas asignadas: ' + routesCodeName.length + '</h5></p> <p>' + routesCodeName.join(' ') + '</p>';
        }

        window.operateEvents = {
            'click .edit': function (e, value, row, index) {
                var branchId = $('#Branch').val();
                //var branchName = $("#Branch option:selected").text();
                window.location = '<%:ResolveClientUrl("~/NoticeRecharge/User/Edit")%>' + '?branchId=' + branchId + '&userId=' + row.Id;

            },
            'click .remove': function (e, value, row, index) {
                $('#ModalDeleteUser').modal('show');
                $('#bntConfirmDelete').click(function () {
                    var branchId = $('#Branch').val();
                    window.location = '<%:ResolveClientUrl("~/NoticeRecharge/User/Deactivate")%>' + '?branchId=' + branchId + '&userId=' + row.Id;
                });
                //alert('You click delet action, row: ' + JSON.stringify(row));
            }
        };

        function getHeight() {
            return 450;
        }
    
    </script>

</asp:Content>
