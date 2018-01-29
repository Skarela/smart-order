<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Alert>" %>
<div class="panel-title">
    Información general
</div>
<%: Html.HiddenFor(model => model.Id)%>
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
            <%: Html.LabelFor(model => model.Description, "Descripción", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Description, new { @class = "span10", placeholder = "Descripción", maxlength = "45", autocomplete = "off" })%>
                <span id="DescriptionMessage"></span>
            </div>
        </div>
        <div id="TypeContainer" class="control-group form-group">
            <%: Html.LabelFor(model => model.Type, "Tipo", new { @class = "control-label" })%>
            <div class="controls">
                <%var typeList = new List<SelectListItem> { new SelectListItem { Value = Model.Type.ToString(), Text = "" } }; %>
                       <%:Html.DropDownList("Type", typeList, new
                       {
                           @class = "form-control span5",
                           data_role = "none",
                           placeholder = "Lista de tipos",
                           id= "Type"
                       })%>
                <span style="display: inline-block; padding-top: 7px;padding-left: 5px" id="TypeMessage"></span>
            </div>
        </div>
    </div>
</div>
