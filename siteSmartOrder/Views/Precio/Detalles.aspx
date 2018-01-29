<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Detalles
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
          <div style="min-height:200px;">
                    <div class="alert alert-info" id="infoBoxPreciosDetalles" style="display: none">
                        <a class="close" onclick="$('#infoBoxPreciosDetalles').hide();">×</a>
                            <strong>No hay datos para mostrar </strong>
                    </div>
                        <table class="table  table-hover table-striped">
                            <thead>
                               <tr>
                                    <th style="text-align:center">C&oacute;digo</th>
                                    <th style="text-align:center"><i class="icon-th-large"></i> Nombre</th>
                                    <th style="text-align:center"><i class=" icon-list-alt"></i>Precio</th>
                                    
                               </tr>
                            </thead>                   
                            <tbody>
                              <% 
                                if (Model.Count > 0)
                                {
                                    foreach (var item in Model)
                                    { 
                              %>
                                <tr>
                                    <td style="text-align:center">
                                    <%:item.productCode %>
                                    </td>
                                    <td style="text-align:center">
                                    <%:item.productName %>
                                    </td>
                                    <td style="text-align:center">
                                    <%:item.price %>
                                    </td>
                                    
                            </tr>
                              <%    }
                                }
                                else
                                {                          
                                 %>
                                 <script type="text/javascript"> $("#infoBoxPreciosDetalles").show();</script>
                                 <%} %>
                            </tbody>
                        </table>
                 
               </div>
           </div>
  </div>
       

</asp:Content>
