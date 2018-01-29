<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Contact>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<div class="panel-title">
    Información general
</div>
<%  var classFaToggle = Model.BranchId.IsGreaterThanZero() ? "fa-toggle-on" : "fa-toggle-off"; %>
<%: Html.HiddenFor(model => model.Id)%>
<%: Html.HiddenFor(model => model.BranchId)%>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Name, "Nombre", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Name, new { @class = "span10", placeholder="Nombre", maxlength="60", autocomplete="off"})%>
                <span id="NameMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Email, "Email", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Email, new { @class = "span10", placeholder = "ejemplo@mail.com", maxlength = "45", autocomplete = "off", style = "text-transform: lowercase" })%>
                <span id="EmailMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Phone, "Teléfono", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Phone, new { @class = "span10 bfh-phone", data_format = "ddddddddddddd", placeholder = "(999) 199 99 99", maxlength = "13", autocomplete = "off" })%>
                <span id="PhoneMessage"></span>
            </div>
        </div>
        <div id="AlertContainer" class="control-group form-group">
            <%: Html.LabelFor(model => model.AlertIds, "Alertas", new { @class = "control-label" })%>
            <div class="controls">
                <%var alertList = Model.AlertIds.Select(alert => new SelectListItem { Value = alert.ToString(), Text = "" }); %>
                       <%:Html.DropDownList("AlertIds", alertList, new
                       {
                           @class = "form-control span10",
                           data_role = "none",
                           placeholder = "Lista de alertas",
                           id = "AlertIds",
                           multiple = "multiple"
                       })%>
                <span style="display: inline-block" id="AlertMessage"></span>
            </div>
        </div>
        
        <div class="control-group form-group">
            <label class = "control-label"> Nivel sucursal</label>
            <div class="controls">
                <button type="button" class="btn btn-small btn-hasBranch"><i class="fa <%:classFaToggle %> "></i></button>
            </div>
        </div>


        <div id="BranchContainer" class="control-group form-group" style="display: none">
            <%: Html.LabelFor(model => model.BranchId, "Sucursal", new { @class = "control-label" })%>
            <div class="controls">
                <input type="text" id="BranchName" class="span10" readonly="readonly" placeholder="Sucursal" />
            </div>
        </div>
    </div>
</div>
