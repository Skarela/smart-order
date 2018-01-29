<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div class="control-group form-group span3">
            <label>
                Nombre</label>
            <input type="text" id="Name" name="Name" placeholder="Nombre" class="form-control"
                autocomplete="off" style="width: 100%;" />
        </div>
        <div class="control-group form-group span3">
            <label>
                Compañía</label>
            <input type="text" id="Company" name="Company" placeholder="Compañía" class="form-control"
                autocomplete="off" style="width: 100%;" />
        </div>
        <div id="IncidentContainer" class="control-group form-group span4">
            <label>
                Tipo de siniestro</label>
            <%:Html.DropDownList("IncidentId", new SelectList(new List<string>()), new { @class = "form-control",
                id = "IncidentId", data_role = "none", @style=" width: 100%;", placeholder="Incidencia" }) %>
        </div>
    </div>
</div>
