<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Models.LogOnModel>" %>
<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Iniciar sesión
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
    .validation-summary-errors li
    {
        font-weight:bold;
        }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $(".validation-summary-errors").addClass("alert");
        $(".validation-summary-errors").addClass("alert-error");
    });

    function iniciarSesion() {
        var valor = $("#password1").val();
        $("#Password").val(hex_md5(valor).toUpperCase());
        $("#iniciaSesionId").submit();

    }
</script>
<div class="row-fluid" style="padding-bottom:10px;">
<div class="span8 offset2">
    <h2>Iniciar sesión</h2>
    <p>
        Especifique su nombre de usuario y contraseña.
    </p>
    <% using (Html.BeginForm("Logon", "Account", FormMethod.Post, new { id = "iniciaSesionId", Style="margin:0 0 0px;" }))
       { %>
    <%: Html.ValidationSummary(true, "No se ha iniciado la sesión. Corrija los errores e inténtelo de nuevo.")%>
        
        <div>
            <fieldset>
                <legend></legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.UserName)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.UserName)%>
                    <!--%: Html.ValidationMessageFor(m => m.UserName)%-->
                </div>
                
                <!--div class="editor-label">
                    <-%: Html.LabelFor(m => m.Password)%>
                </div-->
                <!--div class="editor-field"-->
                    <%: Html.TextBoxFor(m => m.Password, new { type="hidden"})%>
                    <!--%: Html.ValidationMessageFor(m => m.Password)%-->
                <!--/div-->             
            </fieldset>
            
        </div>
    <% } %>      <div class="editor-label">
                 <label for="password1">Password</label>
                 </div>
                 <div class="editor-field">
                 <input type="password" id="password1" onkeypress="evaluateKeyPress(event,'iniciarSesion()')"/>
                 </div>
                 <p>
                    <input type="button" value="Iniciar sesión" class="btn btn-info" onclick="iniciarSesion();" />
                    <!--a href="#" class="btn btn-info"><i class="icon-refresh icon-white"></i> Restablecer</a-->
                </p>
    </div>
<div class="offset3"></div>
    </div>
</asp:Content>