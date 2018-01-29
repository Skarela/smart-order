<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Models.Audit.ExtendAuditCampaign>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Modificar la vigencia de la campaña
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<link href="<%:ResolveClientUrl("~/Content/jtable/themes/workbycloud/jtable.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/jquery.validate.js")%>"></script>

<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/selectize.js")%>"></script>
<link href="<%:ResolveClientUrl("~/Content/bootstrap/css/selectize.bootstrap2.css")%>" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="<%:ResolveClientUrl("~/Content/bootstrap/js/bootstrap-timepicker.min.js")%>"></script>
<link href="<%:ResolveClientUrl("~/Content/bootstrap/css/bootstrap-timepicker.min.css")%>" rel="stylesheet" type="text/css" />
    
   <% using (Html.BeginForm("Extend", "Audit", FormMethod.Post, new { id = "Audit", enctype = "multipart/form-data" })) { %>
   
                
    <input type="hidden" id="id" name="id" value="<%: Model.Id %>"/>

    <div id="contentaudit" class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Audit")%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Create","Audit")%>">Nueva</a></li>
                <li class="active"><a>Vigencia</a></li>
            </ul>
            <h3> Modificar la vigencia de la campaña</h3>
            <div id="successPost" hidden=""> </div>
            <div id="errorForm" hidden=""> </div>
            <div id="errorPost" hidden=""> </div>

            <div class="form-horizontal">
                <div class="control-group">
                    <label class="control-label"> Nueva fecha fin:</label>
                    <div class="controls">
                        <span class="span3">
                            <input id="newDate" name="newDate" class="span10" type="text" style="margin-left: 16px; background-color: white; cursor: pointer" placeholder = "dd/mm/aaaa" maxlength = "10" autocomplete = "off" readonly="readonly"/>
                        </span>
                    </div>
                    <button type="submit" style="margin-left: 10px;" class="btn btn-success pull-left" id="btn_SubmitForm">
                        Guardar
                    </button>
                </div>
            </div>
            <table class="table table-bordered" style="margin-bottom: 0">
                <tr>
                    <td style="background-color: #f5f5f5; width: 20%;">
                        <label class="control-label" style="text-align: left"> Nombre</label>
                    </td>
                    <td style="border-left: 0;">
                        <label class="control-label" style="width: 80%; text-align: left; font-weight: normal;">  <%:Model.Name%></label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #f5f5f5; width: 20%;">
                        <label class="control-label" style="text-align: left"> Fecha inicio</label>
                    </td>
                    <td style="border-left: 0;">
                        <label class="control-label" style="width: 80%; text-align: left; font-weight: normal;">  <%:Model.StartDate%></label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #f5f5f5; width: 20%;">
                        <label class="control-label" style="text-align: left"> Fecha fin</label>
                    </td>
                    <td id="EndDate" data-date="<%: Model.EndDate%>" style="border-left: 0;">
                        <label class="control-label" style="width: 80%; text-align: left; font-weight: normal;"> <%: Model.EndDate%></label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function () {
            ValidateForm();
            var date = new Date();
            datePicker("#newDate", false, $("#EndDate").attr("data-date")); //date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear());//

        });

        //ValidateForm------------------------------------------------------------------------------
        function ValidateForm() {
            $('#Audit').validate({
                submitHandler: function (form) {

                    var isValid = true;

                    $("#errorForm").empty();

                    if ($("#newDate").val() == "" || $("#newDate").val() == null) {
                        $("#errorForm").append("Seleccione una nueva fecha de finalización!");
                        isValid = false;
                    }

                    if (isValid == false) {
                        $("#errorPost").hide();
                        $("#successPost").hide();
                        $("#errorPost").removeClass("alert alert-error");
                        $("#successPost").removeClass("alert alert-info");
                        $("#errorForm").show();
                        $("#errorForm").addClass("alert alert-error");
                        return false;
                    }


                    $("#errorForm").hide();
                    $("#errorForm").removeClass("alert alert-error");
                    form.submit();
                    return true;
                }
            });
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
                    minDate: new Date()
                });
            }
            console.log(date);
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
</asp:Content>
