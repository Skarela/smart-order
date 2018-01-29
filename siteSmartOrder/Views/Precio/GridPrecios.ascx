<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
    <h3>Lista Precios</h3>

     <div style="min-height:200px;">
            <div class="alert alert-info" id="infoBoxPrecios" style="display: none">
                <a class="close" onclick="$('#infoBoxPrecios').hide();">×</a>
                    <strong>No hay datos para mostrar </strong>
            </div>
                <table class="table  table-hover table-striped">
                    <thead>
                       <tr>
                            <th style="text-align:center">C&oacute;digo</th>
                            <th style="text-align:center"><i class="icon-th-large"></i> Nombre</th>
                            <th style="text-align:center"><i class=" icon-list-alt"></i>Tipo</th>
                            <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
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
                            <%:item.code %>
                            </td>
                            <td style="text-align:center">
                            <%:item.name %>
                            </td>
                            <td style="text-align:center">
                            <%:item.isMaster == true? "Maestra":"" %>
                            </td>
                            <td style="text-align:center">
                           
                            <a title="Detalles" href="<%:ResolveUrl("~/Precio/Detalles?id="+item.priceListId) %>"><i class="icon-eye-open"></i></a>
                            </td>
                    </tr>
                      <%    
                            }
                        }
                        else
                        {                          
                         %>
                         <script type="text/javascript">$("#infoBoxPrecios").show();</script>
                         <%
                        } 
                            
                         %>
                    </tbody>
                </table>
                 
       </div>
