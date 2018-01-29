<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div class="control-group form-group span4">
                <label>
                    Seleccione una sucursal</label>
                <%: Html.DropDownList("BranchId", new SelectList(new List<string>()), new { @class="form-control",
            id="BranchId",data_role="none",placeholder="Sucursal",style = "width: 100%;" })%>                
            </div>
        <div class="control-group form-group span4">
            <label>
                Encuesta</label>
            <input type="text" id="Campaign" name="Campaign" placeholder="Encuesta" class="form-control"
                autocomplete="off" style="width: 100%;" />
        </div>
        <div class="control-group form-group span4">
            <label>
                Autor</label>
            <%:Html.DropDownList("UserPortalId", new SelectList(new List<string>()), new { @class = "form-control",
                id = "UserPortalId", data_role = "none", @style=" width: 100%;", placeholder="Usuario" }) %>
        </div>
        </div>
        <div class="controls controls-row">
        <div class="control-group form-group span2">
            <label>
                Fecha Inicio</label>
            <input type="text" id="StartDate" name="StartDate" class="iconPointer" placeholder="dd/mm/aaaa"
                maxlength="25" style="width: 100%;" />
        </div>
        <div class="control-group form-group span2">
            <label>
                Fecha Fin</label>
            <input type="text" id="EndDate" name="EndDate" class="iconPointer" placeholder="dd/mm/aaaa"
                maxlength="10" style="width: 100%;" />
        </div>
        <div class="control-group form-group span5" style="float:right;">
        <br />
            <button id="editAll-btn" type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Editar Seleccion</button>
            <button id="deleteAll-btn" class="btn btn-danger">Eliminar Seleccion</button>
        </div>
    </div>
</div>
