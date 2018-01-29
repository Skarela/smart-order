<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Models.Audit.CreateAuditCampaign>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"> Nueva Campaña </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
<link href="<%:ResolveClientUrl("~/Content/jtable/themes/workbycloud/jtable.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/jquery.validate.js")%>"></script>

<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/selectize.js")%>"></script>
<link href="<%:ResolveClientUrl("~/Content/bootstrap/css/selectize.bootstrap2.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%:ResolveClientUrl("~/Survey/Content/js/SurveySelectize.js")%>"></script>

<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/bootstrap-timepicker.min.js")%>"></script>
<link href="<%:ResolveClientUrl("~/Content/bootstrap/css/bootstrap-timepicker.min.css")%>" rel="stylesheet" type="text/css" />

   <% using (Html.BeginForm("Create", "Audit", FormMethod.Post, new { id = "Audit", enctype = "multipart/form-data" }))
      {
   %>
   
<input type="hidden" id="ViewCreate" name="ViewCreate"/>
<input type="hidden" id="NameLoad" name="NameLoad" value="<%: Model.Name %>"/>
<input type="hidden" id="StartDateLoad" name="StartDateLoad" value="<%: Model.StartDate %>"/>
<input type="hidden" id="EndDateLoad" name="EndDateLoad" value="<%: Model.EndDate %>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2" >
          <%if (Model != null) { %>
          <div class="span12 pagination-right">
          <p>Seleccione una sucursal</p>
          <%: Html.DropDownList("BranchId", new SelectList(Model.Branches, "branchId", "name", Model.Branches.ToString()), new { onchange = "seleccionaSucursal();" })%>
          <legend></legend>  
          </div>
          <%} %>
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Audit")%>">Campañas</a></li>
                <li class="active"><a>Nueva</a></li>
            </ul>
            <h3>Nueva campaña </h3>
            <div  id="successPost" hidden=""></div>
            <div  id="errorForm" hidden=""></div>
            <div  id="errorPost" hidden=""></div>

            <div class="form-horizontal">
                <div class="control-group">
                        <label class="control-label" for="Name">Nombre:</label>
                        <div class="controls">
                         <input id="Name" name="Name" class="span10" type="text" style="margin-left: 16px" placeholder = "Nombre de la campaña" maxlength = "60" autocomplete = "off"/>
                        </div>
                </div>
                <div class="control-group" id="ChoiceUser">
                        <label class="control-label" for="UserIds">Auditor:</label>
                        <div class="controls" >
                        <select id="UserIds" name="UserIds" multiple="multiple" class="span8"></select>
                        <a id="AllUsers" class="btn" style="margin-left: 10px" >Todos</a>  
                        </div>
                </div>
                <div class="control-group">
                        <label class="control-label" for="StartDate">Fecha inicio:</label>
                        <div class="controls">
                         <span class="span3"><input id="StartDate" name="StartDate" class="span10" type="text" style="margin-left: 16px; background-color: white; cursor: pointer" placeholder = "dd/mm/aaaa" maxlength = "10" autocomplete = "off" readonly="readonly"/></span>
			            </div>
                </div>
                <div class="control-group">
                        <label class="control-label" for="EndDate">Fecha fin:</label>
                        <div class="controls">
                         <span class="span3"><input id="EndDate" name="EndDate" class="span10" type="text" style="margin-left: 16px; background-color: white; cursor: pointer" placeholder = "dd/mm/aaaa" maxlength = "10" autocomplete = "off" readonly="readonly"/></span>

                     </div>
                </div>
                <div id="DatesContainer"></div>
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" style="margin:10px;" class="btn btn-success pull-left" id="btn_SubmitForm">Crear campaña</button>
                        <button type="submit" style="margin:10px;" onclick="SubmitNew()" class="btn btn-success pull-left" id="btn_SubmitFormAndNew">Crear y nueva campaña</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

<% } %>

