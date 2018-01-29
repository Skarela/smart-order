<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"> Auditor&iacute;as </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <link href="<%:ResolveClientUrl("~/Content/jtable/themes/workbycloud/jtable.css")%>" rel="stylesheet" type="text/css" />

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
           
          <%if(Model != null){ %>
          <div class="span12 pagination-right">
          <p>Seleccione una sucursal</p>
          <%: Html.DropDownList("BranchCode", new SelectList((IEnumerable)Model, "code", "name", Model.ToString()), new { onchange = "seleccionaSucursal();" })%>
          <legend></legend>  
          </div>
          <%} %>

            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a>Campañas</a></li>
                <li><a href="<%:Url.Action("Create","Audit")%>">Nueva</a></li>
            </ul>
            
            <h3>Lista Campañas</h3>
            <div id="successPost" hidden=""></div>
            <div id="errorPost" hidden=""></div>

            <div class="tab-content">
               <div class="tab-pane active" id="List">
                  
               <div class="form-inline">
                 <input type="text" class="input-large offset6" id="Name" placeholder = "Nombre" maxlength="30" autocomplete="off" />
                 <input type="text" class="input-small" id="Date" placeholder = "dd/mm/aaaa" maxlength="10" autocomplete="off" />
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

        //DatePicker----------------------------------------------------------------------------
        datePicker("#Date", true, "");
        function datePicker(dom, preventDate, date) {
            var dayNames = ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"];
            var dayNamesMin = ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"];
            var monthName = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
            if (preventDate) {
                $(dom).datepicker({
                    currentText: "Now",
                    dateFormat: "dd/mm/yy",
                    dayNames: dayNames,
                    dayNamesMin: dayNamesMin,
                    monthNames: monthName
                });
            } else {
                $(dom).datepicker({
                    currentText: "Now",
                    dateFormat: "dd/mm/yy",
                    dayNames: dayNames,
                    dayNamesMin: dayNamesMin,
                    monthNames: monthName,
                    minDate: 0
                });
            }
            if (date != null) {
                $(dom).val(date);
            }
        }

        //DateNow------------------------------------------------------------------------------------
        var DateNow = function () {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            today = dd + '/' + mm + '/' + yyyy;
            return today;
        };

        //Buscar Resultados--------------------------------------------------------------------------
        $('#btnSearch').click(function (e) {
            e.preventDefault();

            var data = {
                Name: $('#Name').val(),
                Date: $('#Date').val(),
                BranchCode: $('#BranchCode').val()
            };

            $('#Table').jtable('load', data);
        });

        //Buscar Resultados--------------------------------------------------------------------------
        function seleccionaSucursal() {
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
                listAction: '<%:ResolveClientUrl("~/Audit/AuditCampaigns")%>'
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
                Name: {
                    title: 'Nombre',
                    width: '60%',
                    columnResizable: false
                },
                StatusColumn: {
                    title: '<i class="icon-tasks"></i>Estado',
                    width: '10%',
                    columnResizable: false
                },
                StartDate: {
                    title: '<i class="icon-calendar"></i>Inicio',
                    width: '10%',
                    columnResizable: false
                },
                EndDate: {
                    title: '<i class="icon-calendar"></i>Fin',
                    width: '10%',
                    columnResizable: false
                },
                UsersColumn: {
                    title: 'Usuarios',
                    sorting: false,
                    width: '5%',
                    columnResizable: false,
                    display: function (dataAudit) {
                        var $linkUsers = $(dataAudit.record.UsersColumn);

                        $linkUsers.click(function () {
                            var params = "?id=" + dataAudit.record.Id + "&branchCode=" + $('#BranchCode').val();
                            window.location.href = '<%:ResolveClientUrl("~/Audit/Users")%>' + params;
                        });

                        return $linkUsers;
                    }
                },
                ExtendColumn: {
                    title: '<i class="icon-wrench">',
                    sorting: false,
                    width: '5%',
                    columnResizable: false,
                    display: function (dataAudit) {
                        var $linkCustomer = $(dataAudit.record.ExtendColumn);
                        if (dataAudit.record.PosibleExtend) {
                            $linkCustomer.click(function () {
                                window.location.href = '<%:ResolveClientUrl("~/Audit/Extend") + "?id="%>' + dataAudit.record.Id;
                            });
                        }

                        return $linkCustomer;
                    }
                }
            }
        });
        $('#Table').jtable('load', { BranchCode: $('#BranchCode').val() });       

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