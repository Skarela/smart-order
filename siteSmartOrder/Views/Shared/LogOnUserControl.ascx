<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated && Session["UserPortal"] != null)
    {
%>
        <div class="btn-group" style="margin:10px 20px;">
              <a id="bottonSession" class="btn btn-mini" href="<%:ResolveClientUrl("~/Usuario/Index")%>" ><i class="icon-user" ></i> <%:Page.User.Identity.Name%></a>
              <a class="btn btn-mini dropdown-toggle" data-toggle="dropdown" ><span class="caret"></span></a>
              <ul class="dropdown-menu " style="font-size:10px;">
                <li><a href="#"><%: Html.ActionLink("Cerrar sesión", "LogOff", new { Area = "",Controller = "Account" })%></a></li>
              </ul>
         </div>

<%
    }
    else {
        ViewData["urlQRlink"] = null;
%> 
            <a id="bottonSession" class="btn btn-mini" style="margin:10px;" href="<%:ResolveClientUrl("~/Account/LogOn")%>"><i class="icon-user"></i>Iniciar sesi&oacute;n</a>
<%
    }
%>

