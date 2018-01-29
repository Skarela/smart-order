<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="searchContent" class=" well">
    <div class="controls controls-row">
        <div class="control-group form-group span3">
            <label>
                Usuario</label>
            <%: Html.DropDownList("UserPortalId", new SelectList(new List<string>()), new { @class="form-control",
            id="UserPortalId",data_role="none",placeholder="Usuario",style = "width: 100%;" })%>  
        </div>
    </div>
</div>
