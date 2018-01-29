<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %><% var userName = ViewData["UserName"];  var incidentName = ViewData["IncidentName"]; var routeName = ViewData["RouteName"];var phoneNumber = ViewData["PhoneNumber"];%>
<table class="table"> 
    <tbody> 
        <tr> 
            <td class="table-label"> <label class="control-label ">  Usuario:</label> </td> 
            <td class="table-value"> <label class="control-label"> <%: userName%></label>  </td>   
        </tr>
        <tr>   
            <td class="table-label"> <label class="control-label">  Incidencia:</label> </td> 
            <td class="table-value">  <label class="control-label">  <%: incidentName%></label> </td> 
        </tr>
        <tr>   
            <td class="table-label"> <label class="control-label">  Ruta:</label> </td>   
            <td class="table-value">  <label class="control-label">  <%: routeName%></label> </td>  
        </tr> 
        <tr  style="border-bottom: 1px solid #ddd;">   
            <td class="table-label"> <label class="control-label">  Tel&eacute;fono:</label> </td>   
            <td class="table-value">  <label class="control-label">  <%: phoneNumber%></label> </td>  
        </tr> 
    </tbody> 
</table>
<div style="text-align: center;"><textarea style = "resize:vertical;" class = "span5" placeholder="Comentario" maxlength="120" autocomplete="off" id="Comment"/> </div>