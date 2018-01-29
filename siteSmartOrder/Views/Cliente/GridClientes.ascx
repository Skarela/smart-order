<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
    
    <h3>Lista Clientes</h3>
       <div style="min-height:200px;">
            <div class="alert alert-info" id="infoBoxClientes" style="display: none">
                <a class="close" onclick="$('#infoBoxClientes').hide();">×</a>
                    <strong>No hay datos para mostrar </strong>
            </div>
                <table class="table  table-hover table-striped" id="grid">
                    <thead>
                       <tr>
                            <th style="text-align:center">C&oacute;digo</th>
                            <th style="text-align:center"><i class="icon-user"></i>Nombre</th>
                            <th style="text-align:center"><i class="icon-home"></i>Contacto</th>
                            <th style="text-align:center"><i class="icon-wrench"></i>Ruta</th>
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
                            <%:item.contact %> 
                        </td>
                        <td style="text-align:center">
                            <%:item.routeName %> 
                        </td>
                    </tr>
                     <%    }
                        }
                        else
                        {                          
                         %>
                         <script type="text/javascript"> $("#infoBoxClientes").show();</script>
                         <%} %>
                 </tbody>   
                </table>
            </div>
