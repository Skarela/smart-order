<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<%
    var classFaToggle = Model.StartDate.IsNotNullOrEmpty() ? "fa-toggle-on" : "fa-toggle-off";
    %>
<div class="panel-title">
    Información general
</div>
<%: Html.Hidden("CategoryType", (int)CategoryType.Campaign)%>
<%: Html.HiddenFor(model => model.Id)%>
<%: Html.HiddenFor(model => model.SurveyId)%>
<%: Html.HiddenFor(model => model.BranchId)%>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Name, "Nombre", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Name, new { @class = "span10", id="Name", placeholder="Nombre", maxlength="60", autocomplete="off"})%>
                <span id="NameMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Description, "Descripción", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextAreaFor(model => model.Description, new { @class = "span10", id="Description", @style = "resize:vertical;", placeholder = "Descripción", maxlength = "120", autocomplete = "off" })%>
                <span id="DescriptionMessage"></span>
            </div>
        </div>
        <% if(Model.BranchId != null  && Model.BranchId > 0 ) %>
        <%{ %>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.BranchId, "Sucursal", new { @class = "control-label" })%>
            <div class="controls">
                <input type="text" id="BranchName" class="span10" readonly="readonly" placeholder="Sucursal" />
            </div>
        </div>
        <%} %>
    </div>
</div>

<% if(Model.BranchId != null  && Model.BranchId > 0 ) %>
<%{ %>
<div class="panel-title">
   Calendarización
   <button type="button" class="btn btn-small btn-hasDates" style="display:none"><i class="fa <%:classFaToggle %> "></i></button>
</div>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.StartDate, "Fecha inicio", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.StartDate, new { @class = "span10", placeholder = "dd/mm/aaaa", maxlength = "10", name = "StartDate"})%>
                <span id="StartDateMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.EndDate, "Fecha fin", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.EndDate, new { @class = "span10", placeholder = "dd/mm/aaaa", maxlength = "10", name = "EndDate"})%>
                <span id="EndDateMessage"></span>
            </div>
        </div>
    </div>
</div>
<%} %>