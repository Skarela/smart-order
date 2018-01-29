<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<h3>Lista Productos</h3>
        <div style="min-height:200px;">
            <div class="alert alert-info" id="infoBoxProductos" style="display: none">
                <a class="close" onclick="$('#infoBoxProductos').hide();">×</a>
                    <strong>No hay datos para mostrar </strong>
            </div>
             <table class="table  table-hover table-striped" id="xProducts">
                    <thead>
                         <tr>
                             <th style="text-align:center">C&oacute;digo</th>
                             <th style="text-align:center"><i class="icon-folder-open"></i> Descripci&oacute;n</th>
                             <th style="text-align:center"><i class="icon-tags"></i> Marca</th>                            
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
                            <td style='text-align:center'> 
                                <%:item.code %>
                            </td>
                            <td style='text-align:center'>
                                <%:item.name %> 
                            </td>
                            <td style='text-align:center'>
                                <%:item.brand%>
                            </td>
                        </tr>
                         <%    }
                        }
                        else
                        {                          
                         %>
                         <script type="text/javascript">$("#infoBoxProductos").show();</script>
                         <%} %>
                     </tbody>   
            </table>
         </div>
