<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row" style="width: 69%; display: inline-block;">
        <div id="UserIdsContainer" class="control-group form-group span6">
            <label>
                Usuarios</label>
            <%:Html.DropDownList("UserIds", new SelectList(new List<int>()), new { @class = "form-control multiselectize",
                id = "UserIds", data_role = "none", @style=" width: 100%;", multiple="multiple" }) %>
        </div>
        <div id="CustomerIdsContainer" class="control-group form-group span6">
            <label>
                Clientes</label>
            <%:Html.DropDownList("CustomerIds", new SelectList(new List<int>()), new { @class = "form-control multiselectize",
                id = "CustomerIds", data_role = "none", @style=" width: 100%;", multiple="multiple" }) %>
        </div>
    </div>
    <div class="controls controls-row" style="width: 29%; display: inline-block;">
        <div class="control-group form-group span9" style="margin-left: 20px;">
            <label>
                Dentro del rango de fecha</label>
            <input type="text" id="FromCreationDate" name="FromCreationDate" class="iconPointer" placeholder="De fecha"
                maxlength="10" style="width: 100%;" />
            <input type="text" id="ToCreationDate" name="ToCreationDate" class="iconPointer" placeholder="A fecha"
                maxlength="10" style="width: 100%;" />
        </div>
    </div>
</div>

