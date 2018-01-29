<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div id="RouteContainer" class="control-group form-group span4">
            <label>
                Rutas</label>
            <%:Html.DropDownList("RouteId", new SelectList(new List<SelectListItem>()), new{
                @class = "form-control",
                data_role = "none",
                placeholder = "Lista de rutas",
                id = "RouteId"
                })
            %>
        </div>
        <div id="UserContainer" class="control-group form-group span4">
            <label>
                Usuarios</label>
            <%:Html.DropDownList("UserId", new SelectList(new List<SelectListItem>()), new{
                @class = "form-control",
                data_role = "none",
                placeholder = "Lista de usuarios",
                id = "UserId"
                })
            %>
        </div>
        <div id="IncidentContainer" class="control-group form-group span4">
            <label>
                Incidencia</label>
            <%:Html.DropDownList("IncidentId", new SelectList(new List<SelectListItem>()), new{
                @class = "form-control",
                data_role = "none",
                placeholder = "Lista de incidentes",
                id = "IncidentId"
                })
            %>
        </div>
        <div id="AlertsStatusContainer" class="control-group form-group span3">
            <label>
                Estados</label>
            <%:Html.DropDownList("AlertsStatus", new SelectList(new List<SelectListItem>()), new{
                @class = "form-control",
                data_role = "none",
                placeholder = "Lista de estados",
                id = "AlertsStatus"
                })
            %>
        </div>
    </div>
</div>
