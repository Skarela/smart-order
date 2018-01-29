<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Checklist>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<% if(Model.BranchId != null  && Model.BranchId > 0 ) %>
<%{ %>
<div class="panel-title">
    Información general
</div>
<%: Html.Hidden("CategoryType", (int)CategoryType.Checklist)%>
<%: Html.HiddenFor(model => model.Id)%>
<%: Html.HiddenFor(model => model.SurveyId)%>
<%: Html.HiddenFor(model => model.BranchId)%>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.BranchId, "Sucursal", new { @class = "control-label" })%>
            <div class="controls">
                <input type="text" id="BranchName" class="span10" readonly="readonly" placeholder="Sucursal" />
            </div>
        </div>      
    </div>
</div>
  <%} %>