<script type="text/javascript">

    $(function () {

        $("#UserIds").selectize({
                labelField: 'name',
                placeholder: "Lista de empleados",
                searchField: ['name']
            });

        $("#imageLoad").removeClass("imageLoader");
        $("#imageLoad").hide();
        $("#opacityLoader").hide();
        
        ValidateForm();
        UserByBranchDropList("UserIds", 0, $('#BranchId').val(), false);

        document.getElementById("BranchId").onchange = function () {
            $("#UserIds")[0].selectize.clear();
            $("#UserIds")[0].selectize.clearOptions();
            UserByBranchDropList("UserIds", 10, $('#BranchId').val(), true);
        };

        $("#ViewCreate").val(false);
        $("#Name").val($("#NameLoad").val());
        datePicker("#StartDate", false, $("#StartDateLoad").val() == "" ? DateNow : $("#StartDateLoad").val().substring(0, 10));
        datePicker("#EndDate", false, $("#EndDateLoad").val() == "" ? DateNow : $("#EndDateLoad").val().substring(0, 10));
    });

    //Selecciona todos los users-------------------------------------------------------------
    $("#AllUsers").click(function () {
        var options = $("#UserIds")[0].selectize.options;
        $.each(options, function (index, dataOptions) {
            $("#UserIds")[0].selectize.addItem(dataOptions.value);
        });
    });

    //Users-------------------------------------------------------------------------------
    function UserByBranchDropList(id, items, branchId, addOptions) {
        $.ajax({
            url: '<%:ResolveClientUrl("~/Audit/GetUsersByBranch") + "?branchId="%>' + branchId,
            type: "POST",
            dataType: "json",
            async: false,
            success: function (data) {

                if (data.Result == false) {
                    SelectizeAuditError(id, data.Message);
                    return;
                }

                if (data.Records == null || data.Records.length == 0) {
                    SelectizeAuditDisabled(id);
                    return;
                }

                var options = [];
                $.each(data.Records, function(index, dataR) {
                    var option = { value: dataR.userId, name: dataR.code + " - " + dataR.name };
                    options.push(option);
                });

//                if (addOptions) {

                    document.getElementById(id).selectize.enable();
                    $.each(options, function (index, dataOptions) {
                        $("#" + id)[0].selectize.addOption(dataOptions);
                    });
                    return;
//                }

//                if (options != "") {
//                    $("#" + id).selectize({
//                        options: options,
//                        labelField: 'name',
//                        placeholder: "Lista de empleados",
//                        searchField: ['name'],
//                        maxItems: items == 0 ? 1000 : items
//                    });
//                }
                $(".selectize-control").addClass('single');
            },
            error: function () {
                var msg = "Error: No se pudieron cargar los empleados";
                SelectizeAuditError(id, msg);
            }
        });
    }

    //Error-------------------------------------------------------------------------------
    function SelectizeAuditError(id, msg) {
        $("#" + id).selectize({ placeholder: "Error de carga" });
        document.getElementById(id).selectize.disable();
        if (msg != "") $("#errorForm").empty().append(msg);
    }

    function SelectizeAuditDisabled(id) {
        $("#" + id).selectize({ placeholder: "Sin registros" });
        document.getElementById(id).selectize.disable();
    }
   
    //DatePicker----------------------------------------------------------------------------
    function datePicker(dom, preventDate, date) {
        var dayNames = ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"];
        var dayNamesMin = ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"];
        var monthName = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
        if (preventDate) {
            $(dom).datepicker({
                currentText: "Now",
                dateFormat: "dd/mm/yy",
                dayNames: dayNames,
                dayNamesMin: dayNamesMin,
                monthNames: monthName
            });
        } else {
            $(dom).datepicker({
                currentText: "Now",
                dateFormat: "dd/mm/yy",
                dayNames: dayNames,
                dayNamesMin: dayNamesMin,
                monthNames: monthName,
                minDate: 0
            });
        }
        if (date != null) {
            $(dom).val(date);
        }
    }

    //DateNow------------------------------------------------------------------------------------
    var DateNow = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        today = dd + '/' + mm + '/' + yyyy;
        return today;
    };

    //SubmitNew------------------------------------------------------------------------------------
    function SubmitNew() {
        $("#ViewCreate").val(true);
        $('#btn_SubmitForm').click();
    }
    
    //ValidateForm------------------------------------------------------------------------------
    function ValidateForm() {
        $('#Audit').validate({
            submitHandler: function (form) {

                $("#imageLoad").show();
                $("#imageLoad").addClass("imageLoader");
                $("#opacityLoader").show();

                var isValid = true;
                $("#errorForm").empty();

                if ($("#Name").val() == "" || $("#Name").val() == null) {
                    $("#errorForm").append("Ingrese el nombre de la campaña!");
                    isValid = false;
                }

                if ($("#UserIds").val() == "" || $("#UserIds").val() == null) {
                    $("#errorForm").append((isValid ? "" : "<br/>") + "Seleccione al menos un cliente!");
                    isValid = false;
                }

                if ($("#StartDate").val() == "" || $("#StartDate").val() == null) {
                    $("#errorForm").append((isValid ? "" : "<br/>") + "Seleccione una fecha de inicio!");
                    isValid = false;
                }

                if ($("#EndDate").val() == "" || $("#EndDate").val() == null) {
                    $("#errorForm").append((isValid ? "" : "<br/>") + "Seleccione una fecha de finalización!");
                    isValid = false;
                }

                if (isValid == false) {
                    $("#errorPost").hide();
                    $("#successPost").hide();
                    $("#errorPost").removeClass("alert alert-error");
                    $("#successPost").removeClass("alert alert-info");
                    $("#errorForm").show();
                    $("#errorForm").addClass("alert alert-error");


                    $("#imageLoad").hide();
                    $("#imageLoad").removeClass("imageLoader");
                    $("#opacityLoader").hide();

                    return false;
                }


                $("#errorForm").hide();
                $("#errorForm").removeClass("alert alert-error");
                form.submit();
                return true;
            }
        });
    }
</script>
<% if (TempData["Error"] != null)
{%>
    <script type="text/javascript">
        var split = ('<%: TempData["Error"].ToString() %>').split("/");
        $.each(split, function (index, value) {
            $("#errorPost").append((index > 0 ? "<br/>" : "") + value);
        });

        $("#errorPost").addClass("alert alert-error");
        $("#errorPost").show();
    </script>
<%}
if (TempData["Success"] != null)
{ %>
    <script type="text/javascript">
    var split2 = ('<%: TempData["Success"].ToString() %>').split("/");
    $.each(split2, function (index, value) {
        $("#successPost").append((index > 0 ? "<br/>" : "") + value);
    });

    $("#successPost").addClass("alert alert-info");
    $("#successPost").show();
</script>

<%: TempData["Success"] = null %> 
<%: TempData["Error"] = null%>
<% } %>

<div id="opacityLoader">
</div>
<div id="imageLoad" class="imageLoader">
    <div id="loaderImage"></div>
</div>

<style type="text/css">
#opacityLoader  {
    height:100%;
	width:100%;
	position:fixed;
	left:0;
	top:0;
	z-index:1 !important;
	opacity: 0.4;
    filter: alpha(opacity=40); /* msie */
    background-color: #000;
}

.imageLoader {
  position: absolute;
  left: 50%;
  top: 50%;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  z-index: 10 !important;
}
</style>
<script type="text/javascript" src="<%:ResolveClientUrl("~/Survey/Content/js/Loading.js")%>"></script>

</asp:Content>
