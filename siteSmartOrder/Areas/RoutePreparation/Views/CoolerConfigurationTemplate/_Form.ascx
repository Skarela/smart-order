<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.CoolerConfiguration>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%: Html.Hidden("CategoryType", (int)CategoryType.CoolerConfiguration)%>
<%: Html.HiddenFor(model => model.Id)%>
<%: Html.HiddenFor(model => model.SurveyId)%>
<%: Html.HiddenFor(model => model.CustomerId)%>
<%: Html.Hidden("IsTemplate",true)%>
