<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<div class="panel-title">
    Fechas
</div>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.Label("Fecha Inicio:", new {@class = "control-label"})%>
            <div class="controls">
                <input type="text" id="StartDate0" name="StartDate" class="dateSelector span7" placeholder="dd/mm/aaaa" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.Label("Fecha Fin:", new {@class = "control-label"})%>
            <div class="controls">
                <input type="text" id="EndDate0" name="EndDate" class="dateSelector span7" placeholder="dd/mm/aaaa" />
            </div>
        </div>
    </div>
    <span class="has-error error-dates" style="display: none;"><small class="help-block"
        style="margin-left: 20%">La fecha de inicio debe ser menor o igual a la fecha fin.</small>
    </span>
</div>
