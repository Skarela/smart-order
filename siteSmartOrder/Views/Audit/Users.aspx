<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Models.Audit.AuditCampaign>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Usuarios </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <link href="<%:ResolveClientUrl("~/Content/jtable/themes/workbycloud/jtable.css")%>" rel="stylesheet" type="text/css" />
   
   <input type="hidden" id="Id" name="Id" value="<%: Model.Id %>"/>
   <input type="hidden" id="StartDate" name="StartDate" value="<%: Model.StartDate %>"/>
   <input type="hidden" id="EndDate" name="EndDate" value="<%: Model.EndDate %>"/>
   <input type="hidden" id="BranchCode" name="BranchCode" value="<%: Model.BranchCode %>"/>

   <div id="contentaudit" class="row-fluid" style="margin-bottom: 30px;">
      <div class="span8 offset2">
         <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Audit")%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Create","Audit")%>">Nueva</a></li>
                <li class="active"><a>Usuarios</a></li>
         </ul>
         <h3> Usuarios por campaña</h3>
         <h4><%:Model.Name%></h4><br/>
            <div id="successPost" hidden=""></div>
            <div id="errorPost" hidden=""></div>
            <div class="tab-content">
               <div class="tab-pane active" id="List">
                  
               <div class="form-inline">
                 <input type="text" class="input-large offset7" id="User" placeholder = "Nombre o código" maxlength="30" autocomplete="off" />
                 <button class="btn" type="button" id="btnSearch"><i class="icon-search"></i></button>
               </div><br/>
                   

               <div class="table  table-hover table-striped">
                  <div id="Table"></div>
               </div>
               </div>
            </div>
      </div>
   </div>
   
    <script type="text/javascript">

    //Buscar Resultados--------------------------------------------------------------------------
    $('#btnSearch').click(function (e) {
        e.preventDefault();

        var data = {
            AuditCampaignId: $('#Id').val(),
            User: $('#User').val(),
            BranchCode: $('#BranchCode').val(),
            StartDate:  $('#StartDate').val(),
            EndDate:  $('#EndDate').val()
    };

        $('#Table').jtable('load', data);
    });
    
    //Buscar Resultados--------------------------------------------------------------------------
    function seleccionaSucursal()
    {
        $('#btnSearch').click();
    }

    //Boton enter--------------------------------------------------------------------------
    $(document).keypress(function (e) {
        if (e.which == 13)
            $('#btnSearch').click();
    });

    //Tabla--------------------------------------------------------------------------
    $('#Table').jtable({
        actions: {
            listAction: '<%:ResolveClientUrl("~/Audit/GetUsersByAuditCampaign")%>'
        },
        //Options
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Id DESC',
        saveUserPreferences: false,

        //Fields
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            Code: {
                title: 'Código',
                width: '20%',
                columnResizable: true
            },
            Name: {
                title: '<i class="icon-user"></i>Nombre',
                width: '45%',
                columnResizable: true
            },
            StatusColumn: {
                title: '<i class="icon-tasks"></i>Estado',
                sorting: false,
                width: '30%',
                listClass:'text-center',
                columnResizable: false,
            },
            AuditsColumn: {
                title: 'Auditorías',
                sorting: false,
                width: '5%',
                columnResizable: false,
                display: function(dataUser) {
                    var $linkAudits = $(dataUser.record.AuditsColumn);

                    $linkAudits.click(function() {
                        var params = "?auditCampaignId=" + $('#Id').val() + "&userId=" + dataUser.record.Id+ "&branchCode=" + $('#BranchCode').val() + "&startDate=" + $('#StartDate').val() + "&endDate=" + $('#EndDate').val();
                        window.location.href = '<%:ResolveClientUrl("~/Audit/Audits")%>' + params;
                    });

                    return $linkAudits;
                }
            }
        }
    });
    console.log(<%:Model.StartDate%>);
    $('#Table').jtable('load', { AuditCampaignId: $('#Id').val(),BranchCode: $('#BranchCode').val(), StartDate: $('#StartDate').val(), EndDate: $('#EndDate').val() });
    $(".jtable-column-header.text-center").css("text-align", "center");
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
    <%: TempData["Error"] = null%>
<%}
  if (TempData["Success"] != null)
  {%>
    <script type="text/javascript">
        var split = ('<%: TempData["Success"].ToString() %>').split("/");
        $.each(split, function (index, value) {
            $("#successPost").append((index > 0 ? "<br/>" : "") + value);
        });

        $("#successPost").addClass("alert alert-info");
        $("#successPost").show();
    </script>
    <%: TempData["Success"] = null %> 
    <% }%>

</asp:Content>