<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"%>

<asp:content id="Content1" contentplaceholderid="TitleContent" runat="server">
    Versión
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;text-align: center;">
        <div class="span8 offset2">
               <div class="panel-title">
    Versión
</div>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group" style="text-align: center">
        1.0.1
        </div>
    </div>
</div>
        </div>
    </div>

    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>

</asp:content>
