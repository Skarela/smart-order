<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Models.Audit.AuditsContainer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:ResolveClientUrl("~/Content/jtable/themes/workbycloud/jtable.css")%>"
        rel="stylesheet" type="text/css" />
    <div id="contentaudit" class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Audit")%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Create","Audit")%>">Nueva</a></li>
                <li class="active"><a>Auditorías</a></li>
            </ul>
            <h3>
                Auditorías por usuario
                <button type="button" id="btnBack_1" class="btn btn-mini" style="float: right; 
                    vertical-align:middle; text-align:center;">
                    <i class="icon-backward"></i> 
                    Regresar
                </button>
            </h3>
            <div id="successPost" hidden=""> </div>
            <div id="errorPost" hidden=""> </div>
               
            <h4>              
                <%:Model.User.Code + " - " + Model.User.Name%>
                <div class="btn-group pull-right">
                    <button type="button" id="btnAudit" class="btn btn-primary">Auditados</button>
                    <button type="button" id="btnPending" class="btn">Pendientes</button>
                </div>
            </h4>
            <br />
            <div id="tableAudit">
            <%if (Model.AuditsByCustomer.Any())
              { %>
            <%for (int i=0; i < Model.AuditsByCustomer.Count(); i++)// var auditByCustomer in Model.AuditsByCustomer)
              {
                  var auditByCustomer = Model.AuditsByCustomer[i];
            %> 
            <div id="contentSlide<%:i.ToString()%>" class="hero-unit" style="padding-top: 1px !important;
                padding-left: 15px !important; padding-right: 15px !important; padding-bottom: 1px; margin-bottom:15px !important">
                <h4>
                    <%:auditByCustomer.Customer == null ? "No se encontró al cliente" : auditByCustomer.Customer.Name%>
                    <% if (Model.statusAuditCampaign == 1)//Solo las campañas en progreso se pueden reauditar
                       {
                           var audit_ = auditByCustomer.Audits.LastOrDefault();
                           if (audit_ != null && audit_.Status == 0)
                           { %>
                            <button type="button" class="btn btn-mini showModal" data-target="#myModal" data-whatever="<%:audit_.Id%>" 
                                style="float: right; padding: 0px 4px 3px 5px !important; margin-right:23px"><div class="hola"><i class="icon-repeat"></i></div>
                            </button>
                        <%}
                       }%>
                </h4>
                <%foreach (var audit in auditByCustomer.Audits)
                  {
                      var comment = String.IsNullOrEmpty(audit.Comment) ? "-" : audit.Comment;%>
                <h5 style="font-weight: normal; background-color: white; border: thin solid black; border-radius: 5px; padding: 5px;">
                    <div class="form-horizontal" style="padding-right: 18px;">
                        <div class="control-group" style="margin-bottom: 0px !important">
                            <label class="control-label" style="text-align: left">
                                Fecha de captura:</label>
                            
                            <div class="btn btn-mini slide" data-slide="<%:audit.Id%>"
                                style="float: right; padding: 0px 4px 3px 5px !important">
                                <i class="icon-plus"></i>
                            </div>
                            
                            <label class="control-label" style="width: 70%; text-align: left">
                                <%: audit.ClientDate%></label>
                        </div>
                        <div class="control-group" style="margin-bottom: 0px !important">
                            <label class="control-label" style="text-align: left">
                                Comentario:</label>
                            <label class="control-label" style="width: 70%; text-align: left">
                                <%: comment%></label>
                        </div>
                        <div class="control-group" style="margin-bottom: 0px !important">
                            <label class="control-label" style="text-align: left">
                                Latitud:</label>
                            <label class="control-label" style="width: 70%; text-align: left">
                                <%: audit.Latitude%></label>
                        </div>
                        <div class="control-group" style="margin-bottom: 0px !important">
                            <label class="control-label" style="text-align: left">
                                Longitud:</label>
                            <label class="control-label" style="width: 70%; text-align: left">
                                <%: audit.Longitude%></label>
                        </div>
                    </div>
                    <span id="slide<%:audit.Id%>" class="table-striped hideShow" style="display: none;">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <td style="background-color: #E9E9E9; text-align: center; width: 20%;">
                                        Código
                                    </td>
                                    <td style="background-color: #E9E9E9; text-align: center; width: 70%;">
                                        Observación
                                    </td>
                                    <td style="background-color: #E9E9E9; text-align: center; width: 10%;">
                                        Sincronizado
                                    </td>
                                </tr>
                            </thead>
                            <%if (audit.Assets.Any())
                              {
                                  foreach (var asset in audit.Assets)
                                  {
                                      var observation = String.IsNullOrEmpty(asset.Observation) ? "-" : @asset.Observation;
                            %>
                            <tr>
                                <th style="text-align: center;">
                                    <%: asset.Code%>
                                </th>
                                <th style="text-align: center;">
                                    <%:observation%>
                                </th>
                                <th style="text-align: center;">
                                    <%if (asset.Synchronized)
                                      {%>
                                    <span class='label label-success' style='font-size: 11px;'>
                                        <center>
                                            Sincronizado</center>
                                    </span>
                                    <% }
                                      else
                                      { %>
                                    <span class='label label-important' style='font-size: 11px;'>
                                        <center>
                                            No sincronizado</center>
                                    </span>
                                    <% } %>
                                </th>
                            </tr>
                            <% }
                              }
                              else
                              { %>
                            <tr>
                                <th style="text-align: center;">
                                    -
                                </th>
                                <th style="text-align: center;">
                                    -
                                </th>
                                <th style="text-align: center;">
                                    -
                                </th>
                            </tr>
                            <%}%>
                        </table>
                    </span>
                </h5><%} %>
            </div>
            <%
              } if (Model.AuditsByCustomer.Count() > 5)
              { %>
                <button type="button" id="btnBack_2" class="btn btn-mini" style="float: right; 
                vertical-align:middle; text-align:center;">
                    <i class="icon-backward"></i> 
                Regresar
                </button>
            <% }
              }
              else
              { %>
            <div class="hero-unit" style="padding-top: 10px !important; padding-left: 15px !important;
                padding-right: 15px !important; padding-bottom: 10px; text-align: center;">
                Sin Auditorías</div>
            <% } %>
             </div>
            
            <div id="tablePending" hidden>
            <%if (Model.Pendings.Any())
              {%>
                  
                   <%foreach (var pending in Model.Pendings)
                     {%>
            <div class="hero-unit" style="padding-top: 10px !important; padding-left: 15px !important;
                padding-right: 15px !important; padding-bottom: 10px; text-align: center;" >
                <%: pending.Name%>
            </div>
            <% } if (Model.Pendings.Count() > 10)
                     {%>
                       <button type="button" id="btnBack_3" class="btn btn-mini" style="float: right; 
                        vertical-align:middle; text-align:center;"> <i class="icon-backward"></i> 
                            Regresar
                        </button>
              <%}
              }
              else
              { %>
              <div class="hero-unit" style="padding-top: 10px !important; padding-left: 15px !important;
                padding-right: 15px !important; padding-bottom: 10px; text-align: center;">
                Sin Auditorías Pendientes</div>
              <%} %>
              </div>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Confirma reasignar la campaña</h4>
                  </div>
                  <div class="modal-body">
                    
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" id="reassignItem">Reasignar</button>
                  </div>
                </div>
              </div>
            </div>

            
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

            function hideAndShowSlide(numSlide) {
            }

            $(".slide").click(function() {
                if ($(this).children().hasClass("icon-plus")) {
                    $(this).children().removeClass("icon-plus").addClass("icon-minus");
                    $("#slide" + this.getAttribute("data-slide")).css("display","block");
                } else {
                    $(this).children().removeClass("icon-minus").addClass("icon-plus");
                    $("#slide" + this.getAttribute("data-slide")).css("display","none");
                }


            });

            $("#btnAudit").click(function () {
                $("#tableAudit").show();
                $("#tablePending").hide();
                $("#btnAudit").addClass("btn-primary");
                $("#btnPending").removeClass("btn-primary");
            });

            $("#btnPending").click(function () {
                $("#tablePending").show();
                $("#tableAudit").hide();
                $("#btnPending").addClass("btn-primary");
                $("#btnAudit").removeClass("btn-primary");

            });

            function BackToCampaign() {
                var auditCampaignId = <%:Model.auditCampaignId%>;
                var branchCode = <%:Model.branchCode%>;
                var params = "?id=" + auditCampaignId + "&branchCode=" + branchCode;
                window.location.href = '<%:ResolveClientUrl("~/Audit/Users")%>' + params;
            }

            $("#btnBack_1").on( "click", BackToCampaign );
            $("#btnBack_2").on( "click", BackToCampaign );
            $("#btnBack_3").on( "click", BackToCampaign );
            
           $(".showModal").click(function (event) {
                var myModal = $("#myModal");
                myModal.modal();
                var auditId = this.getAttribute("data-whatever");
                var auditCampaignId = <%:Model.auditCampaignId%>;
                var userId = <%:Model.userId%>;
                var branchCode = <%:Model.branchCode%>;
                var statusAuditCampaign = <%:Model.statusAuditCampaign %>
                $("#reassignItem").unbind("click");
                myModal.find("#reassignItem").click(function () {
                    myModal.modal('toggle');
                    var params = "?auditId=" + auditId + "&auditCampaignId=" + auditCampaignId + "&userId=" + userId + "&branchCode=" + branchCode + "&statusAuditCampaign=" + statusAuditCampaign;
                    window.location.href = '<%:ResolveClientUrl("~/Audit/Reassign")%>' + params;
                
                });
            });
        });
    </script>

<% if (TempData["Error"] != null)
{%>
    <script type="text/javascript">
        var split = ('<%: TempData["Error"].ToString() %>').split("/");
        $.each(split, function (index, value) {
            $("#errorPost").append((index > 0 ? "<br/>" : "") + value);
        });

        $("#errorPost").addClass("alert alert-error");
        $("#errorPost").show();
    </script>
<%}
if (TempData["Success"] != null)
{ %>
    <script type="text/javascript">
        var split2 = ('<%: TempData["Success"].ToString() %>').split("/");
        $.each(split2, function (index, value) {
            $("#successPost").append((index > 0 ? "<br/>" : "") + value);
        });

        $("#successPost").addClass("alert alert-info");
        $("#successPost").show();
</script>

<%: TempData["Success"] = null %> 
<%: TempData["Error"] = null%>
<% } %>


</asp:Content>