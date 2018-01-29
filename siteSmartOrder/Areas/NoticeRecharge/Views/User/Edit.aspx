<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.NoticeRecharge.Models.User>" %>
<%@ Import Namespace="siteSmartOrder.Areas.NoticeRecharge.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/css/bootstrap.min.css")%>" />
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/selectize/css/selectize.css")%>" />
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/css/Custom.css")%>" />
    <link rel="Stylesheet" href="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-validator/css/bootstrapValidator.min.css")%>" />

    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/js/jquery-1.11.3.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/bootstrap.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/dropdown.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/selectize/js/selectize.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-validator/js/bootstrapValidator.min.js") %>"></script>
    <script type="text/jscript" src="<%:Url.Content("~/Areas/NoticeRecharge/Content/bootstrap-3.3.6/js/bootstrap-phone.min.js") %>"></script>

<div class="container-fluid">
    <input id="_BranchId" name="_BranchId" type="hidden" value="<%:ViewBag._BranchId %>" />

    <div class="col-md-9 offset2">
        <div class="row">
            <div class="pagination-right">
                <p  style="text-align: right">
                    <h5>Seleccione una sucursal</h5>
                </p>
                <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px; margin-bottom:10px", disabled="disabled" })%>
            </div>
            <legend></legend>
        </div>

        <div class="row">
            <ul class="nav nav-tabs">
                <li >
                    <a id="tab_index" href="#">Usuarios</a>
                </li>
                <!--<li >
                    <a id="tab_new" href="#">Nuevo Usuario</a>
                </li>-->
                <li role="presentation" class="active">
                    <a href="#">Editar Usuario</a>
                </li>
            </ul>
            <br />
        </div>

        <div class="row">
            <h3><b>Editar Usuario</b></h3>
        </div>

        <div class="row">
            <div class="panel-title">
                Informacion General
            </div>
        </div>
        <div class="row">
            <div class="tab-pane active">
                    
                    <% using (Html.BeginForm("Update", "User", FormMethod.Post, new { @class = "form-horizontal", id = "Form", enctype = "multipart/form.data" }))%>
                    <%   {%>
                            <div class="panel-body">
                                <div class="form-group">
                                    <%:Html.LabelFor(model => model.Name, "Nombre", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-8">
                                        <%:Html.TextBoxFor(model => model.Name, new { @class = "form-control", placeholder = "Nombre", type = "text", 
                                        autocomplete="off", style="height:34px"})%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <%:Html.LabelFor(model => model.Mail, "Correo", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-8">
                                        <%:Html.TextBoxFor(model => model.Mail, new { @class = "form-control", placeholder = "ejemplo@mail", type = "email", 
                                        autocomplete = "off", style="height:34px" })%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <%:Html.LabelFor(model => model.PhoneNumber, "Telefono", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-8">
                                        <%:Html.TextBoxFor(model => model.PhoneNumber, new { @class = "form-control bfh-phone", data_format = "(ddd) ddd-dd-dd", 
                                    placeholder = "(999) 199 99 99", autocomplete = "off", style="height:34px" })%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <%:Html.LabelFor(model => model.MailEnabled, "Alerta Correo", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-2">
                                        <%:Html.CheckBoxFor(model => model.MailEnabled, new { @class = "form-control"})%>
                                    </div>
                               
                                    <%:Html.LabelFor(model => model.PhoneNumberEnabled, "Alerta Telefono", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-2">
                                        <%:Html.CheckBoxFor(model => model.PhoneNumberEnabled, new { @class = "form-control"})%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <%:Html.LabelFor(model => model.RoutesIds, "Rutas", new { @class = "col-sm-2 control-label" })%>
                                    <div class="col-sm-8">
                                        <%
                                            
                                            
                             var routesList = Model.RoutesIds.Select(r => new SelectListItem
                             {
                                 Value = r.ToString(),
                                 Text = Model.Routes.FirstOrDefault(R => R.Id == r).Code + "-" + Model.Routes.FirstOrDefault(R => R.Id == r).Name
                             }); %>
                                        <%:Html.DropDownListFor(model => model.RoutesIds, routesList, new { multiple = "multiple", id = "dpRoute" })%>
                                    </div>
                                </div>

                                <%: Html.HiddenFor(model => model.BranchId)%>
                                <%: Html.HiddenFor(model => model.Id)%>

                            </div>
                            <button type="submit" class="btn btn-success">Editar</button>
                            <a id="btn_cancel" class="btn btn-default" href="#">Cancel</a>
                    <%} %>
            </div>
        <br />
        </div>
            
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        var $dpRoute = $('#dpRoute').selectize({
            persist: false,
            createOnBlur: true,
            create: true
        });

        var _url = '<%:ResolveClientUrl("~/NoticeRecharge/Branch/Get")%>';
        $.ajax({
            url: _url,
            success: function (data) {
                var branchSelected = $('#_BranchId').val();
                if (branchSelected == 0 && data != null)
                    branchSelected = data[0].BranchId;
                SetBranches(data, branchSelected);
                SetTabUser(branchSelected);
                ProcessRoutes();
            }, error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
            }
        });

        Validations();
    });

    function SetBranches(data, branchSelected) {
        $.each(data, function (key, value) {
            $('#Branch').append($("<option></option>")
                    .attr("value", value.BranchId)
                    .text(value.Name));
        });
        $('#Branch').val(branchSelected);
    }

    function SetTabUser(branchSelected) {
        var _tab_url = '<%:ResolveClientUrl("~/NoticeRecharge/User/Index")%>' + '?branchId=' + branchSelected;
        $('#tab_index').attr('href', _tab_url);
        $('#btn_cancel').attr('href', _tab_url);

        _tab_url = '<%:ResolveClientUrl("~/NoticeRecharge/User/New")%>' + '?branchId=' + branchSelected;
        $('#tab_new').attr('href', _tab_url);
    }

    function ProcessRoutes() {
        var userId = $('#Id').val();
        var url = '<%:ResolveClientUrl("~/NoticeRecharge/User/GetRoutes")%>' + '?userId=' + userId;
        $.ajax({
            url: url,
            success: function (data) {
                SetRoutes(data);
            }, error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/NoticeRecharge/User/Index")%>';
            }
        });
    }

    function SetRoutes(data) {
        var selectize = $('#dpRoute')[0].selectize
        var routIds = []
        $.each(data, function (index, route) {
            routIds.push(route.Id);
        });
        selectize.setValue(routIds);
    }

    function Validations() {
        $('#Form').bootstrapValidator({
            //container: '#Messages',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                Name: {
                    validators: {
                        notEmpty: {
                            message: 'El nombre no puede estar vacio'
                        }
                    }
                },
                Mail: {
                    validators: {
                        notEmpty: {
                            message: 'El correo no puede estar vacio'
                        },
                        emailAddress: {
                            message: 'El correo es invalido'
                        }
                    }
                },
                PhoneNumber: {
                    validators: {
                        notEmpty: {
                            message: 'El telefono no puede estar vacio'
                        },
                        stringLength: {
                            min: 15,
                            message: 'El numero telefonico es de 10 digitos'
                        }/*,
                        regexp: {
                            regexp: /^[0-9]+$/,
                            message: 'Solo se permiten numeros'
                        }*/
                    }
                }
            }
        });
    }

</script>
</asp:Content>