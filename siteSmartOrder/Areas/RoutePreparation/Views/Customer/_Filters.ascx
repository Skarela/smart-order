<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div class="control-group form-group span3">
            <label>
                Código</label>
            <input type="text" id="Code" name="Code" placeholder="c&oacute;digo" class="form-control"
                autocomplete="off" style="width: 100%;" />
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
    </div>
</div>
