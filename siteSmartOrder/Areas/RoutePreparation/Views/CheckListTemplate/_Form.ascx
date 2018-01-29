<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Checklist>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%: Html.Hidden("CategoryType", (int)CategoryType.Checklist)%>
<%: Html.HiddenFor(model => model.Id)%>
<%: Html.HiddenFor(model => model.SurveyId)%>
<%: Html.HiddenFor(model => model.BranchId)%>
<%: Html.Hidden("IsTemplate",true)%>
