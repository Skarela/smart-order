<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Manager>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<div class="panel-title">
    Información general
</div>
<%: Html.HiddenFor(model => model.Id)%>
<%:Html.HiddenFor(m => m.ImagePath, new { id = "OriginalImagePath" })%>
<div class="panel-body">
    
    <div class="control-group form-group">
        <div style="display: block; padding-left: 40%">              
                <label for="File" id="spanAvatar"></label>
                <img style="width: 140px; height: 140px;border-radius: 10%; border: 3px solid #f1f1f1" class="" id="AvatarPreview" src="<%=Model.Source%>"/>
                <input type="file" name="File" id="File" class="file hide" /> 
        </div>
        <span style="text-align: center; display:none; color:#a94442; font-size: 85%" id="FileMessage">Los archivos v&aacute;lidos son jpg, jpeg, gif, bmp, png, bmp. </span>
    </div>  
    

    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Name, "Nombre", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Name, new { @class = "span10", placeholder="Nombre", maxlength="60", autocomplete="off"})%>
                <span id="NameMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Company, "Compañía", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Company, new { @class = "span10", placeholder = "Compañía", maxlength = "60", autocomplete = "off" })%>
                <span id="CompanyMessage"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Address, "Dirección", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Address, new { @class = "span10", placeholder = "Dirección", maxlength = "120", autocomplete = "off" })%>
                <span id="Span1"></span>
            </div>
        </div>
        <div class="control-group form-group">
            <%: Html.LabelFor(model => model.Email, "Email", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Email, new { @class = "span10", placeholder = "ejemplo@mail.com", maxlength = "45", autocomplete = "off", style = "text-transform: lowercase" })%>
                <span id="EmailMessage"></span>
            </div>
        </div>
        <div class="control-group form-group"   >
            <%: Html.LabelFor(model => model.Phone, "Teléfono", new { @class = "control-label" })%>
            <div class="controls">
                <%: Html.TextBoxFor(model => model.Phone, new { @class = "span10 bfh-phone", data_format = "ddddddddddddd", placeholder = "(999) 199 99 99", maxlength = "13", autocomplete = "off" })%>
                <span id="PhoneMessage"></span>
            </div>
        </div>
        <div id="AssignedIncidentsContainer" class="control-group form-group" data-incidents="<%:Json.Encode(Model.Incidents)%>">
            <%: Html.LabelFor(model => model.AssignedIncidents, "Tipos de Incidencias", new { @class = "control-label" })%>
            <div class="controls">
                       <%:Html.DropDownList("AssignedIncidents", new List<SelectListItem>(), new
                       {
                           @class = "form-control span10",
                           data_role = "none",
                           placeholder = "Lista de Tipos de Incidencias",
                           id = "AssignedIncidents",
                           multiple="multiple",
                       })%>
                <span style="display: inline-block" id="AssignedIncidentsMessage"></span>
            </div>
        </div>
        <div id="AssignedBranchesContainer" class="control-group form-group" data-branches="<%:Json.Encode(Model.Branches)%>">
            <%: Html.LabelFor(model => model.AssignedIncidents, "Sucursales", new { @class = "control-label" })%>
            <div class="controls">
                       <%:Html.DropDownList("AssignedBranches", new List<SelectListItem>(), new
                       {
                           @class = "form-control span10",
                           data_role = "none",
                           placeholder = "Lista de sucursales",
                           id = "AssignedBranches",
                           multiple="multiple",
                       })%>
                <span style="display: inline-block" id="AssignedBranchesMessage"></span>
            </div>
        </div>
    </div>
</div>
