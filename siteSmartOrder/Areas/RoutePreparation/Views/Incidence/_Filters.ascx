<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div class="control-group form-group span2">
            <label>
                Tipo</label>
            <select id="TypeId" name="TypeId" class="iconPointer" style="width: 100%;">
                <option value="0">Todos</option>
                <option value="1">Vendedor</option>
                <option value="2">Ayudante</option>
            </select>
        </div>
        <div class="control-group form-group span2">
            <label>
                Accion</label>
            <select id="ActionId" name="ActionId" class="iconPointer" style="width: 100%;">
                <option value="0">Todos</option>
                <option value="1">Alta</option>
                <option value="2">Baja</option>
            </select>
        </div>

        <div class="control-group form-group span3">
            <label>
                Codigo de la unidad</label>
            <input type="text" id="UnitCode" name="UnitCode" class="iconPointer" placeholder="C2345" style="width: 100%;" />
        </div>

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
    </div>
</div>
